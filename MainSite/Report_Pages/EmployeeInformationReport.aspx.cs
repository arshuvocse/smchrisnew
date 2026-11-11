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
using DAL.Appraisal;
using DAL.BirthDayMailGenerate_DAL;
using DAL.COMMON_DAL;
using DAL.Employee_DAL;
using DAL.ExitManagement_DAL;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAL.Report_DAL;
using DAL.TrainingDAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_UI_EmployeeInformationReport : System.Web.UI.Page
{
    ReportDAL aReportDal=new ReportDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    ShowMessage aShowMessage = new ShowMessage();

    CommonDataLoadDAL aCommonDataLoadDal = new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        //heirerchicalTreeView.Attributes.Add("onclick",
        //      string.Format("document.getElementById('{0}').click();", btnEmpSubmit.ClientID));
        //heirerchicalTreeView.Attributes.Add("onclick", "postBackByObject()");
        if (!IsPostBack)
        {
            ContractualstartDate.Attributes.Add("readonly", "readonly");
            ContractualendDate.Attributes.Add("readonly", "readonly");



            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");
            SeparationEffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            SeparationEffectToDate.Attributes.Add("readonly", "readonly");

            PromotionEffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            PromotionEffectToDate.Attributes.Add("readonly", "readonly");

            IncrementEffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            IncrementEffectToDate.Attributes.Add("readonly", "readonly");

            CheckuptxtDate.Attributes.Add("readonly", "readonly");
            CheckuptxtToDate.Attributes.Add("readonly", "readonly");



            txtRecruitmentfrmDate.Attributes.Add("readonly", "readonly");
            txtRecruitmentToDate.Attributes.Add("readonly", "readonly");


            //txtRecruitmentfrmDate.Text=DateTime.Now.ToString("ddMMM-yyyy");
            //txtRecruitmentToDate.Text = DateTime.Now.ToString("ddMMM-yyyy");


            DropDown();
            GetCheckBox();
            GetCompany();

            ColumnsVisbale();
            heirerchicalTreeView.CollapseAll();

        }
    }
    #region Employee Details
    private void ColumnsVisbale()
    {
       
    }

    private void LoadInitialGridApprisal(GridView gridView)
    {
        gridView.Columns[1].Visible = false;
        gridView.Columns[2].Visible = false;
        gridView.Columns[3].Visible = false;
        gridView.Columns[4].Visible = false;
        gridView.Columns[5].Visible = false;
        gridView.Columns[6].Visible = false;
        gridView.Columns[7].Visible = false;
        gridView.Columns[8].Visible = false;
        gridView.Columns[9].Visible = false;
        gridView.Columns[10].Visible = false;


        //36
        gridView.Columns[11].Visible = false;
        gridView.Columns[12].Visible = false;
        gridView.Columns[13].Visible = false;
        gridView.Columns[14].Visible = false;
        gridView.Columns[15].Visible = false;
        gridView.Columns[16].Visible = false;
        gridView.Columns[17].Visible = false;
        gridView.Columns[18].Visible = false;
        gridView.Columns[19].Visible = false;
        gridView.Columns[20].Visible = false;


        gridView.Columns[21].Visible = false;
        gridView.Columns[22].Visible = false;
        gridView.Columns[23].Visible = false;
        gridView.Columns[24].Visible = false;
        gridView.Columns[25].Visible = false;
        gridView.Columns[26].Visible = false;
        gridView.Columns[27].Visible = false;
        gridView.Columns[28].Visible = false;
        gridView.Columns[29].Visible = false;
        gridView.Columns[30].Visible = false;


        gridView.Columns[31].Visible = false;
        gridView.Columns[32].Visible = false;
        gridView.Columns[33].Visible = false;
        gridView.Columns[34].Visible = false;
        gridView.Columns[35].Visible = false;
        gridView.Columns[36].Visible = false;
        gridView.Columns[37].Visible = false;
        gridView.Columns[38].Visible = false;


        gridView.Columns[39].Visible = false;
        gridView.Columns[40].Visible = false;
        gridView.Columns[41].Visible = false;
        gridView.Columns[42].Visible = false;
        gridView.Columns[43].Visible = false;
        gridView.Columns[44].Visible = false;
        gridView.Columns[45].Visible = false;
        gridView.Columns[46].Visible = false;
        gridView.Columns[47].Visible = false;
        gridView.Columns[48].Visible = false;
        gridView.Columns[49].Visible = false;
        gridView.Columns[50].Visible = false;
        gridView.Columns[51].Visible = false;
        gridView.Columns[52].Visible = false;

        if (cblHeader.Items[0].Selected == true)
        {
            gridView.Columns[1].Visible = true;

        }

        if (cblHeader.Items[0].Selected == false)
        {
            gridView.Columns[1].Visible = false;

        }


        if (cblHeader.Items[1].Selected == true)
        {
            gridView.Columns[2].Visible = true;

        }

        if (cblHeader.Items[1].Selected == false)
        {
            gridView.Columns[2].Visible = false;

        }




        if (cblHeader.Items[2].Selected == true)
        {
            gridView.Columns[3].Visible = true;

        }

        if (cblHeader.Items[2].Selected == false)
        {
            gridView.Columns[3].Visible = false;

        }



        if (cblHeader.Items[3].Selected == true)
        {
            gridView.Columns[4].Visible = true;

        }

        if (cblHeader.Items[3].Selected == false)
        {
            gridView.Columns[4].Visible = false;

        }


        if (cblHeader.Items[4].Selected == true)
        {
            gridView.Columns[5].Visible = true;

        }

        if (cblHeader.Items[4].Selected == false)
        {
            gridView.Columns[5].Visible = false;

        }



        if (cblHeader.Items[5].Selected == true)
        {
            gridView.Columns[6].Visible = true;

        }

        if (cblHeader.Items[5].Selected == false)
        {
            gridView.Columns[6].Visible = false;

        }



        if (cblHeader.Items[6].Selected == true)
        {
            gridView.Columns[7].Visible = true;

        }

        if (cblHeader.Items[6].Selected == false)
        {
            gridView.Columns[7].Visible = false;

        }




        if (cblHeader.Items[7].Selected == true)
        {
            gridView.Columns[8].Visible = true;

        }

        if (cblHeader.Items[7].Selected == false)
        {
            gridView.Columns[8].Visible = false;

        }




        if (cblHeader.Items[8].Selected == true)
        {
            gridView.Columns[9].Visible = true;

        }

        if (cblHeader.Items[8].Selected == false)
        {
            gridView.Columns[9].Visible = false;

        }





        if (cblHeader.Items[9].Selected == true)
        {
            gridView.Columns[10].Visible = true;

        }

        if (cblHeader.Items[9].Selected == false)
        {
            gridView.Columns[10].Visible = false;

        }




        if (cblHeader.Items[10].Selected == true)
        {
            gridView.Columns[11].Visible = true;

        }

        if (cblHeader.Items[10].Selected == false)
        {
            gridView.Columns[11].Visible = false;

        }


        if (cblHeader.Items[11].Selected == true)
        {
            gridView.Columns[12].Visible = true;

        }

        if (cblHeader.Items[11].Selected == false)
        {
            gridView.Columns[12].Visible = false;

        }





        if (cblHeader.Items[12].Selected == true)
        {
            gridView.Columns[13].Visible = true;

        }

        if (cblHeader.Items[12].Selected == false)
        {
            gridView.Columns[13].Visible = false;

        }





        if (cblHeader.Items[13].Selected == true)
        {
            gridView.Columns[14].Visible = true;

        }

        if (cblHeader.Items[13].Selected == false)
        {
            gridView.Columns[14].Visible = false;

        }




        if (cblHeader.Items[14].Selected == true)
        {
            gridView.Columns[15].Visible = true;

        }

        if (cblHeader.Items[14].Selected == false)
        {
            gridView.Columns[15].Visible = false;

        }





        if (cblHeader.Items[15].Selected == true)
        {
            gridView.Columns[16].Visible = true;

        }

        if (cblHeader.Items[15].Selected == false)
        {
            gridView.Columns[16].Visible = false;

        }




        if (cblHeader.Items[16].Selected == true)
        {
            gridView.Columns[17].Visible = true;

        }

        if (cblHeader.Items[16].Selected == false)
        {
            gridView.Columns[17].Visible = false;

        }

        if (cblHeader.Items[17].Selected == true)
        {
            gridView.Columns[18].Visible = true;

        }

        if (cblHeader.Items[17].Selected == false)
        {
            gridView.Columns[18].Visible = false;

        }



        if (cblHeader.Items[18].Selected == true)
        {
            gridView.Columns[19].Visible = true;

        }

        if (cblHeader.Items[18].Selected == false)
        {
            gridView.Columns[19].Visible = false;

        }




        if (cblHeader.Items[19].Selected == true)
        {
            gridView.Columns[20].Visible = true;

        }

        if (cblHeader.Items[19].Selected == false)
        {
            gridView.Columns[20].Visible = false;

        }



        if (cblHeader.Items[20].Selected == true)
        {
            gridView.Columns[21].Visible = true;

        }

        if (cblHeader.Items[20].Selected == false)
        {
            gridView.Columns[21].Visible = false;

        }


        if (cblHeader.Items[21].Selected == true)
        {
            gridView.Columns[22].Visible = true;

        }

        if (cblHeader.Items[21].Selected == false)
        {
            gridView.Columns[22].Visible = false;

        }



        if (cblHeader.Items[22].Selected == true)
        {
            gridView.Columns[23].Visible = true;

        }

        if (cblHeader.Items[22].Selected == false)
        {
            gridView.Columns[23].Visible = false;

        }



        if (cblHeader.Items[23].Selected == true)
        {
            gridView.Columns[24].Visible = true;

        }

        if (cblHeader.Items[23].Selected == false)
        {
            gridView.Columns[24].Visible = false;

        }



        if (cblHeader.Items[24].Selected == true)
        {
            gridView.Columns[25].Visible = true;

        }

        if (cblHeader.Items[24].Selected == false)
        {
            gridView.Columns[25].Visible = false;

        }




        if (cblHeader.Items[25].Selected == true)
        {
            gridView.Columns[26].Visible = true;

        }

        if (cblHeader.Items[25].Selected == false)
        {
            gridView.Columns[26].Visible = false;

        }



        if (cblHeader.Items[26].Selected == true)
        {
            gridView.Columns[27].Visible = true;

        }

        if (cblHeader.Items[26].Selected == false)
        {
            gridView.Columns[27].Visible = false;

        }


        if (cblHeader.Items[27].Selected == true)
        {
            gridView.Columns[28].Visible = true;

        }

        if (cblHeader.Items[27].Selected == false)
        {
            gridView.Columns[28].Visible = false;

        }



        if (cblHeader.Items[28].Selected == true)
        {
            gridView.Columns[29].Visible = true;

        }

        if (cblHeader.Items[28].Selected == false)
        {
            gridView.Columns[29].Visible = false;

        }




        if (cblHeader.Items[29].Selected == true)
        {
            gridView.Columns[30].Visible = true;

        }

        if (cblHeader.Items[29].Selected == false)
        {
            gridView.Columns[30].Visible = false;

        }




        if (cblHeader.Items[30].Selected == true)
        {
            gridView.Columns[31].Visible = true;

        }

        if (cblHeader.Items[30].Selected == false)
        {
            gridView.Columns[31].Visible = false;

        }



        if (cblHeader.Items[31].Selected == true)
        {
            gridView.Columns[32].Visible = true;

        }

        if (cblHeader.Items[31].Selected == false)
        {
            gridView.Columns[32].Visible = false;

        }



        if (cblHeader.Items[32].Selected == true)
        {
            gridView.Columns[33].Visible = true;

        }

        if (cblHeader.Items[32].Selected == false)
        {
            gridView.Columns[33].Visible = false;

        }




        if (cblHeader.Items[33].Selected == true)
        {
            gridView.Columns[34].Visible = true;

        }

        if (cblHeader.Items[33].Selected == false)
        {
            gridView.Columns[34].Visible = false;

        }



        if (cblHeader.Items[34].Selected == true)
        {
            gridView.Columns[35].Visible = true;

        }

        if (cblHeader.Items[34].Selected == false)
        {
            gridView.Columns[35].Visible = false;

        }



        if (cblHeader.Items[35].Selected == true)
        {
            gridView.Columns[36].Visible = true;

        }

        if (cblHeader.Items[35].Selected == false)
        {
            gridView.Columns[36].Visible = false;

        }


        if (cblHeader.Items[36].Selected == true)
        {
            gridView.Columns[37].Visible = true;

        }

        if (cblHeader.Items[36].Selected == false)
        {
            gridView.Columns[37].Visible = false;

        }


        if (cblHeader.Items[37].Selected == true)
        {
            gridView.Columns[38].Visible = true;

        }

        if (cblHeader.Items[37].Selected == false)
        {
            gridView.Columns[38].Visible = false;

        }





        if (cblHeader.Items[38].Selected == true)
        {
            gridView.Columns[39].Visible = true;

        }

        if (cblHeader.Items[38].Selected == false)
        {
            gridView.Columns[39].Visible = false;

        }



        if (cblHeader.Items[39].Selected == true)
        {
            gridView.Columns[40].Visible = true;

        }

        if (cblHeader.Items[39].Selected == false)
        {
            gridView.Columns[40].Visible = false;

        }

        if (cblHeader.Items[40].Selected == true)
        {
            gridView.Columns[41].Visible = true;
            gridView.Columns[42].Visible = true;

        }

        if (cblHeader.Items[40].Selected == false)
        {
            gridView.Columns[41].Visible = false;
            gridView.Columns[42].Visible = false;

        }

        if (cblHeader.Items[41].Selected == true)
        {
            gridView.Columns[43].Visible = true;

        }

        if (cblHeader.Items[41].Selected == false)
        {
            gridView.Columns[43].Visible = false;

        }


        if (cblHeader.Items[42].Selected == true)
        {
            gridView.Columns[44].Visible = true;

        }

        if (cblHeader.Items[42].Selected == false)
        {
            gridView.Columns[44].Visible = false;

        }



        if (cblHeader.Items[43].Selected == true)
        {
            gridView.Columns[45].Visible = true;

        }

        if (cblHeader.Items[43].Selected == false)
        {
            gridView.Columns[45].Visible = false;

        }

        if (cblHeader.Items[44].Selected == true)
        {
            gridView.Columns[46].Visible = true;

        }

        if (cblHeader.Items[44].Selected == false)
        {
            gridView.Columns[46].Visible = false;

        }


        if (cblHeader.Items[45].Selected == true)
        {
            gridView.Columns[47].Visible = true;

        }

        if (cblHeader.Items[45].Selected == false)
        {
            gridView.Columns[47].Visible = false;

        }

        if (cblHeader.Items[46].Selected == true)
        {
            gridView.Columns[48].Visible = true;

        }

        if (cblHeader.Items[46].Selected == false)
        {
            gridView.Columns[48].Visible = false;

        }


        if (cblHeader.Items[47].Selected == true)
        {
            gridView.Columns[49].Visible = true;

        }

        if (cblHeader.Items[47].Selected == false)
        {
            gridView.Columns[49].Visible = false;

        }



        if (cblHeader.Items[48].Selected == true)
        {
            gridView.Columns[50].Visible = true;

        }

        if (cblHeader.Items[48].Selected == false)
        {
            gridView.Columns[50].Visible = false;

        }

        if (cblHeader.Items[49].Selected == true)
        {
            gridView.Columns[51].Visible = true;

        }

        if (cblHeader.Items[49].Selected == false)
        {
            gridView.Columns[51].Visible = false;

        }


        if (cblHeader.Items[50].Selected == true)
        {
            gridView.Columns[52].Visible = true;

        }

        if (cblHeader.Items[50].Selected == false)
        {
            gridView.Columns[52].Visible = false;

        }
    }
    private void LoadInitialGrid(GridView gridView)
    {
        gridView.Columns[1].Visible = false;
        gridView.Columns[2].Visible = false;
        gridView.Columns[3].Visible = false;
        gridView.Columns[4].Visible = false;
        gridView.Columns[5].Visible = false;
        gridView.Columns[6].Visible = false;
        gridView.Columns[7].Visible = false;
        gridView.Columns[8].Visible = false;
        gridView.Columns[9].Visible = false;
        gridView.Columns[10].Visible = false;


        //36
        gridView.Columns[11].Visible = false;
        gridView.Columns[12].Visible = false;
        gridView.Columns[13].Visible = false;
        gridView.Columns[14].Visible = false;
        gridView.Columns[15].Visible = false;
        gridView.Columns[16].Visible = false;
        gridView.Columns[17].Visible = false;
        gridView.Columns[18].Visible = false;
        gridView.Columns[19].Visible = false;
        gridView.Columns[20].Visible = false;


        gridView.Columns[21].Visible = false;
        gridView.Columns[22].Visible = false;
        gridView.Columns[23].Visible = false;
        gridView.Columns[24].Visible = false;
        gridView.Columns[25].Visible = false;
        gridView.Columns[26].Visible = false;
        gridView.Columns[27].Visible = false;
        gridView.Columns[28].Visible = false;
        gridView.Columns[29].Visible = false;
        gridView.Columns[30].Visible = false;


        gridView.Columns[31].Visible = false;
        gridView.Columns[32].Visible = false;
        gridView.Columns[33].Visible = false;
        gridView.Columns[34].Visible = false;
        gridView.Columns[35].Visible = false;
        gridView.Columns[36].Visible = false;
        gridView.Columns[37].Visible = false;
        gridView.Columns[38].Visible = false;



        if (cblHeader.Items[0].Selected == true)
        {
            gridView.Columns[1].Visible = true;

        }

        if (cblHeader.Items[0].Selected == false)
        {
            gridView.Columns[1].Visible = false;

        }


        if (cblHeader.Items[1].Selected == true)
        {
            gridView.Columns[2].Visible = true;

        }

        if (cblHeader.Items[1].Selected == false)
        {
            gridView.Columns[2].Visible = false;

        }




        if (cblHeader.Items[2].Selected == true)
        {
            gridView.Columns[3].Visible = true;

        }

        if (cblHeader.Items[2].Selected == false)
        {
            gridView.Columns[3].Visible = false;

        }



        if (cblHeader.Items[3].Selected == true)
        {
            gridView.Columns[4].Visible = true;

        }

        if (cblHeader.Items[3].Selected == false)
        {
            gridView.Columns[4].Visible = false;

        }


        if (cblHeader.Items[4].Selected == true)
        {
            gridView.Columns[5].Visible = true;

        }

        if (cblHeader.Items[4].Selected == false)
        {
            gridView.Columns[5].Visible = false;

        }



        if (cblHeader.Items[5].Selected == true)
        {
            gridView.Columns[6].Visible = true;

        }

        if (cblHeader.Items[5].Selected == false)
        {
            gridView.Columns[6].Visible = false;

        }



        if (cblHeader.Items[6].Selected == true)
        {
            gridView.Columns[7].Visible = true;

        }

        if (cblHeader.Items[6].Selected == false)
        {
            gridView.Columns[7].Visible = false;

        }




        if (cblHeader.Items[7].Selected == true)
        {
            gridView.Columns[8].Visible = true;

        }

        if (cblHeader.Items[7].Selected == false)
        {
            gridView.Columns[8].Visible = false;

        }




        if (cblHeader.Items[8].Selected == true)
        {
            gridView.Columns[9].Visible = true;

        }

        if (cblHeader.Items[8].Selected == false)
        {
            gridView.Columns[9].Visible = false;

        }





        if (cblHeader.Items[9].Selected == true)
        {
            gridView.Columns[10].Visible = true;

        }

        if (cblHeader.Items[9].Selected == false)
        {
            gridView.Columns[10].Visible = false;

        }




        if (cblHeader.Items[10].Selected == true)
        {
            gridView.Columns[11].Visible = true;

        }

        if (cblHeader.Items[10].Selected == false)
        {
            gridView.Columns[11].Visible = false;

        }


        if (cblHeader.Items[11].Selected == true)
        {
            gridView.Columns[12].Visible = true;

        }

        if (cblHeader.Items[11].Selected == false)
        {
            gridView.Columns[12].Visible = false;

        }





        if (cblHeader.Items[12].Selected == true)
        {
            gridView.Columns[13].Visible = true;

        }

        if (cblHeader.Items[12].Selected == false)
        {
            gridView.Columns[13].Visible = false;

        }





        if (cblHeader.Items[13].Selected == true)
        {
            gridView.Columns[14].Visible = true;

        }

        if (cblHeader.Items[13].Selected == false)
        {
            gridView.Columns[14].Visible = false;

        }




        if (cblHeader.Items[14].Selected == true)
        {
            gridView.Columns[15].Visible = true;

        }

        if (cblHeader.Items[14].Selected == false)
        {
            gridView.Columns[15].Visible = false;

        }





        if (cblHeader.Items[15].Selected == true)
        {
            gridView.Columns[16].Visible = true;

        }

        if (cblHeader.Items[15].Selected == false)
        {
            gridView.Columns[16].Visible = false;

        }




        if (cblHeader.Items[16].Selected == true)
        {
            gridView.Columns[17].Visible = true;

        }

        if (cblHeader.Items[16].Selected == false)
        {
            gridView.Columns[17].Visible = false;

        }

        if (cblHeader.Items[17].Selected == true)
        {
            gridView.Columns[18].Visible = true;

        }

        if (cblHeader.Items[17].Selected == false)
        {
            gridView.Columns[18].Visible = false;

        }



        if (cblHeader.Items[18].Selected == true)
        {
            gridView.Columns[19].Visible = true;

        }

        if (cblHeader.Items[18].Selected == false)
        {
            gridView.Columns[19].Visible = false;

        }




        if (cblHeader.Items[19].Selected == true)
        {
            gridView.Columns[20].Visible = true;

        }

        if (cblHeader.Items[19].Selected == false)
        {
            gridView.Columns[20].Visible = false;

        }



        if (cblHeader.Items[20].Selected == true)
        {
            gridView.Columns[21].Visible = true;

        }

        if (cblHeader.Items[20].Selected == false)
        {
            gridView.Columns[21].Visible = false;

        }


        if (cblHeader.Items[21].Selected == true)
        {
            gridView.Columns[22].Visible = true;

        }

        if (cblHeader.Items[21].Selected == false)
        {
            gridView.Columns[22].Visible = false;

        }



        if (cblHeader.Items[22].Selected == true)
        {
            gridView.Columns[23].Visible = true;

        }

        if (cblHeader.Items[22].Selected == false)
        {
            gridView.Columns[23].Visible = false;

        }



        if (cblHeader.Items[23].Selected == true)
        {
            gridView.Columns[24].Visible = true;

        }

        if (cblHeader.Items[23].Selected == false)
        {
            gridView.Columns[24].Visible = false;

        }



        if (cblHeader.Items[24].Selected == true)
        {
            gridView.Columns[25].Visible = true;

        }

        if (cblHeader.Items[24].Selected == false)
        {
            gridView.Columns[25].Visible = false;

        }




        if (cblHeader.Items[25].Selected == true)
        {
            gridView.Columns[26].Visible = true;

        }

        if (cblHeader.Items[25].Selected == false)
        {
            gridView.Columns[26].Visible = false;

        }



        if (cblHeader.Items[26].Selected == true)
        {
            gridView.Columns[27].Visible = true;

        }

        if (cblHeader.Items[26].Selected == false)
        {
            gridView.Columns[27].Visible = false;

        }


        if (cblHeader.Items[27].Selected == true)
        {
            gridView.Columns[28].Visible = true;

        }

        if (cblHeader.Items[27].Selected == false)
        {
            gridView.Columns[28].Visible = false;

        }



        if (cblHeader.Items[28].Selected == true)
        {
            gridView.Columns[29].Visible = true;

        }

        if (cblHeader.Items[28].Selected == false)
        {
            gridView.Columns[29].Visible = false;

        }




        if (cblHeader.Items[29].Selected == true)
        {
            gridView.Columns[30].Visible = true;

        }

        if (cblHeader.Items[29].Selected == false)
        {
            gridView.Columns[30].Visible = false;

        }




        if (cblHeader.Items[30].Selected == true)
        {
            gridView.Columns[31].Visible = true;

        }

        if (cblHeader.Items[30].Selected == false)
        {
            gridView.Columns[31].Visible = false;

        }



        if (cblHeader.Items[31].Selected == true)
        {
            gridView.Columns[32].Visible = true;

        }

        if (cblHeader.Items[31].Selected == false)
        {
            gridView.Columns[32].Visible = false;

        }



        if (cblHeader.Items[32].Selected == true)
        {
            gridView.Columns[33].Visible = true;

        }

        if (cblHeader.Items[32].Selected == false)
        {
            gridView.Columns[33].Visible = false;

        }




        if (cblHeader.Items[33].Selected == true)
        {
            gridView.Columns[34].Visible = true;

        }

        if (cblHeader.Items[33].Selected == false)
        {
            gridView.Columns[34].Visible = false;

        }



        if (cblHeader.Items[34].Selected == true)
        {
            gridView.Columns[35].Visible = true;

        }

        if (cblHeader.Items[34].Selected == false)
        {
            gridView.Columns[35].Visible = false;

        }



        if (cblHeader.Items[35].Selected == true)
        {
            gridView.Columns[36].Visible = true;

        }

        if (cblHeader.Items[35].Selected == false)
        {
            gridView.Columns[36].Visible = false;

        }


        if (cblHeader.Items[36].Selected == true)
        {
            gridView.Columns[37].Visible = true;

        }

        if (cblHeader.Items[36].Selected == false)
        {
            gridView.Columns[37].Visible = false;

        }


        if (cblHeader.Items[37].Selected == true)
        {
            gridView.Columns[38].Visible = true;

        }

        if (cblHeader.Items[37].Selected == false)
        {
            gridView.Columns[38].Visible = false;

        }
    }

    private void LoadInitialGridForEmpDetail(GridView gridView)
    {
        gridView.Columns[1].Visible = false;
        gridView.Columns[2].Visible = false;
        gridView.Columns[3].Visible = false;
        gridView.Columns[4].Visible = false;
        gridView.Columns[5].Visible = false;
        gridView.Columns[6].Visible = false;
        gridView.Columns[7].Visible = false;
        gridView.Columns[8].Visible = false;
        gridView.Columns[9].Visible = false;
        gridView.Columns[10].Visible = false;


        //36
        gridView.Columns[11].Visible = false;
        gridView.Columns[12].Visible = false;
        gridView.Columns[13].Visible = false;
        gridView.Columns[14].Visible = false;
        gridView.Columns[15].Visible = false;
        gridView.Columns[16].Visible = false;
        gridView.Columns[17].Visible = false;
        gridView.Columns[18].Visible = false;
        gridView.Columns[19].Visible = false;
        gridView.Columns[20].Visible = false;


        gridView.Columns[21].Visible = false;
        gridView.Columns[22].Visible = false;
        gridView.Columns[23].Visible = false;
        gridView.Columns[24].Visible = false;
        gridView.Columns[25].Visible = false;
        gridView.Columns[26].Visible = false;
        gridView.Columns[27].Visible = false;
        gridView.Columns[28].Visible = false;
        gridView.Columns[29].Visible = false;
        gridView.Columns[30].Visible = false;


        gridView.Columns[31].Visible = false;
        gridView.Columns[32].Visible = false;
        gridView.Columns[33].Visible = false;
        gridView.Columns[34].Visible = false;
        gridView.Columns[35].Visible = false;
        gridView.Columns[36].Visible = false;
        gridView.Columns[37].Visible = false;
        gridView.Columns[38].Visible = false;
        gridView.Columns[39].Visible = false;
        gridView.Columns[40].Visible = false;
        gridView.Columns[41].Visible = false;
        gridView.Columns[42].Visible = false;



        if (cblHeader.Items[0].Selected == true)
        {
            gridView.Columns[1].Visible = true;

        }

        if (cblHeader.Items[0].Selected == false)
        {
            gridView.Columns[1].Visible = false;

        }


        if (cblHeader.Items[1].Selected == true)
        {
            gridView.Columns[2].Visible = true;

        }

        if (cblHeader.Items[1].Selected == false)
        {
            gridView.Columns[2].Visible = false;

        }




        if (cblHeader.Items[2].Selected == true)
        {
            gridView.Columns[3].Visible = true;

        }

        if (cblHeader.Items[2].Selected == false)
        {
            gridView.Columns[3].Visible = false;

        }



        if (cblHeader.Items[3].Selected == true)
        {
            gridView.Columns[4].Visible = true;

        }

        if (cblHeader.Items[3].Selected == false)
        {
            gridView.Columns[4].Visible = false;

        }


        if (cblHeader.Items[4].Selected == true)
        {
            gridView.Columns[5].Visible = true;

        }

        if (cblHeader.Items[4].Selected == false)
        {
            gridView.Columns[5].Visible = false;

        }



        if (cblHeader.Items[5].Selected == true)
        {
            gridView.Columns[6].Visible = true;

        }

        if (cblHeader.Items[5].Selected == false)
        {
            gridView.Columns[6].Visible = false;

        }



        if (cblHeader.Items[6].Selected == true)
        {
            gridView.Columns[7].Visible = true;

        }

        if (cblHeader.Items[6].Selected == false)
        {
            gridView.Columns[7].Visible = false;

        }




        if (cblHeader.Items[7].Selected == true)
        {
            gridView.Columns[8].Visible = true;

        }

        if (cblHeader.Items[7].Selected == false)
        {
            gridView.Columns[8].Visible = false;

        }




        if (cblHeader.Items[8].Selected == true)
        {
            gridView.Columns[9].Visible = true;

        }

        if (cblHeader.Items[8].Selected == false)
        {
            gridView.Columns[9].Visible = false;

        }





        if (cblHeader.Items[9].Selected == true)
        {
            gridView.Columns[10].Visible = true;

        }

        if (cblHeader.Items[9].Selected == false)
        {
            gridView.Columns[10].Visible = false;

        }




        if (cblHeader.Items[10].Selected == true)
        {
            gridView.Columns[11].Visible = true;

        }

        if (cblHeader.Items[10].Selected == false)
        {
            gridView.Columns[11].Visible = false;

        }


        if (cblHeader.Items[11].Selected == true)
        {
            gridView.Columns[12].Visible = true;

        }

        if (cblHeader.Items[11].Selected == false)
        {
            gridView.Columns[12].Visible = false;

        }





        if (cblHeader.Items[12].Selected == true)
        {
            gridView.Columns[13].Visible = true;

        }

        if (cblHeader.Items[12].Selected == false)
        {
            gridView.Columns[13].Visible = false;

        }





        if (cblHeader.Items[13].Selected == true)
        {
            gridView.Columns[14].Visible = true;

        }

        if (cblHeader.Items[13].Selected == false)
        {
            gridView.Columns[14].Visible = false;

        }




        if (cblHeader.Items[14].Selected == true)
        {
            gridView.Columns[15].Visible = true;

        }

        if (cblHeader.Items[14].Selected == false)
        {
            gridView.Columns[15].Visible = false;

        }





        if (cblHeader.Items[15].Selected == true)
        {
            gridView.Columns[16].Visible = true;

        }

        if (cblHeader.Items[15].Selected == false)
        {
            gridView.Columns[16].Visible = false;

        }




        if (cblHeader.Items[16].Selected == true)
        {
            gridView.Columns[17].Visible = true;

        }

        if (cblHeader.Items[16].Selected == false)
        {
            gridView.Columns[17].Visible = false;

        }

        if (cblHeader.Items[17].Selected == true)
        {
            gridView.Columns[18].Visible = true;

        }

        if (cblHeader.Items[17].Selected == false)
        {
            gridView.Columns[18].Visible = false;

        }



        if (cblHeader.Items[18].Selected == true)
        {
            gridView.Columns[19].Visible = true;

        }

        if (cblHeader.Items[18].Selected == false)
        {
            gridView.Columns[19].Visible = false;

        }




        if (cblHeader.Items[19].Selected == true)
        {
            gridView.Columns[20].Visible = true;

        }

        if (cblHeader.Items[19].Selected == false)
        {
            gridView.Columns[20].Visible = false;

        }



        if (cblHeader.Items[20].Selected == true)
        {
            gridView.Columns[21].Visible = true;

        }

        if (cblHeader.Items[20].Selected == false)
        {
            gridView.Columns[21].Visible = false;

        }


        if (cblHeader.Items[21].Selected == true)
        {
            gridView.Columns[22].Visible = true;

        }

        if (cblHeader.Items[21].Selected == false)
        {
            gridView.Columns[22].Visible = false;

        }



        if (cblHeader.Items[22].Selected == true)
        {
            gridView.Columns[23].Visible = true;

        }

        if (cblHeader.Items[22].Selected == false)
        {
            gridView.Columns[23].Visible = false;

        }



        if (cblHeader.Items[23].Selected == true)
        {
            gridView.Columns[24].Visible = true;

        }

        if (cblHeader.Items[23].Selected == false)
        {
            gridView.Columns[24].Visible = false;

        }



        if (cblHeader.Items[24].Selected == true)
        {
            gridView.Columns[25].Visible = true;

        }

        if (cblHeader.Items[24].Selected == false)
        {
            gridView.Columns[25].Visible = false;

        }




        if (cblHeader.Items[25].Selected == true)
        {
            gridView.Columns[26].Visible = true;

        }

        if (cblHeader.Items[25].Selected == false)
        {
            gridView.Columns[26].Visible = false;

        }



        if (cblHeader.Items[26].Selected == true)
        {
            gridView.Columns[27].Visible = true;

        }

        if (cblHeader.Items[26].Selected == false)
        {
            gridView.Columns[27].Visible = false;

        }


        if (cblHeader.Items[27].Selected == true)
        {
            gridView.Columns[28].Visible = true;

        }

        if (cblHeader.Items[27].Selected == false)
        {
            gridView.Columns[28].Visible = false;

        }



        if (cblHeader.Items[28].Selected == true)
        {
            gridView.Columns[29].Visible = true;

        }

        if (cblHeader.Items[28].Selected == false)
        {
            gridView.Columns[29].Visible = false;

        }




        if (cblHeader.Items[29].Selected == true)
        {
            gridView.Columns[30].Visible = true;

        }

        if (cblHeader.Items[29].Selected == false)
        {
            gridView.Columns[30].Visible = false;

        }




        if (cblHeader.Items[30].Selected == true)
        {
            gridView.Columns[31].Visible = true;

        }

        if (cblHeader.Items[30].Selected == false)
        {
            gridView.Columns[31].Visible = false;

        }



        if (cblHeader.Items[31].Selected == true)
        {
            gridView.Columns[32].Visible = true;

        }

        if (cblHeader.Items[31].Selected == false)
        {
            gridView.Columns[32].Visible = false;

        }



        if (cblHeader.Items[32].Selected == true)
        {
            gridView.Columns[33].Visible = true;

        }

        if (cblHeader.Items[32].Selected == false)
        {
            gridView.Columns[33].Visible = false;

        }




        if (cblHeader.Items[33].Selected == true)
        {
            gridView.Columns[34].Visible = true;

        }

        if (cblHeader.Items[33].Selected == false)
        {
            gridView.Columns[34].Visible = false;

        }



        if (cblHeader.Items[34].Selected == true)
        {
            gridView.Columns[35].Visible = true;

        }

        if (cblHeader.Items[34].Selected == false)
        {
            gridView.Columns[35].Visible = false;

        }



        if (cblHeader.Items[35].Selected == true)
        {
            gridView.Columns[36].Visible = true;

        }

        if (cblHeader.Items[35].Selected == false)
        {
            gridView.Columns[36].Visible = false;

        }


        if (cblHeader.Items[36].Selected == true)
        {
            gridView.Columns[37].Visible = true;

        }

        if (cblHeader.Items[36].Selected == false)
        {
            gridView.Columns[37].Visible = false;

        }


        if (cblHeader.Items[37].Selected == true)
        {
            gridView.Columns[38].Visible = true;

        }

        if (cblHeader.Items[37].Selected == false)
        {
            gridView.Columns[38].Visible = false;

        }



        if (cblHeader.Items[38].Selected == true)
        {
            gridView.Columns[39].Visible = true;

        }

        if (cblHeader.Items[38].Selected == false)
        {
            gridView.Columns[39].Visible = false;

        }



        if (cblHeader.Items[39].Selected == true)
        {
            gridView.Columns[40].Visible = true;

        }

        if (cblHeader.Items[39].Selected == false)
        {
            gridView.Columns[40].Visible = false;

        }

        if (cblHeader.Items[40].Selected == true)
        {
            gridView.Columns[41].Visible = true;
            gridView.Columns[42].Visible = true;

        }

        if (cblHeader.Items[40].Selected == false)
        {
            gridView.Columns[41].Visible = false;
            gridView.Columns[42].Visible = false;

        }
    }

    private void LoadInitialGridForEmpDetailforOnlyEmp(GridView gridView)
    {
        gridView.Columns[1].Visible = false;
        gridView.Columns[2].Visible = false;
        gridView.Columns[3].Visible = false;
        gridView.Columns[4].Visible = false;
        gridView.Columns[5].Visible = false;
        gridView.Columns[6].Visible = false;
        gridView.Columns[7].Visible = false;
        gridView.Columns[8].Visible = false;
        gridView.Columns[9].Visible = false;
        gridView.Columns[10].Visible = false;


        //36
        gridView.Columns[11].Visible = false;
        gridView.Columns[12].Visible = false;
        gridView.Columns[13].Visible = false;
        gridView.Columns[14].Visible = false;
        gridView.Columns[15].Visible = false;
        gridView.Columns[16].Visible = false;
        gridView.Columns[17].Visible = false;
        gridView.Columns[18].Visible = false;
        gridView.Columns[19].Visible = false;
        gridView.Columns[20].Visible = false;


        gridView.Columns[21].Visible = false;
        gridView.Columns[22].Visible = false;
        gridView.Columns[23].Visible = false;
        gridView.Columns[24].Visible = false;
        gridView.Columns[25].Visible = false;
        gridView.Columns[26].Visible = false;
        gridView.Columns[27].Visible = false;
        gridView.Columns[28].Visible = false;
        gridView.Columns[29].Visible = false;
        gridView.Columns[30].Visible = false;
        gridView.Columns[31].Visible = false;
        gridView.Columns[32].Visible = false;
        gridView.Columns[33].Visible = false;
        gridView.Columns[34].Visible = false;
        gridView.Columns[35].Visible = false;
        gridView.Columns[36].Visible = false;
        gridView.Columns[37].Visible = false;
        gridView.Columns[38].Visible = false;
        gridView.Columns[39].Visible = false;
        gridView.Columns[40].Visible = false;
        gridView.Columns[41].Visible = false;
        gridView.Columns[42].Visible = false;
        gridView.Columns[43].Visible = false;
        gridView.Columns[44].Visible = false;
        gridView.Columns[45].Visible = false;
        gridView.Columns[46].Visible = false;
        gridView.Columns[47].Visible = false;
        gridView.Columns[48].Visible = false;
        gridView.Columns[49].Visible = false;
        gridView.Columns[50].Visible = false;
        gridView.Columns[51].Visible = false;
        gridView.Columns[52].Visible = false;
        gridView.Columns[53].Visible = false;
        gridView.Columns[54].Visible = false;

        gridView.Columns[55].Visible = false;
        gridView.Columns[56].Visible = false;
        gridView.Columns[57].Visible = false;


        gridView.Columns[58].Visible = false;
        gridView.Columns[59].Visible = false;

        gridView.Columns[60].Visible = false;

        gridView.Columns[61].Visible = false;
        gridView.Columns[62].Visible = false;
        gridView.Columns[63].Visible = false;






        if (cblHeader.Items[0].Selected == true)
        {
            gridView.Columns[1].Visible = true;

        }

        if (cblHeader.Items[0].Selected == false)
        {
            gridView.Columns[1].Visible = false;

        }


        if (cblHeader.Items[1].Selected == true)
        {
            gridView.Columns[2].Visible = true;

        }

        if (cblHeader.Items[1].Selected == false)
        {
            gridView.Columns[2].Visible = false;

        }




        if (cblHeader.Items[2].Selected == true)
        {
            gridView.Columns[3].Visible = true;

        }

        if (cblHeader.Items[2].Selected == false)
        {
            gridView.Columns[3].Visible = false;

        }



        if (cblHeader.Items[3].Selected == true)
        {
            gridView.Columns[4].Visible = true;

        }

        if (cblHeader.Items[3].Selected == false)
        {
            gridView.Columns[4].Visible = false;

        }


        if (cblHeader.Items[4].Selected == true)
        {
            gridView.Columns[5].Visible = true;

        }

        if (cblHeader.Items[4].Selected == false)
        {
            gridView.Columns[5].Visible = false;

        }



        if (cblHeader.Items[5].Selected == true)
        {
            gridView.Columns[6].Visible = true;

        }

        if (cblHeader.Items[5].Selected == false)
        {
            gridView.Columns[6].Visible = false;

        }



        if (cblHeader.Items[6].Selected == true)
        {
            gridView.Columns[7].Visible = true;

        }

        if (cblHeader.Items[6].Selected == false)
        {
            gridView.Columns[7].Visible = false;

        }




        if (cblHeader.Items[7].Selected == true)
        {
            gridView.Columns[8].Visible = true;

        }

        if (cblHeader.Items[7].Selected == false)
        {
            gridView.Columns[8].Visible = false;

        }




        if (cblHeader.Items[8].Selected == true)
        {
            gridView.Columns[9].Visible = true;

        }

        if (cblHeader.Items[8].Selected == false)
        {
            gridView.Columns[9].Visible = false;

        }





        if (cblHeader.Items[9].Selected == true)
        {
            gridView.Columns[10].Visible = true;

        }

        if (cblHeader.Items[9].Selected == false)
        {
            gridView.Columns[10].Visible = false;

        }




        if (cblHeader.Items[10].Selected == true)
        {
            gridView.Columns[11].Visible = true;

        }

        if (cblHeader.Items[10].Selected == false)
        {
            gridView.Columns[11].Visible = false;

        }


        if (cblHeader.Items[11].Selected == true)
        {
            gridView.Columns[12].Visible = true;

        }

        if (cblHeader.Items[11].Selected == false)
        {
            gridView.Columns[12].Visible = false;

        }





        if (cblHeader.Items[12].Selected == true)
        {
            gridView.Columns[13].Visible = true;

        }

        if (cblHeader.Items[12].Selected == false)
        {
            gridView.Columns[13].Visible = false;

        }





        if (cblHeader.Items[13].Selected == true)
        {
            gridView.Columns[14].Visible = true;

        }

        if (cblHeader.Items[13].Selected == false)
        {
            gridView.Columns[14].Visible = false;

        }




        if (cblHeader.Items[14].Selected == true)
        {
            gridView.Columns[15].Visible = true;

        }

        if (cblHeader.Items[14].Selected == false)
        {
            gridView.Columns[15].Visible = false;

        }





        if (cblHeader.Items[15].Selected == true)
        {
            gridView.Columns[16].Visible = true;

        }

        if (cblHeader.Items[15].Selected == false)
        {
            gridView.Columns[16].Visible = false;

        }




        if (cblHeader.Items[16].Selected == true)
        {
            gridView.Columns[17].Visible = true;

        }

        if (cblHeader.Items[16].Selected == false)
        {
            gridView.Columns[17].Visible = false;

        }

        if (cblHeader.Items[17].Selected == true)
        {
            gridView.Columns[18].Visible = true;

        }

        if (cblHeader.Items[17].Selected == false)
        {
            gridView.Columns[18].Visible = false;

        }



        if (cblHeader.Items[18].Selected == true)
        {
            gridView.Columns[19].Visible = true;

        }

        if (cblHeader.Items[18].Selected == false)
        {
            gridView.Columns[19].Visible = false;

        }




        if (cblHeader.Items[19].Selected == true)
        {
            gridView.Columns[20].Visible = true;

        }

        if (cblHeader.Items[19].Selected == false)
        {
            gridView.Columns[20].Visible = false;

        }



        if (cblHeader.Items[20].Selected == true)
        {
            gridView.Columns[21].Visible = true;

        }

        if (cblHeader.Items[20].Selected == false)
        {
            gridView.Columns[21].Visible = false;

        }


        if (cblHeader.Items[21].Selected == true)
        {
            gridView.Columns[22].Visible = true;

        }

        if (cblHeader.Items[21].Selected == false)
        {
            gridView.Columns[22].Visible = false;

        }



        if (cblHeader.Items[22].Selected == true)
        {
            gridView.Columns[23].Visible = true;

        }

        if (cblHeader.Items[22].Selected == false)
        {
            gridView.Columns[23].Visible = false;

        }



        if (cblHeader.Items[23].Selected == true)
        {
            gridView.Columns[24].Visible = true;

        }

        if (cblHeader.Items[23].Selected == false)
        {
            gridView.Columns[24].Visible = false;

        }



        if (cblHeader.Items[24].Selected == true)
        {
            gridView.Columns[25].Visible = true;

        }

        if (cblHeader.Items[24].Selected == false)
        {
            gridView.Columns[25].Visible = false;

        }




        if (cblHeader.Items[25].Selected == true)
        {
            gridView.Columns[26].Visible = true;

        }

        if (cblHeader.Items[25].Selected == false)
        {
            gridView.Columns[26].Visible = false;

        }



        if (cblHeader.Items[26].Selected == true)
        {
            gridView.Columns[27].Visible = true;

        }

        if (cblHeader.Items[26].Selected == false)
        {
            gridView.Columns[27].Visible = false;

        }


        if (cblHeader.Items[27].Selected == true)
        {
            gridView.Columns[28].Visible = true;

        }

        if (cblHeader.Items[27].Selected == false)
        {
            gridView.Columns[28].Visible = false;

        }



        if (cblHeader.Items[28].Selected == true)
        {
            gridView.Columns[29].Visible = true;

        }

        if (cblHeader.Items[28].Selected == false)
        {
            gridView.Columns[29].Visible = false;

        }




        if (cblHeader.Items[29].Selected == true)
        {
            gridView.Columns[30].Visible = true;

        }

        if (cblHeader.Items[29].Selected == false)
        {
            gridView.Columns[30].Visible = false;

        }




        if (cblHeader.Items[30].Selected == true)
        {
            gridView.Columns[31].Visible = true;

        }

        if (cblHeader.Items[30].Selected == false)
        {
            gridView.Columns[31].Visible = false;

        }



        if (cblHeader.Items[31].Selected == true)
        {
            gridView.Columns[32].Visible = true;

        }

        if (cblHeader.Items[31].Selected == false)
        {
            gridView.Columns[32].Visible = false;

        }



        if (cblHeader.Items[32].Selected == true)
        {
            gridView.Columns[33].Visible = true;

        }

        if (cblHeader.Items[32].Selected == false)
        {
            gridView.Columns[33].Visible = false;

        }




        if (cblHeader.Items[33].Selected == true)
        {
            gridView.Columns[34].Visible = true;

        }

        if (cblHeader.Items[33].Selected == false)
        {
            gridView.Columns[34].Visible = false;

        }



        if (cblHeader.Items[34].Selected == true)
        {
            gridView.Columns[35].Visible = true;

        }

        if (cblHeader.Items[34].Selected == false)
        {
            gridView.Columns[35].Visible = false;

        }



        if (cblHeader.Items[35].Selected == true)
        {
            gridView.Columns[36].Visible = true;

        }

        if (cblHeader.Items[35].Selected == false)
        {
            gridView.Columns[36].Visible = false;

        }


        if (cblHeader.Items[36].Selected == true)
        {
            gridView.Columns[37].Visible = true;

        }

        if (cblHeader.Items[36].Selected == false)
        {
            gridView.Columns[37].Visible = false;

        }


        if (cblHeader.Items[37].Selected == true)
        {
            gridView.Columns[38].Visible = true;

        }

        if (cblHeader.Items[37].Selected == false)
        {
            gridView.Columns[38].Visible = false;

        }



        if (cblHeader.Items[38].Selected == true)
        {
            gridView.Columns[39].Visible = true;

        }

        if (cblHeader.Items[38].Selected == false)
        {
            gridView.Columns[39].Visible = false;

        }



        if (cblHeader.Items[39].Selected == true)
        {
            gridView.Columns[40].Visible = true;

        }

        if (cblHeader.Items[39].Selected == false)
        {
            gridView.Columns[40].Visible = false;

        }


        if (cblHeader.Items[40].Selected == true)
        {
            gridView.Columns[41].Visible = true;

        }

        if (cblHeader.Items[40].Selected == false)
        {
            gridView.Columns[41].Visible = false;

        }
        //father
        if (cblHeader.Items[41].Selected == true)
        {
            //string text = cblHeader.Items[40].Text;
            gridView.Columns[42].Visible = true;
            gridView.Columns[43].Visible = true;

        }

        if (cblHeader.Items[41].Selected == false)
        {
            gridView.Columns[42].Visible = false;
            gridView.Columns[43].Visible = false;

        }

        //if (cblHeader.Items[41].Selected == true)
        //{
        //    gridView.Columns[43].Visible = true;

        //}

        //if (cblHeader.Items[41].Selected == false)
        //{
        //    gridView.Columns[43].Visible = false;

        //}


        if (cblHeader.Items[42].Selected == true)
        {
            gridView.Columns[44].Visible = true;

        }

        if (cblHeader.Items[42].Selected == false)
        {
            gridView.Columns[44].Visible = false;

        }



        if (cblHeader.Items[43].Selected == true)
        {
            gridView.Columns[45].Visible = true;

        }

        if (cblHeader.Items[43].Selected == false)
        {
            gridView.Columns[45].Visible = false;

        }

        if (cblHeader.Items[44].Selected == true)
        {
            gridView.Columns[46].Visible = true;

        }

        if (cblHeader.Items[44].Selected == false)
        {
            gridView.Columns[46].Visible = false;

        }


        if (cblHeader.Items[45].Selected == true)
        {
            gridView.Columns[47].Visible = true;

        }

        if (cblHeader.Items[45].Selected == false)
        {
            gridView.Columns[47].Visible = false;

        }

        if (cblHeader.Items[46].Selected == true)
        {
            gridView.Columns[48].Visible = true;

        }

        if (cblHeader.Items[46].Selected == false)
        {
            gridView.Columns[48].Visible = false;

        }


        if (cblHeader.Items[47].Selected == true)
        {
            gridView.Columns[49].Visible = true;

        }

        if (cblHeader.Items[47].Selected == false)
        {
            gridView.Columns[49].Visible = false;

        }



        if (cblHeader.Items[48].Selected == true)
        {
            gridView.Columns[50].Visible = true;

        }

        if (cblHeader.Items[48].Selected == false)
        {
            gridView.Columns[50].Visible = false;

        }

        if (cblHeader.Items[49].Selected == true)
        {
            gridView.Columns[51].Visible = true;

        }

        if (cblHeader.Items[49].Selected == false)
        {
            gridView.Columns[51].Visible = false;

        }


        if (cblHeader.Items[50].Selected == true)
        {
            gridView.Columns[52].Visible = true;

        }

        if (cblHeader.Items[50].Selected == false)
        {
            gridView.Columns[52].Visible = false;

        }

        if (cblHeader.Items[51].Selected == true)
        {
            gridView.Columns[53].Visible = true;

        }
        if (cblHeader.Items[51].Selected == false)
        {
            gridView.Columns[53].Visible = false;

        }



        if (cblHeader.Items[52].Selected == true)
        {
            gridView.Columns[54].Visible = true;

        }

        if (cblHeader.Items[52].Selected == false)
        {
            gridView.Columns[54].Visible = false;

        }



        if (cblHeader.Items[53].Selected == true)
        {
            gridView.Columns[55].Visible = true;

        }

        if (cblHeader.Items[53].Selected == false)
        {
            gridView.Columns[55].Visible = false;

        }



        if (cblHeader.Items[54].Selected == true)
        {
            gridView.Columns[56].Visible = true;

        }

        if (cblHeader.Items[54].Selected == false)
        {
            gridView.Columns[56].Visible = false;

        }


        if (cblHeader.Items[55].Selected == true)
        {
            gridView.Columns[57].Visible = true;

        }

        if (cblHeader.Items[55].Selected == false)
        {
            gridView.Columns[57].Visible = false;

        }



        if (cblHeader.Items[56].Selected == true)
        {
            gridView.Columns[58].Visible = true;

        }

        if (cblHeader.Items[56].Selected == false)
        {
            gridView.Columns[58].Visible = false;

        }


        if (cblHeader.Items[57].Selected == true)
        {
            gridView.Columns[59].Visible = true;

        }

        if (cblHeader.Items[57].Selected == false)
        {
            gridView.Columns[59].Visible = false;

        }


        if (cblHeader.Items[58].Selected == true)
        {
            gridView.Columns[60].Visible = true;

        }

        if (cblHeader.Items[58].Selected == false)
        {
            gridView.Columns[60].Visible = false;

        }


        if (cblHeader.Items[59].Selected == true)
        {
            gridView.Columns[61].Visible = true;

        }

        if (cblHeader.Items[59].Selected == false)
        {
            gridView.Columns[61].Visible = false;

        }



        if (cblHeader.Items[60].Selected == true)
        {
            gridView.Columns[62].Visible = true;

        }

        if (cblHeader.Items[60].Selected == false)
        {
            gridView.Columns[62].Visible = false;

        }



        if (cblHeader.Items[61].Selected == true)
        {
            gridView.Columns[63].Visible = true;

        }

        if (cblHeader.Items[61].Selected == false)
        {
            gridView.Columns[63].Visible = false;

        }


    }


    public void LoadData()
    {

        LoadInitialGridForEmpDetailforOnlyEmp(loadGridView);
        DataTable dtdata = aReportDal.GetEmpInfOnlyViewo(Parameter(), ParameterOnlyView());

        if (dtdata.Rows.Count>0)
        {
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();
            lblCount.Text = "Total: " + dtdata.Rows.Count.ToString();
        } 
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            lblCount.Text = "Total: 0" ;
        }
       

       
        
    }
    public void GetCheckBox()
    {
        DataTable dtgrade = aReportDal.GetGrade();
        gradeCheckBoxList.DataValueField = "SalaryGradeId";
        gradeCheckBoxList.DataTextField = "GradeCode";
        gradeCheckBoxList.DataSource = dtgrade;
        gradeCheckBoxList.DataBind();

    

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





        DataTable dtNameEducation = aReportDal.LoadddlEducation();
        cblNameEducation.DataValueField = "EducationNameID";
        cblNameEducation.DataTextField = "Description";
        cblNameEducation.DataSource = dtNameEducation;
        cblNameEducation.DataBind();




        DataTable dtSubjectGroup = aReportDal.LoadddlSubjectGroup();
        cblSubjectGroup.DataValueField = "EducationSubjectGroupID";
        cblSubjectGroup.DataTextField = "Description";
        cblSubjectGroup.DataSource = dtSubjectGroup;
        cblSubjectGroup.DataBind();


        DataTable dtJobleft = aSeparationDAL.GetJobleftType();
        SeparationmanagementCheckBoxList.DataValueField = "JobLeftTypeId";
        SeparationmanagementCheckBoxList.DataTextField = "JobLeftType";
        SeparationmanagementCheckBoxList.DataSource = dtJobleft;
        SeparationmanagementCheckBoxList.DataBind();
 
    }
    public void DropDown()
    {
        aReportDal.LoadCompanyDropDownList(ddlCompany);
        aReportDal.LoadSurveyNameDropDownList(SurveyNameDropDownList);
        

        
        PromotionDAL.LoadPromotionTypeDropDownList(PromotionTypeDropDownList);
        ddlCompany_OnSelectedIndexChanged(null, null);
        empStatusDropDownList_OnSelectedIndexChanged(null, null);
        aReportDal.GetCategory(typeOfPosDropDownList);
        aReportDal.GetSuspendReason(suspendDropDownList);
        aReportDal.GetJobleftReason(jobleftTypeDropDownList);
        aReportDal.LoadddlEducation(ddlEducation);
        aReportDal.LoadddlSubjectGroup(ddlSubject);
        aReportDal.LoadddlCountry(ddlCountry);
        aIncrementDAL.FinYearByCompDropDown(IncrementFinancialYearDropDownList, ddlCompany.SelectedValue);
        aCommonDataLoadDal.LoadIncrementType(IncrementddlIncrementType);




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
        try
        {
            DataTable dtdivdata = aReportDal.GetAllDivision(ddlCompany.SelectedValue);
            for (int i = 0; i < dtdivdata.Rows.Count; i++)
            {
                aTreeView.Nodes.Add(new TreeNode((dtdivdata.Rows[i]["DivisionName"].ToString()) + "(Division)", (dtdivdata.Rows[i]["DivisionId"].ToString())));
                DataTable dtwing =
                    aReportDal.GetAllWing(" AND  tblDivision.DivisionId='" + dtdivdata.Rows[i]["DivisionId"].ToString() +
                                          "'");
                for (int j = 0; j < dtwing.Rows.Count; j++)
                {
                    aTreeView.Nodes[i].ChildNodes.Add(new TreeNode((dtwing.Rows[j]["DivisionWingName"].ToString()) + "(Wing)", (dtwing.Rows[j]["DivisionWId"].ToString())));

                    DataTable dtdeptm = aReportDal.GetAllDepartment(" AND  tblDepartment.DivisionWId='" + dtwing.Rows[j]["DivisionWId"].ToString() +
                                         "' AND tblDepartment.Root='Wing'");
                    for (int k = 0; k < dtdeptm.Rows.Count; k++)
                    {
                        aTreeView.Nodes[i].ChildNodes[j].ChildNodes.Add(new TreeNode((dtdeptm.Rows[j]["DepartmentName"].ToString()) + "(Department)", (dtdeptm.Rows[j]["DepartmentId"].ToString())));

                        DataTable dtsecm1 =
                   aReportDal.GetAllSection(" AND  tblDepartment.DepartmentId='" + dtdeptm.Rows[j]["DepartmentId"].ToString() +
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
        catch (Exception)
        {
            
            //throw;
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
        string param = " ";
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

        param = param + " AND ( ";
        if (div !=string.Empty)
        {

            param = param + "   ( EG.DivisionId " + hierchicalDropDownList.SelectedItem.Text + " (" + div.TrimEnd(',') + ")  or";
        }
        if (wing !=string.Empty)
        {
            param = param + "  ( EG.DivisionWId " + hierchicalDropDownList.SelectedItem.Text + " (" + wing.TrimEnd(',') + ") ) or";
        }
        if (dept != string.Empty)
        {
            param = param + "  ( EG.DepartmentId " + hierchicalDropDownList.SelectedItem.Text + " (" + dept.TrimEnd(',') + ") ) or";
        }
        if (sec != string.Empty)
        {
            param = param + "  ( EG.SectionId " + hierchicalDropDownList.SelectedItem.Text + " (" + sec.TrimEnd(',') + ") ) or";
        }
        if (subsec != string.Empty)
        {
            param = param + "  ( EG.SubSectionId " + hierchicalDropDownList.SelectedItem.Text + " (" + subsec.TrimEnd(',') + ") ) or";
        }

       

        if (div== string.Empty && wing == string.Empty && dept == string.Empty && sec == string.Empty && subsec == string.Empty)
        {
            param = param.TrimEnd("AND ( ".ToCharArray()); 
        }
        else
        {

            param = param.TrimEnd("or".ToCharArray()); 
            param = param + ")";
        }

        if (div != string.Empty)
        {
            param = param + ")";
        }



        return param ;


        
    }


    public string HierchicalParameterForBd()
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
            if (step == "Division")
            {
                div = node.Value + "," + div;
                //param = param + " AND EG.DivisionId='" + node.Value + "' ";
            }
            else if (step == "Wing")
            {
                wing = node.Value + "," + wing;
                //param = param + " AND EG.DivisionWId='" + node.Value + "' ";
            }
            else if (step == "Department")
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
            param = param + " AND e.DivisionId " + hierchicalDropDownList.SelectedItem.Text + " (" + div.TrimEnd(',') +
                    ") ";
        }
        if (wing != string.Empty)
        {
            param = param + " AND e.DivisionWId " + hierchicalDropDownList.SelectedItem.Text + " (" + wing.TrimEnd(',') +
                    ") ";
        }
        if (dept != string.Empty)
        {
            param = param + " AND e.DepartmentId " + hierchicalDropDownList.SelectedItem.Text + " (" + dept.TrimEnd(',') +
                    ")";
        }
        if (sec != string.Empty)
        {
            param = param + " AND e.SectionId " + hierchicalDropDownList.SelectedItem.Text + " (" + sec.TrimEnd(',') +
                    ") ";
        }
        if (subsec != string.Empty)
        {
            param = param + " AND e.SubSectionId " + hierchicalDropDownList.SelectedItem.Text + " (" + subsec.TrimEnd(',') +
                    ") ";
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
        if (repEmpIdHiddenField.Value != string.Empty)
        {
            param = param + " AND EG.EmpInfoId='" + repEmpIdHiddenField.Value + "' ";
        }
        if (typeOfPosDropDownList.SelectedValue != "")
        {
            param = param + " AND EG.EmpCategoryId='" + typeOfPosDropDownList.SelectedValue + "' ";
        }

        
    
        if (GradeParam() != string.Empty)
        {
            param = param + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            param = param + StepParam();
        }
        if (SalLocParam() != string.Empty)
        {
            param = param + SalLocParam();
        }
        if (DesigParam() != string.Empty)
        {
            param = param + DesigParam();
        }

        if (RelagionParam() != string.Empty)
        {
            param = param + RelagionParam();
        }

        if (GendernParam() != string.Empty)
        {
            param = param + GendernParam();
        }

        if (BloodParam() != string.Empty)
        {
            param = param + BloodParam();
        }

        if (PermDistParam() != string.Empty)
        {
            param = param + PermDistParam();
        }

        if (PermThanaParam() != string.Empty)
        {
            param = param + PermThanaParam();
        }




        if (PlaceParam() != string.Empty)
        {
            param = param + PlaceParam();
        }

        //if (nominationTextBox.Text != string.Empty)
        //{
        //    param = param + NominationParam();
        //}

        //if (ddlEducation.SelectedValue != "")
        //{
        //    param = param + EducationNameParam();
        //}

        //if (ddlSubject.SelectedValue != "")
        //{
        //    param = param + SubjectGroupParam();
        //}

        //if (LoadNameEducation()!="")
        //{
        //    param = param + LoadNameEducation();
        //}



        //if (LoadcblSubjectGroup() != "")
        //{
        //    param = param + LoadcblSubjectGroup();
        //}

            
     
             
        

        //if (ddlCountry.SelectedValue != "")
        //{
        //    param = param + CountryParam();
        //}

        //if (ddlTrainigStart.SelectedValue != "1")
        //{
        //    if (ddlTrainigStart.SelectedValue == "2" || ddlTrainigStart.SelectedValue == "3" || ddlTrainigStart.SelectedValue == "4")
        //    {
        //        param = param + " AND TI.TrFromDate " + ddlTrainigStart.SelectedItem.Text + " '" +
        //                trainingStartTextBox.Text + "' ";
        //    }
        //    else
        //    {
        //        param = param + " AND (TI.TrFromDate between '" + trainingStartFRTextBox.Text + "' AND '" + trainingToTextBox.Text + "' )";
        //    }
        //}

        //if (ddlTrainingEnd.SelectedValue != "1")
        //{
        //    if (ddlTrainigStart.SelectedValue == "2" || ddlTrainigStart.SelectedValue == "3" || ddlTrainigStart.SelectedValue == "4")
        //    {
        //        param = param + " AND TI.TrToDate " + ddlTrainigStart.SelectedItem.Text + " '" +
        //                trainingEndTextBox.Text + "' ";
        //    }
        //    else
        //    {
        //        param = param + " AND (TI.TrToDate between '" + trainingEndFRTextBox.Text + "' AND '" + trainingEndToTextBox.Text + "' )";
        //    }
        //}



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

        if ( empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            param = param + "";
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
                  //  param = param + " AND S.ReasonId='" + suspendDropDownList.SelectedValue + "' ";
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
                param = param + " AND EG.DateOfConformation " + endDateDropDownList.SelectedItem.Text + " '" +
                        endDateTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (EG.DateOfConformation between '" + endFromDateTextBox.Text + "' AND '" + endToDateTextBox.Text + "') ";
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



        if (SalaryRangeDropDownList.SelectedValue != "1")
        {
            if (SalaryRangeDropDownList.SelectedValue == "2" || SalaryRangeDropDownList.SelectedValue == "3" || SalaryRangeDropDownList.SelectedValue == "4")
            {
                param = param + " AND ST.GrossAmount " + SalaryRangeDropDownList.SelectedItem.Text + " '" +
                        txtSalaryRange.Text + "' ";
            }
            else
            {
                param = param + " AND (ST.GrossAmount between '" + txtSalaryRangeFrom.Text + "' AND '" + txtSalaryRangeTo.Text + "') ";
            }
        }


        if (HierchicalParameter() != string.Empty)
        {
            param = param + HierchicalParameter();
        }

        return param;
    }


    public string ParameterOnlyView()
    {
        string param = "";

        
        if (empNoTextBox.Text != string.Empty)
        {
            param = param + " AND EG.EmpMasterCode='" + empNoTextBox.Text + "' ";
        }
        if (repEmpIdHiddenField.Value != string.Empty)
        {
            param = param + " AND EG.EmpInfoId='" + repEmpIdHiddenField.Value + "' ";
        }
        if (typeOfPosDropDownList.SelectedValue != "")
        {
            param = param + " AND EG.EmpCategoryId='" + typeOfPosDropDownList.SelectedValue + "' ";
        }



        if (GradeParam() != string.Empty)
        {
            param = param + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            param = param + StepParam();
        }
        if (SalLocParam() != string.Empty)
        {
            param = param + SalLocParam();
        }
        if (DesigParam() != string.Empty)
        {
            param = param + DesigParam();
        }

        if (RelagionParam() != string.Empty)
        {
            param = param + RelagionParam();
        }

        if (GendernParam() != string.Empty)
        {
            param = param + GendernParam();
        }

        if (BloodParam() != string.Empty)
        {
            param = param + BloodParam();
        }

        if (PermDistParam() != string.Empty)
        {
            param = param + PermDistParam();
        }

        if (PermThanaParam() != string.Empty)
        {
            param = param + PermThanaParam();
        }




        if (PlaceParam() != string.Empty)
        {
            param = param + PlaceParam();
        }

        //if (nominationTextBox.Text != string.Empty)
        //{
        //    param = param + NominationParam();
        //}

        //if (ddlEducation.SelectedValue != "")
        //{
        //    param = param + EducationNameParam();
        //}

        //if (ddlSubject.SelectedValue != "")
        //{
        //    param = param + SubjectGroupParam();
        //}

        //if (LoadNameEducation()!="")
        //{
        //    param = param + LoadNameEducation();
        //}



        //if (LoadcblSubjectGroup() != "")
        //{
        //    param = param + LoadcblSubjectGroup();
        //}






        //if (ddlCountry.SelectedValue != "")
        //{
        //    param = param + CountryParam();
        //}

        //if (ddlTrainigStart.SelectedValue != "1")
        //{
        //    if (ddlTrainigStart.SelectedValue == "2" || ddlTrainigStart.SelectedValue == "3" || ddlTrainigStart.SelectedValue == "4")
        //    {
        //        param = param + " AND TI.TrFromDate " + ddlTrainigStart.SelectedItem.Text + " '" +
        //                trainingStartTextBox.Text + "' ";
        //    }
        //    else
        //    {
        //        param = param + " AND (TI.TrFromDate between '" + trainingStartFRTextBox.Text + "' AND '" + trainingToTextBox.Text + "' )";
        //    }
        //}

        //if (ddlTrainingEnd.SelectedValue != "1")
        //{
        //    if (ddlTrainigStart.SelectedValue == "2" || ddlTrainigStart.SelectedValue == "3" || ddlTrainigStart.SelectedValue == "4")
        //    {
        //        param = param + " AND TI.TrToDate " + ddlTrainigStart.SelectedItem.Text + " '" +
        //                trainingEndTextBox.Text + "' ";
        //    }
        //    else
        //    {
        //        param = param + " AND (TI.TrToDate between '" + trainingEndFRTextBox.Text + "' AND '" + trainingEndToTextBox.Text + "' )";
        //    }
        //}



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

        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            param = param + "";
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
                    //  param = param + " AND S.ReasonId='" + suspendDropDownList.SelectedValue + "' ";
                }
            }

        }
        if (emptypeDropDownList.SelectedValue != "3" && emptypeDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            param = param + " AND EG.EmpTypeId" + employementDropDownList.SelectedValue + "'" + emptypeDropDownList.SelectedValue + "' ";
        }
        else
        {
            if (emptypeDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
            {
                param = param + " AND EG.IsProgramContractual" + employementDropDownList.SelectedValue + "'1' ";
            }

        }
        if (joiningDateDropDownList.SelectedValue != "1")
        {
            if (joiningDateDropDownList.SelectedValue == "2" || joiningDateDropDownList.SelectedValue == "3" || joiningDateDropDownList.SelectedValue == "4")
            {
                param = param + " AND EG.DateOfJoin" + joiningDateDropDownList.SelectedItem.Text + " '" +
                        joiningDtTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (EG.DateOfJoin between '" + joiningDtFrTextBox.Text + "' AND '" + joiningDtToTextBox.Text + "' )";
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
                param = param + " AND EG.DateOfConformation " + endDateDropDownList.SelectedItem.Text + " '" +
                        endDateTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (EG.DateOfConformation between '" + endFromDateTextBox.Text + "' AND '" + endToDateTextBox.Text + "') ";
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
        if (confirmStDropDownList.SelectedIndex > 0)
        {
            param = param + " AND EG.ConformationStatus='" + confirmStDropDownList.SelectedValue + "' ";
        }



        if (SalaryRangeDropDownList.SelectedValue != "1")
        {
            if (SalaryRangeDropDownList.SelectedValue == "2" || SalaryRangeDropDownList.SelectedValue == "3" || SalaryRangeDropDownList.SelectedValue == "4")
            {
                param = param + " AND ST.GrossAmount " + SalaryRangeDropDownList.SelectedItem.Text + " '" +
                        txtSalaryRange.Text + "' ";
            }
            else
            {
                param = param + " AND (ST.GrossAmount between '" + txtSalaryRangeFrom.Text + "' AND '" + txtSalaryRangeTo.Text + "') ";
            }
        }


        if (HierchicalParameter() != string.Empty)
        {
            param = param + HierchicalParameter();
        }

        return param;
    }



    public string ParameterReqru()
    {
        string param = "";

        if (ddlCompany.SelectedValue != "")
        {
            param = param + " AND EG.CompanyId='" + ddlCompany.SelectedValue + "' ";
        }
        if (empNoTextBox.Text != string.Empty)
        {
            param = param + " AND EG.EmpMasterCode='" + empNoTextBox.Text + "' ";
        }
        if (repEmpIdHiddenField.Value != string.Empty)
        {
            param = param + " AND EG.EmpInfoId='" + repEmpIdHiddenField.Value + "' ";
        }
        if (typeOfPosDropDownList.SelectedValue != "")
        {
            param = param + " AND EG.EmpCategoryId='" + typeOfPosDropDownList.SelectedValue + "' ";
        }
  
        
         
        return param;
    }

    public string ParameterReqru_3()
    {
        string param = "";


        if (ddlRecruitmentType.SelectedValue != "0")
        {
            param = param + " AND RecruitmentType='" + ddlRecruitmentType.SelectedValue + "' ";
        }

        if (txtRecruitmentfrmDate.Text != "" && txtRecruitmentToDate.Text != "")
        {

            param = param + " AND  convert(Date, proEffectivedate) between '" + txtRecruitmentfrmDate.Text + "' AND '" + txtRecruitmentToDate.Text + "'  ";

        }


        return param;
    }
    public string ParameterOnlyViewRequi()
    {
        string param = "";


       
        if (empNoTextBox.Text != string.Empty)
        {
            param = param + " AND EG.EmpMasterCode='" + empNoTextBox.Text + "' ";
        }
        if (repEmpIdHiddenField.Value != string.Empty)
        {
            param = param + " AND EG.EmpInfoId='" + repEmpIdHiddenField.Value + "' ";
        }
        if (typeOfPosDropDownList.SelectedValue != "")
        {
            param = param + " AND EG.EmpCategoryId='" + typeOfPosDropDownList.SelectedValue + "' ";
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
    CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();

    ProjectWiseEmployeeAllocationEntryDAL dalProject = new ProjectWiseEmployeeAllocationEntryDAL();
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCompany.SelectedValue != "")
        {
          // Session["CompanyId"] = ddlCompany.SelectedValue; 
        }
   

        if (ddlCompany.SelectedValue != "")
        




{

    DataTable dtFinYearList = _trainingDal.GetFianncialYearByComIdChkList(Convert.ToInt32(ddlCompany.SelectedValue));
    chkfin.DataValueField = "Value";
    chkfin.DataTextField = "TextField";
    chkfin.DataSource = dtFinYearList;
    chkfin.DataBind();


            DataTable dtsalloc = aReportDal.GetSalLocbyCompany(Convert.ToInt32(ddlCompany.SelectedValue));
            salLocCheckBoxList.DataValueField = "SalaryLoationId";
            salLocCheckBoxList.DataTextField = "SalaryLocation";
            salLocCheckBoxList.DataSource = dtsalloc;
            salLocCheckBoxList.DataBind();

            empStatusDropDownList_OnSelectedIndexChanged(null, null);
            //using (DataTable dt222 = _commonDataLoad.GetEmpDDL(ddlCompany.SelectedValue.ToString()))
            //{



            //    ddlEmpInfo.DataSource = dt222;
            //    ddlEmpInfo.DataValueField = "EmpInfoId";
            //    ddlEmpInfo.DataTextField = "EmpName";
            //    ddlEmpInfo.DataBind();
            //    ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            //    ddlEmpInfo.SelectedIndex = 0;
            //}
            aReportDal.LoadActionType(ddlAction, ddlCompany.SelectedValue);
            aReportDal.GetDivisionList(NewDivisionDropDownList, ddlCompany.SelectedValue);

            dalProject.LoaProjectDropDownList(ProjectDropDownList, ddlCompany.SelectedValue);


            DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
            KPIddlFinancialYear.DataSource = dt;
            KPIddlFinancialYear.DataValueField = "Value";
            KPIddlFinancialYear.DataTextField = "TextField";
            KPIddlFinancialYear.DataBind();



            appraisalFinYearDropDownList.DataSource = dt;
            appraisalFinYearDropDownList.DataValueField = "Value";
            appraisalFinYearDropDownList.DataTextField = "TextField";
            appraisalFinYearDropDownList.DataBind();



            surveyFinYearDropDownList.DataSource = dt;
            surveyFinYearDropDownList.DataValueField = "Value";
            surveyFinYearDropDownList.DataTextField = "TextField";
            surveyFinYearDropDownList.DataBind();


        }
        else
        {
            ddlEmpInfo.Items.Clear();  
            
        }


        if (ddlCompany.SelectedValue != "")
        {
            heirerchicalTreeView.Nodes.Clear();
             AddTree(heirerchicalTreeView);
            reportDropDownList_SelectedIndexChanged(null, null);
            aEmployeeJobLeftEntryDAL.FinYearByCompDropDown(FinancialYearDropDownList, ddlCompany.SelectedValue);
        }
        else
        {
            reportDropDownList_SelectedIndexChanged(null, null);
            FinancialYearDropDownList.Items.Clear();
        }

        if (ddlCompany.SelectedValue != "")
        {

            PromotionDAL.FinYearByCompDropDown(PromotionFinancialYearDropDownList, ddlCompany.SelectedValue);
        }
        else
        {
            PromotionFinancialYearDropDownList.Items.Clear();
        }

        
       
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

    //protected void empStatusDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    suspend.Visible = false;
    //    jobleft.Visible = false;
    //    if (empStatusDropDownList.SelectedItem.Text=="Inactive")
    //    {
    //        suspend.Visible = true;
    //        jobleft.Visible = true;
    //    }

    //}

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
            string attachment = "attachment; filename=Employee_List_Info.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            loadGridView.AllowPaging = false;



       

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
                                 DateTime.Now.ToString("dd-MMM-yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3> Employee Information	&nbsp;&nbsp;&nbsp;&nbsp;(" + lblCount.Text + ")</h3></span>";

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
        Response.Redirect("EmployeeInformationReport.aspx");
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

    protected void genderCheckBoxList_OnSelectedIndexChanged(object sender, EventArgs e)
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
    }

    protected void bloodgroupCheckBoxList_OnSelectedIndexChanged(object sender, EventArgs e)
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
    }

    protected void religionCheckBoxList_OnSelectedIndexChanged(object sender, EventArgs e)
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
    }

    protected void heirerchicalTreeView_OnSelectedNodeChanged(object sender, EventArgs e)
    {
        foreach (TreeNode node in heirerchicalTreeView.CheckedNodes)
        {
            hierchicalSelectedTextBox.Text = node.Text + "," + hierchicalSelectedTextBox.Text;
          
        }
    }

    protected void stepCheckBoxList_OnSelectedIndexChanged(object sender, EventArgs e)
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
    }

    protected void permThanaCheckBoxList1_OnSelectedIndexChanged(object sender, EventArgs e)
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
    }

    protected void salLocCheckBoxList_OnSelectedIndexChanged(object sender, EventArgs e)
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
    }

    protected void desigCheckBoxList_OnSelectedIndexChanged(object sender, EventArgs e)
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
    }

    protected void cblNameEducation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    #endregion

    public string LoadNameEducation() {
        string NameEducation = "";
        for (int i = 0; i < cblNameEducation.Items.Count; i++)
        {
            if (cblNameEducation.Items[i].Selected)
            {
                NameEducation = NameEducation + "'" + cblNameEducation.Items[i].Value + "'" + ",";
            }
        }
        NameEducation = NameEducation.TrimEnd(',');
        return NameEducation;
    }
   


    public string LoadcblSubjectGroup()
        {
        string SubjectGroup = "";
        for (int i = 0; i < cblSubjectGroup.Items.Count; i++)
        {
            if (cblSubjectGroup.Items[i].Selected)
            {
                SubjectGroup = SubjectGroup + "'" + cblSubjectGroup.Items[i].Value + "'" + ",";
            }
        }
        SubjectGroup = SubjectGroup.TrimEnd(',');
        return SubjectGroup;
    }
 

    protected void cblSubjectGroup_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    #region Education
    protected void lbEducationSearch_OnClick(object sender, EventArgs e)
    {
        EducationLoadData();
      
    }

    public void EducationLoadData()
    {
        LoadInitialGridForEmpDetail(gv_Education);

        DataTable dtdata = aReportDal.EducationGetEmpInfo(EducationParameter());

        if (dtdata.Rows.Count > 0)
        {
            gv_Education.DataSource = dtdata;
            gv_Education.DataBind();
        }
        else
        {
            gv_Education.DataSource = null;
            gv_Education.DataBind();
        }




    }
   

    protected void lbEducationReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");
    }

