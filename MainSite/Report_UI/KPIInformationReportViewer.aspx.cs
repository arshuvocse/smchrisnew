using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DAL.Appraisal;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;

public partial class Report_UI_KPIInformationReportViewer : System.Web.UI.Page
{
    ReportDocument rptdoc = new ReportDocument();

    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();


    protected void Page_Init(object sender, EventArgs e)
    {
        
        int empId = int.Parse(Request.QueryString["EmpInfoId"]);
        int finYear = int.Parse(Request.QueryString["financialYearId"]);

        DataSet mainDS = new DataSet();

        if (empId != null)
        {

            DataTable allDataTable = new DataTable();

            allDataTable = _appPartA.GetEmployeeDetailsKPIREport(Convert.ToInt32(empId), finYear).Copy();
            allDataTable.TableName = "EmployeeInfoListDataTable";
            mainDS.Tables.Add(allDataTable);

            DataTable dtfin = _appPartA.GetFinYear( Convert.ToInt32(finYear));
            string finYeartxt = "";
            if (dtfin.Rows.Count > 0)
            {
                finYeartxt = dtfin.Rows[0]["FinancialYearDesc"].ToString();
            }
         
            DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(Convert.ToInt32(empId), finYeartxt);


            if (dtaa.Rows.Count > 0)
            {

                int mid = Convert.ToInt32(dtaa.Rows[0][0]);



                DataTable dtMaster = _appPartA.GetAppraisalMAstet(mid).Copy();
                dtMaster.TableName = "GetAppraisalSelfMaster";
                mainDS.Tables.Add(dtMaster);


                DataTable gtGetApprisalMasterId = _appPartA.GetAppraisalMasterIdFromAppraisalSelfMasterId(Convert.ToInt32(mid));

                int AppraisalMasterId = 0;
                if (gtGetApprisalMasterId.Rows.Count > 0)
                {
                    AppraisalMasterId = Convert.ToInt32(gtGetApprisalMasterId.Rows[0]["AppraisalMasterId"].ToString());
                }


                DataTable dtFunc = _appPartA.GetAppraisalFuncDetails(AppraisalMasterId).Copy();
        

                if (dtFunc.Rows.Count>0)
                {
                    dtFunc.TableName = "GetAppraisalSelfDetails";
                    mainDS.Tables.Add(dtFunc);
                }
                else
                {

                    DataTable dt2 = _appPartA.GetAppraisalSelfDetails(mid).Copy();
                    dt2.TableName = "GetAppraisalSelfDetails";
                    mainDS.Tables.Add(dt2);
                }



                DataTable dtAppraisalfB = new DataTable();
                dtAppraisalfB = _appPartA.GetAppraisalfB(AppraisalMasterId).Copy();
               
                if (dtAppraisalfB.Rows.Count > 0)
                {
                    dtAppraisalfB.TableName = "GetAppraisalSelfB";
                    mainDS.Tables.Add(dtAppraisalfB);
                }
                else
                {

                DataTable d3 = new DataTable();
                d3 = _appPartA.GetAppraisalSelfB_Rpt(mid).Copy();
                d3.TableName = "GetAppraisalSelfB";
                mainDS.Tables.Add(d3);
                }

                //DataTable dtw2 = _appPartA.GetAppraisalfDetailsFromSup(Convert.ToInt32(mid));
                //GridView1.DataSource = dtw2;
                //GridView1.DataBind();

                DataTable dtw2 = new DataTable();
                dtw2 = _appPartA.GetAppraisalfDetailsFromSup(Convert.ToInt32(mid)).Copy();
                dtw2.TableName = "GetAppraisalFuncAreaapp";
                mainDS.Tables.Add(dtw2);


               

                DataTable dtTrain = new DataTable();
                dtTrain = _appPartA.GetAppraisalTraining(Convert.ToInt32(AppraisalMasterId)).Copy();
                dtTrain.TableName = "AppraisalTraining";
                mainDS.Tables.Add(dtTrain);



                DataTable dtFinalStatus = new DataTable();
                dtFinalStatus = _appraisalPartBdal.GetAppraiSalFinalStatusrpt(Convert.ToInt32(AppraisalMasterId)).Copy();


                if (dtFinalStatus.Rows.Count > 0)
                {
                    try
                    {
                        decimal funcMark = 0;
                        decimal behaveMark = 0;
                        try
                        {
                            funcMark = dtFinalStatus.AsEnumerable().Sum(row => row.Field<decimal>("funcMark"));
                        }
                        catch (Exception)
                        {

                            //throw;
                        }
                        try
                        {
                            behaveMark = dtFinalStatus.AsEnumerable().Sum(row => row.Field<decimal>("behaveMark"));
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        decimal res = funcMark + behaveMark;

                        if (res == 0)
                        {
                            dtFinalStatus = _appraisalPartBdal.GetAppraiSalFinalStatusrptSelf(Convert.ToInt32(AppraisalMasterId)).Copy();
                        }


                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }

                if (dtFinalStatus.Rows.Count==0)
                {
                    dtFinalStatus = _appraisalPartBdal.GetAppraiSalFinalStatusrptSelf(Convert.ToInt32(AppraisalMasterId)).Copy();

                    if (dtFinalStatus.Rows.Count==0)
                    {
                        dtFinalStatus = _appraisalPartBdal.GetAppraiSalFinalStatusrptSelf_KPI(Convert.ToInt32(mid)).Copy();
                         
                    }
                }

            
              

                dtFinalStatus.TableName = "AppraisalFinalStatus";
                mainDS.Tables.Add(dtFinalStatus);

            }


            if (mainDS.Tables[0].Rows.Count > 0)
            { 
          //                mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsKPIInformationDetails.xsd"));
      ShowReport(mainDS, "crpKPIInformamationDetails.rpt");


                //rptdoc.SetDataSource(mainDS);

                //rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
                //    "ProformaInvoice-" + "");
            }

        }

    }
    private void ShowReport(DataSet dsDataSet, string reportName)
    {
        if (dsDataSet.Tables[0].Rows.Count > 0)
        {
            rptdoc.Load(ReportPath(reportName));
            rptdoc.SetDataSource(dsDataSet);
            crReportViewer.ReportSource = rptdoc;
            crReportViewer.DataBind();
        }
        else
        {
            lblMsg.Text = "No Data Found!!!!";
        }

    }
    private string ReportPath(string rptName)
    {
        return Convert.ToString(Server.MapPath("~\\Reports\\CrystalReports\\" + rptName));

    }
    protected void rptViewerBasic_Unload(object sender, EventArgs e)
    {
        if (this.rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
            crReportViewer.Dispose();
        }
    }

    protected void rptViewerBasic_Disposed(object sender, EventArgs e)
    {
        if (this.rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
            crReportViewer.Dispose();
        }
    }
}