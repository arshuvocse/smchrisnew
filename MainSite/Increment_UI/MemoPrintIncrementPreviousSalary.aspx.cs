using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Increment_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Increment_UI_MemoPrintIncrementPreviousSalary : System.Web.UI.Page
{
    MemoPrintIncrementDAL aDAL = new MemoPrintIncrementDAL();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private int mid = 0;
    private int EmpID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {




            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                
          
                mid = int.Parse(Request.QueryString["mid"]);

                


                IncrementIdHiddenField.Value = mid.ToString();
                if (mid > 0)
                {
                    DataTable dtdata = new DataTable();
                    dtdata = aDAL.LoadMemoPrintIncrementByMId(mid);
                    if (dtdata.Rows.Count > 0)
                    {

                        lblLabelInfo.Text = dtdata.Rows[0]["HeaderInfo"].ToString();
                        ComId.Value = dtdata.Rows[0]["CompanyId"].ToString();
                     
                        Session["CompanyId"] = ComId.Value;
                        lblDate.Text =   Convert.ToDateTime(dtdata.Rows[0]["HeaderDate"]).ToString("MMMM dd, yyyy");
                        lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();
                        lblEmployeeCode.Text = dtdata.Rows[0]["EmpCode"].ToString();

                        lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();

                        lblCompany.Text = dtdata.Rows[0]["CompanyName"].ToString();


                        lblDepartment.Text = dtdata.Rows[0]["Department"].ToString();




                        lblOffice.Text = dtdata.Rows[0]["PlaceofPosting"].ToString();
                        txtPreSalStep.Text = dtdata.Rows[0]["PreviousStep"].ToString();
                        txtIncrementalStep.Text = dtdata.Rows[0]["IncrementalStep"].ToString();

                        txtSalutation.Text = dtdata.Rows[0]["Salutation"].ToString();
                          


                        txtComplimentaryClose.Text =  WebUtility.HtmlDecode(dtdata.Rows[0]["ComplimentaryClose"].ToString());
                        txtSincerely.Text = dtdata.Rows[0]["YoursSincerely"].ToString();
                        txtName.Text = WebUtility.HtmlDecode(dtdata.Rows[0]["Name"].ToString());
                        repEmpIdHiddenField.Value = (dtdata.Rows[0]["ToEmployee"].ToString());
                        LoadEmployeeData(mid);






                        string EffectDate = "";
                        DataTable dtdata255 = aDAL.LoadMemoPrintGetEffectivedateIncrementByMId(mid);
                        if (dtdata255.Rows.Count > 0)
                        {


                            HFSalaryGradeId.Value = dtdata255.Rows[0]["SalaryGradeId"].ToString();
                             

                            GradeCode.Value = dtdata255.Rows[0]["GradeCode"].ToString();
                            HFIncrementalStepId.Value = dtdata255.Rows[0]["IncrementalStepId"].ToString();

                            try
                            {
                                EffectDate = Convert.ToDateTime(dtdata255.Rows[0]["EffectiveDate"])
                                             .ToString("MMMM dd, yyyy"
);
                            }
                            catch (Exception)
                            {


                            }

                        }


                        DataTable aDataTable25 =
                    aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(HFIncrementalStepId.Value));
                        Decimal GrossAmount2 = 0;
                        try
                        {
                            GrossAmount2 = Math.Round(Convert.ToDecimal(aDataTable25.Rows[0]["GrossAmount"].ToString()), 0);
                        }
                        catch (Exception)
                        {


                        }
                        string Bodyofletter =
                      "We are glad to inform you that the Company has decided to give you one step annual increment in your existing salary grade, " +
                      GradeCode.Value + ", with effect from " +
                      EffectDate + ". Your revised monthly gross salary will be taka " +
                      GrossAmount2 + ".00 (" + NumberToText(Convert.ToInt64(GrossAmount2)) + " only) in " + txtIncrementalStep.Text + ". The complete detail of your restructured salary is highlighted in Table-1 of this letter. " +
                      EffectDate + ".";

                        txtBodyofletter.Text = Bodyofletter;
                        
                        using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(Convert.ToInt32(repEmpIdHiddenField.Value)))
                        {
                            EmployeeNameTextBox.Text = dtdesignation.Rows[0]["SignatureEmployee"].ToString();
                        }
                        txtCopyTO.Text = WebUtility.HtmlDecode(dtdata.Rows[0]["CopyTo"].ToString());
                        txtCopyTO.Text = txtCopyTO.Text.Replace("&amp", ""); 

                   //      LoadTasksUpdate();
                        editButton.Visible = true;

                        using (
                            DataTable aDataTable =
                                aDAL.LoadMemoPrintIncrementDetailsByMId(mid))
                        {
                            KeyResponGridView.DataSource = aDataTable;
                            KeyResponGridView.DataBind();

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


                                    DataTable dtdata2 = aDAL.LoadMemoPrintGetEffectivedateIncrementByMId(mid);
                                    if (dtdata2.Rows.Count > 0)
                                    {

                                        GradeCode.Value = dtdata2.Rows[0]["GradeCode"].ToString();
                                        HFSalaryGradeId.Value = dtdata2.Rows[0]["SalaryGradeId"].ToString();
                                        HFIncrementalStepId.Value = dtdata2.Rows[0]["IncrementalStepId"].ToString();
                                    }



                                    DataTable aDataTable2 =
                                        aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value),
                                            Convert.ToInt32(HFIncrementalStepId.Value));
                                    Decimal basicAmount = 0;
                                    try
                                    {
                                        basicAmount =
                                            Math.Round(
                                                Convert.ToDecimal(aDataTable2.Rows[0]["BasicAmount"].ToString()), 0);
                                    }
                                    catch (Exception)
                                    {


                                    }


                                    if (lbl_Particulars.Text.Trim() == "Basic Pay")
                                    {
                                        //txtIncrementalStep.Text=



                                        lbl_SalaryBreakUp.Text = Math.Round(basicAmount, 0).ToString();



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
                                        }

                                        if (lbl_Particulars.Text.Trim() == "Medical")
                                        {
                                            Medical = (Math.Round(basicAmount, 0)*10)/100;
                                            lbl_SalaryBreakUp.Text = Math.Round(Medical, 0).ToString();

                                        }


                                        for (int kk = 0; kk < aDataTable.Rows.Count; kk++)
                                        {


                                            if (lbl_Particulars.Text.Trim() == "Conveyance" &&
                                                (aDataTable.Rows[kk]["ParticularsName"].ToString().Trim() == "Conveyance"))
                                            {

                                                lbl_SalaryBreakUp.Text = Math.Round(
                                                    Convert.ToDecimal(aDataTable.Rows[kk]["SalaryBreakUp"].ToString()), 0)
                                                    .ToString();
                                                lbl_SalaryBreakUp.ReadOnly = false;
                                            }

                                        }

                                    }



                                    if (GradeCode.Value.Trim() == "M-6A" || GradeCode.Value.Trim() == "M-6B" ||
                                        GradeCode.Value.Trim() == "M-7" || GradeCode.Value.Trim() == "M-8" ||
                                        GradeCode.Value.Trim() == "M-9")
                                    {

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


                                        for (int kk = 0; kk < aDataTable.Rows.Count; kk++)
                                        {

                                            if (lbl_Particulars.Text.Trim() == "Medical" &&
                                                (aDataTable.Rows[kk]["ParticularsName"].ToString().Trim() == "Medical"))
                                            {

                                                lbl_SalaryBreakUp.Text = Math.Round(
                                                    Convert.ToDecimal(aDataTable.Rows[kk]["SalaryBreakUp"].ToString()), 0)
                                                    .ToString();
                                                lbl_SalaryBreakUp.ReadOnly = false;
                                            }

                                            if (lbl_Particulars.Text.Trim() == "Conveyance" &&
                                                (aDataTable.Rows[kk]["ParticularsName"].ToString().Trim() == "Conveyance"))
                                            {

                                                lbl_SalaryBreakUp.Text = Math.Round(
                                                    Convert.ToDecimal(aDataTable.Rows[kk]["SalaryBreakUp"].ToString()), 0)
                                                    .ToString();
                                                lbl_SalaryBreakUp.ReadOnly = false;

                                            }

                                            if (lbl_Particulars.Text.Trim() == "Washing" &&
                                                (aDataTable.Rows[kk]["ParticularsName"].ToString().Trim() == "Washing"))
                                            {
                                                lbl_SalaryBreakUp.Text = Math.Round(
                                                    Convert.ToDecimal(aDataTable.Rows[kk]["SalaryBreakUp"].ToString()), 0)
                                                    .ToString();

                                                lbl_Particulars.Visible = true;
                                                lbl_SalaryBreakUp.Visible = true;

                                                lbl_SalaryBreakUp.ReadOnly = false;
                                            }
                                        }

                                    }
                                }
                                catch (Exception)
                                {


                                }

                            }
                        }



                        decimal res = 0;


                        for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                        {



                            Label lbl_Particulars =
                               (Label)
                                   KeyResponGridView.Rows[i].FindControl("lbl_Particulars");
                            TextBox lbl_SalaryBreakUp =
                            (TextBox)
                                KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");
                            if (lbl_SalaryBreakUp.Text != "")
                            {


                                if (lbl_Particulars.Text.Trim() != "Total")
                                {
                                    decimal res2 = Convert.ToDecimal(lbl_SalaryBreakUp.Text);

                                    res += Math.Round(res2, 0);
                                }
                            }
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
                   aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(HFIncrementalStepId.Value));

                                Decimal GrossAmount = 0;
                                try
                                {
                                    GrossAmount = Math.Round(Convert.ToDecimal(aDataTable.Rows[0]["GrossAmount"].ToString()), 0);
                                }
                                catch (Exception)
                                {


                                }

                                lbl_Particulars.Font.Bold = true;
                                lbl_SalaryBreakUp.Text = GrossAmount.ToString();
                                lbl_SalaryBreakUp.ReadOnly = true;



                            }


                            if (lbl_Particulars.Text.Trim() == "Medical")
                            {



                                DataTable aDataTable =
                     aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(HFIncrementalStepId.Value));

                                Decimal GrossAmount = 0;
                                try
                                {
                                    GrossAmount = Math.Round(Convert.ToDecimal(aDataTable.Rows[0]["GrossAmount"].ToString()), 0);
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
                                    }
                                }
                            }
                        }

                    }

                    else
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["EmpId"]))
                        {
                            

                              EmpID = int.Parse(Request.QueryString["EmpId"]);
                 
                            lblDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
                            LoadEmployeeData(Convert.ToInt32(mid));
                            MethodAutoId();
                            lblLabelInfo.Text = ComName.Value + "/HR/" + DateTime.Now.Year + " - " +
                                                MasterIdHiddenField.Value.ToString();
                            // LoadTasks();
                            submitButton.Visible = true;
                            string EffectDate = "";
                          DataTable  dtdata2 = aDAL.LoadMemoPrintGetEffectivedateIncrementByMId(mid);
                            if (dtdata2.Rows.Count > 0)
                            {


                                HFSalaryGradeId.Value = dtdata2.Rows[0]["SalaryGradeId"].ToString();
                                DataTable dtdataSig = aDAL.LoadSignaturePerson(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(ComId.Value));

                                if (dtdataSig.Rows.Count > 0)
                                {
                                    repEmpIdHiddenField.Value = dtdataSig.Rows[0]["EmployeeId"].ToString();

                                    DataTable dtdEmp = aDAL.LoadEmpName(Convert.ToInt32(repEmpIdHiddenField.Value));
                                    EmployeeNameTextBox.Text = dtdEmp.Rows[0]["EmpName"].ToString();

                                }

                                GradeCode.Value = dtdata2.Rows[0]["GradeCode"].ToString();
                                HFIncrementalStepId.Value = dtdata2.Rows[0]["IncrementalStepId"].ToString();

                                try
                                {
                                       EffectDate =    Convert.ToDateTime(dtdata2.Rows[0]["EffectiveDate"])
                                                    .ToString("MMMM dd, yyyy"
);
                                }
                                catch (Exception)
                                {
                                    
                                    
                                }
                                
                            }
                          

                            DataTable dtPart = aDAL.LoadParticularsGridView();
                            if (dtPart.Rows.Count > 0)
                            {
                                KeyResponGridView.DataSource = dtPart;
                                KeyResponGridView.DataBind();
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
                             aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(HFIncrementalStepId.Value));
                                        Decimal basicAmount = 0;
                                        try
                                        {
                                            basicAmount = Math.Round(Convert.ToDecimal(aDataTable.Rows[0]["BasicAmount"].ToString()),0);
                                        }
                                        catch (Exception)
                                        {


                                        }


                                        if (lbl_Particulars.Text.Trim() == "Basic Pay")
                                        {
                                            //txtIncrementalStep.Text=



                                            lbl_SalaryBreakUp.Text = Math.Round(basicAmount, 0).ToString();



                                        }

                                        if (GradeCode.Value.Trim() == "Special" || GradeCode.Value.Trim() == "M-1" || GradeCode.Value.Trim() == "M-2A" || GradeCode.Value.Trim() == "M-2B" || GradeCode.Value.Trim() == "M-3A" || GradeCode.Value.Trim() == "M-3B" || GradeCode.Value.Trim() == "M-4" || GradeCode.Value.Trim() == "M-5")
                                        {
                                            decimal Medical = 0;
                                            decimal HouseResnt = 0;
                                            decimal Conveyance = 0;

                                            if (lbl_Particulars.Text.Trim() == "House Rent")
                                            {
                                                HouseResnt = (Math.Round(basicAmount, 0) * 50) / 100;
                                                lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();
                                            }

                                            if (lbl_Particulars.Text.Trim() == "Medical")
                                            {
                                                Medical = (Math.Round(basicAmount, 0) * 10) / 100;
                                                lbl_SalaryBreakUp.Text = Math.Round(Medical, 0).ToString();

                                            }

                                            if (lbl_Particulars.Text.Trim() == "Conveyance")
                                            {
                                                Conveyance = 0;
                                                lbl_SalaryBreakUp.Text = Conveyance.ToString();
                                                lbl_SalaryBreakUp.ReadOnly = false;
                                            }


                                            if (lbl_Particulars.Text.Trim() == "Washing")
                                            {
                                                lbl_SalaryBreakUp.Text = "0";
                                                lbl_Particulars.Text = "";

                                                lbl_Particulars.Visible = false;
                                                lbl_SalaryBreakUp.Visible = false;


                                            }
                                            //basicAmount

                                        }


                                        if (GradeCode.Value.Trim() == "M-6A" || GradeCode.Value.Trim() == "M-6B" || GradeCode.Value.Trim() == "M-7" || GradeCode.Value.Trim() == "M-8" || GradeCode.Value.Trim() == "M-9")
                                        {
                                            decimal Medical = 0;
                                            decimal HouseResnt = 0;
                                            decimal Conveyance = 0;

                                            if (lbl_Particulars.Text.Trim() == "House Rent")
                                            {
                                                HouseResnt = (Math.Round(basicAmount, 0) * 75) / 100;
                                                lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();
                                            }

                                            if (lbl_Particulars.Text.Trim() == "Medical")
                                            {
                                                Medical = (Math.Round(basicAmount, 0) * 10) / 100;
                                                lbl_SalaryBreakUp.Text = Math.Round(Medical, 0).ToString();

                                            }

                                            if (lbl_Particulars.Text.Trim() == "Conveyance")
                                            {
                                                Conveyance = (Math.Round(basicAmount, 0) * 5) / 100;
                                                lbl_SalaryBreakUp.Text = Math.Round(Conveyance, 0).ToString();


                                            }

                                            if (lbl_Particulars.Text.Trim() == "Washing")
                                            {
                                                lbl_SalaryBreakUp.Text = "0";
                                                lbl_Particulars.Text = "";

                                                lbl_Particulars.Visible = false;
                                                lbl_SalaryBreakUp.Visible = false;


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
                                                HouseResnt = (Math.Round(basicAmount, 0) * 63) / 100;
                                                lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();
                                            }

                                            if (lbl_Particulars.Text.Trim() == "Medical")
                                            {
                                                Medical = 0;
                                                lbl_SalaryBreakUp.Text = Medical.ToString();
                                                lbl_SalaryBreakUp.ReadOnly = false;
                                            }

                                            if (lbl_Particulars.Text.Trim() == "Conveyance")
                                            {
                                                Conveyance = 0;
                                                lbl_SalaryBreakUp.Text = Conveyance.ToString();
                                                lbl_SalaryBreakUp.ReadOnly = false;

                                            }

                                            if (lbl_Particulars.Text.Trim() == "Washing")
                                            {
                                                lbl_SalaryBreakUp.Text = "0";

                                                lbl_Particulars.Visible = true;
                                                lbl_SalaryBreakUp.Visible = true;

                                                lbl_SalaryBreakUp.ReadOnly = false;
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
                           aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(HFIncrementalStepId.Value));

                                        Decimal GrossAmount = 0;
                                        try
                                        {
                                            GrossAmount = Math.Round(Convert.ToDecimal(aDataTable.Rows[0]["GrossAmount"].ToString()), 0);
                                        }
                                        catch (Exception)
                                        {


                                        }
                                       
                                            lbl_Particulars.Font.Bold = true;
                                            lbl_SalaryBreakUp.Text = GrossAmount.ToString();
                                            lbl_SalaryBreakUp.ReadOnly = true; 
                  

                                       
                                    }

                                    if (lbl_Particulars.Text.Trim() == "Medical")
                                    {



                                        DataTable aDataTable =
                             aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(HFIncrementalStepId.Value));

                                        Decimal GrossAmount = 0;
                                        try
                                        {
                                            GrossAmount = Math.Round(Convert.ToDecimal(aDataTable.Rows[0]["GrossAmount"].ToString()), 0);
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
                                            }    
                                        }
                                    }
                                }
                                DataTable aDataTable2 =
                         aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(HFIncrementalStepId.Value));
                                Decimal GrossAmount2 = 0;
                                try
                                {
                                    GrossAmount2 = Math.Round(Convert.ToDecimal(aDataTable2.Rows[0]["GrossAmount"].ToString()), 0);
                                }
                                catch (Exception)
                                {


                                }
                              
                                string Bodyofletter =
                              "We are glad to inform you that the Company has decided to give you one step annual increment in your existing salary grade, " +
                              GradeCode.Value + ", with effect from " +
                              EffectDate + ". Your revised monthly gross salary will be taka " +
                              GrossAmount2 + ".00 (" + NumberToText(Convert.ToInt64(GrossAmount2)) + " only) in " + txtIncrementalStep.Text+ ". The complete detail of your restructured salary is highlighted in Table-1 of this letter. " +
                              EffectDate + ".";
                              
                                txtBodyofletter.Text = Bodyofletter;
                            }

                       
                        }
                    }

                   
                }
            }


        }
    }
 
     public static string NumberToText(long number)
    {
        StringBuilder wordNumber = new StringBuilder();                       
 
        string[] powers = new string[] { "Thousand ", "Million ", "Billion " };
        string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        string[] ones = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", 
                                       "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
 
        if (number == 0) { return "Zero"; }
        if (number < 0) 
        { 
            wordNumber.Append("Negative ");
            number = -number;
        }
 
        long[] groupedNumber = new long[] { 0, 0, 0, 0 };
        int groupIndex = 0;
 
        while (number > 0)
        {
            groupedNumber[groupIndex++] = number % 1000;
            number /= 1000;
        }
 
        for (int i = 3; i >= 0; i--)
        {
            long group = groupedNumber[i];
 
            if (group >= 100)
            {
                wordNumber.Append(ones[group / 100 - 1] + " Hundred ");
                group %= 100;
 
                if (group == 0 && i > 0)
                    wordNumber.Append(powers[i - 1]);
            }
 
            if (group >= 20)
            {
                if ((group % 10) != 0)
                    wordNumber.Append(tens[group / 10 - 2] + " " + ones[group % 10 - 1] + " ");
                else
                    wordNumber.Append(tens[group / 10 - 2] + " ");                    
            }
            else if (group > 0)
                wordNumber.Append(ones[group - 1] + " ");
 
            if (group != 0 && i > 0)
                wordNumber.Append(powers[i - 1]);
        }
 
        return wordNumber.ToString().Trim();
    }

    private void MethodAutoId()
    {
        DataTable dt = aDAL.GetId(Convert.ToInt32(ComId.Value));
        MasterIdHiddenField.Value = NoGenerator(Convert.ToInt32(dt.Rows[0][0].ToString()));
    }


    private string  NoGenerator(int id)
    {
        string code = string.Empty;
        int Id = id+1;

 
        code =  Id.ToString();
        return code;
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("IncrementView.aspx");
    }


    private void LoadEmployeeData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aDAL.LoadEmpAllInfofById(id);
        if (dtdata.Rows.Count > 0)
        {

            EmpIdHiddenfield.Value = dtdata.Rows[0]["EmployeeId"].ToString();
            ComId.Value = dtdata.Rows[0]["CompanyId"].ToString();
            Session["CompanyId"] = "";
            Session["CompanyId"] = ComId.Value;
            lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();
            ComName.Value = dtdata.Rows[0]["ShortName"].ToString();


            
                txtSalutation.Text = "Dear " + dtdata.Rows[0]["EmpName"].ToString() + ", ";
            
          
            

            lblCompany.Text = dtdata.Rows[0]["CompanyName"].ToString();
            lblEmployeeCode.Text = dtdata.Rows[0]["EmployeeCode"].ToString();
           
            lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();




            lblDepartment.Text = dtdata.Rows[0]["DivisionName"].ToString();




            lblOffice.Text = dtdata.Rows[0]["SalaryLocation"].ToString();
            txtPreSalStep.Text = dtdata.Rows[0]["CurrentStep"].ToString();
            txtIncrementalStep.Text = dtdata.Rows[0]["IncrementalStep"].ToString();
        


        }
    }

  

    protected void submitButton_Click(object sender, EventArgs e)
    {

        if (IncrementIdHiddenField.Value != "" && IncrementIdHiddenField.Value!=null)
        {
             DataTable aTable =
                             aDAL.DeleteValidattionForEffectiveDate(Convert.ToInt32(IncrementIdHiddenField.Value).ToString());
            if (aTable.Rows.Count > 0)
            {
                ShowMessageBox("Already Exist!! Print It or Reload this Page...");
              
            }
            else
            {
                Save();
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Operation Successfull...');",
                         true);
            }
        }
    }

    public void Save()
    {

        if (Validation())
        {
            MemoPrintIncrementDAO aDAO = new MemoPrintIncrementDAO();

            aDAO.IncrementId = Convert.ToInt32(IncrementIdHiddenField.Value);
            aDAO.CompanyId = Convert.ToInt32(ComId.Value);
            aDAO.HeaderInfo = lblLabelInfo.Text;
            aDAO.HeaderDate = Convert.ToDateTime(lblDate.Text);
            aDAO.EmpCode = lblEmployeeCode.Text;
            aDAO.EmpName = lblEmp.Text;
            aDAO.Designation = lblDesignation.Text;
            aDAO.Department = lblDepartment.Text;
            aDAO.PreviousStep = txtPreSalStep.Text;
            aDAO.PlaceofPosting = lblOffice.Text;
            aDAO.IncrementalStep = txtIncrementalStep.Text;
            aDAO.Salutation = txtSalutation.Text;
            aDAO.FirstParagraph = WebUtility.HtmlEncode(txtBodyofletter.Text);
            aDAO.ComplimentaryClose = WebUtility.HtmlEncode(txtComplimentaryClose.Text);
            aDAO.YoursSincerely = txtSincerely.Text;
            aDAO.Name = WebUtility.HtmlEncode(txtName.Text);
            aDAO.Name = WebUtility.HtmlEncode(txtName.Text);
            aDAO.CopyTo = WebUtility.HtmlEncode(txtCopyTO.Text);
            aDAO.ToEmployee = Convert.ToInt32(repEmpIdHiddenField.Value);

            aDAO.CompanyName = lblCompany.Text;
            aDAO.Subject = txtSubject.Text;

            int id = aDAL.SaveInfo(aDAO);

            aDAL.DeleteMemoIncrementDetails(aDAO.IncrementId.ToString());
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                TextBox lbl_SalaryBreakUp =
                                       (TextBox)
                                           KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");
                Label lbl_Particulars =
                  (Label)
                      KeyResponGridView.Rows[i].FindControl("lbl_Particulars");
                if (lbl_Particulars.Text.Trim() != "")
                {
                    MemoPrintIncrementDetailsDAO ADetailsDao = new MemoPrintIncrementDetailsDAO()
                    {




                        MemoIncrementId = aDAO.IncrementId,

                        PName = lbl_Particulars.Text.Trim(),
                        PAmount = Convert.ToDecimal(lbl_SalaryBreakUp.Text.Trim())

                    };
                    int idd =
                        aDAL.MemoIncrementDetailsSaveInfo(
                            ADetailsDao);
                }
            }


            



       

            //Response.Redirect("MemoPrintIncrement.aspx?mid=" + mid + "&EmpId=" + EmpID);
        }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (IncrementIdHiddenField.Value != "" && IncrementIdHiddenField.Value != null)
        {
            DataTable aTable =
                            aDAL.DeleteValidattionForEffectiveDate(Convert.ToInt32(IncrementIdHiddenField.Value).ToString());
            if (aTable.Rows.Count > 0)
            {
                

            }
            else
            {
                Save();

            }
        }
        
        int midd = int.Parse(Request.QueryString["mid"]);
        PopUp(midd);
    }

    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/MemoPrintIncrementReportViwer.aspx?rptType=" + EmpInfoId + "&rt=MemoPI"  ;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
              Update();
        }
     
    }
    private bool Validation()
    {

        if (KeyResponGridView.Rows.Count==0)
        {
            aShowMessage.ShowMessageBox("Please Enter Particulars & Salary Break-down", this);
            txtPName.Focus();
            return false;
        }


        if (EmployeeNameTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            EmployeeNameTextBox.Focus();
            return false;
        }


        for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
        {


            TextBox lbl_SalaryBreakUp =
                (TextBox)
                    KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");

            if (lbl_SalaryBreakUp.Text == "")
            {
                aShowMessage.ShowMessageBox(aMessages.VArea, this);
                lbl_SalaryBreakUp.Focus();
                return false;
            }
        }





        return true;
    }
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private void Update()
    {
        MemoPrintIncrementDAO aDAO = new MemoPrintIncrementDAO();

        aDAO.IncrementId = Convert.ToInt32(IncrementIdHiddenField.Value);
        aDAO.CompanyId = Convert.ToInt32(ComId.Value);
        aDAO.HeaderInfo = lblLabelInfo.Text;
        aDAO.HeaderDate = Convert.ToDateTime(lblDate.Text);
        aDAO.EmpCode = lblEmployeeCode.Text;
        aDAO.EmpName = lblEmp.Text;
        aDAO.Designation = lblDesignation.Text;
        aDAO.Department = lblDepartment.Text;
        aDAO.PreviousStep = txtPreSalStep.Text;
        aDAO.IncrementalStep = txtIncrementalStep.Text;
        aDAO.Salutation = txtSalutation.Text;
        aDAO.FirstParagraph = WebUtility.HtmlEncode(txtBodyofletter.Text);
        aDAO.ComplimentaryClose = WebUtility.HtmlEncode(txtComplimentaryClose.Text);
        aDAO.YoursSincerely = txtSincerely.Text;
        aDAO.Name = WebUtility.HtmlEncode(txtName.Text);
        aDAO.CopyTo = WebUtility.HtmlEncode(txtCopyTO.Text);
        aDAO.ToEmployee = Convert.ToInt32(repEmpIdHiddenField.Value);
        aDAO.CompanyName = lblCompany.Text;


        aDAL.UpdateInfo(aDAO);

        aDAL.DeleteMemoIncrementDetails(aDAO.IncrementId.ToString());
        for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
        {
            TextBox lbl_SalaryBreakUp =
                                   (TextBox)
                                       KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");
            Label lbl_Particulars =
              (Label)
                  KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

            if (lbl_Particulars.Text.Trim() != "")
            {
                MemoPrintIncrementDetailsDAO ADetailsDao = new MemoPrintIncrementDetailsDAO()
                {
                    MemoIncrementId = aDAO.IncrementId,

                    PName = lbl_Particulars.Text.Trim(),
                    PAmount = Convert.ToDecimal(lbl_SalaryBreakUp.Text.Trim())

                };
                int idd =
                    aDAL.MemoIncrementDetailsSaveInfo(
                        ADetailsDao);
            }
        }


        ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Data Updated Successfull...');",
                  true);
    }

    protected void deleteImageButtonDirectlySupervices_OnClick(object sender, ImageClickEventArgs e)
    {
        
    }

    protected void editImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;


        string jd = KeyResponGridView.Rows[rowindex].Cells[0].Text;
        string amount = KeyResponGridView.Rows[rowindex].Cells[1].Text;
        txtPName.Text = jd;
        txtPAmount.Text = amount;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("PName");
        aDataTable.Columns.Add("PAmount");

        DataRow dataRow;

        if (KeyResponGridView.Rows.Count > 0)
        {
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["PName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                    dataRow["PAmount"] = KeyResponGridView.Rows[i].Cells[1].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        KeyResponGridView.DataSource = aDataTable;
        KeyResponGridView.DataBind();
    }

    protected void deleteImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("PName");
        aDataTable.Columns.Add("PAmount");

        DataRow dataRow;

        if (KeyResponGridView.Rows.Count > 0)
        {
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["PName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                    dataRow["PAmount"] = KeyResponGridView.Rows[i].Cells[1].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        KeyResponGridView.DataSource = aDataTable;
        KeyResponGridView.DataBind();
    }

    protected void EducationRequirementImageButton_Click(object sender, EventArgs e)
    {
        if (AddEducationRequirementValidation())
        {
            
            string educationRequirements = txtPName.Text.Trim();
            string Major = txtPAmount.Text.Trim();

            var aDataTable = new DataTable();


            aDataTable.Columns.Add("PName");
            aDataTable.Columns.Add("PAmount");



            DataRow dataRow;

            if (KeyResponGridView.Rows.Count > 0)
            {
                for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                {
                    if (KeyResponGridView.Rows[i].Cells[0].Text != educationRequirements)
                    {
                        dataRow = aDataTable.NewRow();


                        dataRow["PName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                        dataRow["PAmount"] = KeyResponGridView.Rows[i].Cells[1].Text;

                        aDataTable.Rows.Add(dataRow);
                        txtPName.Text = "";
                        txtPAmount.Text = "";
                    }

                    else
                    {
                        txtPName.Text = "";
                        txtPAmount.Text = "";
                        ShowMessageBox("Data already Exist !!");
                    }
                }

                dataRow = aDataTable.NewRow();


                dataRow["PName"] = educationRequirements;
                dataRow["PAmount"] = Major;

                aDataTable.Rows.Add(dataRow);

                KeyResponGridView.DataSource = aDataTable;
                KeyResponGridView.DataBind();
                txtPName.Text = "";
                txtPAmount.Text = "";

            }


            else
            {
                dataRow = aDataTable.NewRow();

                dataRow["PName"] = educationRequirements;
                dataRow["PAmount"] = Major;


                aDataTable.Rows.Add(dataRow);

                KeyResponGridView.DataSource = aDataTable;
                KeyResponGridView.DataBind();
                txtPName.Text = "";
                txtPAmount.Text = "";
            }
        }

    }
    private bool AddEducationRequirementValidation()
    {
        if (txtPName.Text == "")
        {
            ShowMessageBox("Please Enter Particulars !!!");
            txtPName.Focus();
            return false;
        }


        if (txtPAmount.Text == "")
        {
            ShowMessageBox("Please Enter Salary Break-Up !!!");
            txtPAmount.Focus();
            return false;
        }

        return true;
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void EmployeeNameTextBox_OnTextChanged(object sender, EventArgs e)
    {
        string empName = EmployeeNameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            EmployeeNameTextBox.Text = emp[2];
            repEmpIdHiddenField.Value = emp[0];


        }
        else
        {

            EmployeeNameTextBox.Text = "";
            repEmpIdHiddenField.Value = "";
            ShowMessageBox("Input Correct Data !!");
        }
    }

    protected void lbl_SalaryBreakUp_OnTextChanged(object sender, EventArgs e)
    {
        decimal res = 0;


        for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
        {



            Label lbl_Particulars =
               (Label)
                   KeyResponGridView.Rows[i].FindControl("lbl_Particulars");
            TextBox lbl_SalaryBreakUp =
            (TextBox)
                KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");
            if (lbl_SalaryBreakUp.Text!="")
            {
                
          
            if (lbl_Particulars.Text.Trim() != "Total")
            {
                decimal res2 = Convert.ToDecimal(lbl_SalaryBreakUp.Text);

                res += Math.Round(res2, 0);
            }
            }
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
                lbl_Particulars.Font.Bold = true;
                lbl_SalaryBreakUp.Text = res.ToString();
                lbl_SalaryBreakUp.ReadOnly = true;
            }
        }
    }
}