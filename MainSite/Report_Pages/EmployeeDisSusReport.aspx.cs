using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.ExitManagement_DAL;
using DAL.Permission_DAL;
using DAL.Report_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Report_Pages_EmployeeDisSusReport : System.Web.UI.Page
{

    EmployeeContractualReportDAL aEmployeeJobLeftEntryDAL = new EmployeeContractualReportDAL();

    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    CommonDataLoadDAL aCommonDataLoadDal=new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
           // UserPersmissionValidation();
            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");
            //LoadInfo();
            loadDropDownList();
            CheckBoxData();
        }
    }
    private void loadDropDownList()
    {
        aCommonDataLoadDal.CompanyDropDown(companyDropDownList," WHERE CompanyId IN("+CompanyId()+")");
        aCommonDataLoadDal.LoadIncrementType(ddlIncrementType);
        companyDropDownList.SelectedIndex = 1;
        aEmployeeJobLeftEntryDAL.LoadPromotionTypeDropDownList(PromotionTypeDropDownList);
        aEmployeeJobLeftEntryDAL.FinYearByCompDropDown(FinancialYearDropDownList, companyDropDownList.SelectedValue);
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

    public void UserPersmissionValidation()
    {
        try
        {
            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            if (dtuserpermission.Rows.Count > 0)
            {
                if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
                    dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
                {


                    ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
                    ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

                    addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                        Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            aShowMessage.ShowMessageBox(ex.ToString(), this);
        }
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

    private void LoadInfoSuspend()
    {
        loadGridView.Columns[1].Visible = false;
        loadGridView.Columns[2].Visible = false;
        loadGridView.Columns[3].Visible = false;
        loadGridView.Columns[4].Visible = false;
        loadGridView.Columns[5].Visible = false;
        loadGridView.Columns[6].Visible = false;
        loadGridView.Columns[7].Visible = false;
        loadGridView.Columns[8].Visible = false;
        loadGridView.Columns[9].Visible = false;
        loadGridView.Columns[10].Visible = false;


        //36
        loadGridView.Columns[11].Visible = false;
        loadGridView.Columns[12].Visible = false;
        loadGridView.Columns[13].Visible = false;
        loadGridView.Columns[14].Visible = false;
        loadGridView.Columns[15].Visible = false;
        loadGridView.Columns[16].Visible = false;
        loadGridView.Columns[17].Visible = false;
        loadGridView.Columns[18].Visible = false;
        loadGridView.Columns[19].Visible = false;
        loadGridView.Columns[20].Visible = false;


        loadGridView.Columns[21].Visible = false;
        loadGridView.Columns[22].Visible = false;
        loadGridView.Columns[23].Visible = false;
        loadGridView.Columns[24].Visible = false;
        loadGridView.Columns[25].Visible = false;
        loadGridView.Columns[26].Visible = false;
        loadGridView.Columns[27].Visible = false;
        loadGridView.Columns[28].Visible = false;
        loadGridView.Columns[29].Visible = false;
        loadGridView.Columns[30].Visible = false;


        loadGridView.Columns[31].Visible = false;
        loadGridView.Columns[32].Visible = false;
        loadGridView.Columns[33].Visible = false;
        loadGridView.Columns[34].Visible = false;
        loadGridView.Columns[35].Visible = false;
        loadGridView.Columns[36].Visible = false;
        loadGridView.Columns[37].Visible = false;


        if (cblHeader.Items[0].Selected == true)
        {
            loadGridView.Columns[1].Visible = true;

        }

        if (cblHeader.Items[0].Selected == false)
        {
            loadGridView.Columns[1].Visible = false;

        }


        if (cblHeader.Items[1].Selected == true)
        {
            loadGridView.Columns[2].Visible = true;

        }

        if (cblHeader.Items[1].Selected == false)
        {
            loadGridView.Columns[2].Visible = false;

        }




        if (cblHeader.Items[2].Selected == true)
        {
            loadGridView.Columns[3].Visible = true;

        }

        if (cblHeader.Items[2].Selected == false)
        {
            loadGridView.Columns[3].Visible = false;

        }



        if (cblHeader.Items[3].Selected == true)
        {
            loadGridView.Columns[4].Visible = true;

        }

        if (cblHeader.Items[3].Selected == false)
        {
            loadGridView.Columns[4].Visible = false;

        }


        if (cblHeader.Items[4].Selected == true)
        {
            loadGridView.Columns[5].Visible = true;

        }

        if (cblHeader.Items[4].Selected == false)
        {
            loadGridView.Columns[5].Visible = false;

        }



        if (cblHeader.Items[5].Selected == true)
        {
            loadGridView.Columns[6].Visible = true;

        }

        if (cblHeader.Items[5].Selected == false)
        {
            loadGridView.Columns[6].Visible = false;

        }



        if (cblHeader.Items[6].Selected == true)
        {
            loadGridView.Columns[7].Visible = true;

        }

        if (cblHeader.Items[6].Selected == false)
        {
            loadGridView.Columns[7].Visible = false;

        }




        if (cblHeader.Items[7].Selected == true)
        {
            loadGridView.Columns[8].Visible = true;

        }

        if (cblHeader.Items[7].Selected == false)
        {
            loadGridView.Columns[8].Visible = false;

        }




        if (cblHeader.Items[8].Selected == true)
        {
            loadGridView.Columns[9].Visible = true;

        }

        if (cblHeader.Items[8].Selected == false)
        {
            loadGridView.Columns[9].Visible = false;

        }





        if (cblHeader.Items[9].Selected == true)
        {
            loadGridView.Columns[10].Visible = true;

        }

        if (cblHeader.Items[9].Selected == false)
        {
            loadGridView.Columns[10].Visible = false;

        }




        if (cblHeader.Items[10].Selected == true)
        {
            loadGridView.Columns[11].Visible = true;

        }

        if (cblHeader.Items[10].Selected == false)
        {
            loadGridView.Columns[11].Visible = false;

        }


        if (cblHeader.Items[11].Selected == true)
        {
            loadGridView.Columns[12].Visible = true;

        }

        if (cblHeader.Items[11].Selected == false)
        {
            loadGridView.Columns[12].Visible = false;

        }





        if (cblHeader.Items[12].Selected == true)
        {
            loadGridView.Columns[13].Visible = true;

        }

        if (cblHeader.Items[12].Selected == false)
        {
            loadGridView.Columns[13].Visible = false;

        }





        if (cblHeader.Items[13].Selected == true)
        {
            loadGridView.Columns[14].Visible = true;

        }

        if (cblHeader.Items[13].Selected == false)
        {
            loadGridView.Columns[14].Visible = false;

        }




        if (cblHeader.Items[14].Selected == true)
        {
            loadGridView.Columns[15].Visible = true;

        }

        if (cblHeader.Items[14].Selected == false)
        {
            loadGridView.Columns[15].Visible = false;

        }





        if (cblHeader.Items[15].Selected == true)
        {
            loadGridView.Columns[16].Visible = true;

        }

        if (cblHeader.Items[15].Selected == false)
        {
            loadGridView.Columns[16].Visible = false;

        }




        if (cblHeader.Items[16].Selected == true)
        {
            loadGridView.Columns[17].Visible = true;

        }

        if (cblHeader.Items[16].Selected == false)
        {
            loadGridView.Columns[17].Visible = false;

        }

        if (cblHeader.Items[17].Selected == true)
        {
            loadGridView.Columns[18].Visible = true;

        }

        if (cblHeader.Items[17].Selected == false)
        {
            loadGridView.Columns[18].Visible = false;

        }



        if (cblHeader.Items[18].Selected == true)
        {
            loadGridView.Columns[19].Visible = true;

        }

        if (cblHeader.Items[18].Selected == false)
        {
            loadGridView.Columns[19].Visible = false;

        }




        if (cblHeader.Items[19].Selected == true)
        {
            loadGridView.Columns[20].Visible = true;

        }

        if (cblHeader.Items[19].Selected == false)
        {
            loadGridView.Columns[20].Visible = false;

        }



        if (cblHeader.Items[20].Selected == true)
        {
            loadGridView.Columns[21].Visible = true;

        }

        if (cblHeader.Items[20].Selected == false)
        {
            loadGridView.Columns[21].Visible = false;

        }


        if (cblHeader.Items[21].Selected == true)
        {
            loadGridView.Columns[22].Visible = true;

        }

        if (cblHeader.Items[21].Selected == false)
        {
            loadGridView.Columns[22].Visible = false;

        }



        if (cblHeader.Items[22].Selected == true)
        {
            loadGridView.Columns[23].Visible = true;

        }

        if (cblHeader.Items[22].Selected == false)
        {
            loadGridView.Columns[23].Visible = false;

        }



        if (cblHeader.Items[23].Selected == true)
        {
            loadGridView.Columns[24].Visible = true;

        }

        if (cblHeader.Items[23].Selected == false)
        {
            loadGridView.Columns[24].Visible = false;

        }



        if (cblHeader.Items[24].Selected == true)
        {
            loadGridView.Columns[25].Visible = true;

        }

        if (cblHeader.Items[24].Selected == false)
        {
            loadGridView.Columns[25].Visible = false;

        }




        if (cblHeader.Items[25].Selected == true)
        {
            loadGridView.Columns[26].Visible = true;

        }

        if (cblHeader.Items[25].Selected == false)
        {
            loadGridView.Columns[26].Visible = false;

        }



        if (cblHeader.Items[26].Selected == true)
        {
            loadGridView.Columns[27].Visible = true;

        }

        if (cblHeader.Items[26].Selected == false)
        {
            loadGridView.Columns[27].Visible = false;

        }


        if (cblHeader.Items[27].Selected == true)
        {
            loadGridView.Columns[28].Visible = true;

        }

        if (cblHeader.Items[27].Selected == false)
        {
            loadGridView.Columns[28].Visible = false;

        }



        if (cblHeader.Items[28].Selected == true)
        {
            loadGridView.Columns[29].Visible = true;

        }

        if (cblHeader.Items[28].Selected == false)
        {
            loadGridView.Columns[29].Visible = false;

        }




        if (cblHeader.Items[29].Selected == true)
        {
            loadGridView.Columns[30].Visible = true;

        }

        if (cblHeader.Items[29].Selected == false)
        {
            loadGridView.Columns[30].Visible = false;

        }




        if (cblHeader.Items[30].Selected == true)
        {
            loadGridView.Columns[31].Visible = true;

        }

        if (cblHeader.Items[30].Selected == false)
        {
            loadGridView.Columns[31].Visible = false;

        }



        if (cblHeader.Items[31].Selected == true)
        {
            loadGridView.Columns[32].Visible = true;

        }

        if (cblHeader.Items[31].Selected == false)
        {
            loadGridView.Columns[32].Visible = false;

        }



        if (cblHeader.Items[32].Selected == true)
        {
            loadGridView.Columns[33].Visible = true;

        }

        if (cblHeader.Items[32].Selected == false)
        {
            loadGridView.Columns[33].Visible = false;

        }




        if (cblHeader.Items[33].Selected == true)
        {
            loadGridView.Columns[34].Visible = true;

        }

        if (cblHeader.Items[33].Selected == false)
        {
            loadGridView.Columns[34].Visible = false;

        }



        if (cblHeader.Items[34].Selected == true)
        {
            loadGridView.Columns[35].Visible = true;

        }

        if (cblHeader.Items[34].Selected == false)
        {
            loadGridView.Columns[35].Visible = false;

        }



        if (cblHeader.Items[35].Selected == true)
        {
            loadGridView.Columns[36].Visible = true;

        }

        if (cblHeader.Items[35].Selected == false)
        {
            loadGridView.Columns[36].Visible = false;

        }


        if (cblHeader.Items[36].Selected == true)
        {
            loadGridView.Columns[37].Visible = true;

        }

        if (cblHeader.Items[36].Selected == false)
        {
            loadGridView.Columns[37].Visible = false;

        }

      //  ddd

        DataTable dataTable = aEmployeeJobLeftEntryDAL.LoadInformationALlSuspend(GenerateParamiterListSuspend());

        if (dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }

    private void LoadInfoDisplinary()
    {
        DisplinaryGridView.Columns[1].Visible = false;
        DisplinaryGridView.Columns[2].Visible = false;
        DisplinaryGridView.Columns[3].Visible = false;
        DisplinaryGridView.Columns[4].Visible = false;
        DisplinaryGridView.Columns[5].Visible = false;
        DisplinaryGridView.Columns[6].Visible = false;
        DisplinaryGridView.Columns[7].Visible = false;
        DisplinaryGridView.Columns[8].Visible = false;
        DisplinaryGridView.Columns[9].Visible = false;
        DisplinaryGridView.Columns[10].Visible = false;


        //36
        DisplinaryGridView.Columns[11].Visible = false;
        DisplinaryGridView.Columns[12].Visible = false;
        DisplinaryGridView.Columns[13].Visible = false;
        DisplinaryGridView.Columns[14].Visible = false;
        DisplinaryGridView.Columns[15].Visible = false;
        DisplinaryGridView.Columns[16].Visible = false;
        DisplinaryGridView.Columns[17].Visible = false;
        DisplinaryGridView.Columns[18].Visible = false;
        DisplinaryGridView.Columns[19].Visible = false;
        DisplinaryGridView.Columns[20].Visible = false;


        DisplinaryGridView.Columns[21].Visible = false;
        DisplinaryGridView.Columns[22].Visible = false;
        DisplinaryGridView.Columns[23].Visible = false;
        DisplinaryGridView.Columns[24].Visible = false;
        DisplinaryGridView.Columns[25].Visible = false;
        DisplinaryGridView.Columns[26].Visible = false;
        DisplinaryGridView.Columns[27].Visible = false;
        DisplinaryGridView.Columns[28].Visible = false;
        DisplinaryGridView.Columns[29].Visible = false;
        DisplinaryGridView.Columns[30].Visible = false;


        DisplinaryGridView.Columns[31].Visible = false;
        DisplinaryGridView.Columns[32].Visible = false;
        DisplinaryGridView.Columns[33].Visible = false;
        DisplinaryGridView.Columns[34].Visible = false;
        DisplinaryGridView.Columns[35].Visible = false;
        DisplinaryGridView.Columns[36].Visible = false;
        DisplinaryGridView.Columns[37].Visible = false;


        if (cblHeader.Items[0].Selected == true)
        {
            DisplinaryGridView.Columns[1].Visible = true;

        }

        if (cblHeader.Items[0].Selected == false)
        {
            DisplinaryGridView.Columns[1].Visible = false;

        }


        if (cblHeader.Items[1].Selected == true)
        {
            DisplinaryGridView.Columns[2].Visible = true;

        }

        if (cblHeader.Items[1].Selected == false)
        {
            DisplinaryGridView.Columns[2].Visible = false;

        }




        if (cblHeader.Items[2].Selected == true)
        {
            DisplinaryGridView.Columns[3].Visible = true;

        }

        if (cblHeader.Items[2].Selected == false)
        {
            DisplinaryGridView.Columns[3].Visible = false;

        }



        if (cblHeader.Items[3].Selected == true)
        {
            DisplinaryGridView.Columns[4].Visible = true;

        }

        if (cblHeader.Items[3].Selected == false)
        {
            DisplinaryGridView.Columns[4].Visible = false;

        }


        if (cblHeader.Items[4].Selected == true)
        {
            DisplinaryGridView.Columns[5].Visible = true;

        }

        if (cblHeader.Items[4].Selected == false)
        {
            DisplinaryGridView.Columns[5].Visible = false;

        }



        if (cblHeader.Items[5].Selected == true)
        {
            DisplinaryGridView.Columns[6].Visible = true;

        }

        if (cblHeader.Items[5].Selected == false)
        {
            DisplinaryGridView.Columns[6].Visible = false;

        }



        if (cblHeader.Items[6].Selected == true)
        {
            DisplinaryGridView.Columns[7].Visible = true;

        }

        if (cblHeader.Items[6].Selected == false)
        {
            DisplinaryGridView.Columns[7].Visible = false;

        }




        if (cblHeader.Items[7].Selected == true)
        {
            DisplinaryGridView.Columns[8].Visible = true;

        }

        if (cblHeader.Items[7].Selected == false)
        {
            DisplinaryGridView.Columns[8].Visible = false;

        }




        if (cblHeader.Items[8].Selected == true)
        {
            DisplinaryGridView.Columns[9].Visible = true;

        }

        if (cblHeader.Items[8].Selected == false)
        {
            DisplinaryGridView.Columns[9].Visible = false;

        }





        if (cblHeader.Items[9].Selected == true)
        {
            DisplinaryGridView.Columns[10].Visible = true;

        }

        if (cblHeader.Items[9].Selected == false)
        {
            DisplinaryGridView.Columns[10].Visible = false;

        }




        if (cblHeader.Items[10].Selected == true)
        {
            DisplinaryGridView.Columns[11].Visible = true;

        }

        if (cblHeader.Items[10].Selected == false)
        {
            DisplinaryGridView.Columns[11].Visible = false;

        }


        if (cblHeader.Items[11].Selected == true)
        {
            DisplinaryGridView.Columns[12].Visible = true;

        }

        if (cblHeader.Items[11].Selected == false)
        {
            DisplinaryGridView.Columns[12].Visible = false;

        }





        if (cblHeader.Items[12].Selected == true)
        {
            DisplinaryGridView.Columns[13].Visible = true;

        }

        if (cblHeader.Items[12].Selected == false)
        {
            DisplinaryGridView.Columns[13].Visible = false;

        }





        if (cblHeader.Items[13].Selected == true)
        {
            DisplinaryGridView.Columns[14].Visible = true;

        }

        if (cblHeader.Items[13].Selected == false)
        {
            DisplinaryGridView.Columns[14].Visible = false;

        }




        if (cblHeader.Items[14].Selected == true)
        {
            DisplinaryGridView.Columns[15].Visible = true;

        }

        if (cblHeader.Items[14].Selected == false)
        {
            DisplinaryGridView.Columns[15].Visible = false;

        }





        if (cblHeader.Items[15].Selected == true)
        {
            DisplinaryGridView.Columns[16].Visible = true;

        }

        if (cblHeader.Items[15].Selected == false)
        {
            DisplinaryGridView.Columns[16].Visible = false;

        }




        if (cblHeader.Items[16].Selected == true)
        {
            DisplinaryGridView.Columns[17].Visible = true;

        }

        if (cblHeader.Items[16].Selected == false)
        {
            DisplinaryGridView.Columns[17].Visible = false;

        }

        if (cblHeader.Items[17].Selected == true)
        {
            DisplinaryGridView.Columns[18].Visible = true;

        }

        if (cblHeader.Items[17].Selected == false)
        {
            DisplinaryGridView.Columns[18].Visible = false;

        }



        if (cblHeader.Items[18].Selected == true)
        {
            DisplinaryGridView.Columns[19].Visible = true;

        }

        if (cblHeader.Items[18].Selected == false)
        {
            DisplinaryGridView.Columns[19].Visible = false;

        }




        if (cblHeader.Items[19].Selected == true)
        {
            DisplinaryGridView.Columns[20].Visible = true;

        }

        if (cblHeader.Items[19].Selected == false)
        {
            DisplinaryGridView.Columns[20].Visible = false;

        }



        if (cblHeader.Items[20].Selected == true)
        {
            DisplinaryGridView.Columns[21].Visible = true;

        }

        if (cblHeader.Items[20].Selected == false)
        {
            DisplinaryGridView.Columns[21].Visible = false;

        }


        if (cblHeader.Items[21].Selected == true)
        {
            DisplinaryGridView.Columns[22].Visible = true;

        }

        if (cblHeader.Items[21].Selected == false)
        {
            DisplinaryGridView.Columns[22].Visible = false;

        }



        if (cblHeader.Items[22].Selected == true)
        {
            DisplinaryGridView.Columns[23].Visible = true;

        }

        if (cblHeader.Items[22].Selected == false)
        {
            DisplinaryGridView.Columns[23].Visible = false;

        }



        if (cblHeader.Items[23].Selected == true)
        {
            DisplinaryGridView.Columns[24].Visible = true;

        }

        if (cblHeader.Items[23].Selected == false)
        {
            DisplinaryGridView.Columns[24].Visible = false;

        }



        if (cblHeader.Items[24].Selected == true)
        {
            DisplinaryGridView.Columns[25].Visible = true;

        }

        if (cblHeader.Items[24].Selected == false)
        {
            DisplinaryGridView.Columns[25].Visible = false;

        }




        if (cblHeader.Items[25].Selected == true)
        {
            DisplinaryGridView.Columns[26].Visible = true;

        }

        if (cblHeader.Items[25].Selected == false)
        {
            DisplinaryGridView.Columns[26].Visible = false;

        }



        if (cblHeader.Items[26].Selected == true)
        {
            DisplinaryGridView.Columns[27].Visible = true;

        }

        if (cblHeader.Items[26].Selected == false)
        {
            DisplinaryGridView.Columns[27].Visible = false;

        }


        if (cblHeader.Items[27].Selected == true)
        {
            DisplinaryGridView.Columns[28].Visible = true;

        }

        if (cblHeader.Items[27].Selected == false)
        {
            DisplinaryGridView.Columns[28].Visible = false;

        }



        if (cblHeader.Items[28].Selected == true)
        {
            DisplinaryGridView.Columns[29].Visible = true;

        }

        if (cblHeader.Items[28].Selected == false)
        {
            DisplinaryGridView.Columns[29].Visible = false;

        }




        if (cblHeader.Items[29].Selected == true)
        {
            DisplinaryGridView.Columns[30].Visible = true;

        }

        if (cblHeader.Items[29].Selected == false)
        {
            DisplinaryGridView.Columns[30].Visible = false;

        }




        if (cblHeader.Items[30].Selected == true)
        {
            DisplinaryGridView.Columns[31].Visible = true;

        }

        if (cblHeader.Items[30].Selected == false)
        {
            DisplinaryGridView.Columns[31].Visible = false;

        }



        if (cblHeader.Items[31].Selected == true)
        {
            DisplinaryGridView.Columns[32].Visible = true;

        }

        if (cblHeader.Items[31].Selected == false)
        {
            DisplinaryGridView.Columns[32].Visible = false;

        }



        if (cblHeader.Items[32].Selected == true)
        {
            DisplinaryGridView.Columns[33].Visible = true;

        }

        if (cblHeader.Items[32].Selected == false)
        {
            DisplinaryGridView.Columns[33].Visible = false;

        }




        if (cblHeader.Items[33].Selected == true)
        {
            DisplinaryGridView.Columns[34].Visible = true;

        }

        if (cblHeader.Items[33].Selected == false)
        {
            DisplinaryGridView.Columns[34].Visible = false;

        }



        if (cblHeader.Items[34].Selected == true)
        {
            DisplinaryGridView.Columns[35].Visible = true;

        }

        if (cblHeader.Items[34].Selected == false)
        {
            DisplinaryGridView.Columns[35].Visible = false;

        }



        if (cblHeader.Items[35].Selected == true)
        {
            DisplinaryGridView.Columns[36].Visible = true;

        }

        if (cblHeader.Items[35].Selected == false)
        {
            DisplinaryGridView.Columns[36].Visible = false;

        }


        if (cblHeader.Items[36].Selected == true)
        {
            DisplinaryGridView.Columns[37].Visible = true;

        }

        if (cblHeader.Items[36].Selected == false)
        {
            DisplinaryGridView.Columns[37].Visible = false;

        }

        //  ddd

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

        if (companyDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND DCPA.CompanyInfoId = " + companyDropDownList.SelectedValue;
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




        if (EmployeeIdHiddenField.Value != "")
        {
            parameter = parameter + " AND  DCPA.EmpInfoId = " + EmployeeIdHiddenField.Value;
        }


        return parameter;
    }


    private string GenerateParamiterListSuspend()
    {

        string parameter = "    ";

        if (companyDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND SPND.CompanyInfoId = " + companyDropDownList.SelectedValue;
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




        if (EmployeeIdHiddenField.Value != "")
        {
            parameter = parameter + " AND SPND.EmpInfoId = " + EmployeeIdHiddenField.Value;
        }


        return parameter;
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmployeeJobLeftEntry.aspx"); 
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                  DataTable aTable =
                            aEmployeeJobLeftEntryDAL.DeleteValidattionForEffectiveDate(Idd.ToString());
                if (aTable.Rows.Count > 0)
                {
                    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                    {
                       
                        Session["EmployeeJobLeftId"] = "";
                        Session["EmployeeJobLeftId"] = Idd;

                        Session["Status"] = "Edit";
                        Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
                    }
                }
            }
            //}
            //bool status = false;
            //if (!string.IsNullOrEmpty(datKey[1].ToString()))
            //{
            //    status = Convert.ToBoolean(datKey[1].ToString());
            //}
            //if (status)
            //{
            //    aShowMessage.ShowMessageBox("Employee Already Job Left !!", this);
            //}
            //else
            //{
            //    Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");
            //}

        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                DataTable aTable =
                    aEmployeeJobLeftEntryDAL.DeleteValidattionForEffectiveDate(Idd.ToString());
                if (aTable.Rows.Count > 0)
                {
                    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                    {

                        Session["EmployeeJobLeftId"] = "";
                        Session["EmployeeJobLeftId"] = Idd;
                        Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");
                        Session["Status"] = "View";
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Data can not be Deleted !!", this);
                    }

                }
            }

        }

        if (e.CommandName == "DeleteData")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                Session["EmployeeJobLeftId"] = "";
                Session["EmployeeJobLeftId"] = Idd;
                Session["Status"] = "Delete";
            }
            //bool status = false;
            //if (!string.IsNullOrEmpty(datKey[1].ToString()))
            //{
            //    status = Convert.ToBoolean(datKey[1].ToString());
            //}
            //if (status)
            //{
            //    aShowMessage.ShowMessageBox("Employee Already Job Left !!", this);
            //}
            //else
            //{
            //    Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");
            //}
            //int rowindex = Convert.ToInt32(e.CommandArgument);
            //string EmployeeJobLeftId = loadGridView.DataKeys[rowindex][0].ToString();

            //if (aEmployeeJobLeftEntryDAL.DeleteEmployeeJobLeftById(EmployeeJobLeftId))
            //{
            //    LoadInfo();
            //    aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
               
            //}
        }
    }
    //public void GetCompany()
    //{
    //    DataTable dtcomp = aPermissionDal.GetCompany();
    //    lchk_Company.DataValueField = "CompanyId";
    //    lchk_Company.DataTextField = "ShortName";
    //    lchk_Company.DataSource = dtcomp;
    //    lchk_Company.DataBind();

    //    DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
    //    for (int i = 0; i < userdata.Rows.Count; i++)
    //    {
    //        for (int j = 0; j < lchk_Company.Items.Count; j++)
    //        {
    //            if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
    //            {
    //                lchk_Company.Items[j].Selected = true;


    //            }
    //        }
    //    }
    //}
    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    GridView HeaderGrid = (GridView)sender;
        //    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //    TableCell HeaderCell = new TableCell();

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = " ";
        //    HeaderCell.BackColor = Color.FromName("#F5F5F5");
        //    HeaderCell.BorderColor = Color.FromName("#F5F5F5");

        //    HeaderCell.ColumnSpan = 1;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = " ";
        //    HeaderCell.BackColor = Color.FromName("#F5F5F5");
        //    HeaderCell.BorderColor = Color.FromName("#F5F5F5");


        //    HeaderCell.ColumnSpan = 1;

        //    HeaderGridRow.Cells.Add(HeaderCell);



        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = " ";
        //    HeaderCell.ColumnSpan = 3;
        //    HeaderCell.BackColor = Color.FromName("#F5F5F5");
        //    HeaderCell.BorderColor = Color.FromName("#F5F5F5");
        //    HeaderGridRow.Cells.Add(HeaderCell);


        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = " ";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.BackColor = Color.FromName("#F5F5F5");
        //    HeaderCell.BorderColor = Color.FromName("#F5F5F5");

        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = " ";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.BackColor = Color.FromName("#F5F5F5");
        //    HeaderCell.BorderColor = Color.FromName("#F5F5F5");

        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Promotion Information";
            
        //    HeaderCell.BackColor = Color.SkyBlue;

        //    HeaderCell.ColumnSpan = 4;
        //    HeaderGridRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = " ";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.BackColor = Color.FromName("#F5F5F5");
        //    HeaderCell.BorderColor = Color.FromName("#F5F5F5");
        //    loadGridView.Controls[0].Controls.AddAt(0, HeaderGridRow);

        //} 
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
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
                 loadGridView.DataSource = null;
                 loadGridView.DataBind();
             }
             else
             {
                 loadGridView.DataSource = null;
                 loadGridView.DataBind();
                 DisplinaryGridView.DataSource = null;
                 DisplinaryGridView.DataBind();
             }
        }
         
    }

    protected void reportDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (reportDropDownList.SelectedValue == "Diciplinary")
        {
            if (companyDropDownList.SelectedValue != "")
            {
                //Session["CompanyId"] = "";
                //Session["CompanyId"] = companyDropDownList.SelectedValue;

                aEmployeeJobLeftEntryDAL.LoadActionType(actionTypeDropDownList, companyDropDownList.SelectedValue);

            }
            else
            {
                actionTypeDropDownList.Items.Clear();
            }
        }

        else  if (reportDropDownList.SelectedValue == "Suspend")
        {
            if (companyDropDownList.SelectedValue != "")
            {
                //Session["CompanyId"] = "";
                //Session["CompanyId"] = companyDropDownList.SelectedValue;

                aEmployeeJobLeftEntryDAL.LoadActionTypeForSuspend(actionTypeDropDownList, companyDropDownList.SelectedValue);

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

    public void CheckBoxData()
    {
        DataTable dtgrade = aEmployeeJobLeftEntryDAL.GetJobleftType();
        managementCheckBoxList.DataValueField = "JobLeftTypeId";
        managementCheckBoxList.DataTextField = "JobLeftType";
        managementCheckBoxList.DataSource = dtgrade;
        managementCheckBoxList.DataBind();
    }

    protected void manCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < managementCheckBoxList.Items.Count; i++)
        {
            if (manCheckBox.Checked)
            {
                managementCheckBoxList.Items[i].Selected = true;
            }
            else
            {
                managementCheckBoxList.Items[i].Selected = false
                    ;
            }
        }
    }

    protected void companyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            reportDropDownList_SelectedIndexChanged(null, null);
            aEmployeeJobLeftEntryDAL.FinYearByCompDropDown(FinancialYearDropDownList, companyDropDownList.SelectedValue);
        }
        else
        {
            reportDropDownList_SelectedIndexChanged(null, null);
            FinancialYearDropDownList.Items.Clear();
        }
    }

    public bool Validation()
    {
        if (reportDropDownList.SelectedValue == "0")
        {
            aShowMessage.ShowMessageBox("Please Select this!!!", this);
            reportDropDownList.Focus();
            return false;
        }
        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select a company!!!", this);
            companyDropDownList.Focus();
            return false;
        }


        return true;
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (reportDropDownList.SelectedValue == "Suspend")
        {
            if (loadGridView.Rows.Count > 0)
            {
                string attachment = "attachment; filename=EmployeeSuspendListInfo.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                loadGridView.AllowPaging = false;



                //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                //            false;
                //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                //   false;
                //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                //   false;

                this.LoadInfoSuspend();

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
                string headerTable = @"<span  style='text-align:left'><h3> " + companyDropDownList.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

                string SubTi = @"<span   style='text-align:center'>
   <h3>Upgration List	</h3>

</span>";

                HttpContext.Current.Response.Write(headerTable);
                HttpContext.Current.Response.Write(SubTi);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                showMessageBox("No Data Found!!");
            }
        }
        else if (reportDropDownList.SelectedValue == "Diciplinary")
        {
            if (DisplinaryGridView.Rows.Count > 0)
            {
                string attachment = "attachment; filename=EmployeeDiciplinaryListInfo.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                DisplinaryGridView.AllowPaging = false;



                //DisplinaryGridView.Columns[DisplinaryGridView.Columns.Count - 1].Visible =
                //            false;
                //DisplinaryGridView.Columns[DisplinaryGridView.Columns.Count - 2].Visible =
                //   false;
                //DisplinaryGridView.Columns[DisplinaryGridView.Columns.Count - 3].Visible =
                //   false;

                this.LoadInfoDisplinary();

                // Create a form to contain the grid  
                HtmlForm frm = new HtmlForm();
                DisplinaryGridView.Parent.Controls.Add(frm);
                //frm.Attributes["runat"] = "server";
                //frm.Controls.Add(DisplinaryGridView);
                //frm.RenderControl(htw);

                DisplinaryGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

                // Set background color of each cell of GridView1 header row
                foreach (TableCell tableCell in DisplinaryGridView.HeaderRow.Cells)
                {
                    tableCell.Style["background-color"] = "#E5EEF1";
                }

                // Set background color of each cell of each data row of GridView1
                foreach (GridViewRow gridViewRow in DisplinaryGridView.Rows)
                {
                    gridViewRow.BackColor = System.Drawing.Color.White;

                    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                    {
                        gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                    }
                }


                DisplinaryGridView.RenderControl(htw);
                string headerTable = @"<span  style='text-align:left'><h3> " + companyDropDownList.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

                string SubTi = @"<span   style='text-align:center'>
   <h3>Upgration List	</h3>

</span>";

                HttpContext.Current.Response.Write(headerTable);
                HttpContext.Current.Response.Write(SubTi);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                showMessageBox("No Data Found!!");
            }
        }

       
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=EmployeePromotionListInfo.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
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
     
            loadGridView.RenderControl(hw);
            string headerTable = @"                             
									   <div  style='text-align:left'><h3>SMC/SMC EL											
 </h3>  </div>  
 
   <div   style='text-align:right'>
   <h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></div>";

            string SubTi = @" <br/>
<div   style='text-align:center'>
   <h4>Upgration List</h4>

</div>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
#pragma warning disable CS0612 // Type or member is obsolete
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
#pragma warning restore CS0612 // Type or member is obsolete
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
          
            Response.Write(pdfDoc);
            Response.End();
            loadGridView.AllowPaging = false;
            loadGridView.DataBind();
        }
        else
        {
            showMessageBox("No Data Found!!");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
       // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeePromotionReport.aspx");
    }

    protected void cblHeader_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void SearchEmployeeNameTextBoxTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            string empName = SearchEmployeeNameTextBoxTextBox.Text.Trim();

            if (empName.Contains(':'))
            {
                string[] emp = empName.Split(':');

                SearchEmployeeNameTextBoxTextBox.Text = emp[2];
                EmployeeIdHiddenField.Value = emp[0];



            }
            else
            {

                SearchEmployeeNameTextBoxTextBox.Text = "";
                EmployeeIdHiddenField.Value = "";
                aShowMessage.ShowMessageBox("Input Correct Data !!", this);
            }

        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select a Company !!", this);
            SearchEmployeeNameTextBoxTextBox.Text = "";
            EmployeeIdHiddenField.Value = "";
            companyDropDownList.Focus();
        }
    }
}