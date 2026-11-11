using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.UserProfileDAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class UserProfile_ChangePassword : System.Web.UI.Page
{
    private const int MinPasswordLength = 12;
    private const int MaxPasswordLength = 20;
    private static readonly Regex StrongPasswordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{12,20}$", RegexOptions.Compiled);
    private string _userId;
    ChangePasswordDAL aDAL = new ChangePasswordDAL();
    ShowMessage aShowMessage = new ShowMessage();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            if (Session["EmpInfoId"] != null && Session["EmpInfoId"] != "")
            {

                GetOneRecord(Session["EmpInfoId"].ToString());
            }
        }
    }

    private void GetOneRecord(string id)
    {

        DataTable dataTable = aDAL.GetEmployeeInfoDAL(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows[rowIndex].Field<string>("EmpImage") != "")
            {
                UserImage.ImageUrl = "~/UploadImg/" + dataTable.Rows[rowIndex].Field<string>("EmpImage");
            }
            else
            {
                UserImage.ImageUrl = "../Assets/man-icon.png";
            }


            lblshortName.Text = dataTable.Rows[rowIndex].Field<string>("EmpName");
            lblDesignation.Text = dataTable.Rows[rowIndex].Field<string>("Designation");
            lblID.Text = dataTable.Rows[rowIndex].Field<string>("EmpMasterCode");

           


        }
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (Validat())
        {
            bool status = aDAL.UpdatePass(Convert.ToInt32(_userId), txt_Password.Text.Trim());
            if (status)
            {
                Session["isPassChanged"] = "1";
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Password Updated Successfully...');window.location ='../DashBoard_UI/DashBoard.aspx';",
                  true);
            }


        }
    }

    private bool Validat()
    {
        bool isVAlid = true;
        string newPassword = txt_Password.Text.Trim();
        string confirmPassword = txtConfirm.Text.Trim();

        if (newPassword == "")
        {
            aShowMessage.ShowMessageBox("New Password is Required ", this);
            txt_Password.Focus();
            isVAlid = false;
        }


        if (confirmPassword == "")
        {
            aShowMessage.ShowMessageBox("Confirm Password is Required ", this);
            txtConfirm.Focus();
            isVAlid = false;
        }

        if (!isVAlid)
        {
            return false;
        }

        if (IsSameAsCurrentPassword(newPassword))
        {
            aShowMessage.ShowMessageBox("New Password cannot be same as Old Password ", this);
            txt_Password.Focus();
            return false;
        }

        if (!IsStrongPassword(newPassword))
        {
            aShowMessage.ShowMessageBox("Password must be 12 to 20 characters and contain lowercase, uppercase, numeric digit, and special character. ", this);
            txt_Password.Focus();
            return false;
        }

        if (newPassword != confirmPassword)
        {
            aShowMessage.ShowMessageBox("Password is not Matched ", this);
            txtConfirm.Focus();
            return false;
        }

        return true;
    }

    private bool IsSameAsCurrentPassword(string newPassword)
    {
        DataTable dataTable = aDAL.GetUserPassInfoDAL(_userId);
        if (dataTable.Rows.Count == 0)
        {
            return false;
        }

        string currentPassword = Convert.ToString(dataTable.Rows[0]["Password"]);
        return string.Equals(currentPassword, newPassword, StringComparison.Ordinal);
    }

    private static bool IsStrongPassword(string password)
    {
        return !string.IsNullOrEmpty(password)
            && password.Length >= MinPasswordLength
            && password.Length <= MaxPasswordLength
            && StrongPasswordRegex.IsMatch(password);
    }
}
