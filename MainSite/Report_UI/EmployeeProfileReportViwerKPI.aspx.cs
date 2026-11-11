 
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
using CrystalDecisions.Shared;


public partial class Report_UI_EmployeeProfileReportViwerKPI : System.Web.UI.Page
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
            dtComm = aEmployeeInfoListReportDAL.GetComeInfoDAL(rptType).Copy();
            dtComm.TableName = "EMPCOMDataTable";
            mainDS.Tables.Add(dtComm);

            //if (Convert.ToBoolean(Session["BI"]) == true)
            //{
            //    DataTable allDataTable = new DataTable();
            //    allDataTable = aEmployeeInfoListReportDAL.GetEmployeeInfoDAL(rptType).Copy();
            //    allDataTable.TableName = "EmployeeInfoListDataTable";
            //    mainDS.Tables.Add(allDataTable);


            //    DataTable EmpAchievementsDataTable = new DataTable();
            //    EmpAchievementsDataTable = aEmployeeInfoListReportDAL.GetEmployeeInfoDALBasic("0").Copy();
            //    EmpAchievementsDataTable.TableName = "EmpAchievementsDataTable";
            //    mainDS.Tables.Add(EmpAchievementsDataTable);
            //}
            //else
            {
                DataTable allDataTable = new DataTable();
                allDataTable = aEmployeeInfoListReportDAL.GetEmployeeInfoDAL("0").Copy();
                allDataTable.TableName = "EmployeeInfoListDataTable";
                mainDS.Tables.Add(allDataTable);



                //DataTable basicDt = new DataTable();
                //basicDt = aEmployeeInfoListReportDAL.GetEmployeeInfoDALBasic(rptType).Copy();
                //basicDt.TableName = "dtBasicInfo";
                //mainDS.Tables.Add(basicDt);

                DataTable EmpAchievementsDataTable = new DataTable();
                EmpAchievementsDataTable = aEmployeeInfoListReportDAL.GetEmployeeInfoDALBasic(rptType).Copy();
                EmpAchievementsDataTable.TableName = "EmpAchievementsDataTable";
                mainDS.Tables.Add(EmpAchievementsDataTable);

            }


            //if (Convert.ToBoolean(Session["AI"]) == true )
            //{
            //    DataTable EmpEducationDataTable = new DataTable();
            //    EmpEducationDataTable = aEmployeeInfoListReportDAL.GetEmpEducationInfoDAL(rptType).Copy();
            //    EmpEducationDataTable.TableName = "EmpEducationDataTable";
            //    mainDS.Tables.Add(EmpEducationDataTable);
            //}
            //else
            {
                DataTable EmpEducationDataTable = new DataTable();
                EmpEducationDataTable = aEmployeeInfoListReportDAL.GetEmpEducationInfoDAL("0").Copy();
                EmpEducationDataTable.TableName = "EmpEducationDataTable";
                mainDS.Tables.Add(EmpEducationDataTable);
            }

            //ok

            //if (Convert.ToBoolean(Session["FD"])  ==  true )
            //{
            //    //ok
            //    DataTable EmpChildrenDataTable = new DataTable();
            //    EmpChildrenDataTable = aEmployeeInfoListReportDAL.GetEmpChildrenInfoDAL(rptType).Copy();
            //    EmpChildrenDataTable.TableName = "EmpChildrenDataTable";
            //    mainDS.Tables.Add(EmpChildrenDataTable);


            //}
            //else
            {
                DataTable EmpChildrenDataTable = new DataTable();
                EmpChildrenDataTable = aEmployeeInfoListReportDAL.GetEmpChildrenInfoDAL("0").Copy();
                EmpChildrenDataTable.TableName = "EmpChildrenDataTable";
                mainDS.Tables.Add(EmpChildrenDataTable);
            }

            //ok
              


            //ok
            //if (Convert.ToBoolean(Session["Exp"])  ==  true )
            //{
            //    DataTable EmpExperienceDataTable = new DataTable();
            //    EmpExperienceDataTable = aEmployeeInfoListReportDAL.GetEmpExperienceInfoDAL(rptType).Copy();
            //    EmpExperienceDataTable.TableName = "EmpExperienceDataTable";
            //    mainDS.Tables.Add(EmpExperienceDataTable);

            //}
            //else
            {
                DataTable EmpExperienceDataTable = new DataTable();
                EmpExperienceDataTable = aEmployeeInfoListReportDAL.GetEmpExperienceInfoDAL("0").Copy();
                EmpExperienceDataTable.TableName = "EmpExperienceDataTable";
                mainDS.Tables.Add(EmpExperienceDataTable);
            }

            //if (Convert.ToBoolean(Session["TWI"])  ==  true )
            //{
            //    //ok
            //    DataTable EmpTrainingDataTable = new DataTable();
            //    EmpTrainingDataTable = aEmployeeInfoListReportDAL.GetEmpTrainingInfoDAL(rptType).Copy();
            //    EmpTrainingDataTable.TableName = "EmpTrainingDataTable";
            //    mainDS.Tables.Add(EmpTrainingDataTable);


            //    DataTable EmpTrainingCount = new DataTable();
            //    EmpTrainingCount = aEmployeeInfoListReportDAL.GetEmpTrainingCountInfoDAL(rptType).Copy();
            //    EmpTrainingCount.TableName = "DTEmpTrainingCount";
            //    mainDS.Tables.Add(EmpTrainingCount);

            //}
            //else
            {
                DataTable EmpTrainingDataTable = new DataTable();
                EmpTrainingDataTable = aEmployeeInfoListReportDAL.GetEmpTrainingInfoDAL("0").Copy();
                EmpTrainingDataTable.TableName = "EmpTrainingDataTable";
                mainDS.Tables.Add(EmpTrainingDataTable);


                DataTable EmpTrainingCount = new DataTable();
                EmpTrainingCount = aEmployeeInfoListReportDAL.GetEmpTrainingCountInfoDAL("0").Copy();
                EmpTrainingCount.TableName = "DTEmpTrainingCount";
                mainDS.Tables.Add(EmpTrainingCount);
            }
            //no


            //if (Convert.ToBoolean(Session["NI"])  ==  true )
            //{

            //    //ok
            //    DataTable EmpNomineeDataTable = new DataTable();
            //    EmpNomineeDataTable = aEmployeeInfoListReportDAL.GetEmpNomineeInfoDAL(rptType).Copy();
            //    EmpNomineeDataTable.TableName = "EmpNomineeDataTable";
            //    mainDS.Tables.Add(EmpNomineeDataTable);

            //}
            //else
            {
                DataTable EmpNomineeDataTable = new DataTable();
                EmpNomineeDataTable = aEmployeeInfoListReportDAL.GetEmpNomineeInfoDAL("0").Copy();
                EmpNomineeDataTable.TableName = "EmpNomineeDataTable";
                mainDS.Tables.Add(EmpNomineeDataTable);
            }
          

            ////if (Convert.ToBoolean(Session["PA"])  == true )
            ////{
            ////    DataTable EmpAchievementsDataTable = new DataTable();
            ////    EmpAchievementsDataTable = aEmployeeInfoListReportDAL.GetEmpAchievementsInfoDAL(rptType).Copy();
            ////    EmpAchievementsDataTable.TableName = "EmpAchievementsDataTable";
            ////    mainDS.Tables.Add(EmpAchievementsDataTable);
            ////}
            ////else
            ////{
            ////    DataTable EmpAchievementsDataTable = new DataTable();
            ////    EmpAchievementsDataTable = aEmployeeInfoListReportDAL.GetEmpAchievementsInfoDAL("0").Copy();
            ////    EmpAchievementsDataTable.TableName = "EmpAchievementsDataTable";
            ////    mainDS.Tables.Add(EmpAchievementsDataTable);
            ////}
            //if (Convert.ToBoolean(Session["INI"]) == true)
            //{

            //    if (rptTypeIdMul == "")
            //    {
            //        DataTable IncrementInfoDataTable = new DataTable();
            //        IncrementInfoDataTable = aEmployeeInfoListReportDAL.LoadIncrementInfo(rptType).Copy();
            //        IncrementInfoDataTable.TableName = "IncrementInfoDataTable";
            //        mainDS.Tables.Add(IncrementInfoDataTable);
            //    }
            //    else
            //    {
                     

            //        DataTable IncrementInfoDataTable = new DataTable();
            //        IncrementInfoDataTable = aEmployeeInfoListReportDAL.LoadIncrementInfoMult("EmployeeId IN ( " + rptTypeIdMul + "  ) ").Copy();
            //        IncrementInfoDataTable.TableName = "IncrementInfoDataTable";
            //        mainDS.Tables.Add(IncrementInfoDataTable);
            //    }
            //}
            //else
            {
                DataTable IncrementInfoDataTable = new DataTable();
                IncrementInfoDataTable = aEmployeeInfoListReportDAL.LoadIncrementInfo("0").Copy();
                IncrementInfoDataTable.TableName = "IncrementInfoDataTable";
                mainDS.Tables.Add(IncrementInfoDataTable);
            }
            ////no
                //DataTable OtherEmpHobbyDataTable = new DataTable();
                //OtherEmpHobbyDataTable = aEmployeeInfoListReportDAL.OtherEmpHobbyInfoDAL(rptType).Copy();
                //OtherEmpHobbyDataTable.TableName = "OtherEmpHobbyDataTable";
                //mainDS.Tables.Add(OtherEmpHobbyDataTable);


                ////no
                //DataTable EmpOtherTalentsDataTable = new DataTable();
                //EmpOtherTalentsDataTable = aEmployeeInfoListReportDAL.EmpOtherTalentsInfoDAL(rptType).Copy();
                //EmpOtherTalentsDataTable.TableName = "EmpOtherTalentsDataTable";
                //mainDS.Tables.Add(EmpOtherTalentsDataTable);



            //if (Convert.ToBoolean(Session["PI"]) ==  true )
            //{

            //    if (rptTypeIdMul == "")
            //    {
            //        DataTable Promotion = new DataTable();
            //        Promotion = aEmployeeInfoListReportDAL.GetPromotion(rptType).Copy();
            //        Promotion.TableName = "PromotionDataTable";
            //        mainDS.Tables.Add(Promotion);

            //    }

            //    else
            //    {
            //        DataTable Promotion = new DataTable();
            //        Promotion = aEmployeeInfoListReportDAL.GetPromotionMulti("EmployeeId IN ( " + rptTypeIdMul + "  ) ", "EmpInfoId IN ( " + rptTypeIdMul + "  ) ").Copy();
            //        Promotion.TableName = "PromotionDataTable";
            //        mainDS.Tables.Add(Promotion);
            //    }
                

            //}
            //else
            {
                DataTable Promotion = new DataTable();
                Promotion = aEmployeeInfoListReportDAL.GetPromotion("0").Copy();
                Promotion.TableName = "PromotionDataTable";
                mainDS.Tables.Add(Promotion);

            }


            //if (Convert.ToBoolean(Session["threeParam"]) == true)
            //{

            //    if (rptTypeIdMul == "")
            //    {
            //        DataTable Promotion = new DataTable();
            //        Promotion = aEmployeeInfoListReportDAL.GetPromotionParmThree(rptType).Copy();
            //        Promotion.TableName = "PromotionDataTable_New";
            //        mainDS.Tables.Add(Promotion);

            //    }

            //    else
            //    {
            //        DataTable Promotion = new DataTable();
            //        Promotion = aEmployeeInfoListReportDAL.GetPromotionMultiParmThree("EmployeeId IN ( " + rptTypeIdMul + "  ) ").Copy();
            //        Promotion.TableName = "PromotionDataTable_New";
            //        mainDS.Tables.Add(Promotion);
            //    }


            //}
        //    else
            {
                DataTable Promotion = new DataTable();
                Promotion = aEmployeeInfoListReportDAL.GetPromotionParmThree("0").Copy();
                Promotion.TableName = "PromotionDataTable_New";
                mainDS.Tables.Add(Promotion);

            }

            //if (Convert.ToBoolean(Session["TI"]) ==  true )
            //{
            //    if (rptTypeIdMul=="")
            //    {
            //        DataTable Transfer = new DataTable();
            //        Transfer = aEmployeeInfoListReportDAL.GetTransfer(rptType).Copy();
            //        Transfer.TableName = "TransferDataTable";
            //        mainDS.Tables.Add(Transfer);

            //    }

            //    else
            //    {
            //        DataTable Transfer = new DataTable();
            //        Transfer = aEmployeeInfoListReportDAL.GetTransferMulti("EmployeeId IN ( " + rptTypeIdMul + "  ) ").Copy();
            //        Transfer.TableName = "TransferDataTable";
            //        mainDS.Tables.Add(Transfer);
            //    }
              
            //}
            //else
            {
                DataTable Transfer = new DataTable();
                Transfer = aEmployeeInfoListReportDAL.GetTransfer("0").Copy();
                Transfer.TableName = "TransferDataTable";
                mainDS.Tables.Add(Transfer);
            }

            //if (Convert.ToBoolean(Session["DI"]) ==  true )
            //{


            //    if (rptTypeIdMul == "")
            //    {
            //        DataTable Diciplinary = new DataTable();
            //        Diciplinary = aEmployeeInfoListReportDAL.GetDiciplinary(rptType).Copy();
            //        Diciplinary.TableName = "DiciplinaryDataTable";
            //        mainDS.Tables.Add(Diciplinary);

            //    }

            //    else
            //    {


            //        DataTable Diciplinary = new DataTable();
            //        Diciplinary = aEmployeeInfoListReportDAL.GetDiciplinaryMulti("EmpInfoId IN ( " + rptTypeIdMul + "  ) ").Copy();
            //        Diciplinary.TableName = "DiciplinaryDataTable";
            //        mainDS.Tables.Add(Diciplinary);
                   
            //    }


               
            //}
            //else
            {
                DataTable Diciplinary = new DataTable();
                Diciplinary = aEmployeeInfoListReportDAL.GetDiciplinary("0").Copy();
                Diciplinary.TableName = "DiciplinaryDataTable";
                mainDS.Tables.Add(Diciplinary);
            }



            if (Convert.ToBoolean(Session["KPI"]) == true)
            {

                if (rptTypeIdMul == "")
                {
                    DataTable KPIDataTable = new DataTable();
                    KPIDataTable = aEmployeeInfoListReportDAL.LoadKPIInfolastThreeYears(rptType).Copy();
                    KPIDataTable.TableName = "KPIDataTable";
                    mainDS.Tables.Add(KPIDataTable);
                }
                else
                {


                    DataTable KPIDataTable = new DataTable();
                    KPIDataTable = aEmployeeInfoListReportDAL.LoadKPIInfolastThreeYears(rptTypeIdMul).Copy();
                    KPIDataTable.TableName = "KPIDataTable";
                    mainDS.Tables.Add(KPIDataTable);
                }
            }
            else
            {
                DataTable KPIDataTable = new DataTable();
                KPIDataTable = aEmployeeInfoListReportDAL.LoadKPIInfolastThreeYears("0").Copy();
                KPIDataTable.TableName = "KPIDataTable";
                mainDS.Tables.Add(KPIDataTable);
            }


            //DataTable Exmple01 = new DataTable();
            //Exmple01 = aEmployeeInfoListReportDAL.GetPromotion("0").Copy();
            //Exmple01.TableName = "Exmple01DataTable";
            //mainDS.Tables.Add(Exmple01);


            //DataTable Exmple02 = new DataTable();
            //Exmple02 = aEmployeeInfoListReportDAL.GetPromotion("0").Copy();
            //Exmple02.TableName = "Exmple02DataTable";
            //mainDS.Tables.Add(Exmple02);



            //DataTable Exmple03 = new DataTable();
            //Exmple03 = aEmployeeInfoListReportDAL.GetPromotion("0").Copy();
            //Exmple03.TableName = "Exmple03DataTable";
            //mainDS.Tables.Add(Exmple03);



            //DataTable Exmple04 = new DataTable();
            //Exmple04 = aEmployeeInfoListReportDAL.GetPromotion("0").Copy();
            //Exmple04.TableName = "Exmple04DataTable";
            //mainDS.Tables.Add(Exmple04);


            //DataTable Exmple05 = new DataTable();
            //Exmple04 = aEmployeeInfoListReportDAL.GetPromotion("0").Copy();
            //Exmple05.TableName = "Exmple05DataTable";
            //mainDS.Tables.Add(Exmple05);

            if (mainDS.Tables[0].Rows.Count > 0)
                {
                   //   mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dtEmployeeProfile.xsd"));
                     ShowReport(mainDS, "crpEmployeeProfileReportLatestforKPI.rpt");



                // rptdoc.SetDataSource(mainDS);

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