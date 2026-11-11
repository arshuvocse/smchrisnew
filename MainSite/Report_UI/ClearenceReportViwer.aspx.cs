 
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DAL.MPBudget;
using DAL.Report_DAL;
using DAL.Survey;
using DAL.UserPermissions_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Report_UI_ExitInterViewFormReport : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();


    ClearenceFormDal aEmployeeInfoListReportDAL = new ClearenceFormDal();

    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
     
        DataSet mainDS = new DataSet();
     
        if (rptType != "")
        {
            
             
                DataTable allDataTable = new DataTable();
                allDataTable = aEmployeeInfoListReportDAL.GetClearenceInfo(Convert.ToInt32(rptType)).Copy();
                allDataTable.TableName = "ClearanceDataTable";
                mainDS.Tables.Add(allDataTable);


                DataTable dtResource = new DataTable();
                dtResource = aEmployeeInfoListReportDAL.GetResourceInfo(Convert.ToInt32(rptType)).Copy();

                if (dtResource != null && dtResource.Rows.Count > 0 && dtResource.Columns.Contains("Resource"))
                {
                    dtResource.Columns["Resource"].ReadOnly = false;
                    Dictionary<string, int> seenResources = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    foreach (DataRow row in dtResource.Rows)
                    {
                        if (row["Resource"] != DBNull.Value)
                        {
                            string resVal = row["Resource"].ToString().Trim();
                            if (!string.IsNullOrEmpty(resVal))
                            {
                                if (seenResources.ContainsKey(resVal))
                                {
                                    seenResources[resVal]++;
                                }
                                else
                                {
                                    seenResources[resVal] = 1;
                                }
                            }
                        }
                    }

                    string[] suffixes = { ".", "_", "__", "___", "____", "!", "..", "._", ".!", "_!", "..!" };
                    Dictionary<string, int> currentOccurrence = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                    foreach (DataRow row in dtResource.Rows)
                    {
                        if (row["Resource"] != DBNull.Value)
                        {
                            string resVal = row["Resource"].ToString().Trim();
                            if (!string.IsNullOrEmpty(resVal) && seenResources.ContainsKey(resVal) && seenResources[resVal] > 1)
                            {
                                if (!currentOccurrence.ContainsKey(resVal))
                                {
                                    currentOccurrence[resVal] = 0;
                                }
                                int index = currentOccurrence[resVal];
                                currentOccurrence[resVal]++;

                                string suffix = "";
                                if (index < suffixes.Length)
                                {
                                    suffix = suffixes[index];
                                }
                                else
                                {
                                    suffix = new string('.', index + 1);
                                }

                                row["Resource"] = resVal + suffix;
                            }
                        }
                    }
                }

                dtResource.TableName = "dsResource";
                mainDS.Tables.Add(dtResource);


                DataTable EmpClearance = new DataTable();
                EmpClearance = aEmployeeInfoListReportDAL.Get_EmpClearance(Convert.ToInt32(rptType)).Copy();
                EmpClearance.TableName = "Emp_Clearance";
                mainDS.Tables.Add(EmpClearance);
           
                if (mainDS.Tables[0].Rows.Count > 0)
                {
                    int divisionId = Convert.ToInt32(allDataTable.Rows[0]["DivisionId"]);
                    int companyId = Convert.ToInt32(allDataTable.Rows[0]["CompanyId"]);
                    string reportName = divisionId != 48 && companyId == 2
                        ? "crpClearenceFormReportEL.rpt"
                        : "crpClearenceFormReport.rpt";

                    // mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsClearenceForm.xsd"));
                    ShowReport(mainDS, reportName);

                //rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
                //   "ClearanceForm");
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