protected void lblExportToExcelEducation_Click(object sender, EventArgs e)
    {
        
    }

public string EducationParameter()
{
    string param = "";

    if (ddlCompany.SelectedValue != "")
    {
        param = param + " AND EG.CompanyId='" + ddlCompany.SelectedValue + "' ";
    }
    if (empNoTextBox.Text != string.Empty)
    {
        param = param + " AND EG.EmpMasterCode='" + empNoTextBox.Text + "' ";
    }
    if (repEmpIdHiddenField.Value != string.Empty)
    {
        param = param + " AND edu.EmpInfoId ='" + repEmpIdHiddenField.Value + "' ";
    }


    if (IsEduLastLabel.Checked ==true)
    {
        param = param + " AND edu.EduIsLastLevel=1 ";
    }



    for (int i = 0; i < cblNameEducation.Items.Count; i++)
    {
        if (cblNameEducation.Items[i].Selected)
        {

            param = param + " and  edu.EducationNameId IN (" + LoadNameEducation() + ") ";
        }
    }


    for (int i = 0; i < cblSubjectGroup.Items.Count; i++)
    {
        if (cblSubjectGroup.Items[i].Selected)
        {

            param = param + " and  edu.SubjectGroupId IN (" + LoadcblSubjectGroup() + ") ";
        }
    }

    if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
    {

        param = param + "";
    }

    if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
    {
        param = param + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
    }

    if (GradeParam() != string.Empty)
    {
        param = param + GradeParam();
    }
    if (StepParam() != string.Empty)
    {
        param = param + StepParam();
    }


    if (SalLocParam() != string.Empty)
    {
        param = param + SalLocParam();
    }


    if (PlaceParam() != string.Empty)
    {
        param = param + PlaceParam();
    }


    if (HierchicalParameter() != string.Empty)
    {
        param = param + HierchicalParameter();
    }


      

    
    return param;
}

