using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Pages_DiciplinaryActionReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //LoadDropDownList();
        }
    }

    private void LoadDropDownList()
    {
        //aDepartmentInformationDal.GetCompanyListIntoDropdown(companyDropDownList);
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        dateRange.Visible = false;
        singleDate.Visible = false;
        actionDate.SelectedValue = 1.ToString(CultureInfo.InvariantCulture);
        discActionDropDownList.SelectedIndex = 0;
    }

    protected void actionDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (actionDate.SelectedValue != "")
        {
            SetCritaria(actionDate.SelectedValue);
        }
    }

    private void SetCritaria(string selectedValue)
    {
        singleDate.Visible = false;
        dateRange.Visible = false;

        if (selectedValue == 2.ToString(CultureInfo.InvariantCulture) || selectedValue == 3.ToString(CultureInfo.InvariantCulture) || selectedValue == 4.ToString(CultureInfo.InvariantCulture))
        {
            singleDate.Visible = true;
        }
        else if (selectedValue == 5.ToString(CultureInfo.InvariantCulture))
        {
            dateRange.Visible = true;
        }
    }
}