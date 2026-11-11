 
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Report_DAL;
using DAL.UserPermissions_DAL;


public partial class Report_UI_EmployeeInfoListReportViwer : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();

    EmployeeProfileDAL ddd = new EmployeeProfileDAL();

    EmployeeBeenCardDal aEmployeeInfoListReportDAL = new EmployeeBeenCardDal();
    private string rptType = "";
    protected void Page_Init(object sender, EventArgs e)
    {
          rptType = Request.QueryString["rptType"];
      

        DataSet mainDS = new DataSet();




        string rptTypeIdMul = "";
        if (rptType != "")
        {

            DataTable dtref = ddd.GetRefEmpInfoDAL(rptType);
            if (dtref.Rows.Count > 0)
            {
                DataTable aDataTable = new DataTable();
                aDataTable.Columns.Add("EmpInfoId");
              
                DataRow dataRow = null;
                dataRow = aDataTable.NewRow();
                dataRow["EmpInfoId"] = "0";
                
                aDataTable.Rows.Add(dataRow);
                ReportingEmpData(rptType, dtref);
                string myId = "";
                for (int i = 0; i < dtref.Rows.Count; i++)
                {
                    myId += dtref.Rows[i]["ReferenceID"].ToString().Trim()+",";
                }


                myId = myId.Trim().TrimEnd(',');
                rptTypeIdMul = rptType + "," + myId.Trim();
            }

            if (rptTypeIdMul == "")
            {
                DataTable allDataTable = new DataTable();
                allDataTable = aEmployeeInfoListReportDAL.GetEmployeeInfoDAL(rptType).Copy();
                allDataTable.TableName = "EmployeeInfoListDataTable";
                mainDS.Tables.Add(allDataTable);

                DataTable EmpChildrenDataTable = new DataTable();
                EmpChildrenDataTable = aEmployeeInfoListReportDAL.GetEmpPromotionInfo(rptType).Copy();
                EmpChildrenDataTable.TableName = "EmpPromotionDataTable";
                mainDS.Tables.Add(EmpChildrenDataTable);


                DataTable EmpEducationDataTable = new DataTable();
                EmpEducationDataTable = aEmployeeInfoListReportDAL.GetEmpTransferInfo(rptType).Copy();
                EmpEducationDataTable.TableName = "EmpTransferDataTable";
                mainDS.Tables.Add(EmpEducationDataTable);

                DataTable EmpIncrementDataTable = new DataTable();
                EmpIncrementDataTable = aEmployeeInfoListReportDAL.GetEmpIncrementInfo(rptType).Copy();
                EmpIncrementDataTable.TableName = "EmpIncrementDataTable";
                mainDS.Tables.Add(EmpIncrementDataTable);

                DataTable EmpExperienceDataTable = new DataTable();
                EmpExperienceDataTable = aEmployeeInfoListReportDAL.GetEmpStateTransferInfo(rptType).Copy();
                EmpExperienceDataTable.TableName = "EmpStateTransferDataTable";
                mainDS.Tables.Add(EmpExperienceDataTable);


                DataTable EmpRedesignationDataTable = new DataTable();
                EmpRedesignationDataTable = aEmployeeInfoListReportDAL.EmpRedesignationInfo(rptType).Copy();
                EmpRedesignationDataTable.TableName = "EmpRedesignationDataTableNNNN";
                mainDS.Tables.Add(EmpRedesignationDataTable);
            }
            else
            {
                DataTable allDataTable = new DataTable();
                allDataTable = aEmployeeInfoListReportDAL.GetEmployeeInfoDAL(rptType).Copy();
                allDataTable.TableName = "EmployeeInfoListDataTable";
                mainDS.Tables.Add(allDataTable);

                DataTable EmpChildrenDataTable = new DataTable();
                EmpChildrenDataTable = aEmployeeInfoListReportDAL.GetEmpPromotionInfoMulti(rptTypeIdMul).Copy();
                EmpChildrenDataTable.TableName = "EmpPromotionDataTable";
                mainDS.Tables.Add(EmpChildrenDataTable);


                DataTable EmpEducationDataTable = new DataTable();
                EmpEducationDataTable = aEmployeeInfoListReportDAL.GetEmpTransferInfoMulti(rptTypeIdMul).Copy();
                EmpEducationDataTable.TableName = "EmpTransferDataTable";
                mainDS.Tables.Add(EmpEducationDataTable);

                DataTable EmpIncrementDataTable = new DataTable();
                EmpIncrementDataTable = aEmployeeInfoListReportDAL.GetEmpIncrementInfoMulti(rptTypeIdMul).Copy();
                EmpIncrementDataTable.TableName = "EmpIncrementDataTable";
                mainDS.Tables.Add(EmpIncrementDataTable);

                DataTable EmpExperienceDataTable = new DataTable();
                EmpExperienceDataTable = aEmployeeInfoListReportDAL.GetEmpStateTransferInfoMulti(rptTypeIdMul).Copy();
                EmpExperienceDataTable.TableName = "EmpStateTransferDataTable";
                mainDS.Tables.Add(EmpExperienceDataTable);


                DataTable EmpRedesignationDataTable = new DataTable();
                EmpRedesignationDataTable = aEmployeeInfoListReportDAL.EmpRedesignationInfoMulti(rptTypeIdMul).Copy();
                EmpRedesignationDataTable.TableName = "EmpRedesignationDataTableNNNN";
                mainDS.Tables.Add(EmpRedesignationDataTable);
            }

            if (mainDS.Tables[0].Rows.Count > 0)
                {
               //   mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsEmployeeIBeenCard.xsd"));
                  ShowReport(mainDS, "crpEmployeeBeenCardNew.rpt");
                }
             
           
        }
        
    }


    SupervisorMenuAppDAL ddddd = new SupervisorMenuAppDAL();
    public void ReportingEmpData(string empinfoid, DataTable aDataTable)
    {
        DataRow dataRow = null;
        DataTable dtdata1 = ddd.GetRefEmpInfoDAL2(empinfoid);
        DataTable dtdata = ddddd.LoadEmpGenInfoGetRef(" AND E.EmpInfoId='" + dtdata1.Rows[0]["ReferenceID"].ToString() + "' ");
       
        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["ReferenceID"] = dtdata.Rows[0]["FromEmpInfoId"].ToString();
            
            aDataTable.Rows.Add(dataRow);

            ReportingEmpData(dtdata.Rows[0]["FromEmpInfoId"].ToString(), aDataTable);
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