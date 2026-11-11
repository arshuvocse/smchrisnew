using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO_EF;

public partial class UserSetup_UserEntry : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private  UserDAL _userDal = new UserDAL();
    private int mid = 0;
    private string _userId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }

        if (!IsPostBack)
        {
            ButtonVisible();
            
            LoadInitialDDL();

            radUserType.Items[0].Selected=true;
           
            radUserType_OnSelectedIndexChanged(null, null); 
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                if (mid > 0)
                {
                  
                    using (var db = new HRIS_SMCEntities())
                    {
                        var user = (from u in db.tblUsers where u.UserId == mid select u).FirstOrDefault();

                        foreach (ListItem item in lchk_Company.Items)
                        {
                            int CompanyId = int.Parse(item.Value);
                            var UserCompanyMaping = (from j in db.tblUserCompanyMapings where j.UserId == mid && j.CompanyId == CompanyId select j).FirstOrDefault();

                            if (UserCompanyMaping != null)
                            {
                                item.Selected = true;
                            }
                        }
                        lchk_Company_OnSelectedIndexChanged(null, null);
                        ddlJobLocation.SelectedValue = user.JobLocationID.ToString();
                        //lchk_Company.SelectedValue = user.CompanyId.ToString();
                        //ddlCompany_SelectedIndexChanged(null, null);

                        if (user.UserTypeId==4)
                        {
                            IsSupperAdminCheckBox.Checked=true;
                        }

                        
                        radUserType_OnSelectedIndexChanged(null,null);
                        radUserType.SelectedItem.Text = "Employee";
                        if (radUserType.SelectedItem.Text == "Employee")
                        {
                            iddivEmp.Visible = true;
                            iddivEmpDesig.Visible = true;
                            hdEmpInfoId.Value = user.EmpInfoId.ToString();
                            string emp = (from em in db.tblEmpGeneralInfoes
                                join s in db.tblDesignations on em.DesignationId equals s.DesignationId
                                where em.EmpInfoId == user.EmpInfoId
                                select em.EmpInfoId + ">" + em.EmpName + ">" + s.Designation).FirstOrDefault();

                            if (emp!=null)
                            {
                                txt_Emp.Text = emp.Split('>')[1];
                                txt_EmpDesig.Text = emp.Split('>')[2];
                            }
                          
                        }

                      

                        txt_Username.Text = user.UserName;
                        var password = (user.Password);
                        txt_Password.Text = password;
                        txt_Email.Text = user.Email;
                        txt_Mobile.Text = user.ContactNo;
                        chk_IsActive.Checked = (bool) user.IsActive;

                        if (user.IsmainDasboard!=null)
                        {
                            if (user.IsmainDasboard == true)
                            {
                                rbDashboard.Items[0].Selected = true;
                            }
                            else
                            {
                                rbDashboard.Items[1].Selected = true;
                            }
                        }
                        else
                        {
                            rbDashboard.Items[1].Selected = true;
                        }

                        using (DataTable dt = _userDal.GetUserDepartmentPermissionByUserId(mid))
                        {
                            if (dt.Rows.Count > 0)
                            {
                                //lchk_Department.DataSource = dt;
                                //lchk_Department.DataValueField = "Value";
                                //lchk_Department.DataTextField = "TextField";
                                //lchk_Department.DataBind();
                                try
                                {

                                
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (bool.Parse(dt.Rows[i]["IsActive"].ToString()) == true && lchk_Department.Items[i].Value == dt.Rows[i]["Value"].ToString())
                                    {
                                        lchk_Department.Items[i].Selected = true;
                                    }
                                    
                                }
                                }
                                catch (Exception ex)
                                {

                                    
                                }
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
                btn_Save.Visible = true;
                btn_Save.Text = "Save";
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                btn_Save.Visible = true;
                btn_Save.Text = "Update";
            }

            else if (Session["Status"].ToString() == "View")
            {
                btn_Save.Visible = false; 
            }
            
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("UserList.aspx");
        }

    }
    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetAllCompany())
        {
            lchk_Company.DataSource = dt;
            lchk_Company.DataValueField = "Value";
            lchk_Company.DataTextField = "TextField";
            lchk_Company.DataBind();
        }

        using (DataTable dt = _commonDataLoad.GetUserType())
        {
            radUserType.DataSource = dt;
            radUserType.DataValueField = "Value";
            radUserType.DataTextField = "TextField";
            radUserType.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
        {
            ddlJobLocation.DataSource = dt;
            ddlJobLocation.DataValueField = "Value";
            ddlJobLocation.DataTextField = "TextField";
            ddlJobLocation.DataBind();
        }
    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("UserList.aspx");
    }

    //protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Session["cid"] = radCompany.SelectedValue;
    //    using (DataTable dt = _commonDataLoad.GetDepartmentByCompanyId(int.Parse(radCompany.SelectedValue)))
    //    {
    //        lchk_Department.DataSource = dt;
    //        lchk_Department.DataValueField = "Value";
    //        lchk_Department.DataTextField = "TextField";
    //        lchk_Department.DataBind();
    //    }
    //}

    protected void txt_Username_OnTextChanged(object sender, EventArgs e)
    {
        var Username = txt_Username.Text;
        try
        {
            using (var db = new HRIS_SMCEntities())
            {
                tblUser user = (from j in db.tblUsers where j.UserName == Username select j).FirstOrDefault();
                if (user != null)
                {
                    if (user.UserName.ToLower() == Username.ToLower())
                    {
                        AlertMessageBoxShow("Username " + Username + " already exists...! Please use another one.");
                        txt_Username.Text = String.Empty;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        SaveInformation();
    }

    private void SaveInformation()
    {
        try
        {
            var userType = radUserType.SelectedValue;

            #region validation


            if (string.IsNullOrEmpty(txt_Emp.Text))
            {
                AlertMessageBoxShow("Employee Name required...");
                txt_Emp.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_Username.Text))
            {
                AlertMessageBoxShow("Username required...");
                txt_Username.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_Password.Text))
            {
                AlertMessageBoxShow("Password required...");
                txt_Password.Focus();
                return;
            }

            if (lchk_Company.SelectedIndex < 0)
            {
                AlertMessageBoxShow("Company required...");
                lchk_Company.Focus();
                return;
            }
            if (string.IsNullOrEmpty(userType))
            {
                AlertMessageBoxShow("User type required...");
                return;
            }

            //if (lchk_Department.SelectedIndex < 0)
            //{
            //    AlertMessageBoxShow("Department required...");
            //    return;
            //}
            if (radUserType.SelectedItem.Text == "Employee")
            {
                //if (string.IsNullOrEmpty(hdEmpInfoId.Value))
                //{
                //    AlertMessageBoxShow("Employee required...");
                //    return;
                //}
            }

            //if (ddlJobLocation.SelectedIndex <= 0)
            //{
            //    AlertMessageBoxShow("Job Location required...");
            //    ddlJobLocation.Focus();
            //    return;
            //}
            #endregion
            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            var Username = txt_Username.Text;
            var Password = txt_Password.Text.Trim();
            //DataEncryptDecrypt.EncryptText(txt_Password.Text);
            tblUser user = null;
            using (var db = new HRIS_SMCEntities())
            {
                if (mid > 0)
                {////Edit Mode
                    user = (from j in db.tblUsers where j.UserId == mid select j).FirstOrDefault();
                    //user.CompanyId = int.Parse(lchk_Company.SelectedValue);
                    user.UserTypeId = int.Parse(userType);
                    if (IsSupperAdminCheckBox.Checked)
                    {
                        user.UserTypeId = int.Parse(4.ToString());
                    }
                    user.UserName = Username;
                    user.Password = Password;
                    user.Email = txt_Email.Text;
                    user.ContactNo = txt_Mobile.Text;
                    user.IsActive = chk_IsActive.Checked;
                    user.JobLocationID = int.Parse(ddlJobLocation.SelectedValue);
                    user.UpdateBy = _userId;
                    user.UpdateDate = DateTime.Now;

                    if (rbDashboard.Items[0].Selected == true)
                    {
                        user.IsmainDasboard = true;
                    }

                    else if (rbDashboard.Items[1].Selected == true)
                    {
                        user.IsmainDasboard = false;
                    }
                    else
                    {
                        user.IsmainDasboard = false;
                    }
                    if (radUserType.SelectedItem.Text == "Employee")
                    {
                        if (!string.IsNullOrEmpty(hdEmpInfoId.Value))
                        {
                            user.EmpInfoId = int.Parse(hdEmpInfoId.Value);
                        }
                    }
                    else
                    {
                        user.EmpInfoId = null;

                    }
                    if (hdEmpInfoId.Value == string.Empty)
                    {
                        user.EmpInfoId = null;
                    }



                    db.Database.ExecuteSqlCommand("delete   from tblUserCompanyMaping where UserId ={0}", mid);
                    foreach (ListItem item in lchk_Company.Items)
                    {
                        int CompanyId = int.Parse(item.Value);
                        if (item.Selected)
                        {
                            var UserCompanyMaping = (from j in db.tblUserCompanyMapings where j.UserId == mid && j.CompanyId == CompanyId select j).FirstOrDefault();
                            if (UserCompanyMaping == null)
                            {
                                UserCompanyMaping = new tblUserCompanyMaping();
                                UserCompanyMaping.UserId = mid;
                                UserCompanyMaping.CompanyId = CompanyId;
                                UserCompanyMaping.IsActive = true;
                                db.tblUserCompanyMapings.Add(UserCompanyMaping);
                            }
                            else
                            {
                                UserCompanyMaping.IsActive = true;
                            }
                        }
                        else////Not Checked
                        {
                            var UserCompanyMaping = (from j in db.tblUserCompanyMapings where j.UserId == mid && j.CompanyId == CompanyId select j).FirstOrDefault();
                            if (UserCompanyMaping != null)
                            {
                                UserCompanyMaping.IsActive = false;
                            }
                        }
                    }


                    db.Database.ExecuteSqlCommand("delete   from tblUserDepartmentPermission where UserId ={0}", mid);
                    foreach (ListItem item in lchk_Department.Items)
                    {
                        int DepartmentId = int.Parse(item.Value);
                        if (item.Selected)
                        {
                            tblUserDepartmentPermission userDepartmentPermission = (from ud in db.tblUserDepartmentPermissions
                                                                                    where ud.UserId == mid & ud.DepartmentId == DepartmentId
                                                                                    select ud).FirstOrDefault();
                            if (userDepartmentPermission == null)
                            {
                                userDepartmentPermission = new tblUserDepartmentPermission();
                                userDepartmentPermission.UserId = user.UserId;
                                userDepartmentPermission.DepartmentId = DepartmentId;
                                userDepartmentPermission.IsActive = true;
                                userDepartmentPermission.EntryBy = _userId;
                                userDepartmentPermission.EntryDate = DateTime.Now;
                                db.tblUserDepartmentPermissions.Add(userDepartmentPermission);
                            }
                            else
                            {
                                userDepartmentPermission.UserId = user.UserId;
                                userDepartmentPermission.DepartmentId = DepartmentId;
                                userDepartmentPermission.IsActive = true;
                                userDepartmentPermission.UpdateBy = _userId;
                                userDepartmentPermission.UpdateDate = DateTime.Now;
                            }
                        }
                        else
                        {
                            tblUserDepartmentPermission userDepartmentPermission = (from ud in db.tblUserDepartmentPermissions
                                                                                    where ud.UserId == mid & ud.DepartmentId == DepartmentId
                                                                                    select ud).FirstOrDefault();
                            if (userDepartmentPermission != null)
                            {
                                userDepartmentPermission.IsActive = false;
                                userDepartmentPermission.UpdateBy = _userId;
                                userDepartmentPermission.UpdateDate = DateTime.Now;
                            }
                        }
                    }
                    db.SaveChanges();
                }
                else
                {////New Mode
                    user = new tblUser();
                    {
                        //CompanyId = int.Parse(lchk_Company.SelectedValue),
                        user.UserTypeId = int.Parse(userType);
                        if (IsSupperAdminCheckBox.Checked)
                        {
                            user.UserTypeId = int.Parse(4.ToString());
                        }

                        user.UserName = Username;
                        user.Password = Password;
                        user.Email = txt_Email.Text;
                        user.ContactNo = txt_Mobile.Text;
                        user.JobLocationID = int.Parse(ddlJobLocation.SelectedValue);
                        user.EntryBy = _userId;
                        user.EntryDate = DateTime.Now;
                        user.IsActive = chk_IsActive.Checked;

                        if (rbDashboard.Items[0].Selected == true)
                        {
                            user.IsmainDasboard = true;
                        }

                        else if (rbDashboard.Items[1].Selected == true)
                        {
                            user.IsmainDasboard = false;
                        }
                        else
                        {
                            user.IsmainDasboard = false;
                        }

                    };



                    if (radUserType.SelectedItem.Text == "Employee")
                    {
                        if (!string.IsNullOrEmpty(hdEmpInfoId.Value))
                        {
                            user.EmpInfoId = int.Parse(hdEmpInfoId.Value);
                        }
                    }
                    else
                    {
                        user.EmpInfoId = null;
                    }
                    if (hdEmpInfoId.Value == string.Empty)
                    {
                        user.EmpInfoId = null;
                    }
                    db.tblUsers.Add(user);
                    db.SaveChanges();

                    foreach (ListItem item in lchk_Company.Items)
                    {
                        int CompanyId = int.Parse(item.Value);
                        if (item.Selected)
                        {
                            var UserCompanyMaping = new tblUserCompanyMaping();
                            UserCompanyMaping.UserId = user.UserId;
                            UserCompanyMaping.CompanyId = CompanyId;
                            UserCompanyMaping.IsActive = true;
                            db.tblUserCompanyMapings.Add(UserCompanyMaping);
                        }
                    }

                    foreach (ListItem item in lchk_Department.Items)
                    {
                        if (item.Selected)
                        {
                            tblUserDepartmentPermission userDepartmentPermission = new tblUserDepartmentPermission();
                            userDepartmentPermission.UserId = user.UserId;
                            userDepartmentPermission.DepartmentId = int.Parse(item.Value);
                            userDepartmentPermission.IsActive = true;
                            userDepartmentPermission.EntryBy = _userId;
                            userDepartmentPermission.EntryDate = DateTime.Now;
                            db.tblUserDepartmentPermissions.Add(userDepartmentPermission);
                        }
                    }
                    db.SaveChanges();
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...');window.location ='UserEntry.aspx';",
                true);

            //AlertMessageBoxShow("Operation Successful...");
            //System.Threading.Thread.Sleep(2000);
            //Response.Redirect("UserEntry.aspx");
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("UserEntry.aspx");
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    public bool IsValidEmailAddress(string email)
    {
        try
        {
            var emailChecked = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void txt_Email_OnTextChanged(object sender, EventArgs e)
    {
        var Email = txt_Email.Text;
        if (!IsValidEmailAddress(Email))
        {
            txt_Email.Text=String.Empty;
            AlertMessageBoxShow("Not a valid email address...!");
            return;
        }
        try
        {
            using (var db = new HRIS_SMCEntities())
            {
                tblUser user = (from j in db.tblUsers where j.Email == Email select j).FirstOrDefault();
                if (user != null)
                {
                    if (user.Email.ToLower() == Email.ToLower())
                    {
                        AlertMessageBoxShow("Email " + Email + " already exists...! Please use another one.");
                        txt_Email.Text = String.Empty;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }

    protected void txt_Mobile_OnTextChanged(object sender, EventArgs e)
    {
        var Mobile = txt_Mobile.Text;
        try
        {
            using (var db = new HRIS_SMCEntities())
            {
                tblUser user = (from j in db.tblUsers where j.ContactNo == Mobile select j).FirstOrDefault();
                if (user != null)
                {
                    if (user.ContactNo.ToLower() == Mobile.ToLower())
                    {
                        AlertMessageBoxShow("Mobile " + Mobile + " already exists...! Please use another one.");
                        txt_Mobile.Text = String.Empty;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }

    protected void radUserType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (radUserType.SelectedItem.Text == "Employee")
        {
            iddivEmp.Visible = true;
            iddivEmpDesig.Visible = true;
        }
        else
        {
            iddivEmp.Visible = false;
            iddivEmpDesig.Visible = false;
            hdEmpInfoId.Value = string.Empty;
        }
        if (radUserType.SelectedItem.Text == "Admin" || radUserType.SelectedItem.Text == "Super Admin")
        {
            foreach (ListItem dept in lchk_Department.Items)
            {
                dept.Selected = true;
            }
        }
        else
        {
            foreach (ListItem dept in lchk_Department.Items)
            {
                dept.Selected = false;
            }
        }
    }

    protected void txt_Emp_OnTextChanged(object sender, EventArgs e)
    {
        string Emp = txt_Emp.Text;


        if (!string.IsNullOrEmpty(Emp) && Emp.Length>10)
        {
            hdEmpInfoId.Value = Emp.Split(':')[0];
            txt_Emp.Text = Emp.Split(':')[2];
            txt_EmpDesig.Text = Emp.Split(':')[3];

            LoadEmpInfo(hdEmpInfoId.Value);
        }
        else
        {
            hdEmpInfoId.Value = "";
            txt_Emp.Text = "";
            txt_EmpDesig.Text = "";
            ShowMessageBox("Input Correct Data !!");
            txt_Email.Text = "";
            txt_Mobile.Text = "";
        }
        
    }

    private void LoadEmpInfo(string EmpID)
    {
        DataTable dtdata = _commonDataLoad.GetEmpDataConatct(EmpID);
        if (dtdata.Rows.Count > 0)
        {

            txt_Email.Text = dtdata.Rows[0]["OfficialEmail"].ToString();
            txt_Mobile.Text = dtdata.Rows[0]["PersonalMobile"].ToString();
        }
        else
        {

        }
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void lchk_Company_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedCompanys = string.Empty;
        foreach (ListItem item in lchk_Company.Items)
        {
            if (item.Selected)
            {
                selectedCompanys = selectedCompanys + item.Value + ",";
            }
        }
        ////getting selected company by a comma separated string and remove last comma
        if (!string.IsNullOrEmpty(selectedCompanys))
        {
            selectedCompanys = selectedCompanys.Remove(selectedCompanys.Length - 1);
            Session["selectedCompanys"] = selectedCompanys;
            using (DataTable dt = _commonDataLoad.GetDepartmentBySelectedCompany(selectedCompanys))
            {
                lchk_Department.DataSource = dt;
                lchk_Department.DataValueField = "Value";
                lchk_Department.DataTextField = "TextField";
                lchk_Department.DataBind();
            }
        }
        else
        {
            lchk_Department.Items.Clear();
        }

        SelectUnseclect.Checked = false;

    }

    protected void SelectUnseclect_OnCheckedChanged(object sender, EventArgs e)
    {
        if (SelectUnseclect.Checked==true)
        {
            foreach (ListItem li in lchk_Department.Items)
            {
                li.Selected = true;
            }
         }
        else
        {
            foreach (ListItem li in lchk_Department.Items)
            {
                li.Selected = false;
            } 
        }
      
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        SaveInformation();
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        tblUser Bat = null;



        using (var db = new HRIS_SMCEntities())
        {
            //Bat = (from j in db.tblUsers where j.UserId == mId select j).FirstOrDefault();

            //db.tblProductInfoes.Remove(Bat);
            //db.SaveChanges();
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //    "alert",
            //    "alert('Operation Successful...! ');window.location ='UserEntry.aspx';",
            //    true);


        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}