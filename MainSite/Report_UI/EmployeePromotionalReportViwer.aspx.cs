 
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


public partial class Report_UI_EmployeePromotionalReportViwer : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();
    EmployeeProfileDAL ddd = new EmployeeProfileDAL();

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
    EmployeeProfileDAL aEmployeeInfoListReportDAL = new EmployeeProfileDAL();
    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
        string header = Request.QueryString["header"];
     
      

        DataSet mainDS = new DataSet();

        string rptTypeIdMul = "";


        if (rptType != "")
        {

            DataTable dtref = aEmployeeInfoListReportDAL.GetRefEmpInfoDAL(rptType);
            if (dtref.Rows.Count>0)
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
                    myId += dtref.Rows[i]["ReferenceID"].ToString().Trim() + ",";
                }


                myId = myId.Trim().TrimEnd(',');
                rptTypeIdMul = rptType + "," + myId.Trim();
            }



            DataTable dtComm = new DataTable();
            dtComm = aEmployeeInfoListReportDAL.GetComeInfoDAL_Pro(rptType, header).Copy();
            dtComm.TableName = "EMPCOMDataTable";
            mainDS.Tables.Add(dtComm);

            if (Convert.ToBoolean(Session["BI"]) == true)
            {
                DataTable allDataTable = new DataTable();
                allDataTable = aEmployeeInfoListReportDAL.GetEmployeeInfoDAL(rptType).Copy();
                allDataTable.TableName = "EmployeeInfoListDataTable";
                mainDS.Tables.Add(allDataTable);

 
            }
            else
            {
                DataTable allDataTable = new DataTable();
                allDataTable = aEmployeeInfoListReportDAL.GetEmployeeInfoDAL("0").Copy();
                allDataTable.TableName = "EmployeeInfoListDataTable";
                mainDS.Tables.Add(allDataTable);



                 

            }


          
 

  
            if (Convert.ToBoolean(Session["SP"]) == true)
            {

                if (rptTypeIdMul == "")
                {
                    DataTable IncrementInfoDataTable = new DataTable();
                    IncrementInfoDataTable = aEmployeeInfoListReportDAL.LoadIncrementInfo_Pro(rptType).Copy();
                    IncrementInfoDataTable.TableName = "IncrementInfoDataTable";
                    mainDS.Tables.Add(IncrementInfoDataTable);
                }
                else
                {


                    DataTable IncrementInfoDataTable = new DataTable();
                    IncrementInfoDataTable = aEmployeeInfoListReportDAL.LoadIncrementInfoMult_Pro(" IN ( " + rptTypeIdMul + "  ) ").Copy();
                    IncrementInfoDataTable.TableName = "IncrementInfoDataTable";
                    mainDS.Tables.Add(IncrementInfoDataTable);
                }
            }
            else
            {
                DataTable IncrementInfoDataTable = new DataTable();
                IncrementInfoDataTable = aEmployeeInfoListReportDAL.LoadIncrementInfo_Pro("0").Copy();
                IncrementInfoDataTable.TableName = "IncrementInfoDataTable";
                mainDS.Tables.Add(IncrementInfoDataTable);
            }
             


            if (Convert.ToBoolean(Session["PI"]) ==  true )
            {

                if (rptTypeIdMul == "")
                {
                    DataTable Promotion = new DataTable();
                    Promotion = aEmployeeInfoListReportDAL.GetPromotion_Pro(rptType).Copy();
                    Promotion.TableName = "PromotionDataTable";
                    mainDS.Tables.Add(Promotion);

                }

                else
                {
                    DataTable Promotion = new DataTable();
                    Promotion = aEmployeeInfoListReportDAL.GetPromotionMulti_Pro(" IN ( " + rptTypeIdMul + "  ) ").Copy();
                    Promotion.TableName = "PromotionDataTable";
                    mainDS.Tables.Add(Promotion);
                }
                

            }
            else
            {
                DataTable Promotion = new DataTable();
                Promotion = aEmployeeInfoListReportDAL.GetPromotion_Pro("0").Copy();
                Promotion.TableName = "PromotionDataTable";
                mainDS.Tables.Add(Promotion);

            }
            if (Convert.ToBoolean(Session["UGI"]) == true)
            {
                if (rptTypeIdMul=="")
                {
                    DataTable Transfer = new DataTable();
                    Transfer = aEmployeeInfoListReportDAL.GetPromotion_Pro_Up(rptType).Copy();
                    Transfer.TableName = "UpgradationDataTable";
                    mainDS.Tables.Add(Transfer);

                }

                else
                {
                    DataTable Transfer = new DataTable();
                    Transfer = aEmployeeInfoListReportDAL.GetPromotionMulti_Pro_Up(" IN ( " + rptTypeIdMul + "  ) ").Copy();
                    Transfer.TableName = "UpgradationDataTable";
                    mainDS.Tables.Add(Transfer);
                }
              
            }
            else
            {
                DataTable Transfer = new DataTable();
                Transfer = aEmployeeInfoListReportDAL.GetPromotion_Pro_Up("0").Copy();
                Transfer.TableName = "UpgradationDataTable";
                mainDS.Tables.Add(Transfer);
            }

           



            if (mainDS.Tables[0].Rows.Count > 0)
                {
                     //  mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dtEmployeePromotional.xsd"));
                        ShowReport(mainDS, "crpEmployeePromotional.rpt");
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