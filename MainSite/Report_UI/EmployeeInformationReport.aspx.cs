using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using DAL.Permission_DAL;
using DAL.Report_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_UI_EmployeeInformationReport : System.Web.UI.Page
{
    ReportDAL aReportDal=new ReportDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    ShowMessage aShowMessage = new ShowMessage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDown();
            GetCheckBox();
            GetCompany();

            ColumnsVisbale();

        }
    }

    private void ColumnsVisbale()
    {
       
    }

    public void LoadData()
    {
        loadGridView.Columns[1].Visible = false;
        loadGridView.Columns[2].Visible = false;
        loadGridView.Columns[3].Visible = false;
        loadGridView.Columns[4].Visible = false;
        loadGridView.Columns[5].Visible = false;
        loadGridView.Columns[6].Visible = false;
        loadGridView.Columns[7].Visible = false;
        loadGridView.Columns[8].Visible = false;
        loadGridView.Columns[9].Visible = false;
        loadGridView.Columns[10].Visible = false;


        //36
        loadGridView.Columns[11].Visible = false;
        loadGridView.Columns[12].Visible = false;
        loadGridView.Columns[13].Visible = false;
        loadGridView.Columns[14].Visible = false;
        loadGridView.Columns[15].Visible = false;
        loadGridView.Columns[16].Visible = false;
        loadGridView.Columns[17].Visible = false;
        loadGridView.Columns[18].Visible = false;
        loadGridView.Columns[19].Visible = false;
        loadGridView.Columns[20].Visible = false;


        loadGridView.Columns[21].Visible = false;
        loadGridView.Columns[22].Visible = false;
        loadGridView.Columns[23].Visible = false;
        loadGridView.Columns[24].Visible = false;
        loadGridView.Columns[25].Visible = false;
        loadGridView.Columns[26].Visible = false;
        loadGridView.Columns[27].Visible = false;
        loadGridView.Columns[28].Visible = false;
        loadGridView.Columns[29].Visible = false;
        loadGridView.Columns[30].Visible = false;


        loadGridView.Columns[31].Visible = false;
        loadGridView.Columns[32].Visible = false;
        loadGridView.Columns[33].Visible = false;
        loadGridView.Columns[34].Visible = false;
        loadGridView.Columns[35].Visible = false;
        loadGridView.Columns[36].Visible = false;


        if (cblHeader.Items[0].Selected == true)
        {
            loadGridView.Columns[1].Visible = true;

        }

        if (cblHeader.Items[0].Selected == false)
        {
            loadGridView.Columns[1].Visible = false;

        }


        if (cblHeader.Items[1].Selected == true)
        {
            loadGridView.Columns[2].Visible = true;

        }

        if (cblHeader.Items[1].Selected == false)
        {
            loadGridView.Columns[2].Visible = false;

        }




        if (cblHeader.Items[2].Selected == true)
        {
            loadGridView.Columns[3].Visible = true;

        }

        if (cblHeader.Items[2].Selected == false)
        {
            loadGridView.Columns[3].Visible = false;

        }



        if (cblHeader.Items[3].Selected == true)
        {
            loadGridView.Columns[4].Visible = true;

        }

        if (cblHeader.Items[3].Selected == false)
        {
            loadGridView.Columns[4].Visible = false;

        }


        if (cblHeader.Items[4].Selected == true)
        {
            loadGridView.Columns[5].Visible = true;

        }

        if (cblHeader.Items[4].Selected == false)
        {
            loadGridView.Columns[5].Visible = false;

        }



        if (cblHeader.Items[5].Selected == true)
        {
            loadGridView.Columns[6].Visible = true;

        }

        if (cblHeader.Items[5].Selected == false)
        {
            loadGridView.Columns[6].Visible = false;

        }



        if (cblHeader.Items[6].Selected == true)
        {
            loadGridView.Columns[7].Visible = true;

        }

        if (cblHeader.Items[6].Selected == false)
        {
            loadGridView.Columns[7].Visible = false;

        }




        if (cblHeader.Items[7].Selected == true)
        {
            loadGridView.Columns[8].Visible = true;

        }

        if (cblHeader.Items[7].Selected == false)
        {
            loadGridView.Columns[8].Visible = false;

        }




        if (cblHeader.Items[8].Selected == true)
        {
            loadGridView.Columns[9].Visible = true;

        }

        if (cblHeader.Items[8].Selected == false)
        {
            loadGridView.Columns[9].Visible = false;

        }





        if (cblHeader.Items[9].Selected == true)
        {
            loadGridView.Columns[10].Visible = true;

        }

        if (cblHeader.Items[9].Selected == false)
        {
            loadGridView.Columns[10].Visible = false;

        }




        if (cblHeader.Items[10].Selected == true)
        {
            loadGridView.Columns[11].Visible = true;

        }

        if (cblHeader.Items[10].Selected == false)
        {
            loadGridView.Columns[11].Visible = false;

        }


        if (cblHeader.Items[11].Selected == true)
        {
            loadGridView.Columns[12].Visible = true;

        }

        if (cblHeader.Items[11].Selected == false)
        {
            loadGridView.Columns[12].Visible = false;

        }





        if (cblHeader.Items[12].Selected == true)
        {
            loadGridView.Columns[13].Visible = true;

        }

        if (cblHeader.Items[12].Selected == false)
        {
            loadGridView.Columns[13].Visible = false;

        }





        if (cblHeader.Items[13].Selected == true)
        {
            loadGridView.Columns[14].Visible = true;

        }

        if (cblHeader.Items[13].Selected == false)
        {
            loadGridView.Columns[14].Visible = false;

        }




        if (cblHeader.Items[14].Selected == true)
        {
            loadGridView.Columns[15].Visible = true;

        }

        if (cblHeader.Items[14].Selected == false)
        {
            loadGridView.Columns[15].Visible = false;

        }





        if (cblHeader.Items[15].Selected == true)
        {
            loadGridView.Columns[16].Visible = true;

        }

        if (cblHeader.Items[15].Selected == false)
        {
            loadGridView.Columns[16].Visible = false;

        }




        if (cblHeader.Items[16].Selected == true)
        {
            loadGridView.Columns[17].Visible = true;

        }

        if (cblHeader.Items[16].Selected == false)
        {
            loadGridView.Columns[17].Visible = false;

        }




        if (cblHeader.Items[18].Selected == true)
        {
            loadGridView.Columns[19].Visible = true;

        }

        if (cblHeader.Items[18].Selected == false)
        {
            loadGridView.Columns[19].Visible = false;

        }




        if (cblHeader.Items[19].Selected == true)
        {
            loadGridView.Columns[20].Visible = true;

        }

        if (cblHeader.Items[19].Selected == false)
        {
            loadGridView.Columns[20].Visible = false;

        }



        if (cblHeader.Items[20].Selected == true)
        {
            loadGridView.Columns[21].Visible = true;

        }

        if (cblHeader.Items[20].Selected == false)
        {
            loadGridView.Columns[21].Visible = false;

        }


        if (cblHeader.Items[21].Selected == true)
        {
            loadGridView.Columns[22].Visible = true;

        }

        if (cblHeader.Items[21].Selected == false)
        {
            loadGridView.Columns[22].Visible = false;

        }



        if (cblHeader.Items[22].Selected == true)
        {
            loadGridView.Columns[23].Visible = true;

        }

        if (cblHeader.Items[22].Selected == false)
        {
            loadGridView.Columns[23].Visible = false;

        }



        if (cblHeader.Items[23].Selected == true)
        {
            loadGridView.Columns[24].Visible = true;

        }

        if (cblHeader.Items[23].Selected == false)
        {
            loadGridView.Columns[24].Visible = false;

        }



        if (cblHeader.Items[24].Selected == true)
        {
            loadGridView.Columns[25].Visible = true;

        }

        if (cblHeader.Items[24].Selected == false)
        {
            loadGridView.Columns[25].Visible = false;

        }




        if (cblHeader.Items[25].Selected == true)
        {
            loadGridView.Columns[26].Visible = true;

        }

        if (cblHeader.Items[25].Selected == false)
        {
            loadGridView.Columns[26].Visible = false;

        }



        if (cblHeader.Items[26].Selected == true)
        {
            loadGridView.Columns[27].Visible = true;

        }

        if (cblHeader.Items[26].Selected == false)
        {
            loadGridView.Columns[27].Visible = false;

        }


        if (cblHeader.Items[27].Selected == true)
        {
            loadGridView.Columns[28].Visible = true;

        }

        if (cblHeader.Items[27].Selected == false)
        {
            loadGridView.Columns[28].Visible = false;

        }



        if (cblHeader.Items[28].Selected == true)
        {
            loadGridView.Columns[29].Visible = true;

        }

        if (cblHeader.Items[28].Selected == false)
        {
            loadGridView.Columns[29].Visible = false;

        }




        if (cblHeader.Items[29].Selected == true)
        {
            loadGridView.Columns[30].Visible = true;

        }

        if (cblHeader.Items[29].Selected == false)
        {
            loadGridView.Columns[30].Visible = false;

        }




        if (cblHeader.Items[30].Selected == true)
        {
            loadGridView.Columns[31].Visible = true;

        }

        if (cblHeader.Items[30].Selected == false)
        {
            loadGridView.Columns[31].Visible = false;

        }



        if (cblHeader.Items[31].Selected == true)
        {
            loadGridView.Columns[32].Visible = true;

        }

        if (cblHeader.Items[31].Selected == false)
        {
            loadGridView.Columns[32].Visible = false;

        }



        if (cblHeader.Items[32].Selected == true)
        {
            loadGridView.Columns[33].Visible = true;

        }

        if (cblHeader.Items[32].Selected == false)
        {
            loadGridView.Columns[33].Visible = false;

        }




        if (cblHeader.Items[33].Selected == true)
        {
            loadGridView.Columns[34].Visible = true;

        }

        if (cblHeader.Items[33].Selected == false)
        {
            loadGridView.Columns[34].Visible = false;

        }



        if (cblHeader.Items[34].Selected == true)
        {
            loadGridView.Columns[35].Visible = true;

        }

        if (cblHeader.Items[34].Selected == false)
        {
            loadGridView.Columns[35].Visible = false;

        }



        if (cblHeader.Items[35].Selected == true)
        {
            loadGridView.Columns[36].Visible = true;

        }

        if (cblHeader.Items[35].Selected == false)
        {
            loadGridView.Columns[36].Visible = false;

        }


      
        DataTable dtdata = aReportDal.GetEmpInfo(Parameter());

        if (dtdata.Rows.Count>0)
        {
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
       

       
        
    }
    public void GetCheckBox()
    {
        DataTable dtgrade = aReportDal.GetGrade();
        gradeCheckBoxList.DataValueField = "SalaryGradeId";
        gradeCheckBoxList.DataTextField = "GradeCode";
        gradeCheckBoxList.DataSource = dtgrade;
        gradeCheckBoxList.DataBind();

        DataTable dtsalloc = aReportDal.GetSalLoc();
        salLocCheckBoxList.DataValueField = "SalaryLoationId";
        salLocCheckBoxList.DataTextField = "SalaryLocation";
        salLocCheckBoxList.DataSource = dtsalloc;
        salLocCheckBoxList.DataBind();

        DataTable dtdesig = aReportDal.GetDesig();
        desigCheckBoxList.DataValueField = "DesignationId";
        desigCheckBoxList.DataTextField = "Designation";
        desigCheckBoxList.DataSource = dtdesig;
        desigCheckBoxList.DataBind();

        DataTable dtDistrict = aReportDal.GetDistrict();
        permDistCheckBoxList.DataValueField = "DistrictID";
        permDistCheckBoxList.DataTextField = "Titel";
        permDistCheckBoxList.DataSource = dtDistrict;
        permDistCheckBoxList.DataBind();


        DataTable dtNomination = aReportDal.GetNominationPurposes();
        nominationCheckBoxList.DataValueField = "NPID";
        nominationCheckBoxList.DataTextField = "Description";
        nominationCheckBoxList.DataSource = dtNomination;
        nominationCheckBoxList.DataBind();
 
    }
    public void DropDown()
    {
        aReportDal.LoadCompanyDropDownList(ddlCompany);

        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);
        aReportDal.GetCategory(typeOfPosDropDownList);
        aReportDal.GetSuspendReason(suspendDropDownList);
        aReportDal.GetJobleftReason(jobleftTypeDropDownList);
        aReportDal.LoadddlEducation(ddlEducation);
        aReportDal.LoadddlSubjectGroup(ddlSubject);
        aReportDal.LoadddlCountry(ddlCountry);
    }
    protected void NewDivisionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (NewDivisionDropDownList.SelectedValue != "")
        {
            aReportDal.GetDivisionWingList(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
            aReportDal.GetDepartmentByDivList(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
            aReportDal.GetSectionByDivList(NewSectionDropDownList, NewDivisionDropDownList.SelectedValue);
            aReportDal.GetSubSectionListAll(NewSubSectionDropDownList, NewDivisionDropDownList.SelectedValue);
        }
        else
        {
            NewDivisionDropDownList.Items.Clear();
        }
    }

    protected void NewWingDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (NewWingDropDownList.SelectedValue != "")
        {
            aReportDal.GetDepartmentList(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
        }
        else
        {
            NewDepartmentDropDownList1.Items.Clear();
        }
    }

    public void AddTree(TreeView aTreeView)
    {
        DataTable dtdivdata = aReportDal.GetAllDivision(ddlCompany.SelectedValue);
        for (int i = 0; i < dtdivdata.Rows.Count; i++)
        {
            aTreeView.Nodes.Add(new TreeNode((dtdivdata.Rows[i]["DivisionName"].ToString())+"(Division)", (dtdivdata.Rows[i]["DivisionId"].ToString())));
            DataTable dtwing =
                aReportDal.GetAllWing(" AND  tblDivision.DivisionId='" + dtdivdata.Rows[i]["DivisionId"].ToString() +
                                      "'");
            for (int j = 0; j < dtwing.Rows.Count; j++)
            {
                aTreeView.Nodes[i].ChildNodes.Add(new TreeNode((dtwing.Rows[j]["DivisionWingName"].ToString())+"(Wing)", (dtwing.Rows[j]["DivisionWId"].ToString())));

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
    protected void NewDepartmentDropDownList1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (NewDepartmentDropDownList1.SelectedValue != "")
        {
            aReportDal.GetSectionList(NewSectionDropDownList, NewDepartmentDropDownList1.SelectedValue);
            DataTable dtgetdata = aReportDal.GetDepartmentRelaton(NewDepartmentDropDownList1.SelectedValue, "");
            if (dtgetdata.Rows.Count > 0)
            {
                if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
                {
                    wing.Visible = false;
                    NewWingDropDownList.Items.Clear();
                    aReportDal.GetDivisionWingListAll(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                    NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                }
                else
                {
                    wing.Visible = true;
                    NewWingDropDownList.Items.Clear();
                    aReportDal.GetDivisionWingList(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                    NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                }
            }
        }
        else
        {
            NewSectionDropDownList.Items.Clear();
        }
    }

    protected void NewSectionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtgetdata1 = aReportDal.GetSectionRelaton(NewSectionDropDownList.SelectedValue, "");
        if (dtgetdata1.Rows.Count > 0)
        {
            if (dtgetdata1.Rows[0]["Invisible"].ToString() == "True")
            {
                dept.Visible = false;
                NewDepartmentDropDownList1.Items.Clear();
                aReportDal.GetDepartmentByDivListAll(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
                NewDepartmentDropDownList1.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
            else
            {
                dept.Visible = true;
                NewDepartmentDropDownList1.Items.Clear();
                aReportDal.GetDepartmentByDivList(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
                NewDepartmentDropDownList1.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
        }
        DataTable dtgetdata = aReportDal.GetDepartmentRelaton(NewDepartmentDropDownList1.SelectedValue, "");
        if (dtgetdata.Rows.Count > 0)
        {
            if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
            {
                wing.Visible = false;
                NewWingDropDownList.Items.Clear();
                aReportDal.GetDivisionWingListAll(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
            else
            {
                wing.Visible = true;
                NewWingDropDownList.Items.Clear();
                aReportDal.GetDivisionWingList(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
        }
    }

    protected void NewSubSectionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtgetdata2 = aReportDal.GetSubSectionRelaton(NewSubSectionDropDownList.SelectedValue, "");
        if (dtgetdata2.Rows.Count > 0)
        {
            if (dtgetdata2.Rows[0]["Invisible"].ToString() == "True")
            {
                sec.Visible = false;
                NewSectionDropDownList.Items.Clear();
                aReportDal.GetSectionByDivListAll(NewSectionDropDownList, NewDivisionDropDownList.SelectedValue);
                NewSectionDropDownList.SelectedValue = dtgetdata2.Rows[0]["SectionId"].ToString();
            }
            else
            {
                sec.Visible = true;
                NewSectionDropDownList.Items.Clear();
                aReportDal.GetSectionByDivList(NewSectionDropDownList, NewDivisionDropDownList.SelectedValue);
                NewSectionDropDownList.SelectedValue = dtgetdata2.Rows[0]["SectionId"].ToString();
            }
        }
        DataTable dtgetdata1 = aReportDal.GetSectionRelaton(NewSectionDropDownList.SelectedValue, "");
        if (dtgetdata1.Rows.Count > 0)
        {
            if (dtgetdata1.Rows[0]["Invisible"].ToString() == "True")
            {
                dept.Visible = false;
                NewDepartmentDropDownList1.Items.Clear();
                aReportDal.GetDepartmentByDivListAll(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
                NewDepartmentDropDownList1.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
            else
            {
                dept.Visible = true;
                NewDepartmentDropDownList1.Items.Clear();
                aReportDal.GetDepartmentByDivList(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
                NewDepartmentDropDownList1.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
        }
        DataTable dtgetdata = aReportDal.GetDepartmentRelaton(NewDepartmentDropDownList1.SelectedValue, "");
        if (dtgetdata.Rows.Count > 0)
        {
            if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
            {
                wing.Visible = false;
                NewWingDropDownList.Items.Clear();
                aReportDal.GetDivisionWingListAll(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
            else
            {
                wing.Visible = true;
                NewWingDropDownList.Items.Clear();
                aReportDal.GetDivisionWingList(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
        }

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
            if (step=="Division")
            {
                div = node.Value + "," + div;
                //param = param + " AND EG.DivisionId='" + node.Value + "' ";
            }
            else if (step == "Wing")
            {
                wing = node.Value + "," + wing;
                //param = param + " AND EG.DivisionWId='" + node.Value + "' ";
            }
            else if (step=="Department")
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

            param = param + " AND ( EG.DivisionId " + hierchicalDropDownList.SelectedItem.Text + " (" + div.TrimEnd(',') + ") ";
        }
        if (wing != string.Empty)
        {
            param = param + " or ( EG.DivisionWId " + hierchicalDropDownList.SelectedItem.Text + " (" + wing.TrimEnd(',') + ") ) ";
        }
        if (dept != string.Empty)
        {
            param = param + " or ( EG.DepartmentId " + hierchicalDropDownList.SelectedItem.Text + " (" + dept.TrimEnd(',') + ") ) ";
        }
        if (sec != string.Empty)
        {
            param = param + " or ( EG.SectionId " + hierchicalDropDownList.SelectedItem.Text + " (" + sec.TrimEnd(',') + ") ) ";
        }
        if (subsec != string.Empty)
        {
            param = param + " or ( EG.SubSectionId " + hierchicalDropDownList.SelectedItem.Text + " (" + subsec.TrimEnd(',') + ") ) ";
        }

        if (div == string.Empty && wing == string.Empty && dept == string.Empty && sec == string.Empty && subsec == string.Empty)
        {

        }
        else
        {
            param = param + ")";
        }
        return param;
        
    }

    public string GradeParam()
    {
        string param = "";
        string grade = "";

        for (int i = 0; i < gradeCheckBoxList.Items.Count; i++)
        {
            if (gradeCheckBoxList.Items[i].Selected)
            {
                grade = gradeCheckBoxList.Items[i].Value + "," + grade;
            }
        }
        if (grade!=string.Empty)
        {
            param = param + " AND EG.SalaryGradeId " + gradeDropDownList.SelectedItem.Text + " (" + grade.TrimEnd(',') +
                    ")";
        }
        else
        {
            param = "";
        }
        return param;
        
    }


    public string DistParam()
    {
        string param = "";
        string grade = "";

        for (int i = 0; i < permDistCheckBoxList.Items.Count; i++)
        {
            if (permDistCheckBoxList.Items[i].Selected)
            {
                grade = permDistCheckBoxList.Items[i].Value + "," + grade;
            }
        }
        if (grade != string.Empty)
        {
            param = param + " AND EG.PermanentDistrict " + permDistDropDownList.SelectedItem.Text + " (" + grade.TrimEnd(',') +
                    ")";
        }
        else
        {
            param = "";
        }
        return param;

    }


    public string DistParam2()
    {
        string param = "";
        string grade = "";

        for (int i = 0; i < permDistCheckBoxList.Items.Count; i++)
        {
            if (permDistCheckBoxList.Items[i].Selected)
            {
                grade = permDistCheckBoxList.Items[i].Value + "," + grade;
            }
        }
        if (grade != string.Empty)
        {
            param = param + " AND EG.DistrictID " + permDistDropDownList.SelectedItem.Text + " (" + grade.TrimEnd(',') +
                    ")";
        }
        else
        {
            param = "";
        }
        return param;

    }

    public string StepParam()
    {
        string param = "";
        string step = "";

        for (int i = 0; i < stepCheckBoxList.Items.Count; i++)
        {
            if (stepCheckBoxList.Items[i].Selected)
            {
                step = stepCheckBoxList.Items[i].Value + "," + step;
            }
        }
        if (step != string.Empty)
        {
            param = param + " AND EG.SalaryStepId " + stepDropDownList.SelectedItem.Text + " (" + step.TrimEnd(',') +
                    ")";
        }

        return param;

    }
    public string SalLocParam()
    {
        string param = "";
        string salloc = "";

        for (int i = 0; i < salLocCheckBoxList.Items.Count; i++)
        {
            if (salLocCheckBoxList.Items[i].Selected)
            {
                salloc = salLocCheckBoxList.Items[i].Value + "," + salloc;
            }
        }
        if (salloc != string.Empty)
        {
            param = param + " AND EG.SalaryLoationId " + presentOfcDropDownList4.SelectedItem.Text + " (" + salloc.TrimEnd(',') +
                    ")";
        }

        return param;

    }

    public string DesigParam()
    {
        string param = "";
        string desig = "";

        for (int i = 0; i < desigCheckBoxList.Items.Count; i++)
        {
            if (desigCheckBoxList.Items[i].Selected)
            {
                desig = desigCheckBoxList.Items[i].Value + "," + desig;
            }
        }
        if (desig != string.Empty)
        {
            param = param + " AND EG.DesignationId " + desigDropDownList.SelectedItem.Text + " (" + desig.TrimEnd(',') +
                    ")";
        }

        return param;

    }
    public string ReligionParam()
    {
        string param = "";
        string text = "";

        for (int i = 0; i < religionCheckBoxList.Items.Count; i++)
        {
            if (religionCheckBoxList.Items[i].Selected)
            {
                text = religionCheckBoxList.Items[i].Value + "," + text;
            }
        }
        if (text != string.Empty)
        {
            param = param + " AND EG.Religion " + religionDropDownList1.SelectedItem.Text + " (" + text.TrimEnd(',') +
                    ")";
        }

        return param;

    }
    public string GendernParam()
    {
        string param = "";
        string text = "";

        for (int i = 0; i < genderCheckBoxList.Items.Count; i++)
        {
            if (genderCheckBoxList.Items[i].Selected)
            {
                text = "'" + genderCheckBoxList.Items[i].Value + "'," + text;
            }
        }
        if (text != string.Empty)
        {
            param = param + " AND EG.Gender " + genderDropDownList.SelectedItem.Text + " (" + text.TrimEnd(',') +
                    ")";
        }

        return param;

    }
    public string BloodParam()
    {
        string param = "";
        string text = "";

        for (int i = 0; i < bloodgroupCheckBoxList.Items.Count; i++)
        {
            if (bloodgroupCheckBoxList.Items[i].Selected)
            {
                text = "'" + bloodgroupCheckBoxList.Items[i].Value + "'," + text;
            }
        }
        if (text != string.Empty)
        {
            param = param + " AND EG.BloodGroup " + bloodGroupDropDownList.SelectedItem.Text + " (" + text.TrimEnd(',') +
                    ")";
        }

        return param;

    }

    public string PermDistParam()
    {
        string param = "";
        string text = "";

        for (int i = 0; i < permDistCheckBoxList.Items.Count; i++)
        {
            if (permDistCheckBoxList.Items[i].Selected)
            {
                text = permDistCheckBoxList.Items[i].Value + "," + text;
            }
        }
        if (text != string.Empty)
        {
            param = param + " AND EG.PermanentDistrict " + permDistDropDownList.SelectedItem.Text + " (" + text.TrimEnd(',') +
                    ")";
        }

        return param;

    }
    public string PermThanaParam()
    {
        string param = "";
        string text = "";

        for (int i = 0; i < permThanaCheckBoxList1.Items.Count; i++)
        {
            if (permThanaCheckBoxList1.Items[i].Selected)
            {
                text = permThanaCheckBoxList1.Items[i].Value + "," + text;
            }
        }
        if (text != string.Empty)
        {
            param = param + " AND EG.PermanentThana " + permThanaDropDownList.SelectedItem.Text + " (" + text.TrimEnd(',') +
                    ")";
        }

        return param;

    }
    public string NominationParam()
    {
        string param = "";
        string text = "";

        for (int i = 0; i < nominationCheckBoxList.Items.Count; i++)
        {
            if (nominationCheckBoxList.Items[i].Selected)
            {
                text = nominationCheckBoxList.Items[i].Value + "," + text;
            }
        }
        if (text != string.Empty)
        {
            param = param + " AND NM.NominationPurpose " + permThanaDropDownList.SelectedItem.Text + " (" + text.TrimEnd(',') +
                    ")";
        }

        return param;

    }
    public string Parameter()
    {
        string param = "";

        if (ddlCompany.SelectedValue !="")
        {
            param = param + " AND EG.CompanyId='" + ddlCompany.SelectedValue + "' ";
        }
        if (empNoTextBox.Text!=string.Empty)
        {
            param = param + " AND EG.EmpMasterCode='" + empNoTextBox.Text + "' ";
        }
        if (SearchEmployeeNameTextBoxTextBox.Text != string.Empty)
        {
            param = param + " AND EG.EmpName='"+SearchEmployeeNameTextBoxTextBox.Text.Trim()+"' ";
        }
      
        if (gradeTextBox.Text !=string.Empty)
        {
            param = param + GradeParam();
        }
        if (stepTextBox.Text !=string.Empty)
        {
            param = param + StepParam();
        }
        if (presentOfcTextBox.Text !=string.Empty)
        {
            param = param + SalLocParam();
        }
        if (desigTextBox.Text!=string.Empty)
        {
            param = param + DesigParam();
        }

        if (religionTextBox.Text != string.Empty)
        {
            param = param + RelagionParam();
        }

        if (genderTextBox.Text != string.Empty)
        {
            param = param + GendernParam();
        }

        if (bloodgroupTextBox.Text != string.Empty)
        {
            param = param + BloodParam();
        }

        if (permDistTextBox.Text != string.Empty)
        {
            param = param + PermDistParam();
        }

        if (permThanaTextBox.Text != string.Empty)
        {
            param = param + PermThanaParam();
        }

        if (nominationTextBox.Text != string.Empty)
        {
            param = param + NominationParam();
        }

        if (ddlEducation.SelectedValue != "")
        {
            param = param + EducationNameParam();
        }

        if (ddlSubject.SelectedValue != "")
        {
            param = param + SubjectGroupParam();
        }


        if (ddlCountry.SelectedValue != "")
        {
            param = param + CountryParam();
        }

        if (ddlTrainigStart.SelectedValue != "1")
        {
            if (ddlTrainigStart.SelectedValue == "2" || ddlTrainigStart.SelectedValue == "3" || ddlTrainigStart.SelectedValue == "4")
            {
                param = param + " AND TI.TrFromDate " + ddlTrainigStart.SelectedItem.Text + " '" +
                        trainingStartTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (TI.TrFromDate between '" + trainingStartFRTextBox.Text + "' AND '" + trainingToTextBox.Text + "' )";
            }
        }

        if (ddlTrainingEnd.SelectedValue != "1")
        {
            if (ddlTrainigStart.SelectedValue == "2" || ddlTrainigStart.SelectedValue == "3" || ddlTrainigStart.SelectedValue == "4")
            {
                param = param + " AND TI.TrToDate " + ddlTrainigStart.SelectedItem.Text + " '" +
                        trainingEndTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (TI.TrToDate between '" + trainingEndFRTextBox.Text + "' AND '" + trainingEndToTextBox.Text + "' )";
            }
        }



        if (actionDateDropDownList.SelectedValue != "1")
        {
            if (actionDateDropDownList.SelectedValue == "2" || actionDateDropDownList.SelectedValue == "3" || actionDateDropDownList.SelectedValue == "4")
            {
                param = param + " AND DA.EffectiveDate " + actionDateDropDownList.SelectedItem.Text + " '" +
                        actionDateTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (DA.EffectiveDate between '" + actionFRDateTextBox.Text + "' AND '" + actionToDateTextBox.Text + "' )";
            }
        }



        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            param = param + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }
        else
        {

            if (empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
            {
                param = param + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
                if (jobleftTypeDropDownList.SelectedIndex > 0)
                {
                    param = param + " AND EJ.JobLeftTypeId='" + jobleftTypeDropDownList.SelectedValue + "' ";
                }
                if (suspendDropDownList.SelectedIndex > 0)
                {
                    param = param + " AND S.ReasonId='" + suspendDropDownList.SelectedValue + "' ";
                }
            }
            
        }
        if (emptypeDropDownList.SelectedValue != "3" && emptypeDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            param = param + " AND EG.EmpTypeId"+employementDropDownList.SelectedValue+"'" + emptypeDropDownList.SelectedValue + "' ";
        }
        else
        {
            if (emptypeDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
            {
                param = param + " AND EG.IsProgramContractual" + employementDropDownList.SelectedValue + "'1' ";
            }
            
        }
        if (joiningDateDropDownList.SelectedValue !="1")
        {
            if (joiningDateDropDownList.SelectedValue == "2" || joiningDateDropDownList.SelectedValue == "3" || joiningDateDropDownList.SelectedValue == "4")
            {
                param = param + " AND EG.DateOfJoin" + joiningDateDropDownList.SelectedItem.Text + " '" +
                        joiningDtTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (EG.DateOfJoin between '"+joiningDtFrTextBox.Text+"' AND '"+joiningDtToTextBox.Text+"' )";
            }
        }
        if (retirementDropDownList.SelectedValue != "1")
        {
            if (retirementDropDownList.SelectedValue == "2" || retirementDropDownList.SelectedValue == "3" || retirementDropDownList.SelectedValue == "4")
            {
                param = param + " AND EG.DateOfRetirement" + retirementDropDownList.SelectedItem.Text + " '" +
                        retTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND ( EG.DateOfRetirement between '" + retfromTextBox.Text + "' AND '" + retToTextBox.Text + "' )";
            }
        }
        if (ageDropDownList.SelectedValue != "1")
        {
            if (ageDropDownList.SelectedValue == "2" || ageDropDownList.SelectedValue == "3" || ageDropDownList.SelectedValue == "4")
            {
                param = param + " AND DATEDIFF(year,EG.DateOfBirth,GETDATE())" + ageDropDownList.SelectedItem.Text + " '" +
                        ageTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (DATEDIFF(year,EG.DateOfBirth,GETDATE()) between '" + ageMinTextBox.Text + "' AND '" + ageMaxTextBox.Text + "') ";
            }
        }
        if (serviceLengthDropDownList.SelectedValue != "1")
        {
            if (serviceLengthDropDownList.SelectedValue == "2" || serviceLengthDropDownList.SelectedValue == "3" || serviceLengthDropDownList.SelectedValue == "4")
            {
                param = param + " AND DATEDIFF(year,EG.DateOfJoin,GETDATE())" + serviceLengthDropDownList.SelectedItem.Text + " '" +
                        serviceLengthTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (DATEDIFF(year,EG.DateOfJoin,GETDATE()) between '" + serviceLengthFrTextBox.Text + "' AND '" + serviceLengthToTextBox.Text + "') ";
            }
        }
        if (endDateDropDownList.SelectedValue != "1")
        {
            if (endDateDropDownList.SelectedValue == "2" || endDateDropDownList.SelectedValue == "3" || endDateDropDownList.SelectedValue == "4")
            {
                param = param + " AND EG.DateOfConfirmation " + endDateDropDownList.SelectedItem.Text + " '" +
                        endDateTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (EG.DateOfConfirmation between '" + endFromDateTextBox.Text + "' AND '" + endToDateTextBox.Text + "') ";
            }
        }
        if (dobDropDownList.SelectedValue != "1")
        {
            if (dobDropDownList.SelectedValue == "2" || dobDropDownList.SelectedValue == "3" || dobDropDownList.SelectedValue == "4")
            {
                param = param + " AND EG.DateOfBirth " + dobDropDownList.SelectedItem.Text + " '" +
                        dobTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (EG.DateOfBirth between '" + dobfromTextBox.Text + "' AND '" + dobtoTextBox.Text + "') ";
            }
        }
        if (turnOverdtDropDownList.SelectedValue != "1")
        {
            if (turnOverdtDropDownList.SelectedValue == "2" || turnOverdtDropDownList.SelectedValue == "3" || turnOverdtDropDownList.SelectedValue == "4")
            {
                param = param + " AND EJ.JobLeftDate " + turnOverdtDropDownList.SelectedItem.Text + " '" +
                        turnOverDateTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (EJ.JobLeftDate between '" + turnOverDateFrTextBox.Text + "' AND '" + turnOverDatetoTextBox.Text + "') ";
            }
        }
        if (confirmStDropDownList.SelectedIndex >0)
        {
            param = param + " AND EG.ConformationStatus='" + confirmStDropDownList.SelectedValue + "' ";
        }

        if (HierchicalParameter() != string.Empty)
        {
            param = param + HierchicalParameter();
        }
        return param;
    }

    //Other Information
    public string RelagionParam()
    {
        string param = "";
        string relagion = "";

        for (int i = 0; i < religionCheckBoxList.Items.Count; i++)
        {
            if (religionCheckBoxList.Items[i].Selected)
            {
                relagion = "'"+ religionCheckBoxList.Items[i].Value + "'," + relagion;
            }
        }
        if (relagion != string.Empty)
        {
            param = param + " AND EG.Religion " + religionDropDownList1.SelectedItem.Text + " (" + relagion.TrimEnd(',') +
                    ")";
        }

        return param;

    }

    public string EducationNameParam()
    {
        string param = "";

        if (ddlEducation.SelectedValue != "")
        {
            param = param + " AND ED.EducationNameId " + ddlEducationIn.SelectedValue + " (" + ddlEducation.SelectedValue + " ) ";
        }

        return param;

    }


    public string SubjectGroupParam()
    {
        string param = "";

        if (ddlSubject.SelectedValue != "")
        {
            param = param + " AND ED.SubjectGroupId " + ddlSubjectGroupIn.SelectedValue + " (" + ddlSubject.SelectedValue + " ) ";
        }

        return param;

    }

    public string CountryParam()
    {
        string param = "";

        if (ddlCountry.SelectedValue != "")
        {
            param = param + " AND TI.TrainingCountry " + ddlCountryIn.SelectedValue + " (" + ddlCountry.SelectedValue + " ) ";
        }

        return param;

    }


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCompany.SelectedValue != "")
        {
           Session["CompanyId"] = ddlCompany.SelectedValue; 
        }

        if (ddlCompany.SelectedValue != "")
        {
            aReportDal.LoadActionType(ddlAction, ddlCompany.SelectedValue);
            aReportDal.GetDivisionList(NewDivisionDropDownList, ddlCompany.SelectedValue);
        }
        
        AddTree(heirerchicalTreeView);
    }

    protected void heirchichalButton_OnClick(object sender, EventArgs e)
    {
        mpe_1.Show();
    }

    protected void btnEmpSubmit_OnClick(object sender, EventArgs e)
    {
        
        foreach (TreeNode node in heirerchicalTreeView.CheckedNodes)
        {
            hierchicalSelectedTextBox.Text = node.Text + "," + hierchicalSelectedTextBox.Text;

        }
        hierchicalSelectedTextBox.Text=hierchicalSelectedTextBox.Text.TrimEnd(',');
        mpe_1.Hide();
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        foreach (TreeNode node in heirerchicalTreeView.Nodes)
        {
            node.Checked = false;
            if (node.Expanded==true)
            {
                node.Expanded = false;
            }
            ClearNode(node);

        }
        hierchicalSelectedTextBox.Text = string.Empty;
    }

    public void ClearNode(TreeNode aTreeNode)
    {
        foreach (TreeNode node in aTreeNode.ChildNodes)
        {
            if (node.Expanded == true)
            {
                node.Expanded = false;
            }
            node.Checked = false;
            ClearNode(node);

        }
    }

    protected void btnNo_OnClick(object sender, EventArgs e)
    {
        mpe_1.Hide();
    }
    public string CompanyId()
    {
        string companyid = "";
        for (int i = 0; i < lchk_Company.Items.Count; i++)
        {
            if (lchk_Company.Items[i].Selected)
            {
                companyid = companyid + "'" + lchk_Company.Items[i].Value + "'" + ",";
            }
        }
        companyid = companyid.TrimEnd(',');
        return companyid;
    }

    public void GetCompany()
    {
        try
        {
            DataTable dtcomp = aPermissionDal.GetCompany();
            lchk_Company.DataValueField = "CompanyId";
            lchk_Company.DataTextField = "ShortName";
            lchk_Company.DataSource = dtcomp;
            lchk_Company.DataBind();

            DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
            for (int i = 0; i < userdata.Rows.Count; i++)
            {
                for (int j = 0; j < lchk_Company.Items.Count; j++)
                {
                    if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
                    {
                        lchk_Company.Items[j].Selected = true;


                    }
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("/Default.aspx");
        }
    }

    protected void Button2_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < gradeCheckBoxList.Items.Count; i++)
        {
            if (gradeCheckBoxList.Items[i].Selected)
            {
                if (AlreadyExistOrNot(gradeTextBox, gradeCheckBoxList.Items[i].Text))
                {
                    gradeTextBox.Text = gradeCheckBoxList.Items[i].Text + "," + gradeTextBox.Text;
                }

                
            }
            
        }
        gradeTextBox.Text = gradeTextBox.Text.TrimEnd(',');
        ModalPopupExtender1.Hide();
        if (GradeParam() !=string.Empty)
        {
            DataTable dtstep = aReportDal.GetStep(GradeParam());
            stepCheckBoxList.DataValueField = "SalaryStepId";
            stepCheckBoxList.DataTextField = "SalaryStepName";
            stepCheckBoxList.DataSource = dtstep;
            stepCheckBoxList.DataBind();    
        }
        
    }

    private bool AlreadyExistOrNot(TextBox textBox, string txtValue)
    {
        if (textBox.Text != "")
        {
            if (textBox.Text.Contains(','))
            {
                string[] grade = textBox.Text.Split(',');

                for (int i = 0; i < grade.Length; i++)
                {
                    if (txtValue == grade[i])
                    {
                        return false;
                    }
                }
            }
            else
            {
                string grade = textBox.Text;

                if (txtValue == grade)
                {
                    return false;
                }
            }
        }

        return true;
    }

    protected void Button3_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }

    protected void Button4_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }

    protected void Button5_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < gradeCheckBoxList.Items.Count; i++)
        {
            gradeCheckBoxList.Items[i].Selected = false;

        }
        gradeTextBox.Text = string.Empty;
        stepCheckBoxList.Items.Clear();
    }

    protected void Button6_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }

    protected void Button7_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < stepCheckBoxList.Items.Count; i++)
        {
            stepCheckBoxList.Items[i].Selected = false;
        }

        stepTextBox.Text = string.Empty;
    }

    protected void Button8_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < stepCheckBoxList.Items.Count; i++)
        {
            if (stepCheckBoxList.Items[i].Selected)
            {
                if (AlreadyExistOrNot(stepTextBox, stepCheckBoxList.Items[i].Text))
                {
                    stepTextBox.Text = stepCheckBoxList.Items[i].Text + "," + stepTextBox.Text;
                }               
            }
        }
        stepTextBox.Text = stepTextBox.Text.TrimEnd(',');
        ModalPopupExtender2.Hide();
    }

    protected void joiningDateDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        joiningDatesingle.Visible = false;
        joiningdateRange.Visible = false;
        if (joiningDateDropDownList.SelectedValue=="5")
        {
            joiningdateRange.Visible = true;
        }
        else
        {

            if (joiningDateDropDownList.SelectedValue != "1")
            {
                joiningDatesingle.Visible = true;
            }
            
        }

    }

    protected void endDateDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        singleEndDate.Visible = false;
        endDateRange.Visible = false;
        if (endDateDropDownList.SelectedValue=="5")
        {
            endDateRange.Visible = true;
        }
        else
        {
            if (endDateDropDownList.SelectedValue != "1")
            {
                singleEndDate.Visible = true;
            }
            
        }
    }

    protected void ageDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        agesingle.Visible = false;
        agerange.Visible = false;
        
        if (ageDropDownList.SelectedValue=="5")
        {
            agerange.Visible = true;
        }
        else
        {

            if (ageDropDownList.SelectedValue != "1")
            {
                agesingle.Visible = true;
            }
            
        }
    }

    protected void dobDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        dobsingle.Visible = false;
        dobrange.Visible = false;
        if (dobDropDownList.SelectedValue=="5")
        {
            dobrange.Visible = true;
        }
        else
        {

            if (dobDropDownList.SelectedValue != "1")
            {
                dobsingle.Visible = true;
            }
            
        }
    }

    protected void turnOverdtDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        turnDtRange.Visible = false;
        turnDtSingle.Visible = false;

        if (turnOverdtDropDownList.SelectedValue=="5")
        {
            turnDtRange.Visible = true;
        }
        else
        {
            if (turnOverdtDropDownList.SelectedValue != "1")
            {
                turnDtSingle.Visible = true;
            }
            
        }
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void empStatusDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        suspend.Visible = false;
        jobleft.Visible = false;
        if (empStatusDropDownList.SelectedItem.Text=="Inactive")
        {
            suspend.Visible = true;
            jobleft.Visible = true;
        }

    }

    protected void Button10_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < salLocCheckBoxList.Items.Count; i++)
        {
            if (salLocCheckBoxList.Items[i].Selected)
            {
                if (AlreadyExistOrNot(presentOfcTextBox, salLocCheckBoxList.Items[i].Text))
                {
                    presentOfcTextBox.Text = salLocCheckBoxList.Items[i].Text + "," + presentOfcTextBox.Text;
                }                
            }
        }
        presentOfcTextBox.Text = presentOfcTextBox.Text.TrimEnd(',');
        ModalPopupExtender3.Hide();
    }

    protected void Button11_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender3.Hide();
    }

    protected void  Button12_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender3.Show();
    }

    protected void Button13_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < salLocCheckBoxList.Items.Count; i++)
        {
            salLocCheckBoxList.Items[i].Selected = false;
        }
        presentOfcTextBox.Text = string.Empty;
        ModalPopupExtender3.Hide();
    }

    protected void Button14_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < desigCheckBoxList.Items.Count; i++)
        {
            if (desigCheckBoxList.Items[i].Selected)
            {
                if (AlreadyExistOrNot(desigTextBox, desigCheckBoxList.Items[i].Text))
                {
                    desigTextBox.Text = desigCheckBoxList.Items[i].Text + "," + desigTextBox.Text;
                }
                
            }
        }
        desigTextBox.Text = desigTextBox.Text.TrimEnd(',');
        ModalPopupExtender4.Hide();
    }

    protected void Button15_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender4.Hide();
    }

    protected void Button16_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender4.Show();
    }

    protected void Button17_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < desigCheckBoxList.Items.Count; i++)
        {
            desigCheckBoxList.Items[i].Selected = false;
        }
        desigTextBox.Text = string.Empty;
        ModalPopupExtender4.Hide();
    }

    protected void retirementDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        retirement.Visible = false;
        retirementbt.Visible = false;
        if (retirementDropDownList.SelectedValue == "5")
        {
            retirementbt.Visible = true;
        }
        else
        {
            if (retirementDropDownList.SelectedValue != "1")
            {
                retirement.Visible = true;
            }
            
        }
    }

    protected void serviceLengthDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        serviceL.Visible = false;
        serviceLBt.Visible = false;
        if (serviceLengthDropDownList.SelectedValue == "5")
        {
            serviceLBt.Visible = true;
        }
        else
        {
            if (serviceLengthDropDownList.SelectedValue != "1")
            {
                serviceL.Visible = true;
            }
            
        }
    }

    protected void Button18_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < religionCheckBoxList.Items.Count; i++)
        {
            if (religionCheckBoxList.Items[i].Selected)
            {
                if (AlreadyExistOrNot(religionTextBox, religionCheckBoxList.Items[i].Text))
                {
                    religionTextBox.Text = religionCheckBoxList.Items[i].Text + "," + religionTextBox.Text; 
                }                
            }
        }
        religionTextBox.Text = religionTextBox.Text.TrimEnd(',');
        ModalPopupExtender5.Hide();
    }

    protected void Button19_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender5.Hide();
    }

    protected void Button20_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < genderCheckBoxList.Items.Count; i++)
        {
            if (genderCheckBoxList.Items[i].Selected)
            {
                if (AlreadyExistOrNot(genderTextBox, genderCheckBoxList.Items[i].Text))
                {
                    genderTextBox.Text = genderCheckBoxList.Items[i].Text + "," + genderTextBox.Text;
                }                
            }
        }
        genderTextBox.Text = genderTextBox.Text.TrimEnd(',');
        ModalPopupExtender6.Hide();
    }

    protected void Button21_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender6.Hide();
    }

    protected void Button24_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender5.Show();
    }

    protected void Button25_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < religionCheckBoxList.Items.Count; i++)
        {
            religionCheckBoxList.Items[i].Selected = false;
        }
        religionTextBox.Text = string.Empty;
        ModalPopupExtender5.Hide();
    }

    protected void Button26_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender6.Show();
    }

    protected void Button27_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < genderCheckBoxList.Items.Count; i++)
        {
            genderCheckBoxList.Items[i].Selected = false;
        }
        genderTextBox.Text = string.Empty;
        ModalPopupExtender6.Hide();
    }

    protected void Button28_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender7.Show();
    }

    protected void Button29_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < bloodgroupCheckBoxList.Items.Count; i++)
        {
            bloodgroupCheckBoxList.Items[i].Selected = false;
        }
        bloodgroupTextBox.Text = string.Empty;
        ModalPopupExtender7.Hide();
    }

    protected void Button30_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender8.Show();
    }

    protected void Button31_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < permDistCheckBoxList.Items.Count; i++)
        {
            permDistCheckBoxList.Items[i].Selected = false;
        }
        permDistTextBox.Text = string.Empty;
        ModalPopupExtender8.Hide();

        permThanaCheckBoxList1.Items.Clear();
        
    }

    protected void Button32_OnClick(object sender, EventArgs e)
    {

        ModalPopupExtender9.Show();

        //for (int i = 0; i < permThanaCheckBoxList1.Items.Count; i++)
        //{
        //    if (permThanaCheckBoxList1.Items[i].Selected)
        //    {
        //        if (AlreadyExistOrNot(permThanaTextBox, permThanaCheckBoxList1.Items[i].Text))
        //        {
        //            permThanaCheckBoxList1.Text = permThanaCheckBoxList1.Items[i].Text + "," + permThanaCheckBoxList1.Text;
        //        }

        //    }

            
        //    //permThanaCheckBoxList1.Items[i].Selected = false;
        //}
        //permThanaTextBox.Text = string.Empty;
        //ModalPopupExtender9.Hide();
    }

    protected void Button33_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < permThanaCheckBoxList1.Items.Count; i++)
        {
            permThanaCheckBoxList1.Items[i].Selected = false;
        }

        permThanaTextBox.Text = "";
        ModalPopupExtender9.Hide();
    }

    protected void Button34_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender10.Show();
    }

    protected void Button35_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < nominationCheckBoxList.Items.Count; i++)
        {
            nominationCheckBoxList.Items[i].Selected = false;
        }
        nominationTextBox.Text = string.Empty;
        ModalPopupExtender10.Hide();
    }

    protected void Button22_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < bloodgroupCheckBoxList.Items.Count; i++)
        {
            if (bloodgroupCheckBoxList.Items[i].Selected)
            {
                if (AlreadyExistOrNot(bloodgroupTextBox, bloodgroupCheckBoxList.Items[i].Text))
                {
                    bloodgroupTextBox.Text = bloodgroupCheckBoxList.Items[i].Text + "," + bloodgroupTextBox.Text;
                }
                
            }
        }
        bloodgroupTextBox.Text = bloodgroupTextBox.Text.TrimEnd(',');
        ModalPopupExtender7.Hide();
    }

    protected void Button23_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender7.Hide();
    }

    protected void Button36_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < permDistCheckBoxList.Items.Count; i++)
        {
            if (permDistCheckBoxList.Items[i].Selected)
            {
                if (AlreadyExistOrNot(permDistTextBox, permDistCheckBoxList.Items[i].Text))
                {
                    permDistTextBox.Text = permDistCheckBoxList.Items[i].Text + "," + permDistTextBox.Text;
                }
                
            }
        }
        permDistTextBox.Text = permDistTextBox.Text.TrimEnd(',');
        ModalPopupExtender8.Hide();


        if (DistParam() != string.Empty)
        {
            DataTable dtstep = aReportDal.GetThana(DistParam2());
            permThanaCheckBoxList1.DataValueField = "ThanaID";
            permThanaCheckBoxList1.DataTextField = "Thana";
            permThanaCheckBoxList1.DataSource = dtstep;
            permThanaCheckBoxList1.DataBind();
        }
    }

    protected void Button37_OnClick(object sender, EventArgs e)
    {
       
        ModalPopupExtender8.Hide();
    }

    protected void Button38_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < permThanaCheckBoxList1.Items.Count; i++)
        {
            if (permThanaCheckBoxList1.Items[i].Selected)
            {
                if (AlreadyExistOrNot(permThanaTextBox, permThanaCheckBoxList1.Items[i].Text))
                {
                    permThanaTextBox.Text = permThanaCheckBoxList1.Items[i].Text + "," + permThanaTextBox.Text; 
                }                
            }
        }
        permThanaTextBox.Text = permThanaTextBox.Text.TrimEnd(',');
        ModalPopupExtender9.Hide();
    }

    protected void Button40_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < nominationCheckBoxList.Items.Count; i++)
        {
            if (nominationCheckBoxList.Items[i].Selected)
            {
                if (AlreadyExistOrNot(nominationTextBox, nominationCheckBoxList.Items[i].Text))
                {
                    nominationTextBox.Text = nominationCheckBoxList.Items[i].Text + "," + nominationTextBox.Text;
                }              
            }
        }
        nominationTextBox.Text = nominationTextBox.Text.TrimEnd(',');
        ModalPopupExtender10.Hide();
    }

    protected void Button39_OnClick(object sender, EventArgs e)
    {
        
        ModalPopupExtender9.Hide();
    }

    protected void Button41_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender10.Hide();
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

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=EmployeePromotionListInfo.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            loadGridView.AllowPaging = false;



            //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
            //            false;
            //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
            //   false;
            //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
            //   false;

            this.LoadData();

            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            loadGridView.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            loadGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in loadGridView.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in loadGridView.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }


            loadGridView.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> " + ddlCompany.SelectedItem.Text +
                                 "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " +
                                 DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3> Employee Information	</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void actionDateDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        diciplinaryDTRange.Visible = false;
        diciplinaryDTSingle.Visible = false;

        if (actionDateDropDownList.SelectedValue == "5")
        {
            diciplinaryDTRange.Visible = true;
        }
        else
        {
            diciplinaryDTSingle.Visible = true;
        }
    }


    protected void ddlTrainigStart_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        trainingRangeDt.Visible = false;
        trainingSingleDt.Visible = false;

        if (ddlTrainigStart.SelectedValue == "5")
        {
            trainingRangeDt.Visible = true;
        }
        else
        {
            trainingSingleDt.Visible = true;
        }
    }

    protected void ddlTrainingEnd_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        trainingEndRangeDt.Visible = false;
        trainingEndSingleDt.Visible = false;

        if (ddlTrainingEnd.SelectedValue == "5")
        {
            trainingEndRangeDt.Visible = true;
        }
        else
        {
            trainingEndSingleDt.Visible = true;
        }
    }

    protected void Button42_OnClick(object sender, EventArgs e)
    {
        emptypeDropDownList.SelectedValue = 0.ToString(); 
        employementDropDownList.SelectedValue = "";
    }

    protected void Button43_OnClick(object sender, EventArgs e)
    {
        joiningDateDropDownList.SelectedValue = 1.ToString();
        joiningDtTextBox.Text = "";
        joiningDtFrTextBox.Text = "";
        joiningDtToTextBox.Text = "";
        joiningDateDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void Button44_OnClick(object sender, EventArgs e)
    {
        retirementDropDownList.SelectedValue = 1.ToString();
        retTextBox.Text = "";
        retfromTextBox.Text = "";
        retToTextBox.Text = "";
        retirementDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void Button45_OnClick(object sender, EventArgs e)
    {
        ageDropDownList.SelectedValue = 1.ToString();
        ageTextBox.Text = "";
        ageMinTextBox.Text = "";
        ageMaxTextBox.Text = "";
        ageDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void Button46_OnClick(object sender, EventArgs e)
    {
        serviceLengthDropDownList.SelectedValue = 1.ToString();
        serviceLengthTextBox.Text = "";
        serviceLengthFrTextBox.Text = "";
        serviceLengthToTextBox.Text = "";
        serviceLengthDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void Button47_OnClick(object sender, EventArgs e)
    {
        endDateDropDownList.SelectedValue = 1.ToString();
        endDateTextBox.Text = "";
        endFromDateTextBox.Text = "";
        endToDateTextBox.Text = "";
        endDateDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void Button48_OnClick(object sender, EventArgs e)
    {
        dobDropDownList.SelectedValue = 1.ToString();
        dobTextBox.Text = "";
        dobfromTextBox.Text = "";
        dobtoTextBox.Text = "";
        dobDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void Button49_OnClick(object sender, EventArgs e)
    {
        confirmStDropDownList.SelectedValue = "";
        typeOfPosDropDownList.SelectedValue = "";
    }

    protected void Button50_OnClick(object sender, EventArgs e)
    {
        turnOverdtDropDownList.SelectedValue = 1.ToString();
        turnOverDateTextBox.Text = "";
        turnOverDateFrTextBox.Text = "";
        turnOverDatetoTextBox.Text = "";
        turnOverdtDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void gradeCheckBoxList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
         for (int i = 0; i < gradeCheckBoxList.Items.Count; i++)
        {
            if (gradeCheckBoxList.Items[i].Selected)
            {
                if (AlreadyExistOrNot(gradeTextBox, gradeCheckBoxList.Items[i].Text))
                {
                    gradeTextBox.Text = gradeCheckBoxList.Items[i].Text + "," + gradeTextBox.Text;
                }

                
            }
            
        }
        gradeTextBox.Text = gradeTextBox.Text.TrimEnd(',');
        ModalPopupExtender1.Hide();
        if (GradeParam() !=string.Empty)
        {
            DataTable dtstep = aReportDal.GetStep(GradeParam());
            stepCheckBoxList.DataValueField = "SalaryStepId";
            stepCheckBoxList.DataTextField = "SalaryStepName";
            stepCheckBoxList.DataSource = dtstep;
            stepCheckBoxList.DataBind();    
        }
    }

    protected void permDistCheckBoxList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < permDistCheckBoxList.Items.Count; i++)
        {
            if (permDistCheckBoxList.Items[i].Selected)
            {
                if (AlreadyExistOrNot(permDistTextBox, permDistCheckBoxList.Items[i].Text))
                {
                    permDistTextBox.Text = permDistCheckBoxList.Items[i].Text + "," + permDistTextBox.Text;
                }

            }
        }
        permDistTextBox.Text = permDistTextBox.Text.TrimEnd(',');
        ModalPopupExtender8.Hide();


        if (DistParam() != string.Empty)
        {
            DataTable dtstep = aReportDal.GetThana(DistParam2());
            permThanaCheckBoxList1.DataValueField = "ThanaID";
            permThanaCheckBoxList1.DataTextField = "Thana";
            permThanaCheckBoxList1.DataSource = dtstep;
            permThanaCheckBoxList1.DataBind();
        }
    }

    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        
    }

  

    protected void cblHeader_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void loadGridView_OnPageIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void loadGridView_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        this.LoadData();
    }

    protected void loadGridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "50px");
        }
    }
}