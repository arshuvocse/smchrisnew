 
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MPBudget;
using DAL.Transfer_DAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;


public partial class Report_UI_EmployeePromotionEntryViewReportViwer : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();


    tblEmployeePromotionEntryDAL aListReportDAL = new tblEmployeePromotionEntryDAL();
    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
      

        DataSet mainDS = new DataSet();
     



        if (rptType != "")
        {
            
             
                DataTable allDataTable = new DataTable();
                allDataTable = aListReportDAL.GetEmployeePromotionInfoDALrpt(rptType).Copy();
             //   HiddenEmpId.Value = allDataTable.Rows[0]["EmployeeId"].ToString();
                allDataTable.TableName = "EmployeePromotionInfoListDataTable";
                mainDS.Tables.Add(allDataTable);



                DataTable PreSuperviseDataTable = new DataTable();
                PreSuperviseDataTable = aListReportDAL.RPTPreLoadSuperviseEmployee(rptType).Copy();
                PreSuperviseDataTable.TableName = "PreLoadSuperviseInfoListDataTable";
                mainDS.Tables.Add(PreSuperviseDataTable);


                DataTable NewSuperviseDataTable = new DataTable();
                NewSuperviseDataTable = aListReportDAL.RPTNewEmpTransferAndRedesignationDS(rptType).Copy();
                NewSuperviseDataTable.TableName = "NEWLoadSuperviseInfoListDataTable";
                mainDS.Tables.Add(NewSuperviseDataTable);

              


                if (mainDS.Tables[0].Rows.Count > 0)
                {
               //  mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\EmployeePromotionListInfo.xsd"));
                ShowReport(mainDS, "rptEmployeePromotionList.rpt");
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