using DAL.TrainingDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Training_TrainingCalendar : System.Web.UI.Page
{

    private TrainingDAL _trainingDal = new TrainingDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadCalander();
    }

    private void LoadCalander()
    {
        DataTable dt = _trainingDal.GetTrainingCalander();


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i > 0)
            {
                if (dt.Rows[i]["TrainingBudgetMasterId"].ToString() == dt.Rows[i - 1]["TrainingBudgetMasterId"].ToString())
                {
                    dt.Rows[i]["TrainingTitle"] = "";
                    dt.Rows[i]["FinancialYearDesc"] = "";
                    dt.Rows[i]["CostParticipant"] ="";
                    dt.Rows[i]["Budget"] = "";
                    dt.Rows[i]["exInt"] = "";
                    dt.Rows[i]["Duration"] = "";
                    
                }
            }
        }


        gv_trainingCal.DataSource = dt;
        gv_trainingCal.DataBind();
    }


}