protected void SSGradeCheck_OnCheckedChanged(object sender, EventArgs e)
{
    for (int i = 0; i < gradeCheckBoxList.Items.Count; i++)
    {
        if (SSGradeCheck.Checked)
        {
            gradeCheckBoxList.Items[i].Selected = true;
        }
        else
        {
            gradeCheckBoxList.Items[i].Selected = false
                ;
        }
    }

    gradeCheckBoxList_OnSelectedIndexChanged(null, null);
}

protected void SSstepCheckBox_OnCheckedChanged(object sender, EventArgs e)
{
    for (int i = 0; i < stepCheckBoxList.Items.Count; i++)
    {
        if (SSstepCheckBox.Checked)
        {
            stepCheckBoxList.Items[i].Selected = true;
        }
        else
        {
            stepCheckBoxList.Items[i].Selected = false
                ;
        }
    }
}

protected void SSDistrictCk_OnCheckedChanged(object sender, EventArgs e)
{
    for (int i = 0; i < permDistCheckBoxList.Items.Count; i++)
    {
        if (SSDistrictCk.Checked)
        {
            permDistCheckBoxList.Items[i].Selected = true;
        }
        else
        {
            permDistCheckBoxList.Items[i].Selected = false
                ;
        }
    }
    permDistCheckBoxList_OnSelectedIndexChanged(null, null);
}

