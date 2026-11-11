using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class AllPrintLetter_plToWhomITMayConcern : System.Web.UI.Page
{

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private int mid = 0;
    private string _userId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ButtonVisible();
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        var emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                        empMasterCode.Text =
                            emp.EmpMasterCode;
                        if (emp.Gender=="Male")
                        {
                            txtName.Text="Mr. "+ emp.EmpName;
                         }

                        if (emp.Gender == "FeMale")
                        {
                            txtName.Text = "Ms. " + emp.EmpName;
                        }

                        Session["CompanyId"] = "";
                        Session["CompanyId"] = emp.CompanyId;

                        using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                        {
                            lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();
                            txtJobLength.Text = dtdesignation.Rows[0]["LengthServicewithSMC"].ToString();

                        }

                        lblEmpName.Text = emp.EmpName;
                        txtPassport.Text = emp.PassportNo;


                        using (var dbc = new HRIS_SMC_DBEntities())
                        {
                            
                            var dataload = dbc.tblITMayConcerns.Where(a => a.EmployeeId == mid).ToList();


                            loadGridView.DataSource = dataload;
                            loadGridView.DataBind();
                        }
                    }
                }
            }
        }
    }
    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                submitButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("~/UserSetup/EmployeeInfoList.aspx");
        }

    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
      
    }

    protected void EmployeeNameTextBox_OnTextChanged(object sender, EventArgs e)
    {
        string empName = EmployeeNameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            EmployeeNameTextBox.Text = emp[2];
            repEmpIdHiddenField.Value = emp[0];
           

        }
        else
        {

            EmployeeNameTextBox.Text = "";
            repEmpIdHiddenField.Value = "";
            ShowMessageBox("Input Correct Data !!");
        }
    }


    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            tblITMayConcern Bat = null;


            try
            {
                using (var db = new HRIS_SMC_DBEntities())
                {
                    if (mid > 0)
                    {

                        Bat = new tblITMayConcern();


                        Bat.EmployeeId = int.Parse(hdpk.Value) > 0 ? int.Parse(hdpk.Value) : (int?)null;
                        Bat.Header = string.IsNullOrEmpty(lblHeader.Text) ? null : lblHeader.Text;
                        Bat.FirstPara = txtOne.Text + ' ' + txtName.Text + ' ' + txtTwo.Text + ' ' + txtJobLength.Text + ". " + txtThree.Text + ' ' + txtFreeThree.Text + ' ' + txtFour.Text + ' ' + txtFromDate.Text + ' ' + txtTo.Text + ' ' + txtToDate.Text + ". " + txtFive.Text + ' ' + txtPassport.Text+ " . " ;

                        Bat.SeceondPara = txtPara2.Text;

                        Bat.ThirdPara = txtpara3.Text;
                        Bat.ToEmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
                        Bat.EntryBy = Convert.ToString(Session["LoginName"].ToString());
                        Bat.EntryDate = DateTime.Now;
                        db.tblITMayConcerns.Add(Bat);
                        db.SaveChanges();

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
"alert",
"alert('Operation Successful...! ');",
true);
                        PopUp(Bat.ITMayConcernId);

                        using (var dbc = new HRIS_SMC_DBEntities())
                        {

                            var dataload = dbc.tblITMayConcerns.Where(a => a.EmployeeId == mid).ToList();


                            loadGridView.DataSource = dataload;
                            loadGridView.DataBind();
                        }

                    }
                    else
                    {
                       

                    }

          
                }
            }
            catch (Exception)
            {


            }
        }

    }

    private bool Validation()
    {



        if (txtFreeThree.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            txtFreeThree.Focus();
            return false;
        }
        if (txtFromDate.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            txtFromDate.Focus();
            return false;
        }

        if (txtToDate.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            txtToDate.Focus();
            return false;
        }

        if (EmployeeNameTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            EmployeeNameTextBox.Focus();
            return false;
        }



      




        return true;
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
       Response.Redirect("/UserSetup/EmployeeInfoList.aspx"); 
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        PopUp(Convert.ToInt32(e.CommandArgument.ToString()));
    }

    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/ITMayConcernReportViewer.aspx?rptType=" + EmpInfoId + "&rptT=ITFromEmpInfo"  ;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }
}