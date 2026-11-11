using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.Panal_BLL;
using DAL.COMMON_DAL;

public partial class _Default : System.Web.UI.Page
{
    private const int MaxUserNameLength = 20;
    private const int MaxPasswordLength = 20;
    private static readonly Regex UserNameRegex = new Regex("^[A-Za-z0-9]{1,20}$", RegexOptions.Compiled);

    DataTable aTableLogin = new DataTable();
    PanalBLL aPanalBll = new PanalBLL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Session["UserId"] = null;
            Session["LoginName"] = null;
            Session["UserTypeId"] = null;
            Session["Designation"] = null;
            Session["DepartmentId"] = null;
            Session["DivisionId"] = null;
            Session["EmpInfoId"] = null;
            Session["EmpInfoIdLat"] = null;
            Session["CompanyId"] = null;
            Session["CompanyId_nnnn"] = null;
            Session["SalaryLoationId"] = null;
        }

    }

    
    public static string getOperatinSystemDetails(string browserDetails)
    {
        try
        {
            switch (browserDetails.Substring(browserDetails.LastIndexOf("Windows NT") + 11, 3).Trim())
            {
                case "6.2":
                    return "Windows 8";
                case "6.1":
                    return "Windows 7";
                case "6.0":
                    return "Windows Vista";
                case "5.2":
                    return "Windows XP 64-Bit Edition";
                case "5.1":
                    return "Windows XP";
                case "5.0":
                    return "Windows 2000";
                default:
                    return browserDetails.Substring(browserDetails.LastIndexOf("Windows NT"), 14);
            }
        }
        catch
        {
            if (browserDetails.Length > 149)
                return browserDetails.Substring(0, 149);
            else
                return browserDetails;
        }
    }
    protected void loginButton_Click(object sender, EventArgs e)
    {
        msgLabel.Text = "";
        string loginName = string.Empty;
        DateTime nowdate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
        string passwordText = string.Empty;
        loginName = userNameTextBox.Text.Trim();

        if (!IsValidUserName(loginName))
        {
            msgLabel.Text = string.IsNullOrEmpty(loginName)
                ? "Input User Name Please!!!"
                : "User Name allows only letters and numbers, maximum 20 characters.";
            return;
        }

        passwordText = passwordTextBox.Text.Trim();
        if (!IsValidPasswordLength(passwordText))
        {
            msgLabel.Text = string.IsNullOrEmpty(passwordText)
                ? "Input Password Please!!!"
                : "Password cannot be more than 20 characters.";
            return;
        }

      //  DateTime expdate = Convert.ToDateTime("30/06/2030");
      //  if (nowdate <= expdate)
        {
            if (!string.IsNullOrEmpty(loginName))
            {
                if (!string.IsNullOrEmpty(passwordTextBox.Text.Trim()))
                {
                    passwordText = passwordTextBox.Text.Trim();
                  //  passwordText = DataEncryptDecrypt.EncryptText(passwordTextBox.Text.Trim());

                    aTableLogin = aPanalBll.Login(loginName, passwordText);
                    if (aTableLogin.Rows.Count > 0)
                    {
                        Session["UserId"] = aTableLogin.Rows[0]["UserId"].ToString().Trim();
                        Session["LoginName"] = aTableLogin.Rows[0]["UserName"].ToString().Trim();
                        Session["UserTypeId"] = aTableLogin.Rows[0]["UserTypeId"].ToString().Trim();
                        Session["Designation"] = aTableLogin.Rows[0]["Designation"].ToString().Trim();
                        Session["DepartmentId"] = aTableLogin.Rows[0]["DepartmentId"].ToString().Trim();
                        Session["DivisionId"] = aTableLogin.Rows[0]["DivisionId"].ToString().Trim();
                        Session["isPassChanged"] = aTableLogin.Rows[0]["isPassChanged"].ToString().Trim();
                        //Session["UserType"] = aTableLogin.Rows[0]["UserType"].ToString().Trim();
                        //Session["EmpMasterCode"] = aTableLogin.Rows[0]["EmpMasterCode"].ToString().Trim();
                        Session["EmpInfoId"] = aTableLogin.Rows[0]["EmpInfoId"].ToString().Trim();
                        Session["EmpInfoIdLat"] = aTableLogin.Rows[0]["EmpInfoId"].ToString().Trim();
                        Session["CompanyId"] = aTableLogin.Rows[0]["CompanyId"].ToString().Trim();
                        Session["CompanyId_nnnn"] = aTableLogin.Rows[0]["CompanyId"].ToString().Trim();
                        Session["SalaryLoationId"] = aTableLogin.Rows[0]["SalaryLoationId"].ToString().Trim();
                     
                        Session["UserTime"] = DateTime.Now.ToString("f");




                        //aPanalBll.LoginLog(Session["UserId"].ToString(), Session["LoginName"].ToString(), DateTime.Now, ipAddress, browserName, browserVersion, operatingSystem);

                        Session["EmailID"] = "shuvono-reply@smc-bd.org";
                        Session["AppPass"] = "smc12345";

                        //try
                        //{
                            //if (Session["LoginName"].ToString() == "50088")
                            //{
                            //    Response.Redirect("DashBoard_UI/ApprovalNotificationHome.aspx");
                            //}

                            bool ISMainDash = Convert.ToBoolean(aTableLogin.Rows[0]["IsmainDasboard"].ToString().Trim());

                            if(ISMainDash==true)
                            {
                                Response.Redirect("/ChartTest/ChartNew.aspx");
                            }
                            else
                            {
                                Response.Redirect("DashBoard_UI/DashBoard.aspx");
                            }

                          
                        //}
                        //catch (Exception)
                        //{
                        //    Response.Redirect("DashBoard_UI/DashBoard.aspx");
                        //    //throw;
                        //}
                    }
                    else
                    {
                        msgLabel.Text = "Input valid User ID and Password Please!!!!";
                    }
                }
                else
                {
                    msgLabel.Text = "Input Password Please!!!";
                }
            }
            else
            {
                msgLabel.Text = "Input Use Name Please!!!";
            }
        }
       // else
        //{
            //showMessageBox("Login Failed due to Date Expeired Please contact with Creatrix !!!!");
        //}
    }

    private static bool IsValidUserName(string userName)
    {
        return !string.IsNullOrEmpty(userName)
            && userName.Length <= MaxUserNameLength
            && UserNameRegex.IsMatch(userName);
    }

    private static bool IsValidPasswordLength(string password)
    {
        return !string.IsNullOrEmpty(password)
            && password.Length <= MaxPasswordLength;
    }
}
