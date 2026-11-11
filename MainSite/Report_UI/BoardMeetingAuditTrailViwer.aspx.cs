 
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MeetingMinorsDAL;
using DAL.MPBudget;
using DAL.Report_DAL;
using DAL.Survey;
using DAL.UserPermissions_DAL;


public partial class Report_UI_BoardMeetingAuditTrailViwer : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();


    ClearenceFormDal aEmployeeInfoListReportDAL = new ClearenceFormDal();

    protected void Page_Init(object sender, EventArgs e)
    {
        MeetingEntryDAL AMeetingEntryDAL = new MeetingEntryDAL();

        MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();

        string rptType = Request.QueryString["rptType"];
        string MID = Request.QueryString["MID"];
     
        DataSet mainDS = new DataSet();

        if (rptType == "Document")
        {


            DataTable dtMaster = new DataTable();
            dtMaster = AMAsterDal.GetMasterByIdAudit(MID).Copy();
            dtMaster.TableName = "dtMasterAuditTrail";
            mainDS.Tables.Add(dtMaster);


            DataTable dtDocumentList = new DataTable();
            dtDocumentList = AMAsterDal.GetDocumentListByIdAudit(MID).Copy();
            dtDocumentList.TableName = "dtDocumentList";
            mainDS.Tables.Add(dtDocumentList);


            DataTable dtMemberList = new DataTable();
            dtMemberList = AMAsterDal.GetMemberListByIdAudit(MID).Copy();
            dtMemberList.TableName = "dtMemberList";
            mainDS.Tables.Add(dtMemberList);



            


                
           
                if (mainDS.Tables[0].Rows.Count > 0)
                {
                   //     mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsDocumentAuditTrailNew.xsd"));
                   ShowReport(mainDS, "crpDocumentAuditTrail.rpt");
                }
             
           
        }


        if (rptType == "Meeting")
        {


            DataTable dtMaster = new DataTable();
            dtMaster = AMeetingEntryDAL.GetMasterByIdAudit(MID).Copy();
            dtMaster.TableName = "dtMasterAuditTrail";
            mainDS.Tables.Add(dtMaster);


            DataTable dtDocumentList = new DataTable();
            dtDocumentList = AMeetingEntryDAL.GetDocumentListByIdAudit(MID).Copy();
            dtDocumentList.TableName = "dtDocumentListAT";
            mainDS.Tables.Add(dtDocumentList);


            DataTable dtMemberList = new DataTable();
            dtMemberList = AMeetingEntryDAL.GetMemberListByIdAudit(MID).Copy();
            dtMemberList.TableName = "dtMemberListAT";
            mainDS.Tables.Add(dtMemberList);


            DataTable dtAgendaList = new DataTable();
            dtAgendaList = AMeetingEntryDAL.GetAgendaListByIdAudit(MID).Copy();
            dtAgendaList.TableName = "dtAgendaListAT";
            mainDS.Tables.Add(dtAgendaList);








            if (mainDS.Tables[0].Rows.Count > 0)
            {
                //    mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsMeetingAuditTrail.xsd"));
                  ShowReport(mainDS, "crpMeetingAuditTrail.rpt");
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