using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Pages_TrainingReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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

    protected void submitButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void endDateDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (endDateDropDownList.SelectedValue != "")
        {
            SetEndCritaria(endDateDropDownList.SelectedValue);
        }
    }

    private void SetEndCritaria(string selectedValue)
    {
        singleEndDate.Visible = false;
        endDateRange.Visible = false;

        if (selectedValue == 2.ToString(CultureInfo.InvariantCulture) || selectedValue == 3.ToString(CultureInfo.InvariantCulture) || selectedValue == 4.ToString(CultureInfo.InvariantCulture))
        {
            singleEndDate.Visible = true;
        }
        else if (selectedValue == 5.ToString(CultureInfo.InvariantCulture))
        {
            endDateRange.Visible = true;
        }
    }

    protected void trainingDayesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (trainingDayesDropDownList.SelectedValue != "")
        {
            SetTrainingDayesCritaria(trainingDayesDropDownList.SelectedValue);
        }
    }

    private void SetTrainingDayesCritaria(string selectedValue)
    {
        noOfDay.Visible = false;
        dayesRange.Visible = false;

        if (selectedValue == 2.ToString(CultureInfo.InvariantCulture) || selectedValue == 3.ToString(CultureInfo.InvariantCulture) || selectedValue == 4.ToString(CultureInfo.InvariantCulture))
        {
            noOfDay.Visible = true;
        }
        else if (selectedValue == 5.ToString(CultureInfo.InvariantCulture))
        {
            dayesRange.Visible = true;
        }
    }

    protected void feesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (feesDropDownList.SelectedValue != "")
        {
            SetFeesCritaria(feesDropDownList.SelectedValue);
        }
    }

    private void SetFeesCritaria(string selectedValue)
    {
        singleFee.Visible = false;
        feeRange.Visible = false;

        if (selectedValue == 2.ToString(CultureInfo.InvariantCulture) || selectedValue == 3.ToString(CultureInfo.InvariantCulture) || selectedValue == 4.ToString(CultureInfo.InvariantCulture))
        {
            singleFee.Visible = true;
        }
        else if (selectedValue == 5.ToString(CultureInfo.InvariantCulture))
        {
            feeRange.Visible = true;
        }
    }

    protected void scoreForDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (scoreForDropDownList.SelectedValue != "")
        {
            SetScoreCritaria(scoreForDropDownList.SelectedValue);
        }
    }

    private void SetScoreCritaria(string selectedValue)
    {
        singleScore.Visible = false;
        scoreRange.Visible = false;

        if (selectedValue == 2.ToString(CultureInfo.InvariantCulture) || selectedValue == 3.ToString(CultureInfo.InvariantCulture) || selectedValue == 4.ToString(CultureInfo.InvariantCulture))
        {
            singleScore.Visible = true;
        }
        else if (selectedValue == 5.ToString(CultureInfo.InvariantCulture))
        {
            scoreRange.Visible = true;
        }
    }

    protected void yearDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (yearDropDownList.SelectedValue != "")
        {
            SetAnnouncementYearCritaria(yearDropDownList.SelectedValue);
        }
    }

    private void SetAnnouncementYearCritaria(string selectedValue)
    {
        singleYear.Visible = false;
        yearRange.Visible = false;

        if (selectedValue == 2.ToString(CultureInfo.InvariantCulture) || selectedValue == 3.ToString(CultureInfo.InvariantCulture) || selectedValue == 4.ToString(CultureInfo.InvariantCulture))
        {
            singleYear.Visible = true;
        }
        else if (selectedValue == 5.ToString(CultureInfo.InvariantCulture))
        {
            yearRange.Visible = true;
        }
    }
}