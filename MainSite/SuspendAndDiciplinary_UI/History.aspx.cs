using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.SuspendAndDiciplinary_Dal;

public partial class SuspendAndDiciplinary_UI_History : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void DiciplinaryAction()
    {
        loadGridView.Columns.Clear();
        BoundField aBoundField = null;
        aBoundField = new BoundField();
        aBoundField.DataField = "EmpMasterCode";
        aBoundField.HeaderText = "Employee Code";
        loadGridView.Columns.Add(aBoundField);


        aBoundField = new BoundField();
        aBoundField.DataField = "EmpName";
        aBoundField.HeaderText = "Employee Name";
        loadGridView.Columns.Add(aBoundField);

        aBoundField = new BoundField();
        aBoundField.DataField = "EffectiveDate";
        aBoundField.HeaderText = "Effective Date";
        loadGridView.Columns.Add(aBoundField);

        aBoundField = new BoundField();
        aBoundField.DataField = "Description";
        aBoundField.HeaderText = "Description";
        loadGridView.Columns.Add(aBoundField);

        aBoundField = new BoundField();
        aBoundField.DataField = "Remarks";
        aBoundField.HeaderText = "Remarks";
        loadGridView.Columns.Add(aBoundField);

        //aBoundField = new BoundField();
        //aBoundField.DataField = "isWarningLetter";
        //aBoundField.HeaderText = "Warning Letter";
        //loadGridView.Columns.Add(aBoundField);

        //aBoundField = new BoundField();
        //aBoundField.DataField = "isHoldIncrement";
        //aBoundField.HeaderText = "Hold Increment";
        //loadGridView.Columns.Add(aBoundField);

        //aBoundField = new BoundField();
        //aBoundField.DataField = "isHoldIncentive";
        //aBoundField.HeaderText = "Hold Incentive";
        //loadGridView.Columns.Add(aBoundField);
        
        //aBoundField = new BoundField();
        //aBoundField.DataField = "isTermination";
        //aBoundField.HeaderText = "Termination";
        //loadGridView.Columns.Add(aBoundField);
        
        //aBoundField = new BoundField();
        //aBoundField.DataField = "isDeductionOfSalary";
        //aBoundField.HeaderText = "Deduction Of Salary";
        //loadGridView.Columns.Add(aBoundField);
        
        //aBoundField = new BoundField();
        //aBoundField.DataField = "is7DaysSalaryDeduction";
        //aBoundField.HeaderText = "7 Days Salary Deduction";
        //loadGridView.Columns.Add(aBoundField);



        DiciplinaryActionDal actionDal = new DiciplinaryActionDal();
        DataTable dtdata = actionDal.GetDiciplinaryActionInfo(empCodeTexBox.Text);
        loadGridView.DataSource = dtdata;
        loadGridView.DataBind();
    }

    public void SuspandView()
    {
        loadGridView.Columns.Clear();
        BoundField aBoundField = null;
        aBoundField= new BoundField();
        aBoundField.DataField = "EmpMasterCode";
        aBoundField.HeaderText = "Employee Code";
        loadGridView.Columns.Add(aBoundField);


        aBoundField = new BoundField();
        aBoundField.DataField = "EmpName";
        aBoundField.HeaderText = "Employee Name";
        loadGridView.Columns.Add(aBoundField);
        
        aBoundField = new BoundField();
        aBoundField.DataField = "EffectiveDate";
        aBoundField.HeaderText = "Effective Date";
        loadGridView.Columns.Add(aBoundField);
        
        aBoundField = new BoundField();
        aBoundField.DataField = "Description";
        aBoundField.HeaderText = "Description";
        loadGridView.Columns.Add(aBoundField);
        
        aBoundField = new BoundField();
        aBoundField.DataField = "Remarks";
        aBoundField.HeaderText = "Remarks";
        loadGridView.Columns.Add(aBoundField);
        
        aBoundField = new BoundField();
        aBoundField.DataField = "isSuspensionLetter";
        aBoundField.HeaderText = "Suspension Letter";
        loadGridView.Columns.Add(aBoundField);
        
        aBoundField = new BoundField();
        aBoundField.DataField = "isWithPay";
        aBoundField.HeaderText = "With Pay";
        loadGridView.Columns.Add(aBoundField);
        
        aBoundField = new BoundField();
        aBoundField.DataField = "isWithoutPay";
        aBoundField.HeaderText = "Without Pay";
        loadGridView.Columns.Add(aBoundField);


        EmployeeSuspendDal aEmployeeSuspendDal=new EmployeeSuspendDal();
        DataTable dtdata = aEmployeeSuspendDal.LoadSuspend(empCodeTexBox.Text);
        loadGridView.DataSource = dtdata;
        loadGridView.DataBind();
    }

    protected void empCodeTexBox_OnTextChanged(object sender, EventArgs e)
    {
        //if (Session["History"].ToString()=="Suspand")
        //{
        //    SuspandView();
        //}
        //else if (Session["History"].ToString() == "Decip")
        //{
            DiciplinaryAction();
        //}
    }
}