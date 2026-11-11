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

public partial class AllPrintLetter_plSeparation : System.Web.UI.Page
{

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    int EmpID ;
    int JobLeftID ;
    string _userId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            using (var db = new HRIS_SMC_DBEntities())
            {
                if (!string.IsNullOrEmpty(Request.QueryString["EmpId"]))
                {
                     EmpID = int.Parse(Request.QueryString["EmpId"]);
                    hdpk.Value = EmpID.ToString();
                    if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
                    {
                         JobLeftID = int.Parse(Request.QueryString["mid"]);
                        JobLeftIddPK.Value = JobLeftID.ToString();
                        var Checkexist =
                            (from j in db.tblSeparationITMayConcerns where j.EmployeeId == EmpID && j.JobleftTypeID==JobLeftID select j).FirstOrDefault
                                ();


                        if (Checkexist==null)
                        {
                            using (var dbemp = new HRIS_SMCEntities())
                            {
                                var emp =
                                    (from j in dbemp.tblEmpGeneralInfoes where j.EmpInfoId == EmpID select j)
                                        .FirstOrDefault();

                               
                                Session["CompanyId"] = emp.CompanyId;
                                empMasterCode.Text = emp.EmpMasterCode;



                                string SeparationDate = "";

                               

                                        using (DataTable dtdesignation = _commonDataLoad.GetSeparationByEmpId(JobLeftID)
                                            )
                                        {
                                            SeparationDate =
                                                Convert.ToDateTime(dtdesignation.Rows[0]["JobLeftDate"])
                                                    .ToString("dd-MMM-yyyy");


                                        }
                                

                                  string JoinDate = "";
                 
                        try
                        {
                                JoinDate=  Convert.ToDateTime(emp.DateOfJoin).ToString("dd-MMM-yyyy");

                        }
                        catch (Exception)
                        {
                            
                            //throw;
                        }

                        string CompanyName = "";
                        string JobLength = "";
                        string Designation = "";
                        string EmpCat = "";
                        using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(EmpID))
                        {
                            lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();
                            Designation = dtdesignation.Rows[0]["Designation"].ToString();
                              CompanyName= dtdesignation.Rows[0]["CompanyName"].ToString();
                              JobLength = dtdesignation.Rows[0]["LengthServicewithSMC"].ToString();
                              EmpCat = dtdesignation.Rows[0]["EmpCategoryName"].ToString();

                        }

                      
                       

                        string name = "";
                         if (emp.Gender=="Male")
                        {
                            name="Mr. "+ emp.EmpName;
                         }

                        if (emp.Gender == "FeMale")
                        {
                           name = "Ms. " + emp.EmpName;
                        }
                        string MasterId = emp.EmpMasterCode;

                        txtFirstPara.Text = "We are Pleased to certify that " + name + " bearing ID # " + MasterId +
                                            " was in the employment of " + CompanyName + ", a subsiidiary of " +
                                            CompanyName + " for the period " + JoinDate + " to " + SeparationDate + ". He Joined here as " + Designation+ ", in " + EmpCat+". ";



                        txtPara2.Text = "During these " + JobLength + " association with us, he was found sincere, honest and dutiful towards his duties.";

                        txtpara3.Text = "We wish him all the best.";
                        lblEmpName.Text = emp.EmpName;

                        submitButton.Visible = true;
                        
                            }

                        }
                        else
                        {



                            using (var dbs = new HRIS_SMC_DBEntities())
                            {
                              var  sep =  (from j in dbs.tblSeparationITMayConcerns where j.EmployeeId == EmpID && j.JobleftTypeID == JobLeftID select j).FirstOrDefault();

                                MaiMasterId.Value = sep.SeparationITMayConcern.ToString();

                                using (var dbemp = new HRIS_SMCEntities())
                                {
                                    var emp =
                                        (from j in dbemp.tblEmpGeneralInfoes where j.EmpInfoId == EmpID select j)
                                            .FirstOrDefault();


                                    Session["CompanyId"] = emp.CompanyId;
                                    empMasterCode.Text = emp.EmpMasterCode;
                                    lblEmpName.Text = emp.EmpName;
                                }

                                using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(EmpID))
                                {
                                    lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();
                                }

                                txtFirstPara.Text = sep.FirstPara;
                                txtPara2.Text = sep.SeceondPara;
                                txtpara3.Text = sep.ThirdPara;
                                repEmpIdHiddenField.Value=sep.ToEmployeeId.ToString();

                                using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(EmpID))
                                {
                                    EmployeeNameTextBox.Text = dtdesignation.Rows[0]["SignatureEmployee"].ToString();
                                }
                                editButton.Visible = true;

                            }

                            
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
            Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntryView.aspx");
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


             
            EmpID = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            JobLeftID = string.IsNullOrEmpty(JobLeftIddPK.Value) ? 0 : int.Parse(JobLeftIddPK.Value);
            tblSeparationITMayConcern Bat = null;


            try
            {
                using (var db = new HRIS_SMC_DBEntities())
                {


                    Bat = new tblSeparationITMayConcern();


                    Bat.EmployeeId = int.Parse(hdpk.Value) > 0 ? int.Parse(hdpk.Value) : (int?)null;
                    Bat.JobleftTypeID = int.Parse(JobLeftIddPK.Value) > 0 ? int.Parse(JobLeftIddPK.Value) : (int?)null;
                        Bat.Header = string.IsNullOrEmpty(lblHeader.Text) ? null : lblHeader.Text;
                    Bat.FirstPara = txtFirstPara.Text.Trim();

                    Bat.SeceondPara = txtPara2.Text.Trim();

                        Bat.ThirdPara = txtpara3.Text;
                        Bat.ToEmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
                        Bat.EntryBy = Convert.ToString(Session["LoginName"].ToString());
                        Bat.EntryDate = DateTime.Now;
                        db.tblSeparationITMayConcerns.Add(Bat);
                        db.SaveChanges();

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
"alert",
"alert('Operation Successful...! ');",
true);
                      //  PopUp(Bat.ITMayConcernId);
                        PopUp(Bat.SeparationITMayConcern);

                    


                }
            }
            catch (Exception)
            {


            }
        }

    }

    private bool Validation()
    {


 

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
        if (MaiMasterId.Value!="")
        {
            if (Validation())
            {


                int masterIDD = string.IsNullOrEmpty(MaiMasterId.Value) ? 0 : int.Parse(MaiMasterId.Value);
                EmpID = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
                JobLeftID = string.IsNullOrEmpty(JobLeftIddPK.Value) ? 0 : int.Parse(JobLeftIddPK.Value);
                tblSeparationITMayConcern upBat = null;


                try
                {
                    using (var db = new HRIS_SMC_DBEntities())
                    {

                        var up = (from j in db.tblSeparationITMayConcerns where j.SeparationITMayConcern == masterIDD select j).FirstOrDefault();




                        up.FirstPara = txtFirstPara.Text.Trim();

                        up.SeceondPara = txtPara2.Text.Trim();

                        up.ThirdPara = txtpara3.Text;
                        up.ToEmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
                        up.UpdateBy = Convert.ToInt32(Session["UserId"]); ;
                        up.UpdateDate = DateTime.Now;
                        
                        db.SaveChanges();

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
"alert",
"alert('Operation Successful...! ');",
true);
                        PopUp(up.SeparationITMayConcern);





                    }
                }
                catch (Exception)
                {


                }
            }
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntryView.aspx"); 
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        PopUp(Convert.ToInt32(e.CommandArgument.ToString()));
    }

    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/ITMayConcernReportViewer.aspx?rptType=" + EmpInfoId + "&rptT=ITSeparInfo";
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }
}