protected void SSThanaCK_OnCheckedChanged(object sender, EventArgs e)
{
    for (int i = 0; i < permThanaCheckBoxList1.Items.Count; i++)
    {
        if (SSThanaCK.Checked)
        {
            permThanaCheckBoxList1.Items[i].Selected = true;
        }
        else
        {
            permThanaCheckBoxList1.Items[i].Selected = false
                ;
        }
    }
}

protected void SSOfficeCK_OnCheckedChanged(object sender, EventArgs e)
{
    for (int i = 0; i < salLocCheckBoxList.Items.Count; i++)
    {
        if (SSOfficeCK.Checked)
        {
            salLocCheckBoxList.Items[i].Selected = true;
            chkPlace_OnSelectedIndexChanged(null, null);
        }
        else
        {
            salLocCheckBoxList.Items[i].Selected = false
                ;
        }
    }
}

protected void SSDesignationCK_OnCheckedChanged(object sender, EventArgs e)
{
    for (int i = 0; i < desigCheckBoxList.Items.Count; i++)
    {
        if (SSDesignationCK.Checked)
        {
            desigCheckBoxList.Items[i].Selected = true;
        }
        else
        {
            desigCheckBoxList.Items[i].Selected = false
                ;
        }
    }
}

    #endregion

#region Suspend/Diciplinary
EmployeeContractualReportDAL aEmployeeJobLeftEntryDAL = new EmployeeContractualReportDAL();
    protected void reportDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (reportDropDownList.SelectedValue == "Diciplinary")
        {
            if (ddlCompany.SelectedValue != "")
            {
                //Session["CompanyId"] = "";
                //Session["CompanyId"] = companyDropDownList.SelectedValue;

                aEmployeeJobLeftEntryDAL.LoadActionType(actionTypeDropDownList, ddlCompany.SelectedValue);

            }
            else
            {
                actionTypeDropDownList.Items.Clear();
            }
        }

        else if (reportDropDownList.SelectedValue == "Suspend")
        {
            if (ddlCompany.SelectedValue != "")
            {
                //Session["CompanyId"] = "";
                //Session["CompanyId"] = companyDropDownList.SelectedValue;

                aEmployeeJobLeftEntryDAL.LoadActionTypeForSuspend(actionTypeDropDownList, ddlCompany.SelectedValue);

            }
            else
            {
                actionTypeDropDownList.Items.Clear();
            }
        }

        else if (reportDropDownList.SelectedValue == "0")
        {
            actionTypeDropDownList.Items.Clear();
        }
    }


    

    protected void SearchButtonDisciplinaryAction_OnClick(object sender, EventArgs e)
    {
        if (DisplinaryValidation())
        {
            if (reportDropDownList.SelectedValue == "Suspend")
            {
                LoadInfoSuspend();
                DisplinaryGridView.DataSource = null;
                DisplinaryGridView.DataBind();
            }
            else if (reportDropDownList.SelectedValue == "Diciplinary")
            {
                LoadInfoDisplinary();
                SusPendGridView.DataSource = null;
                SusPendGridView.DataBind();
            }
            else
            {
                SusPendGridView.DataSource = null;
                SusPendGridView.DataBind();
                DisplinaryGridView.DataSource = null;
                DisplinaryGridView.DataBind();
            }
        }
         
    }

    private void LoadInfoSuspend()
    {
        LoadInitialGridForEmpDetail(SusPendGridView);
        DataTable dataTable = aEmployeeJobLeftEntryDAL.LoadInformationALlSuspend(GenerateParamiterListSuspend());

        if (dataTable.Rows.Count > 0)
        {
            SusPendGridView.DataSource = dataTable;
            SusPendGridView.DataBind();
        }
        else
        {
            SusPendGridView.DataSource = null;
            SusPendGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }

    private string GenerateParamiterListSuspend()
    {

        string parameter = "    ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND SPND.CompanyInfoId = " + ddlCompany.SelectedValue;
        }





        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND SPND.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND SPND.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND SPND.Effectivedate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        if (actionTypeDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND SPND.ReasonId = " + actionTypeDropDownList.SelectedValue;
        }




        if (repEmpIdHiddenField.Value != "")
        {
            parameter = parameter + " AND SPND.EmpInfoId = " + repEmpIdHiddenField.Value;
        }

        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }



        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }


        return parameter;
    }

    public bool DisplinaryValidation()
    {
        if (reportDropDownList.SelectedValue == "0")
        {
            aShowMessage.ShowMessageBox("Please Select this!!!", this);
            reportDropDownList.Focus();
            return false;
        }
        if (ddlCompany.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select a company!!!", this);
            ddlCompany.Focus();
            return false;
        }


        return true;
    }


    private void LoadInfoDisplinary()
    {
        LoadInitialGridForEmpDetail(DisplinaryGridView);

        DataTable dataTable = aEmployeeJobLeftEntryDAL.LoadInformationALlDisiplinary(GenerateParamiterListDisplinary());

        if (dataTable.Rows.Count > 0)
        {
            DisplinaryGridView.DataSource = dataTable;
            DisplinaryGridView.DataBind();
        }
        else
        {
            DisplinaryGridView.DataSource = null;
            DisplinaryGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }


    private string GenerateParamiterListDisplinary()
    {

        string parameter = "    ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND DCPA.CompanyInfoId = " + ddlCompany.SelectedValue;
        }



        if (FinancialYearDropDownList.SelectedValue != "")
        {
            parameter = parameter + "  AND DCPA.FinancialYearId = " + FinancialYearDropDownList.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND DCPA.EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND DCPA.EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND DCPA.EffectiveDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        if (actionTypeDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND DCPA.ReasonId = " + actionTypeDropDownList.SelectedValue;
        }




        if (repEmpIdHiddenField.Value != "")
        {
            parameter = parameter + " AND  DCPA.EmpInfoId = " + repEmpIdHiddenField.Value;
        }


        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }


        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }
        return parameter;
    }


    protected void ResetSearchButtonDisciplinaryAction_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");
    }







