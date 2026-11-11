 
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using DAL.Increment_DAL;
using DAL.MPBudget;
using DAL.UserPermissions_DAL;


public partial class Report_UI_MemoPrintIncrementReportViwer : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();


    MemoPrintIncrementDAL aDAL = new MemoPrintIncrementDAL();
    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
        string rtT = Request.QueryString["rt"];
      

        DataSet mainDS = new DataSet();


        if (rtT == "MemoPI")
        {
              if (rptType != "")
        {
            
             
                DataTable allDataTable = new DataTable();
                allDataTable = aDAL.GetMemoPrintIncrementInfoDALrpt(rptType).Copy();
                allDataTable.TableName = "MemoIncrementListDataTable";
                mainDS.Tables.Add(allDataTable);
            string ComIDd = "";
                if (allDataTable.Rows.Count>0)
            {
                ComIDd = allDataTable.Rows[0]["CompanyId"].ToString();
            }

             

                DataTable DetailsDataTable = new DataTable();
                DetailsDataTable = aDAL.LoadParticularsDetailsrpt(rptType).Copy();
                DetailsDataTable.TableName = "MemoIncrementDetailsListDataTable";
                mainDS.Tables.Add(DetailsDataTable);


                DataTable DetailsDataTable2 = new DataTable();
                DetailsDataTable2 = aDAL.LoadParticularsDetailsrpt_Total(rptType).Copy();
                int a = Convert.ToInt32(DetailsDataTable2.Rows[0]["PAmount"]);

                DataTable DetailsDataTableAmountInword = new DataTable();
                DetailsDataTableAmountInword = aDAL.LoadParticularsDetailsrpt_TotalAmountInWord(a).Copy();
                DetailsDataTableAmountInword.TableName = "MemoIncrementAmountInwordDataTable";
                mainDS.Tables.Add(DetailsDataTableAmountInword);


                if (mainDS.Tables[0].Rows.Count > 0)
                {
                    string comid = Request.QueryString["rt"];
                     //  mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\MemoIncrementAmountInwordDataTable.xsd"));
                    if (ComIDd == "1")
                    {
                        ShowReport(mainDS, "crpMemoIncrementListInfoReport.rpt");

                    }
                    else
                    {
                      //  ShowReport(mainDS, "crpMemoIncrementListInfoReportSMCEL.rpt");

                        ShowReport(mainDS, "crpMemoIncrementListInfoReport2.rpt");
                        
                    }
                      
                       //rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
                       //  "Annual Increment Letter Information_" + DateTime.Now);
                }
             
           
        }
        }



        if (rtT == "MemoPIAll")
        {
            if (rptType != "")
            {
                string PrintType = Request.QueryString["PrintType"];


                DataTable allDataTable = new DataTable();
                allDataTable = aDAL.GetMemoPrintIncrementInfoDALrptAll(rptType).Copy();
                allDataTable.TableName = "MemoIncrementListDataTable";
                mainDS.Tables.Add(allDataTable);

                string ComIDd = "";
                if (allDataTable.Rows.Count > 0)
                {
                    ComIDd = allDataTable.Rows[0]["CompanyId"].ToString();
                }
             
                //string empName = rptType.Trim();

                //if (empName.Contains(','))
                //{
                //    string[] emp = empName.Split(',');

                //    for (int i = 0; i < empName.Length; i++)
                //    {
                //        string mmm = emp[i];

                //        DataTable DetailsDataTable = new DataTable();
                //        DetailsDataTable = aDAL.LoadParticularsDetailsrptALL(mmm).Copy();
                //        DetailsDataTable.TableName = "MemoIncrementDetailsListDataTable";
                //        mainDS.Tables.Add(DetailsDataTable);
                //    }
                   

                
                //}
                //else
                //{
                    DataTable DetailsDataTable = new DataTable();
                    DetailsDataTable = aDAL.LoadParticularsDetailsrptALL(rptType).Copy();
                    DetailsDataTable.TableName = "MemoIncrementDetailsListDataTable";
                    mainDS.Tables.Add(DetailsDataTable);
                //}


                if (mainDS.Tables[0].Rows.Count > 0)
                {
                //    mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\MemoIncrementListInfo.xsd"));
                 
                    //rptdoc.Refresh();
                    //rptdoc.PrintToPrinter(2, true, 1, 2);
                    if (PrintType == "PDF")
                    {
                    if (ComIDd == "1")
                    {
                        ShowReport(mainDS, "crpMemoIncrementListInfoReportMulti.rpt");
                        rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
                  "Annual Increment Letter Information_" + DateTime.Now);

                    }
                    else
                    {
                       // ShowReport(mainDS, "crpMemoIncrementListInfoReportMultiSMCEL.rpt");
                        ShowReport(mainDS, "crpMemoIncrementListInfoReportMulti2.rpt");

                        rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
                "Annual Increment Letter Information_" + DateTime.Now);
                    }
                    }

                    if (PrintType == "DOC")
                    {
                        if (ComIDd == "1")
                        {
                            ShowReport(mainDS, "crpMemoIncrementListInfoReportMulti.rpt");
                            rptdoc.ExportToHttpResponse(  ExportFormatType.WordForWindows, Response, true,
                      "Annual Increment Letter Information_" + DateTime.Now);

                        }
                        else
                        {
                            ShowReport(mainDS, "crpMemoIncrementListInfoReportMulti2.rpt");

                        //    ShowReport(mainDS, "crpMemoIncrementListInfoReportMultiSMCEL.rpt");

                            rptdoc.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true,
                    "Annual Increment Letter Information_" + DateTime.Now);
                        }
                    }
                }


            }
        }


        if (rtT == "MemoPromotion")
        {
            if (rptType != "")
            {


                DataTable allDataTable = new DataTable();
                allDataTable = aDAL.GetMemoPrintPromotionInfoDALrpt(rptType).Copy();
                allDataTable.TableName = "MemoPromotionListDataTable";
                mainDS.Tables.Add(allDataTable);

                DataTable DetailsDataTable = new DataTable();
                DetailsDataTable = aDAL.LoadParticularsPromotionDetailsrpt(rptType).Copy();
                DetailsDataTable.TableName = "MemoPromotionDetailsListDataTable";
                mainDS.Tables.Add(DetailsDataTable);


                if (mainDS.Tables[0].Rows.Count > 0)
                {
               //    mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\MemoPromotionListInfo.xsd"));
                   ShowReport(mainDS, "crpMemoPromotionListInfoReport.rpt");
                }


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