using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.ExitManagement_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Font = System.Drawing.Font;

public partial class Report_Pages_EmployeeSeparationReport : System.Web.UI.Page
{

    EmployeeSeparationReportDAL aSeparationDAL = new EmployeeSeparationReportDAL();

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
            SeparationEffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            SeparationEffectToDate.Attributes.Add("readonly", "readonly");
            //LoadInfo();
            loadDropDownList();
            CheckBoxData();
        }
    }
    private void loadDropDownList()
    {
        aCommonDataLoadDal.CompanyDropDown(companyDropDownList," WHERE CompanyId IN("+CompanyId()+")");
        companyDropDownList.SelectedIndex = 1;
        aSeparationDAL.FinYearByCompDropDown(SeparationFinancialYearDropDownList, companyDropDownList.SelectedValue);

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

                    SeparationloadGridView.Columns[SeparationloadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    SeparationloadGridView.Columns[SeparationloadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    SeparationloadGridView.Columns[SeparationloadGridView.Columns.Count - 3].Visible =
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

    private void LoadInfo()
    {
        SeparationloadGridView.Columns[1].Visible = false;
        SeparationloadGridView.Columns[2].Visible = false;
        SeparationloadGridView.Columns[3].Visible = false;
        SeparationloadGridView.Columns[4].Visible = false;
        SeparationloadGridView.Columns[5].Visible = false;
        SeparationloadGridView.Columns[6].Visible = false;
        SeparationloadGridView.Columns[7].Visible = false;
        SeparationloadGridView.Columns[8].Visible = false;
        SeparationloadGridView.Columns[9].Visible = false;
        SeparationloadGridView.Columns[10].Visible = false;


        //36
        SeparationloadGridView.Columns[11].Visible = false;
        SeparationloadGridView.Columns[12].Visible = false;
        SeparationloadGridView.Columns[13].Visible = false;
        SeparationloadGridView.Columns[14].Visible = false;
        SeparationloadGridView.Columns[15].Visible = false;
        SeparationloadGridView.Columns[16].Visible = false;
        SeparationloadGridView.Columns[17].Visible = false;
        SeparationloadGridView.Columns[18].Visible = false;
        SeparationloadGridView.Columns[19].Visible = false;
        SeparationloadGridView.Columns[20].Visible = false;


        SeparationloadGridView.Columns[21].Visible = false;
        SeparationloadGridView.Columns[22].Visible = false;
        SeparationloadGridView.Columns[23].Visible = false;
        SeparationloadGridView.Columns[24].Visible = false;
        SeparationloadGridView.Columns[25].Visible = false;
        SeparationloadGridView.Columns[26].Visible = false;
        SeparationloadGridView.Columns[27].Visible = false;
        SeparationloadGridView.Columns[28].Visible = false;
        SeparationloadGridView.Columns[29].Visible = false;
        SeparationloadGridView.Columns[30].Visible = false;


        SeparationloadGridView.Columns[31].Visible = false;
        SeparationloadGridView.Columns[32].Visible = false;
        SeparationloadGridView.Columns[33].Visible = false;
        SeparationloadGridView.Columns[34].Visible = false;
        SeparationloadGridView.Columns[35].Visible = false;
        SeparationloadGridView.Columns[36].Visible = false;
        SeparationloadGridView.Columns[37].Visible = false;


        if (cblHeader.Items[0].Selected == true)
        {
            SeparationloadGridView.Columns[1].Visible = true;

        }

        if (cblHeader.Items[0].Selected == false)
        {
            SeparationloadGridView.Columns[1].Visible = false;

        }


        if (cblHeader.Items[1].Selected == true)
        {
            SeparationloadGridView.Columns[2].Visible = true;

        }

        if (cblHeader.Items[1].Selected == false)
        {
            SeparationloadGridView.Columns[2].Visible = false;

        }




        if (cblHeader.Items[2].Selected == true)
        {
            SeparationloadGridView.Columns[3].Visible = true;

        }

        if (cblHeader.Items[2].Selected == false)
        {
            SeparationloadGridView.Columns[3].Visible = false;

        }



        if (cblHeader.Items[3].Selected == true)
        {
            SeparationloadGridView.Columns[4].Visible = true;

        }

        if (cblHeader.Items[3].Selected == false)
        {
            SeparationloadGridView.Columns[4].Visible = false;

        }


        if (cblHeader.Items[4].Selected == true)
        {
            SeparationloadGridView.Columns[5].Visible = true;

        }

        if (cblHeader.Items[4].Selected == false)
        {
            SeparationloadGridView.Columns[5].Visible = false;

        }



        if (cblHeader.Items[5].Selected == true)
        {
            SeparationloadGridView.Columns[6].Visible = true;

        }

        if (cblHeader.Items[5].Selected == false)
        {
            SeparationloadGridView.Columns[6].Visible = false;

        }



        if (cblHeader.Items[6].Selected == true)
        {
            SeparationloadGridView.Columns[7].Visible = true;

        }

        if (cblHeader.Items[6].Selected == false)
        {
            SeparationloadGridView.Columns[7].Visible = false;

        }




        if (cblHeader.Items[7].Selected == true)
        {
            SeparationloadGridView.Columns[8].Visible = true;

        }

        if (cblHeader.Items[7].Selected == false)
        {
            SeparationloadGridView.Columns[8].Visible = false;

        }




        if (cblHeader.Items[8].Selected == true)
        {
            SeparationloadGridView.Columns[9].Visible = true;

        }

        if (cblHeader.Items[8].Selected == false)
        {
            SeparationloadGridView.Columns[9].Visible = false;

        }





        if (cblHeader.Items[9].Selected == true)
        {
            SeparationloadGridView.Columns[10].Visible = true;

        }

        if (cblHeader.Items[9].Selected == false)
        {
            SeparationloadGridView.Columns[10].Visible = false;

        }




        if (cblHeader.Items[10].Selected == true)
        {
            SeparationloadGridView.Columns[11].Visible = true;

        }

        if (cblHeader.Items[10].Selected == false)
        {
            SeparationloadGridView.Columns[11].Visible = false;

        }


        if (cblHeader.Items[11].Selected == true)
        {
            SeparationloadGridView.Columns[12].Visible = true;

        }

        if (cblHeader.Items[11].Selected == false)
        {
            SeparationloadGridView.Columns[12].Visible = false;

        }





        if (cblHeader.Items[12].Selected == true)
        {
            SeparationloadGridView.Columns[13].Visible = true;

        }

        if (cblHeader.Items[12].Selected == false)
        {
            SeparationloadGridView.Columns[13].Visible = false;

        }





        if (cblHeader.Items[13].Selected == true)
        {
            SeparationloadGridView.Columns[14].Visible = true;

        }

        if (cblHeader.Items[13].Selected == false)
        {
            SeparationloadGridView.Columns[14].Visible = false;

        }




        if (cblHeader.Items[14].Selected == true)
        {
            SeparationloadGridView.Columns[15].Visible = true;

        }

        if (cblHeader.Items[14].Selected == false)
        {
            SeparationloadGridView.Columns[15].Visible = false;

        }





        if (cblHeader.Items[15].Selected == true)
        {
            SeparationloadGridView.Columns[16].Visible = true;

        }

        if (cblHeader.Items[15].Selected == false)
        {
            SeparationloadGridView.Columns[16].Visible = false;

        }




        if (cblHeader.Items[16].Selected == true)
        {
            SeparationloadGridView.Columns[17].Visible = true;

        }

        if (cblHeader.Items[16].Selected == false)
        {
            SeparationloadGridView.Columns[17].Visible = false;

        }

        if (cblHeader.Items[17].Selected == true)
        {
            SeparationloadGridView.Columns[18].Visible = true;

        }

        if (cblHeader.Items[17].Selected == false)
        {
            SeparationloadGridView.Columns[18].Visible = false;

        }



        if (cblHeader.Items[18].Selected == true)
        {
            SeparationloadGridView.Columns[19].Visible = true;

        }

        if (cblHeader.Items[18].Selected == false)
        {
            SeparationloadGridView.Columns[19].Visible = false;

        }




        if (cblHeader.Items[19].Selected == true)
        {
            SeparationloadGridView.Columns[20].Visible = true;

        }

        if (cblHeader.Items[19].Selected == false)
        {
            SeparationloadGridView.Columns[20].Visible = false;

        }



        if (cblHeader.Items[20].Selected == true)
        {
            SeparationloadGridView.Columns[21].Visible = true;

        }

        if (cblHeader.Items[20].Selected == false)
        {
            SeparationloadGridView.Columns[21].Visible = false;

        }


        if (cblHeader.Items[21].Selected == true)
        {
            SeparationloadGridView.Columns[22].Visible = true;

        }

        if (cblHeader.Items[21].Selected == false)
        {
            SeparationloadGridView.Columns[22].Visible = false;

        }



        if (cblHeader.Items[22].Selected == true)
        {
            SeparationloadGridView.Columns[23].Visible = true;

        }

        if (cblHeader.Items[22].Selected == false)
        {
            SeparationloadGridView.Columns[23].Visible = false;

        }



        if (cblHeader.Items[23].Selected == true)
        {
            SeparationloadGridView.Columns[24].Visible = true;

        }

        if (cblHeader.Items[23].Selected == false)
        {
            SeparationloadGridView.Columns[24].Visible = false;

        }



        if (cblHeader.Items[24].Selected == true)
        {
            SeparationloadGridView.Columns[25].Visible = true;

        }

        if (cblHeader.Items[24].Selected == false)
        {
            SeparationloadGridView.Columns[25].Visible = false;

        }




        if (cblHeader.Items[25].Selected == true)
        {
            SeparationloadGridView.Columns[26].Visible = true;

        }

        if (cblHeader.Items[25].Selected == false)
        {
            SeparationloadGridView.Columns[26].Visible = false;

        }



        if (cblHeader.Items[26].Selected == true)
        {
            SeparationloadGridView.Columns[27].Visible = true;

        }

        if (cblHeader.Items[26].Selected == false)
        {
            SeparationloadGridView.Columns[27].Visible = false;

        }


        if (cblHeader.Items[27].Selected == true)
        {
            SeparationloadGridView.Columns[28].Visible = true;

        }

        if (cblHeader.Items[27].Selected == false)
        {
            SeparationloadGridView.Columns[28].Visible = false;

        }



        if (cblHeader.Items[28].Selected == true)
        {
            SeparationloadGridView.Columns[29].Visible = true;

        }

        if (cblHeader.Items[28].Selected == false)
        {
            SeparationloadGridView.Columns[29].Visible = false;

        }




        if (cblHeader.Items[29].Selected == true)
        {
            SeparationloadGridView.Columns[30].Visible = true;

        }

        if (cblHeader.Items[29].Selected == false)
        {
            SeparationloadGridView.Columns[30].Visible = false;

        }




        if (cblHeader.Items[30].Selected == true)
        {
            SeparationloadGridView.Columns[31].Visible = true;

        }

        if (cblHeader.Items[30].Selected == false)
        {
            SeparationloadGridView.Columns[31].Visible = false;

        }



        if (cblHeader.Items[31].Selected == true)
        {
            SeparationloadGridView.Columns[32].Visible = true;

        }

        if (cblHeader.Items[31].Selected == false)
        {
            SeparationloadGridView.Columns[32].Visible = false;

        }



        if (cblHeader.Items[32].Selected == true)
        {
            SeparationloadGridView.Columns[33].Visible = true;

        }

        if (cblHeader.Items[32].Selected == false)
        {
            SeparationloadGridView.Columns[33].Visible = false;

        }




        if (cblHeader.Items[33].Selected == true)
        {
            SeparationloadGridView.Columns[34].Visible = true;

        }

        if (cblHeader.Items[33].Selected == false)
        {
            SeparationloadGridView.Columns[34].Visible = false;

        }



        if (cblHeader.Items[34].Selected == true)
        {
            SeparationloadGridView.Columns[35].Visible = true;

        }

        if (cblHeader.Items[34].Selected == false)
        {
            SeparationloadGridView.Columns[35].Visible = false;

        }



        if (cblHeader.Items[35].Selected == true)
        {
            SeparationloadGridView.Columns[36].Visible = true;

        }

        if (cblHeader.Items[35].Selected == false)
        {
            SeparationloadGridView.Columns[36].Visible = false;

        }


        if (cblHeader.Items[36].Selected == true)
        {
            SeparationloadGridView.Columns[37].Visible = true;

        }

        if (cblHeader.Items[36].Selected == false)
        {
            SeparationloadGridView.Columns[37].Visible = false;

        }

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



    private void LoadWPPFInfo()
    {
        if (empStatusDropDownList.SelectedValue == "Inactive")
        {
            DataTable dataTable = aSeparationDAL.LoadWPPFInformationALl(SeprationGenerateParamiterList());

            if (dataTable.Rows.Count > 0)
            {
                LoadInitialGrid(WPPFGridView);
                WPPFGridView.DataSource = dataTable;
                WPPFGridView.DataBind();
            }
            else
            {
                WPPFGridView.DataSource = null;
                WPPFGridView.DataBind();
                aShowMessage.ShowMessageBox("No Data Found!!", this);
            }
        }

        if (empStatusDropDownList.SelectedValue == "Active")
        {
            DataTable dataTable = aSeparationDAL.LoadEmpIsActiveTrue(" AND EG.CompanyId = " + companyDropDownList.SelectedValue);

            if (dataTable.Rows.Count > 0)
            {
                LoadInitialGrid(WPPFGridView);
                WPPFGridView.DataSource = dataTable;
                WPPFGridView.DataBind();
            }
            else
            {
                WPPFGridView.DataSource = null;
                WPPFGridView.DataBind();
                aShowMessage.ShowMessageBox("No Data Found!!", this);
            }
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
    private string SeprationGenerateParamiterList()
    {

        string parameter = "   ";

        if (companyDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND EG.CompanyId = " + companyDropDownList.SelectedValue;
        }

       

        //if (empStatusDropDownList.SelectedItem.Text == "Active" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        //{
        //    parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        //}


        if (empStatusDropDownList.SelectedItem.Text == "Inactive" && empStatusDropDownList.SelectedValue != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND EG.EmployeeStatus='" + empStatusDropDownList.SelectedItem.Text + "' ";
        }

        if (JobLeftType() != string.Empty)
        {
            parameter = parameter + JobLeftType();   
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

      

        return parameter;
    }



    public string JobLeftType()
    {
        string param = "";
        string text = "";

        for (int i = 0; i < SeparationmanagementCheckBoxList.Items.Count; i++)
        {
            if (SeparationmanagementCheckBoxList.Items[i].Selected)
            {
                text = SeparationmanagementCheckBoxList.Items[i].Value + "," + text;
            }
        }
        if (text != string.Empty)
        {
            param = param + " AND EPE.JobLeftTypeId in " +   " (" + text.TrimEnd(',') +
                    ")";
        }

        return param;
    }
    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmployeeJobLeftEntry.aspx"); 
    }

    protected void SeparationloadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = SeparationloadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                  DataTable aTable =
                            aSeparationDAL.DeleteValidattionForEffectiveDate(Idd.ToString());
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


            var datKey = SeparationloadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                DataTable aTable =
                    aSeparationDAL.DeleteValidattionForEffectiveDate(Idd.ToString());
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


            var datKey = SeparationloadGridView.DataKeys[rowindex];
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
            //string EmployeeJobLeftId = SeparationloadGridView.DataKeys[rowindex][0].ToString();

            //if (aSeparationDAL.DeleteEmployeeJobLeftById(EmployeeJobLeftId))
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
    protected void SeparationloadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void eparationmanSearchButton_OnClick(object sender, EventArgs e)
    {
        if (reportDropDownList.SelectedValue != "0")
        {
            if (reportDropDownList.SelectedValue == "Inquiry")
            {
                if (Validation())
                {
                    LoadInfo();
                    WPPFGridView.DataSource = null;
                    WPPFGridView.DataBind();
                }
                else
                {

                }
            }

            if (reportDropDownList.SelectedValue == "WPPF")
            {
                if (Validation2())
                {
                    LoadWPPFInfo();
                    SeparationloadGridView.DataSource = null;
                    SeparationloadGridView.DataBind();
                }
              
            }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select this!!!", this);
            reportDropDownList.Focus();
        }
        


    }

    public bool Validation()
    {
        if (reportDropDownList.SelectedValue != "Inquiry")
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

    public bool Validation2()
    {
        if (reportDropDownList.SelectedValue != "WPPF")
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

    protected void eparationmanlbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeSeparationReport.aspx");
    }

    protected void reportDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeparationloadGridView.DataSource = null;
        SeparationloadGridView.DataBind();

        WPPFGridView.DataSource = null;
        WPPFGridView.DataBind();
    }

    public void CheckBoxData()
    {
        DataTable dtgrade = aSeparationDAL.GetJobleftType();
        SeparationmanagementCheckBoxList.DataValueField = "JobLeftTypeId";
        SeparationmanagementCheckBoxList.DataTextField = "JobLeftType";
        SeparationmanagementCheckBoxList.DataSource = dtgrade;
        SeparationmanagementCheckBoxList.DataBind();
    }

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

    protected void companyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {

            aSeparationDAL.FinYearByCompDropDown(SeparationFinancialYearDropDownList, companyDropDownList.SelectedValue);
        }
        else
        {
            SeparationFinancialYearDropDownList.Items.Clear();
        }
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (reportDropDownList.SelectedValue == "Inquiry")
        {
            if (SeparationloadGridView.Rows.Count > 0)
            {
                string attachment = "attachment; filename=EmployeeSeparationListInfo.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                SeparationloadGridView.AllowPaging = false;



                //SeparationloadGridView.Columns[SeparationloadGridView.Columns.Count - 1].Visible =
                //            false;
                //SeparationloadGridView.Columns[SeparationloadGridView.Columns.Count - 2].Visible =
                //   false;
                //SeparationloadGridView.Columns[SeparationloadGridView.Columns.Count - 3].Visible =
                //   false;

                this.LoadInfo();

                // Create a form to contain the grid  
                HtmlForm frm = new HtmlForm();
                SeparationloadGridView.Parent.Controls.Add(frm);
                //frm.Attributes["runat"] = "server";
                //frm.Controls.Add(SeparationloadGridView);
                //frm.RenderControl(htw);

                SeparationloadGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

                // Set background color of each cell of GridView1 header row
                foreach (TableCell tableCell in SeparationloadGridView.HeaderRow.Cells)
                {
                    tableCell.Style["background-color"] = "#E5EEF1";
                }

                // Set background color of each cell of each data row of GridView1
                foreach (GridViewRow gridViewRow in SeparationloadGridView.Rows)
                {
                    gridViewRow.BackColor = System.Drawing.Color.White;

                    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                    {
                        gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                    }
                }

                SeparationloadGridView.RenderControl(htw);
                string headerTable = @"<span  style='text-align:left'><h3> " + companyDropDownList.SelectedItem.Text +
                                     "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " +
                                     DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

                string SubTi = @"<span   style='text-align:center'>
   <h3>Separation List	</h3>

</span>";

                HttpContext.Current.Response.Write(headerTable);
                HttpContext.Current.Response.Write(SubTi);

                string style = @"<style> .text { mso-number-format:\@; } </style> ";
                Response.Write(style);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                showMessageBox("No Data Found!!");
            }
        }

        if (reportDropDownList.SelectedValue == "WPPF")
        {
            if (WPPFGridView.Rows.Count > 0)
            {
                string attachment = "attachment; filename=WPPFList(All Employees)(PF).xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                WPPFGridView.AllowPaging = false;



                //SeparationloadGridView.Columns[SeparationloadGridView.Columns.Count - 1].Visible =
                //            false;
                //SeparationloadGridView.Columns[SeparationloadGridView.Columns.Count - 2].Visible =
                //   false;
                //SeparationloadGridView.Columns[SeparationloadGridView.Columns.Count - 3].Visible =
                //   false;

                this.LoadInfo();

                // Create a form to contain the grid  
                HtmlForm frm = new HtmlForm();
                WPPFGridView.Parent.Controls.Add(frm);
                //frm.Attributes["runat"] = "server";
                //frm.Controls.Add(SeparationloadGridView);
                //frm.RenderControl(htw);

                WPPFGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

                // Set background color of each cell of GridView1 header row
                foreach (TableCell tableCell in WPPFGridView.HeaderRow.Cells)
                {
                    tableCell.Style["background-color"] = "#E5EEF1";
                }

                // Set background color of each cell of each data row of GridView1
                foreach (GridViewRow gridViewRow in WPPFGridView.Rows)
                {
                    gridViewRow.BackColor = System.Drawing.Color.White;

                    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                    {
                        gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                    }
                }

                WPPFGridView.RenderControl(htw);
                string headerTable = @"<span  style='text-align:left'><h3> " + companyDropDownList.SelectedItem.Text +
                                     "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " +
                                     DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

                string SubTi = @"<span   style='text-align:center'>
   <h3>WPPF List (All Employees) (PF)	</h3>

</span>";

                HttpContext.Current.Response.Write(headerTable);
                HttpContext.Current.Response.Write(SubTi);

                string style = @"<style> .text { mso-number-format:\@; } </style> ";
                Response.Write(style);
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
        if (SeparationloadGridView.Rows.Count > 0)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Employees.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            SeparationloadGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in SeparationloadGridView.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in SeparationloadGridView.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }
            SeparationloadGridView.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
#pragma warning disable CS0612 // Type or member is obsolete
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
#pragma warning restore CS0612 // Type or member is obsolete
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            Phrase phrase = null;
            PdfPCell cell = null;
            PdfPTable table = null;
            //Color color = null;
            pdfDoc.Open();
            table = new PdfPTable(2);
            table.TotalWidth = 500f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 0.3f, 0.7f });

            //Company Logo
            cell = ImageCell("~/Assets/LogoForLeftCorner.jpg", 30f, PdfPCell.ALIGN_CENTER);
            table.AddCell(cell);
//http://localhost:30407/Assets/login page.jpg
            //Company Name and Address
            phrase = new Phrase();
            phrase.Add(new Chunk("Microsoft Northwind Traders Company\n\n", FontFactory.GetFont("Arial", 16)));
            htmlparser.Parse(sr);
            pdfDoc.Close();
          
            Response.Write(pdfDoc);
            Response.End();
            SeparationloadGridView.AllowPaging = false;
            SeparationloadGridView.DataBind();
        }
        else
        {
            showMessageBox("No Data Found!!");
        }
    }

    private static PdfPCell ImageCell(string path, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
        image.ScalePercent(scale);
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = new BaseColor(Color.White);
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 0f;
        return cell;
    }
    private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
    {
        PdfContentByte contentByte = writer.DirectContent;
        contentByte.SetColorStroke(new BaseColor(color));
        contentByte.MoveTo(x1, y1);
        contentByte.LineTo(x2, y2);
        contentByte.Stroke();
    }
    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = new BaseColor(Color.White);
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void cblHeader_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}