#endregion




    #region Promotion

    EmployeePromotionReportDAL PromotionDAL = new EmployeePromotionReportDAL();
    protected void lblPromotionSearch_OnClick(object sender, EventArgs e)
    {

        if (PromotionReportType.SelectedValue != "0")
        {
            if (PromotionReportType.SelectedValue == "1")
            {
                LoadInfoPromotion();
                NoPromoGridView.DataSource = null;
                NoPromoGridView.DataBind();
            }

            else if (PromotionReportType.SelectedValue == "2")
            {
                LoadInfoPromotionNoooooo();
                PromotionloadGridView.DataSource = null;
                PromotionloadGridView.DataBind();
            }
            else
            {
                PromotionloadGridView.DataSource = null;
                PromotionloadGridView.DataBind();
                NoPromoGridView.DataSource = null;
                NoPromoGridView.DataBind();
            }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select Report type", this);
        }

       
    }


    private void LoadInfoPromotion()
    {
        LoadInitialGridForEmpDetail(PromotionloadGridView);
        DataTable dataTable = PromotionDAL.LoadInformationALl22(PromotionGenerateParamiterList(), PromotionGenerateParamiterList2(), PromotionGenerateParamiterList_sp());

        if (dataTable.Rows.Count > 0)
        {
            PromotionloadGridView.DataSource = dataTable;
            PromotionloadGridView.DataBind();
        }
        else
        {
            PromotionloadGridView.DataSource = null;
            PromotionloadGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }


    private void LoadInfoPromotionNoooooo()
    {
        LoadInitialGridForEmpDetail(NoPromoGridView);
        DataTable dataTable = PromotionDAL.NOLoadInformationALl(NoPromotionGenerateParamiterList(), NoPromotionGenerateParamiterList2(), NoPromotiontblZHistory(), NoPromotiontblZ());

        if (dataTable.Rows.Count > 0)
        {
            NoPromoGridView.DataSource = dataTable;
            NoPromoGridView.DataBind();
        }
        else
        {
            NoPromoGridView.DataSource = null;
            NoPromoGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }
      



  private string NoPromotionGenerateParamiterList()
    {

        string parameter = "    ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EG.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (repEmpIdHiddenField.Value != "")
        {
            parameter = parameter + " AND EG.EmpInfoId = " + repEmpIdHiddenField.Value;
        }

     


        if (NoPromoEffectiveDropDownList.SelectedValue != "1")
        {
            if (NoPromoEffectiveDropDownList.SelectedValue == "2" || NoPromoEffectiveDropDownList.SelectedValue == "3" || NoPromoEffectiveDropDownList.SelectedValue == "4")
            {
                parameter = parameter + @"   AND  EG.EmpInfoId  not IN (SELECT EmployeeId FROM tblEmployeePromotionEntry EPE
								WHERE  ( (EPE.IsDelete IS NULL) OR (EPE.IsDelete = 0) ) AND EPE.NPromoTypeId=1 AND  EPE.Effectivedate  " + NoPromoEffectiveDropDownList.SelectedItem.Text + " '" +
                        NoPromotxtEffectiveSingle.Text + "' )";
            }
            else
            {
                parameter = parameter + @"  AND (EG.EmpInfoId  not IN (SELECT EmployeeId FROM tblEmployeePromotionEntry EPE
								WHERE  ( (EPE.IsDelete IS NULL) OR (EPE.IsDelete = 0) ) AND EPE.NPromoTypeId=1 AND  EPE.Effectivedate BETWEEN  '" + NoPromotxtEffectiveFromDate.Text + "' AND '" + NoPromotxtEffectiveToDate.Text + "')) ";
            }
        }
        else
        { 
            parameter = parameter +  @"   AND  EG.EmpInfoId  not IN ( SELECT EmployeeId FROM tblEmployeePromotionEntry EPE
								WHERE  ( (EPE.IsDelete IS NULL) OR (EPE.IsDelete = 0) ) ) AND EPE.NPromoTypeId=1 ";

        }

       
        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }

        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }


        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }



        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }




        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }


        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }



        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }

      
       
        return parameter;
    }

  private string NoPromotiontblZHistory()
  {
      string parameter = "    ";

      if (NoPromoEffectiveDropDownList.SelectedValue != "1")
      {
          if (NoPromoEffectiveDropDownList.SelectedValue == "2" || NoPromoEffectiveDropDownList.SelectedValue == "3" || NoPromoEffectiveDropDownList.SelectedValue == "4")
          {
              parameter = parameter + @"     and      EPE.EffectDate  " + NoPromoEffectiveDropDownList.SelectedItem.Text + " '" +
                      NoPromotxtEffectiveSingle.Text + "' ";
          }
          else
          {
              parameter = parameter + @"       and   EPE.EffectDate BETWEEN '" + NoPromotxtEffectiveFromDate.Text + "' AND '" + NoPromotxtEffectiveToDate.Text + "' ";
          }
      }
      return parameter;

  }


  private string NoPromotiontblZ()
  {
      string parameter = "    ";

      if (NoPromoEffectiveDropDownList.SelectedValue != "1")
      {
          if (NoPromoEffectiveDropDownList.SelectedValue == "2" || NoPromoEffectiveDropDownList.SelectedValue == "3" || NoPromoEffectiveDropDownList.SelectedValue == "4")
          {
              parameter = parameter + @"     AND  EPE.Effectivedate  " + NoPromoEffectiveDropDownList.SelectedItem.Text + " '" +
                      NoPromotxtEffectiveSingle.Text + "'  ";
          }
          else
          {
              parameter = parameter + @"  AND  EPE.Effectivedate BETWEEN  '" + NoPromotxtEffectiveFromDate.Text + "' AND '" + NoPromotxtEffectiveToDate.Text + "'  ";
          }
      }
      return parameter;

  }
    
    private string NoPromotionGenerateParamiterList2()
  {

      string parameter = "    ";

      if (ddlCompany.SelectedValue != "")
      {
          parameter = parameter + " AND EG.CompanyId = " + ddlCompany.SelectedValue;
      }

      if (repEmpIdHiddenField.Value != "")
      {
          parameter = parameter + " AND EG.EmpInfoId = " + repEmpIdHiddenField.Value;
      }




      if (NoPromoEffectiveDropDownList.SelectedValue != "1")
      {
          if (NoPromoEffectiveDropDownList.SelectedValue == "2" || NoPromoEffectiveDropDownList.SelectedValue == "3" || NoPromoEffectiveDropDownList.SelectedValue == "4")
          {
              parameter = parameter + @"  AND  EG.EmpInfoId  not IN (SELECT EmployeeId FROM tblPromotionUpgrationHistory EPE
								WHERE   EPE.TypeOfPromotion IN ('Promotion')            and      EPE.EffectDate  " + NoPromoEffectiveDropDownList.SelectedItem.Text + " '" +
                      NoPromotxtEffectiveSingle.Text + "' )";
          }
          else
          {
              parameter = parameter + @"    AND (EG.EmpInfoId  not IN (SELECT EmployeeId FROM tblPromotionUpgrationHistory EPE
								WHERE EPE.TypeOfPromotion IN ('Promotion')        and   EPE.EffectDate BETWEEN '" + NoPromotxtEffectiveFromDate.Text + "' AND '" + NoPromotxtEffectiveToDate.Text + "')) ";
          }
      }
      else
      {
          parameter = parameter + @"  AND  EG.EmpInfoId  not IN (SELECT EmployeeId FROM tblPromotionUpgrationHistory EPE where   EPE.TypeOfPromotion IN ('Promotion')         
								 )";
      }





      if (HierchicalParameter() != string.Empty)
      {
          parameter = parameter + HierchicalParameter();
      }

      if (GradeParam() != string.Empty)
      {
          parameter = parameter + GradeParam();
      }
      if (StepParam() != string.Empty)
      {
          parameter = parameter + StepParam();
      }


      if (SalLocParam() != string.Empty)
      {
          parameter = parameter + SalLocParam();
      }


      if (PlaceParam() != string.Empty)
      {
          parameter = parameter + PlaceParam();
      }
      if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
      {

          parameter = parameter + "";
      }

      if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
      {
          parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
      }



      return parameter;
  }



    private string PromotionGenerateParamiterList()
    {

        string parameter = "    ";


        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.CompanyId = " + ddlCompany.SelectedValue;
        }

     

        if (repEmpIdHiddenField.Value != "")
        {
            parameter = parameter + " AND EPE.EmployeeId = " + repEmpIdHiddenField.Value;
        }

        if (PromotionFinancialYearDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.FinancialYearId = " + PromotionFinancialYearDropDownList.SelectedValue;
        }

        if (PromotionEffectiveDateTextBox.Text != string.Empty && PromotionEffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + PromotionEffectiveDateTextBox.Text + "' AND '" + PromotionEffectToDate.Text + "' ";
        }
        if (PromotionEffectiveDateTextBox.Text != string.Empty && PromotionEffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + PromotionEffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (PromotionEffectiveDateTextBox.Text == string.Empty && PromotionEffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + PromotionEffectToDate.Text + "' AND '" + PromotionEffectToDate.Text + "' ";
        }

        if (PromotionTypeDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND PT.PromotionTypeId = " + PromotionTypeDropDownList.SelectedValue;
        }

        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }


        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }

        return parameter;
    }



    private string PromotionGenerateParamiterList_sp()
    {

        string parameter = "    ";


       


        if (repEmpIdHiddenField.Value != "")
        {
            parameter = parameter + " AND EPE.EmployeeId = " + repEmpIdHiddenField.Value;
        }

        if (PromotionFinancialYearDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND fn.FinancialYearDesc = '" + PromotionFinancialYearDropDownList.SelectedItem.Text+"'";
        }

        if (PromotionEffectiveDateTextBox.Text != string.Empty && PromotionEffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + PromotionEffectiveDateTextBox.Text + "' AND '" + PromotionEffectToDate.Text + "' ";
        }
        if (PromotionEffectiveDateTextBox.Text != string.Empty && PromotionEffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + PromotionEffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (PromotionEffectiveDateTextBox.Text == string.Empty && PromotionEffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + PromotionEffectToDate.Text + "' AND '" + PromotionEffectToDate.Text + "' ";
        }

        if (PromotionTypeDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND PT.PromotionTypeId = " + PromotionTypeDropDownList.SelectedValue;
        }

        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }


        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }

        return parameter;
    }


    private string PromotionGenerateParamiterList2()
    {

        string parameter = "    ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EG.CompanyId= " + ddlCompany.SelectedValue;
        }


        if (repEmpIdHiddenField.Value != "")
        {
            parameter = parameter + " AND EPE.EmployeeId = " + repEmpIdHiddenField.Value;
        }

       

        if (PromotionEffectiveDateTextBox.Text != string.Empty && PromotionEffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.EffectDate BETWEEN '" + PromotionEffectiveDateTextBox.Text + "' AND '" + PromotionEffectToDate.Text + "' ";
        }
        if (PromotionEffectiveDateTextBox.Text != string.Empty && PromotionEffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.EffectDate BETWEEN '" + PromotionEffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (PromotionEffectiveDateTextBox.Text == string.Empty && PromotionEffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.EffectDate BETWEEN '" + PromotionEffectToDate.Text + "' AND '" + PromotionEffectToDate.Text + "' ";
        }

        if (PromotionTypeDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.TypeOfPromotion = '" + PromotionTypeDropDownList.SelectedItem.Text+"'";
        }

        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }


        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }

        return parameter;
    }

    protected void lblPromotionReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");
    }




    #endregion


    #region Separationman
    EmployeeSeparationReportDAL aSeparationDAL = new EmployeeSeparationReportDAL();
    protected void SeparationmanCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < SeparationmanagementCheckBoxList.Items.Count; i++)
        {
            if (SeparationmanCheckBox.Checked)
            {
                SeparationmanagementCheckBoxList.Items[i].Selected = true;
            }
            else
            {
                SeparationmanagementCheckBoxList.Items[i].Selected = false
                    ;
            }
        }
    }


    private string SeprationGenerateParamiterList()
    {

        string parameter = "   ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.CompanyId = " + ddlCompany.SelectedValue;
        }


        //if (SeparationFinancialYearDropDownList.SelectedValue != "")
        //{
        //    parameter = parameter + " AND EPE.SubmissionDate = " + SeparationFinancialYearDropDownList.SelectedValue;
        //}

        if (SeparationEffectiveDateTextBox.Text != string.Empty && SeparationEffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + SeparationEffectiveDateTextBox.Text + "' AND '" + SeparationEffectToDate.Text + "' ";
        }
        if (SeparationEffectiveDateTextBox.Text != string.Empty && SeparationEffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + SeparationEffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (SeparationEffectiveDateTextBox.Text == string.Empty && SeparationEffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + SeparationEffectToDate.Text + "' AND '" + SeparationEffectToDate.Text + "' ";
        }

        for (int i = 0; i < SeparationmanagementCheckBoxList.Items.Count; i++)
        {
            if (SeparationmanagementCheckBoxList.Items[i].Selected)
            {

                parameter = parameter + " and  EPE.JobLeftTypeId IN (" + JobLeftType() + ") ";
            }
        }




        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
       


        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }

        return parameter;
    }

    public string JobLeftType()
    {
        string companyid = "";
        for (int i = 0; i < SeparationmanagementCheckBoxList.Items.Count; i++)
        {
            if (SeparationmanagementCheckBoxList.Items[i].Selected)
            {
                companyid = companyid + "'" + SeparationmanagementCheckBoxList.Items[i].Value + "'" + ",";
            }
        }
        companyid = companyid.TrimEnd(',');
        return companyid;
    }
    private void LoadInfoSeparation()
    {
        LoadInitialGridForEmpDetail(SeparationloadGridView);
        DataTable dataTable = aSeparationDAL.LoadInformationALl(SeprationGenerateParamiterList());

        if (dataTable.Rows.Count > 0)
        {
            SeparationloadGridView.DataSource = dataTable;
            SeparationloadGridView.DataBind();
        }
        else
        {
            SeparationloadGridView.DataSource = null;
            SeparationloadGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }
    
    protected void eparationmanSearchButton_OnClick(object sender, EventArgs e)
    {
        LoadInfoSeparation();
    }

    protected void eparationmanlbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");
    }

    #endregion



    #region Increment
    protected void IncrementSearchButton_OnClick(object sender, EventArgs e)
    {
        LoadInfoIncrement();
    }

    protected void IncrementlbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");

    }

    IncrementReportDAL aIncrementDAL = new IncrementReportDAL();
    private void LoadInfoIncrement()
    {
        LoadInitialGridForEmpDetail(IncrementloadGridView);
        DataTable dataTable = aIncrementDAL.LoadInformationALlRpt(IncrementGenerateParamiterList(), IncrementGenerateParamiterList2());

        if (dataTable.Rows.Count > 0)
        {
            IncrementloadGridView.DataSource = dataTable;
            IncrementloadGridView.DataBind();
        }
        else
        {
            IncrementloadGridView.DataSource = null;
            IncrementloadGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }


    private string IncrementGenerateParamiterList()
    {

        string parameter = "    ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND INC.CompanyId = " + ddlCompany.SelectedValue;
        }



        if (IncrementFinancialYearDropDownList.SelectedValue != "")
        {
            parameter = parameter + "  AND INC.FinancialYearId = " + IncrementFinancialYearDropDownList.SelectedValue;
        }


        if (IncrementEffectiveDateTextBox.Text != string.Empty && IncrementEffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.EffectiveDate BETWEEN '" + IncrementEffectiveDateTextBox.Text + "' AND '" + IncrementEffectToDate.Text + "' ";
        }
        if (IncrementEffectiveDateTextBox.Text != string.Empty && IncrementEffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND INC.EffectiveDate BETWEEN '" + IncrementEffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (IncrementEffectiveDateTextBox.Text == string.Empty && IncrementEffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.EffectiveDate BETWEEN '" + IncrementEffectToDate.Text + "' AND '" + IncrementEffectToDate.Text + "' ";
        }

        if (IncrementddlIncrementType.SelectedValue != "")
        {
            parameter = parameter + " AND INC.IncrementTypeId = " + IncrementddlIncrementType.SelectedValue;
        }




        if (repEmpIdHiddenField.Value != "")
        {
            parameter = parameter + " AND INC.EmployeeId = " + repEmpIdHiddenField.Value;
        }

        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }



        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }

        return parameter;
    }


    private string IncrementGenerateParamiterList2()
    {

        string parameter = "    ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EG.CompanyId = " + ddlCompany.SelectedValue;
        }


 


        if (IncrementEffectiveDateTextBox.Text != string.Empty && IncrementEffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.EffectiveDate BETWEEN '" + IncrementEffectiveDateTextBox.Text + "' AND '" + IncrementEffectToDate.Text + "' ";
        }
        if (IncrementEffectiveDateTextBox.Text != string.Empty && IncrementEffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND INC.EffectiveDate BETWEEN '" + IncrementEffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (IncrementEffectiveDateTextBox.Text == string.Empty && IncrementEffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.EffectiveDate BETWEEN '" + IncrementEffectToDate.Text + "' AND '" + IncrementEffectToDate.Text + "' ";
        }

        if (IncrementddlIncrementType.SelectedValue != "")
        {
            parameter = parameter + " AND INC.Remarks = '" + IncrementddlIncrementType.SelectedItem.Text+"'";
        }




        if (repEmpIdHiddenField.Value != "")
        {
            parameter = parameter + " AND INC.EmployeeId = " + repEmpIdHiddenField.Value;
        }

        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }



        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }

        return parameter;
    }

    #endregion


    #region _BirthDay
    private BirthDayMailGenerateDAL _BirthDay = new BirthDayMailGenerateDAL();
    protected void EmpBirthDaySearchButton_OnClick(object sender, EventArgs e)
    {
        if (EmpBirthDayValidation())
        {
            EmpBirthDayLoadEMPInfo();
        }
    }

    private void EmpBirthDayLoadEMPInfo()
    {
        DataTable jobCreationInfos = new DataTable();

        jobCreationInfos = _BirthDay.GetEMpInfos(EmpBirthDayGenerateParameter());
        EmpBirthDayloadGridView.DataSource = jobCreationInfos;
        EmpBirthDayloadGridView.DataBind();
    }

    private string EmpBirthDayGenerateParameter()
    {
        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + "  and    com.CompanyId = '" + ddlCompany.SelectedValue + "'";
        }

        if (MonthDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and   FORMAT(EG.DateOfBirth, 'MMMM') = '" + MonthDropDownList.SelectedValue + "'";
        }

        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }




        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }


        return parameter;
    }


    private bool EmpBirthDayValidation()
    {


        if (ddlCompany.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select a Company!!", this);
            ddlCompany.Focus();
            return false;
        }

        if (MonthDropDownList.SelectedValue == "0")
        {
            aShowMessage.ShowMessageBox("Please Select a Month!!", this);
            MonthDropDownList.Focus();
            return false;
        }


        return true;
    }

 
    protected void EmpBirthDaybtnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");
    }
   #endregion



    #region Checkup

    protected void CheckupSearchButton_OnClick(object sender, EventArgs e)
    {
        if ((CheckupddlStatus.SelectedValue!="0")&&(CheckuptxtDate.Text != "") && (CheckuptxtToDate.Text != ""))
        {
            LoadCheckupInformation();
        }
        else
        {
            CheckupGridView1.DataSource = null;
            CheckupGridView1.DataBind();
          
            aShowMessage.ShowMessageBox("Please Select all the searching criteria !!!", this);
        }
    }

    protected void CheckupResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");
    }



    VivaSetupInfoEntryDAL aCheckupDaL = new VivaSetupInfoEntryDAL();
    private void LoadCheckupInformation()
    {
        LoadInitialGridForEmpDetail(CheckupGridView1);
        try
        {
            if (CheckupddlStatus.SelectedValue == "1")
            {

                DataTable dataTable = aCheckupDaL.NewGeMedicalCheckupDetailsInfoRPT(CheckupCheckupparam());

                if (dataTable.Rows.Count > 0)
                {
                    CheckupGridView1.DataSource = dataTable;
                    CheckupGridView1.DataBind();

                }
                else
                {
                    CheckupGridView1.DataSource = null;
                    CheckupGridView1.DataBind();
                    aShowMessage.ShowMessageBox("No Data Found !!!", this);
                }
            }

            else if (CheckupddlStatus.SelectedValue == "2")
            {

                DataTable dataTable = aCheckupDaL.NEWGeMedicalCheckupDetailsInfoNotRPT(CheckupCheckupparam());

                if (dataTable.Rows.Count > 0)
                {
                    CheckupGridView1.DataSource = dataTable;
                    CheckupGridView1.DataBind();

                }
                else
                {
                    CheckupGridView1.DataSource = null;
                    CheckupGridView1.DataBind();
                    aShowMessage.ShowMessageBox("No Data Found !!!", this);
                }
            }
            else
            {
                CheckupGridView1.DataSource = null;
                CheckupGridView1.DataBind();
                CheckupddlStatus.Focus();
                aShowMessage.ShowMessageBox("Please Select Status !!!", this);
            }

        }
        catch (Exception)
        {

            //throw;
        }
    }

    private string CheckupCheckupparam()
    {
        string Checkupparameter = " ";

        if (CheckupddlStatus.SelectedValue == "1")
        {
            if (ddlCompany.SelectedValue != "")
            {
                Checkupparameter = Checkupparameter + " AND   tblMedicalCheckUp.CompanyId  =  '" + ddlCompany.SelectedValue + "' ";
            }


            if (CheckuptxtDate.Text != string.Empty && CheckuptxtToDate.Text != string.Empty)
            {
                Checkupparameter = Checkupparameter + " AND MCM.Date BETWEEN '" + CheckuptxtDate.Text + "' AND '" + CheckuptxtToDate.Text + "' ";
            }
            if (CheckuptxtDate.Text != string.Empty && CheckuptxtToDate.Text == string.Empty)
            {
                Checkupparameter = Checkupparameter + " AND  MCM.Date BETWEEN '" + CheckuptxtDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
            }

            if (CheckuptxtDate.Text == string.Empty && CheckuptxtToDate.Text != string.Empty)
            {
                Checkupparameter = Checkupparameter + " AND  MCM.Date BETWEEN '" + CheckuptxtToDate.Text + "' AND '" + CheckuptxtToDate.Text + "' ";
            }



            if (repEmpIdHiddenField.Value != "")
            {
                Checkupparameter = Checkupparameter + " AND tblMedicalCheckUp.EmpInfoId='" + repEmpIdHiddenField.Value + "' ";
            }

            if (HierchicalParameter() != string.Empty)
            {
                Checkupparameter = Checkupparameter + HierchicalParameter();
            }



            if (GradeParam() != string.Empty)
            {
                Checkupparameter = Checkupparameter + GradeParam();
            }
            if (StepParam() != string.Empty)
            {
                Checkupparameter = Checkupparameter + StepParam();
            }


            if (SalLocParam() != string.Empty)
            {
                Checkupparameter = Checkupparameter + SalLocParam();
            }


            if (PlaceParam() != string.Empty)
            {
                Checkupparameter = Checkupparameter + PlaceParam();
            }
            if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
            {

                Checkupparameter = Checkupparameter + "";
            }

            if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
            {
                Checkupparameter = Checkupparameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
            }
        }


        if (CheckupddlStatus.SelectedValue == "2")
        {

            if (ddlCompany.SelectedValue != "")
            {
                Checkupparameter = Checkupparameter + " AND    EG.CompanyId  =  '" + ddlCompany.SelectedValue + "' ";
            }


            if (repEmpIdHiddenField.Value != "")
            {
                Checkupparameter = Checkupparameter + " AND EG.EmpInfoId='" + repEmpIdHiddenField.Value + "' ";
            }


            if (HierchicalParameter() != string.Empty)
            {
                Checkupparameter = Checkupparameter + HierchicalParameter();
            }



            if (GradeParam() != string.Empty)
            {
                Checkupparameter = Checkupparameter + GradeParam();
            }
            if (StepParam() != string.Empty)
            {
                Checkupparameter = Checkupparameter + StepParam();
            }


            if (SalLocParam() != string.Empty)
            {
                Checkupparameter = Checkupparameter + SalLocParam();
            }


            if (PlaceParam() != string.Empty)
            {
                Checkupparameter = Checkupparameter + PlaceParam();
            }
            if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
            {

                Checkupparameter = Checkupparameter + "";
            }

            if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
            {
                Checkupparameter = Checkupparameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
            }


        }




        return Checkupparameter;
    }

    #endregion

    #region Experience
    protected void ExperienceSearchButton_OnClick(object sender, EventArgs e)
    {
        LoadExperienceInformation();
    }

    private EmpExperienceDAL _EmpExperienceDAL = new EmpExperienceDAL();
    private void LoadExperienceInformation()
    {
        LoadInitialGridForEmpDetail(gv_Experience);
        try
        {


            DataTable dataTable = _EmpExperienceDAL.ReportAllGetDTEmpExperience(Experienceparam());

                if (dataTable.Rows.Count > 0)
                {
                    gv_Experience.DataSource = dataTable;
                    gv_Experience.DataBind();

                }
                else
                {
                    gv_Experience.DataSource = null;
                    gv_Experience.DataBind();
                    
                }
            
        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected  void ExperienceResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");
    }

    private string Experienceparam()
    {
        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND    EG.CompanyId  =  '" + ddlCompany.SelectedValue + "' ";
        }


        if (repEmpIdHiddenField.Value != "")
        {
            parameter = parameter + " AND EG.EmpInfoId='" + repEmpIdHiddenField.Value + "' ";
        }

        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }

        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }

        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }

        return parameter;
    }

    #endregion


    #region Contractual
    protected void ContractualSearchButton_OnClick(object sender, EventArgs e)
    {
        if (ContractualddlType.SelectedValue != "0")
        {
            LoadContractualInformation();
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select Type",this);
            ContractualddlType.Focus();
        }
    }





    private void LoadContractualInformation()
    {
        LoadInitialGrid(ContractualGridView);
        try
        {
           

            ContractualGridView.Columns[40].Visible = false;
            ContractualGridView.Columns[41].Visible = false;
            ContractualGridView.Columns[42].Visible = false;
            ContractualGridView.Columns[43].Visible = false;


            ContractualGridView.Columns[44].Visible = false;
            ContractualGridView.Columns[45].Visible = false;
            ContractualGridView.Columns[46].Visible = false;
               
                

            if (ContractualddlType.SelectedValue=="1")
            {
                ContractualGridView.Columns[40].Visible = true;
                ContractualGridView.Columns[41].Visible = true;
            }

            if (ContractualddlType.SelectedValue == "2")
            {
                ContractualGridView.Columns[42].Visible = true;
                ContractualGridView.Columns[43].Visible = true;
            }

            if (ContractualddlType.SelectedValue == "3")
            {
                ContractualGridView.Columns[44].Visible = true;
                ContractualGridView.Columns[45].Visible = true;
            }

            if (ContractualddlType.SelectedValue == "4")
            {
               
            }

            if (ContractualddlType.SelectedValue == "5")
            {
                ContractualGridView.Columns[46].Visible = true;


            }

            DataTable dataTable = aReportDal.LoadContractualEmpInfo(ContractualParameter(), ContractualParameter2());
          

            if (dataTable.Rows.Count > 0)
            {
                ContractualGridView.DataSource = dataTable;
                ContractualGridView.DataBind();

            }
            else
            {
                ContractualGridView.DataSource = null;
                ContractualGridView.DataBind();

            }

        }
        catch (Exception)
        {

            //throw;
        }
    }
    public string ContractualParameter()
    {
        string param = "";
        if (ddlCompany.SelectedValue != "")
        {
            param = param + " AND  Com.CompanyId='" + ddlCompany.SelectedValue + "' ";
        }


        if (ddlEmpInfo.SelectedValue != "")
        {
            param = param + " AND EPE.EmployeeId='" + ddlEmpInfo.SelectedValue + "' ";

        }

        if (ContractualstartDate.Text != string.Empty && ContractualendDate.Text != string.Empty)
        {
            param = param + " AND EPE.EffectiveDate BETWEEN '" + ContractualstartDate.Text + "' AND '" + ContractualendDate.Text + "' ";
        }
        if (ContractualstartDate.Text != string.Empty && ContractualendDate.Text == string.Empty)
        {
            param = param + " AND  EPE.EffectiveDate BETWEEN '" + ContractualstartDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (ContractualstartDate.Text == string.Empty && ContractualendDate.Text != string.Empty)
        {
            param = param + " AND  EPE.EffectiveDate BETWEEN '" + ContractualendDate.Text + "' AND '" + ContractualendDate.Text + "' ";
        }

       
        if (ContractualddlType.SelectedValue == "1")
        {
            param = param + " AND  EPE.IsExtension=1 ";
        }

        if (ContractualddlType.SelectedValue == "2")
        {
            param = param + " AND  EPE.IsRenew=1 ";
        }

        if (ContractualddlType.SelectedValue == "3")
        {
            param = param + " AND  epe.IsPermanentToContractual=1 ";
        }

        if (ContractualddlType.SelectedValue == "4")
        {
            param = param + " AND   epe.IsContractualToPermanent=1 ";
        }
        if (ContractualddlType.SelectedValue == "5")
        {
            param = param + " AND   epe.isToProject=1 ";
        }

        if (HierchicalParameter() != string.Empty)
        {
            param = param + HierchicalParameter();
        }



        if (GradeParam() != string.Empty)
        {
            param = param + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            param = param + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            param = param + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            param = param + PlaceParam();
        }
       
        return param;
    }


    public string ContractualParameter2()
    {
        string param = "";
        if (ddlCompany.SelectedValue != "")
        {
            param = param + " AND  EG.CompanyId='" + ddlCompany.SelectedValue + "' ";
        }


        if (ddlEmpInfo.SelectedValue != "")
        {
            param = param + " AND EPE.EmployeeId='" + ddlEmpInfo.SelectedValue + "' ";

        }

        if (ContractualddlType.SelectedValue != "0")
        {
            param = param + " AND  EPE.TypeOfStateChange= '" + ContractualddlType .SelectedItem.Text+ "'";
        }

        

        if (ContractualstartDate.Text != string.Empty && ContractualendDate.Text != string.Empty)
        {
            param = param + " AND EPE.EffectiveDate BETWEEN '" + ContractualstartDate.Text + "' AND '" + ContractualendDate.Text + "' ";
        }
        if (ContractualstartDate.Text != string.Empty && ContractualendDate.Text == string.Empty)
        {
            param = param + " AND  EPE.EffectiveDate BETWEEN '" + ContractualstartDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (ContractualstartDate.Text == string.Empty && ContractualendDate.Text != string.Empty)
        {
            param = param + " AND  EPE.EffectiveDate BETWEEN '" + ContractualendDate.Text + "' AND '" + ContractualendDate.Text + "' ";
        }


        

        if (HierchicalParameter() != string.Empty)
        {
            param = param + HierchicalParameter();
        }



        if (GradeParam() != string.Empty)
        {
            param = param + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            param = param + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            param = param + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            param = param + PlaceParam();
        }

        return param;
    }
    protected void ContractualResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");
    }

    #endregion

    protected void SalaryRangeDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        

        SalDtRange.Visible = false;
        SalDtSingle.Visible = false;

        if (SalaryRangeDropDownList.SelectedValue == "5")
        {
            SalDtRange.Visible = true;
        }
        else
        {
            if (SalaryRangeDropDownList.SelectedValue != "1")
            {
                SalDtSingle.Visible = true;
            }

        }
    }

    protected void asadassdButton50_OnClick(object sender, EventArgs e)
    {
        SalaryRangeDropDownList.SelectedValue = 1.ToString();
        txtSalaryRange.Text = "";
        txtSalaryRangeFrom.Text = "";
        txtSalaryRangeTo.Text = "";
        SalaryRangeDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void ProjectSearchButton_OnClick(object sender, EventArgs e)
    {

        if (ProjectDropDownList.SelectedValue!="")
        {
            LoadProjectInformation();
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select Project!!",this);
        }
       

    }

    protected void ProjectResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");
    }


    private void LoadProjectInformation()
    {
        LoadInitialGrid(ProjectGridView);
        try
        {


            DataTable dataTable = _EmpExperienceDAL.ReportAllGetDTEmpProject(" and dtl.ProjectId=" + ProjectDropDownList.SelectedValue+" "+ parmProject());

            if (dataTable.Rows.Count > 0)
            {
                ProjectGridView.DataSource = dataTable;
                ProjectGridView.DataBind();

            }
            else
            {
                ProjectGridView.DataSource = null;
                ProjectGridView.DataBind();

            }

        }
        catch (Exception)
        {

            //throw;
        }
    }

    private string parmProject()
    {
        string param = " ";


        if (ddlCompany.SelectedValue != "")
        {
            param = param + " AND  EG.CompanyId='" + ddlCompany.SelectedValue + "' ";
        }
        if (ddlEmpInfo.SelectedValue != "")
        {
            param = param + " AND mas.EmpInfoId = " + ddlEmpInfo.SelectedValue;
        }
        if (HierchicalParameter() != string.Empty)
        {
            param = param + HierchicalParameter();
        }



        if (GradeParam() != string.Empty)
        {
            param = param + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            param = param + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            param = param + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            param = param + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            param = param + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            param = param + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }

        return param;
    }

    protected void KPISearchButton_OnClick(object sender, EventArgs e)
    {
        if ((KPIddlFinancialYear.SelectedValue != "") && (KPIStatusDropDownList.SelectedValue != "0"))
        {
            LoadKPIInformation();
        }
        else
        {
            gv_kpiSetup.DataSource = null;
            gv_kpiSetup.DataBind();

            aShowMessage.ShowMessageBox("Please Select all the searching criteria !!!", this);
        }
       
    }


    KPISETUPListDAL aKPInDal = new KPISETUPListDAL();
    private void LoadKPIInformation()
    {
        gv_kpiSetup.DataSource = null;
        gv_kpiSetup.DataBind();
        //LoadInitialGrid(gv_kpiSetup);
        try
        {
            if (KPIStatusDropDownList.SelectedValue == "1")
            {

                DataTable dt = aKPInDal.rptGetKpiSetupListINNNN(KPIGenerateParameter(), KPIGenerateParameter_SP(), KPIddlFinancialYear.SelectedItem.Text);
                if (dt.Rows.Count > 0)
                {
                    gv_kpiSetup.DataSource = dt;
                    gv_kpiSetup.DataBind();
                }
                else
                {
                    gv_kpiSetup.DataSource = null;
                    gv_kpiSetup.DataBind();

                }
            }

            else if (KPIStatusDropDownList.SelectedValue == "2")
            {
                DataTable dt = aKPInDal.rptGetKpiSetupListNOTINNN(KPIGenerateParameter(), KPIGenerateParameter_SP(), KPIddlFinancialYear.SelectedItem.Text);
                if (dt.Rows.Count > 0)
                {
                    gv_kpiSetup.DataSource = dt;
                    gv_kpiSetup.DataBind();
                }
                else
                {
                    gv_kpiSetup.DataSource = null;
                    gv_kpiSetup.DataBind();

                }
            }

        }
        catch (Exception)
        {

            //throw;
        }
    }

    private string KPIGenerateParameter()
    {
        string parameter = " ";


        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND   com.CompanyId  =  '" + ddlCompany.SelectedValue + "' ";
        }


        if (KPIddlFinancialYear.SelectedValue != "")
        {
            parameter = parameter + " AND  d.FinancialYearDesc =  '" + KPIddlFinancialYear.SelectedItem.Text + "'  ";
        }

        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }


        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }

        return parameter;
    }

    private string KPIGenerateParameter_SP()
    {
        string parameter = " ";


       

        if (KPIddlFinancialYear.SelectedValue != "")
        {
            parameter = parameter + " AND  d.FinancialYearDesc =  '" + KPIddlFinancialYear.SelectedItem.Text + "'  ";
        }

        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }


        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }

        return parameter;
    }

    protected void KPIResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");
    }

    protected void KPIddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void chkPlace_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (SalLocParam() != string.Empty)
        {
            DataTable dtstep = aReportDal.GetPlace(SalLocParam());
            chkPlace.DataValueField = "JobLocationID";
            chkPlace.DataTextField = "Location";
            chkPlace.DataSource = dtstep;
            chkPlace.DataBind();
        }
    }

    public string PlaceParam()
    {
        string param = "";
        string place = "";

        for (int i = 0; i < chkPlace.Items.Count; i++)
        {
            if (chkPlace.Items[i].Selected)
            {
                place = chkPlace.Items[i].Value + "," + place;
            }
        }
        if (place != string.Empty)
        {
            param = param + " AND EG.JobLocationId " + PlaceDropDownList.SelectedItem.Text + " (" + place.TrimEnd(',') +
                    ")";
        }
        else
        {
            param = "";
        }
        return param;

    }

    protected void SSchkPlace_OnCheckedChanged_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkPlace.Items.Count; i++)
        {
            if (SSchkPlace.Checked)
            {
                chkPlace.Items[i].Selected = true;
            }
            else
            {
                chkPlace.Items[i].Selected = false
                    ;
            }
        }
    }

    protected void PromotionReportType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        PromotionloadGridView.DataSource = null;
        PromotionloadGridView.DataBind();
        NoPromoGridView.DataSource = null;
        NoPromoGridView.DataBind();
        if (PromotionReportType.SelectedValue!="0")
        {
            if (PromotionReportType.SelectedValue=="1")
            {
                divPromotion.Visible = true;
                divNoPromotion.Visible = false;
            }

            else if (PromotionReportType.SelectedValue == "2")
            {
                divPromotion.Visible = false;
                divNoPromotion.Visible = true;
            }
            else
            {
                divPromotion.Visible = false;
                divNoPromotion.Visible = false;
            }
        }
    }

    protected void NoPromoEffectiveDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        NoPromoRange.Visible = false;
        noPromoSingle.Visible = false;

        if (NoPromoEffectiveDropDownList.SelectedValue == "5")
        {
            NoPromoRange.Visible = true;
        }
        else
        {
            if (NoPromoEffectiveDropDownList.SelectedValue != "1")
            {
                noPromoSingle.Visible = true;
            }

        }
    }

    protected void Button5sssss_OnClick(object sender, EventArgs e)
    {
        NoPromoEffectiveDropDownList.SelectedValue = 1.ToString();
        NoPromotxtEffectiveSingle.Text = "";
        NoPromotxtEffectiveFromDate.Text = "";
        NoPromotxtEffectiveToDate.Text = "";
        NoPromoEffectiveDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void FamilySearchButton_OnClick(object sender, EventArgs e)
    {
        if (repEmpIdHiddenField.Value != "")
        {
            LoadEmployeeFamily();
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select a Employee", this);
            SearchEmployeeNameTextBoxTextBox.Focus();

        }
    }

    protected void FamilyResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");

        
    }

    private void LoadEmployeeFamily()
    {



        DataTable FamilyInformationdataTable = aReportDal.GetEmployeeFamilyInfoDAL(Convert.ToInt32(repEmpIdHiddenField.Value).ToString());

        const int FamilyInformationrowIndex = 0;

        if (FamilyInformationdataTable.Rows.Count > 0)
        {


            using (DataTable dtSpouse = aCommonDataLoadDal.GetDTEmpSpouseByEmpId(Convert.ToInt32(repEmpIdHiddenField.Value)))
            {
                gv_Spouse.DataSource = dtSpouse;
                gv_Spouse.DataBind();
            }

            if ((FamilyInformationdataTable.Rows[FamilyInformationrowIndex]["DateOfMarriage"] != DBNull.Value))
            {
                lblMarriageDate.Text = FamilyInformationdataTable.Rows[FamilyInformationrowIndex].Field<DateTime>("DateOfMarriage").ToString("dd-MMM-yyyy");
            }


        }

        using (DataTable dtChildren = aReportDal.GetDTEmpChildrenByEmpId(Convert.ToInt32(repEmpIdHiddenField.Value)))
        {
            if (dtChildren.Rows.Count > 0)
            {
                
                gv_Children.DataSource = dtChildren;
                gv_Children.DataBind();
            }
            else
            {
                gv_Children.DataSource = null;
                gv_Children.DataBind();
            }

        }
    }

    protected void appraisalSearchButton_OnClick(object sender, EventArgs e)
    {
        if ((appraisalFinYearDropDownList.SelectedValue != "") && (appraisalStatusDropDownList.SelectedValue != ""))
        {
            LoadappraisalInformation();
        }
        else
        {
            gvApprisalFinal.DataSource = null;
            gvApprisalFinal.DataBind();

            aShowMessage.ShowMessageBox("Please Select all the searching criteria !!!", this);
        }
    }

    private string appraisalGenerateParameter()
    {
        string parameter = " ";


        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND   com.CompanyId  =  '" + ddlCompany.SelectedValue + "' ";
        }


        if (appraisalFinYearDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND  d.FinancialYearDesc =  '" + appraisalFinYearDropDownList.SelectedItem.Text + "'  ";
        }

        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + " AND  EG.EmpInfoId =  '" + ddlEmpInfo.SelectedValue + "'  ";
        }

 

        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }



        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        

        return parameter;
    }


    private string appraisalGenerateParameter_SP()
    {
        string parameter = " ";


       


        if (appraisalFinYearDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND  d.FinancialYearDesc =  '" + appraisalFinYearDropDownList.SelectedItem.Text + "'  ";
        }

        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + " AND  EG.EmpInfoId =  '" + ddlEmpInfo.SelectedValue + "'  ";
        }



        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }



        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }



        return parameter;
    }

    private void LoadappraisalInformation()
    {
        gvApprisalFinal.DataSource = null;
        gvApprisalFinal.DataBind();

        
        try
        {
            if (appraisalStatusDropDownList.SelectedValue == "1")
            {
                LoadInitialGridApprisal(gvApprisalFinal);

               

                gvApprisalFinal.Columns[68].Visible = false;
                gvApprisalFinal.Columns[69].Visible = false;
                gvApprisalFinal.Columns[70].Visible = false;
                gvApprisalFinal.Columns[71].Visible = false;
                gvApprisalFinal.Columns[72].Visible = false;
                gvApprisalFinal.Columns[73].Visible = false;
                gvApprisalFinal.Columns[74].Visible = false;
                gvApprisalFinal.Columns[75].Visible = false;
                

                string step = "";

                for (int i = 0; i < chkfin.Items.Count; i++)
                {
                    if (chkfin.Items[i].Selected)
                    {
                        step = chkfin.Items[i].Text + "," + step;
                    }
                }

                string lastStep = step.TrimEnd(',');



                if (lastStep.Contains(','))
                {
                    string[] emp = lastStep.Split(',');

                    //SearchEmployeeNameTextBoxTextBox.Text = emp[2];
                    //repEmpIdHiddenField.Value = emp[0];

                    for (int i = 0; i < emp.Length; i++)
                    {


                        if (emp[i] == "2014-2015")
                        {
                            gvApprisalFinal.Columns[68].Visible = true;

                        }




                        if (emp[i] == "2015-2016")
                        {
                            gvApprisalFinal.Columns[69].Visible = true;

                        }





                        if (emp[i] == "2016-2017")
                        {
                            gvApprisalFinal.Columns[70].Visible = true;

                        }






                        if (emp[i] == "2017-2018")
                        {
                            gvApprisalFinal.Columns[71].Visible = true;

                        }

                        if (emp[i] == "2018-2019")
                        {
                            gvApprisalFinal.Columns[72].Visible = true;

                        }


                        if (emp[i] == "2019-2020")
                        {
                            gvApprisalFinal.Columns[73].Visible = true;

                        }
                        if (emp[i] == "2020-2021")
                        {
                            gvApprisalFinal.Columns[74].Visible = true;

                        }
                        if (emp[i] == "2021-2022")
                        {
                            gvApprisalFinal.Columns[75].Visible = true;

                        }

                    }

                    //productNameTextBox.Text = productInfo[1];
                    //string productCode = productCodeTextBox.Text.Trim();

                }





                DataTable dt = aReportDal.rptGetApprisalSetupListINNNNNew(
     appraisalGenerateParameter(),
     appraisalGenerateParameter_SP(),
     appraisalFinYearDropDownList.SelectedItem.Text
 );

                if (dt.Rows.Count > 0)
                {
                    // Filter rows where TotalMarks > 0
                    DataRow[] filteredRows = dt.Select("TotalMarks > 0");

                    if (filteredRows.Length > 0)
                    {
                        gvApprisalFinal.DataSource = filteredRows.CopyToDataTable();
                        gvApprisalFinal.DataBind();
                    }
                    else
                    {
                        gvApprisalFinal.DataSource = null;
                        gvApprisalFinal.DataBind();
                    }
                }
                else
                {
                    gvApprisalFinal.DataSource = null;
                    gvApprisalFinal.DataBind();
                }

            }

            else if (appraisalStatusDropDownList.SelectedValue == "2")
            {
                LoadInitialGridApprisal(gvApprisalFinal);



                gvApprisalFinal.Columns[68].Visible = false;
                gvApprisalFinal.Columns[69].Visible = false;
                gvApprisalFinal.Columns[70].Visible = false;
                gvApprisalFinal.Columns[71].Visible = false;
                gvApprisalFinal.Columns[72].Visible = false;
                gvApprisalFinal.Columns[73].Visible = false;
                gvApprisalFinal.Columns[74].Visible = false;
                gvApprisalFinal.Columns[75].Visible = false;


                string step = "";

                for (int i = 0; i < chkfin.Items.Count; i++)
                {
                    if (chkfin.Items[i].Selected)
                    {
                        step = chkfin.Items[i].Text + "," + step;
                    }
                }

                string lastStep = step.TrimEnd(',');



                if (lastStep.Contains(','))
                {
                    string[] emp = lastStep.Split(',');

                    //SearchEmployeeNameTextBoxTextBox.Text = emp[2];
                    //repEmpIdHiddenField.Value = emp[0];

                    for (int i = 0; i < emp.Length; i++)
                    {


                        if (emp[i] == "2014-2015")
                        {
                            gvApprisalFinal.Columns[68].Visible = true;

                        }




                        if (emp[i] == "2015-2016")
                        {
                            gvApprisalFinal.Columns[69].Visible = true;

                        }





                        if (emp[i] == "2016-2017")
                        {
                            gvApprisalFinal.Columns[70].Visible = true;

                        }






                        if (emp[i] == "2017-2018")
                        {
                            gvApprisalFinal.Columns[71].Visible = true;

                        }

                        if (emp[i] == "2018-2019")
                        {
                            gvApprisalFinal.Columns[72].Visible = true;

                        }


                        if (emp[i] == "2019-2020")
                        {
                            gvApprisalFinal.Columns[73].Visible = true;

                        }
                        if (emp[i] == "2020-2021")
                        {
                            gvApprisalFinal.Columns[74].Visible = true;

                        }
                        if (emp[i] == "2021-2022")
                        {
                            gvApprisalFinal.Columns[75].Visible = true;

                        }

                    }

                    //productNameTextBox.Text = productInfo[1];
                    //string productCode = productCodeTextBox.Text.Trim();

                }


                DataTable dt = aReportDal.rptGetApprisalSetupListNotComplete(appraisalGenerateParameter(), appraisalGenerateParameter_SP(), appraisalFinYearDropDownList.SelectedItem.Text);
                if (dt.Rows.Count > 0)
                {
                    gvApprisalFinal.DataSource = dt;
                    gvApprisalFinal.DataBind();
                }
                else
                {
                    gvApprisalFinal.DataSource = null;
                    gvApprisalFinal.DataBind();

                }
            }

        }
        catch (Exception)
        {

            //throw;
        }
    }



    protected void appraisalResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");
    }

    protected void surveySearchButton_OnClick(object sender, EventArgs e)
    {
        if ((surveyFinYearDropDownList.SelectedValue != "") && (surveyStatusDropDownList.SelectedValue != "0") && (SurveyNameDropDownList.SelectedValue != ""))
        {
            LoadsurveyInformation();
        }
        else
        {
            gv_serveyGridView.DataSource = null;
            gv_serveyGridView.DataBind();

            aShowMessage.ShowMessageBox("Please Select all the searching criteria !!!", this);
        }
    }


   

    private void LoadsurveyInformation()
    {
        LoadInitialGrid(gv_serveyGridView);
        try
        {
            if (surveyStatusDropDownList.SelectedValue == "1")
            {

                DataTable dt = aReportDal.rptGetSurveySetupListINNNN(serveyGenerateParameter());
                if (dt.Rows.Count > 0)
                {
                    gv_serveyGridView.DataSource = dt;
                    gv_serveyGridView.DataBind();
                }
                else
                {
                    gv_serveyGridView.DataSource = null;
                    gv_serveyGridView.DataBind();

                }
            }

            else if (surveyStatusDropDownList.SelectedValue == "2")
            {
                DataTable dt = aReportDal.rptGetSurveySetupListNOTINNN(serveyGenerateParameter());
                if (dt.Rows.Count > 0)
                {
                    gv_serveyGridView.DataSource = dt;
                    gv_serveyGridView.DataBind();
                }
                else
                {
                    gv_serveyGridView.DataSource = null;
                    gv_serveyGridView.DataBind();

                }
            }

        }
        catch (Exception)
        {

            //throw;
        }
    }

    private string serveyGenerateParameter()
    {
        string parameter = " ";


        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND   com.CompanyId  =  '" + ddlCompany.SelectedValue + "' ";
        }


        if (surveyFinYearDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND  d.FinancialYearId =  '" + surveyFinYearDropDownList.SelectedValue + "'  ";
        }


        if (SurveyNameDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND  A.SurveyName =  '" + SurveyNameDropDownList.SelectedItem.Text + "'  ";
        }

        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }


        if (GradeParam() != string.Empty)
        {
            parameter = parameter + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            parameter = parameter + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            parameter = parameter + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            parameter = parameter + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            parameter = parameter + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }

        return parameter;
    }

    protected void surveyResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInformationReport.aspx");
    }

    protected void TrainDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Trainsingle.Visible = false;
        Trainrange.Visible = false;
        if (TrainDropDownList.SelectedValue == "5")
        {
            Trainrange.Visible = true;
        }
        else
        {

            if (TrainDropDownList.SelectedValue != "1")
            {
                Trainsingle.Visible = true;
            }

        }
    }

    protected void asdsaButton48_OnClick(object sender, EventArgs e)
    {
        TrainDropDownList.SelectedValue = 1.ToString();
        TrainTextBox.Text = "";
        TrainfromTextBox.Text = "";
        TraintoTextBox.Text = "";
        TrainDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void TrainEndDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        TrainEndSingleDiv.Visible = false;
        TrainEndRanDiv.Visible = false;
        if (TrainEndDropDownList.SelectedValue == "5")
        {
            TrainEndRanDiv.Visible = true;
        }
        else
        {

            if (TrainEndDropDownList.SelectedValue != "1")
            {
                TrainEndSingleDiv.Visible = true;
            }

        }
    }

    protected void asdsasaaButton48_OnClick(object sender, EventArgs e)
    {
        TrainEndDropDownList.SelectedValue = 1.ToString();
        txtTrainEndDate.Text = "";
        txtTrainEndStartDate.Text = "";
        txtTrainEndEndDate.Text = "";
        TrainEndDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void TrDaysDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        TrDaySingleDiv.Visible = false;
        TrDayRangeDiv.Visible = false;
        if (TrDaysDropDownList.SelectedValue == "5")
        {
            TrDayRangeDiv.Visible = true;
        }
        else
        {

            if (TrDaysDropDownList.SelectedValue != "1")
            {
                TrDaySingleDiv.Visible = true;
            }

        }
    }

    protected void asadassasdsadButton50_OnClick(object sender, EventArgs e)
    {
        TrDaysDropDownList.SelectedValue = 1.ToString();
        txtTrDaysDateSingle.Text = "";
        txtTrDaysDateStart.Text = "";
        txtTrDaysDateEnd.Text = "";
        TrainEndDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void trFeesDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        trFeesSingleDiv.Visible = false;
        trFeesReangeDiv.Visible = false;
        if (trFeesDropDownList.SelectedValue == "5")
        {
            trFeesReangeDiv.Visible = true;
        }
        else
        {

            if (trFeesDropDownList.SelectedValue != "1")
            {
                trFeesSingleDiv.Visible = true;
            }

        }
    }

    protected void asaasdasdassdButton50_OnClick(object sender, EventArgs e)
    {
        trFeesDropDownList.SelectedValue = 1.ToString();
        txttrFeesSingleDate.Text = "";
        txttrFeesStartDate.Text = "";
        txttrFeesEndDate.Text = "";
        trFeesDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void ScoreDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ScoreRangeDiv.Visible = false;
        ScoreSingleDiv.Visible = false;
        if (ScoreDropDownList.SelectedValue == "5")
        {
            ScoreRangeDiv.Visible = true;
        }
        else
        {

            if (ScoreDropDownList.SelectedValue != "1")
            {
                ScoreSingleDiv.Visible = true;
            }

        }
    }

    protected void TrainingTitleDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void lbTrainingSearch_OnClick(object sender, EventArgs e)
    {
        DataTable TrainingInformationdataTable = aReportDal.GetEmpTrainingInfoDAL(GenerateTrainingPam());
        if (TrainingInformationdataTable.Rows.Count > 0)
        {
            LoadInitialGridForEmpDetail(gv_Training);
            gv_Training.DataSource = TrainingInformationdataTable;
            gv_Training.DataBind();

        }
        else
        {
            gv_Training.DataSource = null;
            gv_Training.DataBind();
        }
    }

    private string GenerateTrainingPam() 
    {
        string param = " ";

        if (TrainDropDownList.SelectedValue != "1")
        {
            if (TrainDropDownList.SelectedValue == "2" || TrainDropDownList.SelectedValue == "3" || TrainDropDownList.SelectedValue == "4")
            {
                param = param + " AND EG.TrFromDate " + TrainDropDownList.SelectedItem.Text + " '" +
                        TrainTextBox.Text + "' ";
            }
            else
            {
                param = param + " AND (EG.TrFromDate between '" + TrainfromTextBox.Text + "' AND '" + TraintoTextBox.Text + "') ";
            }
        }



        if (TrainEndDropDownList.SelectedValue != "1")
        {
            if (TrainEndDropDownList.SelectedValue == "2" || TrainEndDropDownList.SelectedValue == "3" || TrainEndDropDownList.SelectedValue == "4")
            {
                param = param + " AND EG.TrToDate " + TrainEndDropDownList.SelectedItem.Text + " '" +
                        txtTrainEndDate.Text + "' ";
            }
            else
            {
                param = param + " AND (EG.TrToDate between '" + txtTrainEndStartDate.Text + "' AND '" + txtTrainEndEndDate.Text + "') ";
            }
        }




        if (TrDaysDropDownList.SelectedValue != "1")
        {
            if (TrDaysDropDownList.SelectedValue == "2" || TrDaysDropDownList.SelectedValue == "3" || TrDaysDropDownList.SelectedValue == "4")
            {
                param = param + " AND EG.TrainingDays " + TrDaysDropDownList.SelectedItem.Text + " '" +
                        txtTrDaysDateSingle.Text + "' ";
            }
            else
            {
                param = param + " AND (EG.TrainingDays between '" + txtTrDaysDateStart.Text + "' AND '" + txtTrDaysDateEnd.Text + "') ";
            }
        }



        if (trFeesDropDownList.SelectedValue != "1")
        {
            if (trFeesDropDownList.SelectedValue == "2" || trFeesDropDownList.SelectedValue == "3" || trFeesDropDownList.SelectedValue == "4")
            {
                param = param + " AND  EG.GrandTotal " + trFeesDropDownList.SelectedItem.Text + " '" +
                        txttrFeesSingleDate.Text + "' ";
            }
            else
            {
                param = param + " AND ( EG.GrandTotal between '" + txttrFeesStartDate.Text + "' AND '" + txttrFeesEndDate.Text + "') ";
            }
        }


        if (repEmpIdHiddenField.Value!="")
        {
            param = param + " AND  EG.EmpInfoId = '" + repEmpIdHiddenField.Value +"'";
             
        }

        if (ddlCompany.SelectedValue != "")
        {
            param = param + " AND EG.ComId='" + ddlCompany.SelectedValue + "' ";
        }


        //if (ScoreDropDownList.SelectedValue != "1")
        //{
        //    if (ScoreDropDownList.SelectedValue == "2" || ScoreDropDownList.SelectedValue == "3" || ScoreDropDownList.SelectedValue == "4")
        //    {
        //        param = param + " AND ST.GrossAmount " + ScoreDropDownList.SelectedItem.Text + " '" +
        //                txtScore.Text + "' ";
        //    }
        //    else
        //    {
        //        param = param + " AND (ST.GrossAmount between '" + txtScoreStart.Text + "' AND '" + txtScoreEnd.Text + "') ";
        //    }
        //}



        if (GradeParam() != string.Empty)
        {
            param = param + GradeParam();
        }
        if (StepParam() != string.Empty)
        {
            param = param + StepParam();
        }


        if (SalLocParam() != string.Empty)
        {
            param = param + SalLocParam();
        }


        if (PlaceParam() != string.Empty)
        {
            param = param + PlaceParam();
        }
        if (empStatusDropDownList.SelectedValue == 0.ToString(CultureInfo.InvariantCulture))
        {

            param = param + "";
        }

        if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            param = param + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }




        return param;
    }

    protected void lbTrainingReset_OnClick(object sender, EventArgs e)
    {
         Response.Redirect("EmployeeInformationReport.aspx");
    }

    protected void ddlEmpInfo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpInfo.SelectedValue!="")
        {
            repEmpIdHiddenField.Value=ddlEmpInfo.SelectedValue;
        }
        else
        {
            repEmpIdHiddenField.Value = "";
        }
    }

    protected void empStatusDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (empStatusDropDownList.SelectedValue == "0")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDL(ddlCompany.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }
        else if (empStatusDropDownList.SelectedValue == "Yes")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(ddlCompany.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }

        else if (empStatusDropDownList.SelectedValue == "Special")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlySpecialTransfer(ddlCompany.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }

        else
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAInactive(ddlCompany.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }
    }

    protected void chkAllfin_OnCheckedChanged(object sender, EventArgs e)
    {

        for (int i = 0; i < chkfin.Items.Count; i++)
        {
            if (chkAllfin.Checked)
            {
                chkfin.Items[i].Selected = true;
            }
            else
            {
                chkfin.Items[i].Selected = false
                    ;
            }
        }
    }

    protected void appraisalStatusDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (appraisalStatusDropDownList.SelectedValue=="1")
        {
            finList.Visible = true;
        }
        else
        {
            finList.Visible = false;
            
        }
    }

    protected void lbRequirmentSearch_OnClick(object sender, EventArgs e)
    {
        DataTable dtdata = aReportDal.GetEmpInfOnlyViewRequirment(ParameterReqru(), ParameterOnlyViewRequi(), ParameterReqru_3());

        if (dtdata.Rows.Count > 0)
        {
            gv_Recruitment.DataSource = dtdata;
            gv_Recruitment.DataBind();
          //  lblCount.Text = "Total: " + dtdata.Rows.Count.ToString();
        }
        else
        {
            gv_Recruitment.DataSource = null;
            gv_Recruitment.DataBind();
           // lblCount.Text = "Total: 0";
        }
    }

    protected void chkRpt_OnCheckedChanged(object sender, EventArgs e)
    {

        for (int i = 0; i < cblHeader.Items.Count; i++)
        {
            if (chkRpt.Checked)
            {
                cblHeader.Items[i].Selected = true;
            }
            else
            {
                cblHeader.Items[i].Selected = false
                    ;
            }
        }

    }



}