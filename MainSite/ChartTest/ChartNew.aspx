<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ChartNew.aspx.cs" Inherits="ChartTest_ChartNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="../Assets/js/vendors/jquery/jquery.min.js"></script>--%>
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
 
    <link href="Chart.min.css" rel="stylesheet" />
     <script src="chart.min.js"></script>
    <script src="Chart.bundle.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style>
        .elegantshd {
            color: #131313;
            letter-spacing: .15em;
            text-shadow: 2px 2px 4px #000000;
            font-family: 'Kreon', serif;
            vertical-align: middle;
            text-decoration-style: wavy;
        }



        .btnexcel {
           
            border: none;
            color: #131313!important;
           
            padding-left: 24px !important;
            padding-top: 8px !important;
            padding-bottom: 8px !important;
            padding-right: 24px !important;
            text-align: center!important;
            text-decoration: none!important;
            display: inline-block!important;
            font-size: 12px!important;
            margin: 4px 2px!important;
            cursor: pointer!important;
            background: url(../Assets/excel.png);
            background-position:center!important;
            background-repeat:no-repeat!important;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35)!important;
        }

        .boxStyle {
            border-left: 3px solid #4D97C2;
            border-radius: 6px;
            border-right: none;
            border-top: none;
            border-bottom: none;
            box-shadow: 0 1px 3px rgba(0,0,0,0.08), 0 1px 2px rgba(0,0,0,0.10);
            transition: all 0.3s cubic-bezier(.25,.8,.25,1);
            /*-webkit-box-shadow: 1px 1px 4px 1px rgba(0,0,0,0.41);
            -moz-box-shadow: 1px 1px 4px 1px rgba(0,0,0,0.41);
            box-shadow: 1px 1px 4px 1px rgba(0,0,0,0.41);*/
        }

        .nogap > .col {
            padding-left: 7.5px;
            padding-right: 7.5px;
        }

        .nogap > .col:first-child {
            padding-left: 15px;
        }

        .nogap > .col:last-child {
            padding-right: 15px;
        }


        .LockOn {
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 2147483647;
            width: 100%;
            height: 2215px;
            background-color: #676767;
            vertical-align: bottom;
            padding-top: 20%;
            filter: alpha(opacity=85);
            opacity: 0.85;
            font-size: large;
            color: #676767;
            font-style: italic;
            font-weight: 400;
            background-image: url("../Assets/img/LatestLoading.gif");
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center;
        }


         

        #cpFormBody_ContractualGridView td, #cpFormBody_RetirementGridView td, #cpFormBody_ProbationGridView td {
            border: 1px solid #ddd;
            padding: 8px;
        }

        #cpFormBody_ContractualGridView tr:nth-child(even), #cpFormBody_RetirementGridView tr:nth-child(even),#cpFormBody_ProbationGridView tr:nth-child(even){background-color: white;}

 

        #cpFormBody_ContractualGridView th, #cpFormBody_RetirementGridView th,#cpFormBody_ProbationGridView th {
            padding: 10px;
            border-style: none;

            background-color: #CCCCCC;
            color: black;
            font-weight: bold;
            font-size: 13px;
        }



        #cpFormBody_GridView1 td, #cpFormBody_RetirementGridView td, #cpFormBody_ProbationGridView td {
            border: 1px solid #ddd;
            padding: 8px;
        }

        #cpFormBody_GridView1 tr:nth-child(even), #cpFormBody_RetirementGridView tr:nth-child(even),#cpFormBody_ProbationGridView tr:nth-child(even){background-color: white;}

 

        #cpFormBody_GridView1 th, #cpFormBody_RetirementGridView th,#cpFormBody_ProbationGridView th {
            padding: 10px;
            border-style: none;

            background-color: #CCCCCC;
            color: black;
            font-weight: bold;
            font-size: 13px;
        }
    </style>
    <div class="content" id="content">
        <div class="container-fluid" style="background-color: white">
            <div class="card" style="background-color: white">
                <div class="card-body" style="background-color: white">
                           <div id="coverScreen" class="LockOn">
                </div>
                 

                    <div class="row"  >
                        <div class="col-md-10"> <h1><i class="fa fa-dashboard"></i>&nbsp; Dashboard</h1></div>
                        <div class="col-md-2">
                      <div class="form-group">
                                <label>Company</label>
                            <select id="ddlCompany" class="form-control form-control-sm"></select>
                      </div>
                        </div>

                    </div>

                    <div class="row" style="padding: 5px;">
                        <div class="col-md-10"></div>
                        <div class="col-md-2">
                            
                            <input type="button" id="btnExport" title="Export to Excel"   class="pull-right btnexcel " value=" " />
                                

                        </div>
                    </div>
                    <div class="row nogap">

                        <div class="col-md-12">
                            <div class=" text-center">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-bordered text-center thead-dark table-striped table-hover"  >
                                 

                            </asp:GridView>
                                </div>
                        </div>
                    </div>
                    <br />
                    <div class="row nogap">

                        <div class="col-md-6 ">
                            <div class="boxStyle text-center">
                                <div class="card-header" style="background-color: white!important">
                                    <label class="elegantshd" style="font-size: 14px; text-align: left!important">Employee Gender Ratio</label>
                                    <hr />
                                    <div class="input-group">
                                        <span class="input-group-addon" style="padding: 9px;">Division:</span>
                                        <select id="ddlDivision" class="form-control form-control-sm col-md-4"></select>

                                        <span class="input-group-addon" style="padding: 9px;">Department:</span>
                                        <select id="ddlDept" class="form-control form-control-sm col-md-4"></select>
                                    </div>

                                    <canvas id="genderPieChartByDept"></canvas>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="boxStyle text-center">
                                <div class="card-header" style="background-color: white!important">
                                    <label class="elegantshd text-center" style="font-size: 14px; text-align: center">Employee Type Ratio</label>
                                    <hr />
                                    <div class="input-group">
                                        <span class="input-group-addon" style="padding: 9px;">Division:</span>
                                        <select id="ddlDivisionETR" class="form-control form-control-sm col-md-4"></select>

                                        <span class="input-group-addon" style="padding: 9px;">Department:</span>
                                        <select id="ddlDeptETR" class="form-control form-control-sm col-md-4"></select>
                                    </div>

                                    <canvas id="genderDoughnutChartByDept"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row nogap">
                        <div class="col-md-6">
                            <div class="boxStyle text-center">
                                <div class="card-header" style="background-color: white!important">
                            <label class="elegantshd text-center" style="font-size: 14px; text-align: center">Grade wise Employee</label>
                            <hr />
                            <div class="input-group">
                                <span class="input-group-addon" style="padding: 9px;">Division:</span>
                                <select id="ddlDivisionGWE" class="form-control form-control-sm col-md-4"></select>

                                <span class="input-group-addon" style="padding: 9px;">Department:</span>
                                <select id="ddlDeptGWE" class="form-control form-control-sm col-md-4"></select>

                            </div>
                            <canvas id="horizontalBarGradewiseEmployeeByDept"></canvas>
                        </div>
                                </div>
                             </div>
                        


                        <div class="col-md-6">
                            <div class="boxStyle text-center">
                                <div class="card-header" style="background-color: white!important">
                                    <label class="elegantshd text-center" style="font-size: 14px; text-align: center">Age Group</label>
                                    <hr />
                                    <div class="input-group">
                                        <span class="input-group-addon" style="padding: 9px;">Division:</span>
                                        <select id="ddlDivisionAgeGroup" class="form-control form-control-sm col-md-4"></select>

                                        <span class="input-group-addon" style="padding: 9px;">Department:</span>
                                        <select id="ddlDeptAgeGroup" class="form-control form-control-sm col-md-4"></select>

                                    </div>
                                    

                                    <canvas id="BubbleChartAgeGroupByDept"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>

                    <br />
                    <div class="row nogap">
                         
                        
                            <div class="col-md-6">
                            <div class="boxStyle text-center">
                                <div class="card-header" style="background-color: white!important">
                                    <label class="elegantshd text-center" style="font-size: 14px; text-align: center">Length of Service</label>
                                    <hr />

                                    <div class="input-group">
                                        
                                         <span class="input-group-addon" style="padding: 9px;">Division:</span>
                                        <select id="ddlDivLengthofService" class="form-control form-control-sm col-md-4"></select>
                                        <span class="input-group-addon" style="padding: 9px;">Department:</span>
                                        <select id="ddlDeptLengthofService" class="form-control form-control-sm col-md-4"></select>


                                        <span class="input-group-addon"  style="padding: 9px;display: none">Reference Date:</span>
                                        <input class="form-control form-control-sm col-md-2" style="display: none" id="refDate" />
                                    </div>
                                    <canvas id="BarChartLengthofServiceByDept"></canvas>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6" >
                            <div class="boxStyle text-center">
                                <div class="card-header" style="background-color: white!important">
                                    <label class="elegantshd text-center" style="font-size: 14px; text-align: center">Year Wise Seperation</label>
                                    <hr />
                                    <div class="input-group">
                                        <span class="input-group-addon" style="padding: 9px;">Division:</span>
                                        <select id="ddlYearWiseSeperationDivision" class="form-control form-control-sm col-md-4"></select>
                                        <span class="input-group-addon" style="padding: 9px;">Department:</span>
                                        <select id="ddlYearWiseSeperationDept" class="form-control form-control-sm col-md-4"></select>
                                        <span class="input-group-addon" style="padding: 9px;display: none">From Year:</span>
                                        <select id="ddlFromYear" runat="server" style="display: none" class="form-control form-control-sm col-md-2"></select>

                                        <span class="input-group-addon" style="padding: 9px;display: none">To Year:</span>
                                        <select id="ddlToYear" runat="server" style="display: none" class="form-control form-control-sm col-md-2"></select>
                                    </div>
                                    <canvas id="BubbleChartYearWiseSeperationByDept"></canvas>
                                </div>
                            </div>
                        </div> 
                    </div>

                    <br />
                    <div class="row nogap">

                    


                        <div class="col-md-6" runat="server" Visible="False">
                            <div class="boxStyle text-center">
                                <div class="card-header" style="background-color: white!important">
                                    <label class="elegantshd text-center" style="font-size: 14px; text-align: center">Upcomming Training List</label>
                                    <input type="button" id="btnloadGridView"  title="Export to Excel" class="pull-right btnexcel" value=" " />

                                    <br />
                                    <br />
                                    <hr />
                                    <div id="gridContainer14" style="height: 100px; overflow: auto; width: auto;">
                                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered text-center thead-dark" DataKeyNames="TrainingMasterId"
                                            AllowPaging="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TrainingTitle" HeaderText="Training Title" />

                                                <asp:BoundField DataField="TrainingStart" HeaderText="Start Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                <asp:BoundField DataField="TrainingEnd" HeaderText="End Date" DataFormatString="{0:dd-MMM-yyyy}" />





                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row nogap">

                        <div class="col-md-6">
                            <div class="boxStyle text-center">
                                <div class="card-header" style="background-color: white!important">
                                    <label class="elegantshd text-center" style="font-size: 14px; text-align: center">Probation Employee List</label>

                                    <input type="button" id="btnExportProbition"  title="Export to Excel" class="pull-right btnexcel" value=" " />

                                    <br />
                                    <br />
                                    <hr />

                                    <div class="input-group">
                                        <span class="input-group-addon" style="padding: 9px;">Division:</span>
                                        <select id="ddlDivisionProbationEmployee" class="form-control form-control-sm col-md-4"></select>
                                        
                                        
                                         <span class="input-group-addon" style="padding: 9px;">Department:</span>
                                        <select id="ddlDeptProbationEmployee" class="form-control form-control-sm col-md-4"></select>
                                    </div>
                                    <div id="gridContainer1" style="height: 300px; overflow: auto; width: auto;">
                                        <asp:GridView ID="ProbationGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered text-center thead-dark table-striped table-hover">
                                        <%--    <Columns>

                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" />
                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                                                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                <asp:BoundField DataField="ProbationEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                <asp:BoundField DataField="DateOfJoin" HeaderText="Joining Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                            </Columns>--%>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="boxStyle text-center">
                                <div class="card-header" style="background-color: white!important">
                                    <label class="elegantshd text-center" style="font-size: 14px; text-align: center">Contractual Employee List</label>
                                    <input type="button" id="btnContractual"  title="Export to Excel" class="pull-right btnexcel" value=" " />

                                    <br />
                                    <br />
                                    <hr />

                                    <div class="input-group">
                                        <span class="input-group-addon" style="padding: 9px;">Division:</span>
                                        <select id="ddlDivisionContractualEmployee" class="form-control form-control-sm col-md-4"></select>
                                        
                                        
                                         <span class="input-group-addon" style="padding: 9px;">Department:</span>
                                        <select id="ddlDeptContractualEmployee" class="form-control form-control-sm col-md-4"></select>
                                    </div>

                                    <div id="sda" style="height: 300px; overflow: auto; width: auto;">
                                        <asp:GridView ID="ContractualGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered text-center thead-dark table-striped table-hover"  >
                                            <Columns>
                                               <%-- <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" />
                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                                                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                <asp:BoundField DataField="ContractEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                <asp:BoundField DataField="DateOfJoin" HeaderText="Joining Date" DataFormatString="{0:dd-MMM-yyyy}" />--%>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row nogap">
                         
                        <div class="col-md-6 "  >
                             <div class="boxStyle text-center">
                         
                            <label class="elegantshd text-center" style="font-size: 14px; text-align: center">Retirement List</label>
                            <input type="button" id="btnRetirement"  title="Export to Excel" class="pull-right btnexcel" value=" " />

                            <br />
                            <br />
                            <hr />
                            <div class="input-group">
                                <span class="input-group-addon" style="padding: 9px;">Division:</span>
                                <select id="ddlDivisionRetirementEmployee" class="form-control form-control-sm col-md-4"></select>
                                    
                                    <span class="input-group-addon" style="padding: 9px;">Department:</span>
                                <select id="ddlDeptRetirementEmployee" class="form-control form-control-sm col-md-4"></select>
                            </div>
                            <div id="sdssa" style="height: 300px; overflow: auto; width: auto;">
                                <asp:GridView ID="RetirementGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered  text-center thead-light" >
                                    <%--<Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        <asp:BoundField DataField="DateOfRetirement" HeaderText="Date Of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="DateOfJoin" HeaderText="Joining Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                    </Columns>--%>
                                </asp:GridView>
                            </div>
                        </div></div>

                        <div class="col-md-6">
                            <%--     <label class="elegantshd text-center" style="font-size: 14px; text-align: center">Contractual Employee List</label>
                                <hr />--%>
                        </div>
                     
                </div>
                    
                    
                    
                    
                       <br />
                                         <br />
                                      <div class="row" runat="server" Visible="False">
                                <style>
                                    .tblTHColorChang{
                                        background-color: #EDF2F5!important;
                                        font-weight: bold;
                                        font-size: 13px;
                                    }


.title-widget {
	color: #898989;
	font-size: 20px;
	font-weight: 300;
	line-height: 1;
	position: relative;
	text-transform: uppercase;
	font-family: 'Fjalla One', sans-serif;
	margin-top: 0;
	margin-right: 0;
	margin-bottom: 25px;
	 
	padding-left: 12px;

}

.title-widget::before {
    background-color: #ea5644;
    content: "";
    height: 22px;
    left: 0px;
    position: absolute;
    top: -2px;
    width: 5px;
}


                                </style>
                                         
                                         
                                            <div class="col-md-2" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                      
                                          <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">KPI Information</h2>
                                     <table class="table table-bordered table-striped">
                                         
                                      
                                                  
                                                    
                                                     <tr >
                                                        
<td  class="tblTHColorChang" style="width: 20%; padding: 10px;text-align: center">Self
    
    
     <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">KPI
                                                        <table style="height: 100px" class="table table-bordered table-striped">
                                                    <tr>
                                                        <td   style="width: 20%; padding: 10px;">
                                                            
                                                            <asp:Label runat="server" ID="lblKPISelfStatus"></asp:Label> 
                                                        </td>
                                                        </tr>     
                                                            </table>
                                                        </td>
                                                          <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Appraisal
                                                              
                                                              
                                                              
                                                                 <table style="height: 100px" class="table table-bordered table-striped">
                                                    <tr>
                                                        <td   style="width: 20%; padding: 10px;">
                                                            
                                                            <asp:Label runat="server" ID="lblApprisalSelfStatus"></asp:Label> 
                                                        </td>
                                                        </tr>     
                                                            </table>
                                                          </td>

                                                    </tr>
                                                    </table>
</td>

                                                         
                                                              
                                          
                                                         
                                                     
                                                         
                                                         
                                                           <td  class="tblTHColorChang" style="width: 20%; padding: 10px;text-align: center">As Approver
                                                             
                                                                  <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">KPI
                                                            
                                                            
                                                              <table class="table table-bordered table-striped" style="height: 100px">
                                                    <tr>
                                                        <td   style="width: 20%; padding: 10px;">
                                                            
                                                         Done   
                                                        </td>
                                                        
                                                          <td   style="width: 20%; padding: 10px;">
                                                            
                                                           <asp:Label runat="server" ID="lblKpiDone"></asp:Label> 
                                                        </td>
                                                        </tr>     
                                                                  
                                                                  <tr>
                                                        <td   style="width: 20%; padding: 10px;">
                                                            
                                                          Pending   
                                                        </td>
                                                                      
                                                                        <td   style="width: 20%; padding: 10px;">
                                                            
                                                           <asp:Label runat="server" ID="lblKPIPending"></asp:Label> 
                                                        </td>
                                                        </tr>    
                                                            </table>
                                                        </td>
                                                          <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Appraisal
                                                              
                                                              
                                                           <table class="table table-bordered table-striped" style="height: 100px">
                                                    <tr>
                                                        <td   style="width: 20%; padding: 10px;">
                                                            
                                                         Done   
                                                        </td>
                                                        
                                                          <td   style="width: 20%; padding: 10px;">
                                                            
                                                         <asp:Label runat="server" ID="lblAppDone"></asp:Label> 
                                                        </td>
                                                        </tr>     
                                                                  
                                                                  <tr>
                                                        <td   style="width: 20%; padding: 10px;">
                                                            
                                                          Pending  
                                                        </td>
                                                                      
                                                                        <td   style="width: 20%; padding: 10px;">
                                                  <asp:Label runat="server" ID="lblAppPending"></asp:Label> 
                                                        </td>
                                                        </tr>    
                                                            </table>
                                                          </td>

                                                    </tr>
                                                    </table>

                                                           </td>
                                                    </tr>
                                         
                                         
                              
                                                    
                                                    
                                    </table>
                                            </div>

            </div>
        </div>
    </div>
    </div>
    <style>
        .bgcolornull {
            background-color: white;
        }
    </style>
   
  
   
    <script>
        
        $(document).ready(function () {


            $(document).ajaxStart(function () {
                $("#coverScreen").show();
            });

            $(document).ajaxStop(function () {
                $("#coverScreen").hide();
            });

             
            var yourDateValue = new Date();
            var formattedDate = yourDateValue.toISOString().substr(0, 10);
            //Assign date value to date textbox
            $('#refDate').val(formattedDate);

             
            $.ajax({
                type: "POST",
                async: false,
                url: "ChartNew.aspx/LoadCompany",
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $('#ddlCompany').empty();
                     

                    var result = data.d;

                    var selectOptions = '';
                    for (var i in result) {
                        selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
                    }
                    $('#ddlCompany').append(selectOptions);
                   

                },
                error: function (err) {
                    alert(err);
                }
            });
        });


        //$(document).ready(function () {
        //    //  LoadDept();
        //     
        //    LoadCompany();
        //    //$("#ddlCompany")[0].selectedIndex = 1;
        //    //var comId = $('#ddlCompany').val();
        //    //$('#ddlCompany').val(comId).trigger('change');
        //});
        $(document).ready(function () {
            //$("#ddlDept")[0].selectedIndex = 1;
            //var selectedDept = $('#ddlDept').val();
            //$('#ddlDept').val(selectedDept).trigger('change');////Dept val 14 is for Human Resource


           
            var comId = $('#ddlCompany').val();
            $('#ddlCompany').val(comId).trigger('change');


        });
        //$(document).ready(function () {

        //    $("#ddlYearWiseSeperationDept")[0].selectedIndex = 1;
        //    var selectedDept = $('#ddlYearWiseSeperationDept').val();

        //    $('#ddlYearWiseSeperationDept').val(selectedDept).trigger('change');////Dept val 14 is for Human Resource
        //});



        //$(document).ready(function () {

        //    $("#ddlDeptAgeGroup")[0].selectedIndex = 1;
        //    var selectedDept = $('#ddlDeptAgeGroup').val();

        //    $('#ddlDeptAgeGroup').val(selectedDept).trigger('change');////Dept val 14 is for Human Resource
        //});

        //$(document).ready(function () {

        //    $("#ddlDeptLengthofService")[0].selectedIndex = 1;
        //    var selectedDept = $('#ddlDeptLengthofService').val();

        //    $('#ddlDeptLengthofService').val(selectedDept).trigger('change');////Dept val 14 is for Human Resource
        //});



       
       



        $('#ddlCompany').on('change',
            function () {
                 
                var comId = $('#ddlCompany').val();

                $.ajax({
                    type: "POST",
                    async: false,
                    dataType: "JSON",
                    url: "ChartNew.aspx/LoadDivisionByComId",

                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId }),

                    success: function (data) {
                        $('#ddlDivision').empty();
                        $('#ddlDivisionETR').empty();
                        $('#ddlDivisionAgeGroup').empty();
                        $('#ddlYearWiseSeperationDivision').empty();
                        $('#ddlDivisionGWE').empty();
                        $('#ddlDivLengthofService').empty();
                        $('#ddlDivisionProbationEmployee').empty();
                        $('#ddlDivisionContractualEmployee').empty();
                        $('#ddlDivisionRetirementEmployee').empty();
                        var result = data.d;

                        var selectOptions = '';
                        for (var i in result) {
                            selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
                        }
                        $('#ddlDivision').append(selectOptions);
                        $("#ddlDivision")[0].selectedIndex = 1;

                        $('#ddlDivisionETR').append(selectOptions);
                        $("#ddlDivisionETR")[0].selectedIndex = 1;

                        $('#ddlDivisionAgeGroup').append(selectOptions);
                        $("#ddlDivisionAgeGroup")[0].selectedIndex = 1;


                        $('#ddlYearWiseSeperationDivision').append(selectOptions);
                        $("#ddlYearWiseSeperationDivision")[0].selectedIndex = 1;


                        $('#ddlDivisionGWE').append(selectOptions);
                        $("#ddlDivisionGWE")[0].selectedIndex = 1;


                        $('#ddlDivLengthofService').append(selectOptions);
                        $("#ddlDivLengthofService")[0].selectedIndex = 1;


                        $('#ddlDivisionProbationEmployee').append(selectOptions);
                        $("#ddlDivisionProbationEmployee")[0].selectedIndex = 1;


                        $('#ddlDivisionContractualEmployee').append(selectOptions);
                        $("#ddlDivisionContractualEmployee")[0].selectedIndex = 1;



                        $('#ddlDivisionRetirementEmployee').append(selectOptions);
                        $("#ddlDivisionRetirementEmployee")[0].selectedIndex = 1;
                        
                    },
                    error: function (err) {
                        alert(err);
                    }


                });
            });

        $('#ddlCompany').on('change',
     function () {
         var comId = $('#ddlCompany').val();
      
         $.ajax({
             type: "POST",
             async: false,
             dataType: "JSON",
             url: "ChartNew.aspx/EmployeeJoinLeftRetireInfo",

             contentType: "application/json;charset=utf-8",
             data: JSON.stringify({ "comId": comId }),

             success: function (data) {

                 $("#cpFormBody_GridView1").empty();

                 if (data.d.length > 0) {

                     $("#cpFormBody_GridView1").append("  <tr> <th colspan='1' style=' background-color: white;'> </th> <th colspan='3' style=' background-color: rgb(0, 209, 209);'>Joining</th> <th colspan='3' style=' background-color: rgb(240, 160, 138);'>Separation</th> <th colspan='3' style=' background-color: rgb(255, 98, 131);'>Retirement</th> </tr> " +
                         "<tr><th>Month</th><th>Permanent</th> <th>Contractual</th> <th>Program Contractual</th>  <th>Permanent</th>  <th>Contractual</th>   <th>Program Contractual</th><th>Permanent</th></tr>");
                     for (var i = 0; i < data.d.length; i++) {
                         debugger ;
                         $("#cpFormBody_GridView1").append("<tr><td>" +
                         data.d[i].Month + "</td> <td>" +
                         data.d[i].PermJoin + "</td> <td>" +
                         data.d[i].ContJoin + "</td> <td>" +
                         data.d[i].PermContJoin + "</td> <td>" +
                         data.d[i].LeftPer + "</td> <td>" +
                         data.d[i].LeftPermCont + "</td> <td>" +

                         data.d[i].LeftPermCont + "</td> <td>" +
                          data.d[i].RetirePerm +
                             //"</td> <td>" +
                             // data.d[i].RetireCont + "</td> <td>" +
                             // data.d[i].RetirePermCont + 
                             

                             "</td></tr>");
                     }
                 }
             },
             error: function (result) {


             }


         });
     });
        
       

        //Probation Employee List
        $('#ddlCompany').on('change',
           function () {
              
             var comId = $('#ddlCompany').val();
             var selecteddiv = $('#ddlDivisionProbationEmployee').val();
             $.ajax({
                 type: "POST",
                 async: false,
                 dataType: "JSON",
                 url: "ChartNew.aspx/EmployeeProbationEmployeeListInfoFordiv",

                 contentType: "application/json;charset=utf-8",
                 data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),

                 success: function (data) {
                      
                     $("#cpFormBody_ProbationGridView").empty();

                     if (data.d.length > 0) {
                          
                         $("#cpFormBody_ProbationGridView").append("<tr><th>Employee ID</th> <th>Employee Name</th> <th>Department</th>  <th>Designation</th>  <th>Probation End Date</th>   <th>Joining Date</th></tr>");
                         for (var i = 0; i < data.d.length; i++) {

                             $("#cpFormBody_ProbationGridView").append("<tr><td>" +
                             data.d[i].EmpMasterCode + "</td> <td>" +
                             data.d[i].EmpName + "</td> <td>" +
                             data.d[i].DepartmentName + "</td> <td>" +
                             data.d[i].Designation + "</td> <td>" +
                             data.d[i].ProbationEndDate + "</td> <td>" +
                             
                             data.d[i].DateOfJoin + "</td></tr>");
                         }
                     }
                 },
                 error: function (result) {
                           

                 }


             });
           });


        $('#ddlDivisionProbationEmployee').on('change',
          function () {
              
              var comId = $('#ddlCompany').val();
              var selecteddiv = $('#ddlDivisionProbationEmployee').val();

              $.ajax({
                  type: "POST",
                  async: false,
                  dataType: "JSON",
                  url: "ChartNew.aspx/EmployeeProbationEmployeeListInfoFordiv",

                  contentType: "application/json;charset=utf-8",
                  data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),

                  success: function (data) {
                     
                      $("#cpFormBody_ProbationGridView").empty();

                      if (data.d.length > 0) {
                          
                          $("#cpFormBody_ProbationGridView").append("<tr><th>Employee ID</th> <th>Employee Name</th> <th>Department</th>  <th>Designation</th>  <th>Probation End Date</th>   <th>Joining Date</th></tr>");
                          for (var i = 0; i < data.d.length; i++) {

                              $("#cpFormBody_ProbationGridView").append("<tr><td>" +
                              data.d[i].EmpMasterCode + "</td> <td>" +
                              data.d[i].EmpName + "</td> <td>" +
                              data.d[i].DepartmentName + "</td> <td>" +
                              data.d[i].Designation + "</td> <td>" +
                              data.d[i].ProbationEndDate + "</td> <td>" +

                              data.d[i].DateOfJoin + "</td></tr>");
                          }
                      }
                  },
                  error: function (result) {


                  }


              });
          });


        $('#ddlDeptProbationEmployee').on('change',
        function () {

            var comId = $('#ddlCompany').val();
            var selecteddiv = $('#ddlDivisionProbationEmployee').val();
            var selectedDept = $('#ddlDeptProbationEmployee').val();
            $.ajax({
                type: "POST",
                async: false,
                dataType: "JSON",
                url: "ChartNew.aspx/EmployeeProbationEmployeeListInfoForDept",

                contentType: "application/json;charset=utf-8",
                data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv, "selectedDept": selectedDept }),

                success: function (data) {
                    
                    $("#cpFormBody_ProbationGridView").empty();

                    if (data.d.length > 0) {
                        
                        $("#cpFormBody_ProbationGridView").append("<tr><th>Employee ID</th> <th>Employee Name</th> <th>Department</th>  <th>Designation</th>  <th>Probation End Date</th>   <th>Joining Date</th></tr>");
                        for (var i = 0; i < data.d.length; i++) {

                            $("#cpFormBody_ProbationGridView").append("<tr><td>" +
                            data.d[i].EmpMasterCode + "</td> <td>" +
                            data.d[i].EmpName + "</td> <td>" +
                            data.d[i].DepartmentName + "</td> <td>" +
                            data.d[i].Designation + "</td> <td>" +
                            data.d[i].ProbationEndDate + "</td> <td>" +

                            data.d[i].DateOfJoin + "</td></tr>");
                        }
                    }
                },
                error: function (result) {


                }


            });
        });

        $('#ddlCompany').on('change',
        function () {

            var comId = $('#ddlCompany').val();
            var selecteddiv = $('#ddlDivisionContractualEmployee').val();
            $.ajax({
                type: "POST",
                async: false,
                dataType: "JSON",
                url: "ChartNew.aspx/EmployeeProbationContractualInfoFordiv",

                contentType: "application/json;charset=utf-8",
                data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),

                success: function (data) {

                    $("#cpFormBody_ContractualGridView").empty();

                    if (data.d.length > 0) {

                        $("#cpFormBody_ContractualGridView").append("<tr><th>Employee ID</th> <th>Employee Name</th> <th>Department</th>  <th>Designation</th>  <th>Contractual End Date</th>   <th>Joining Date</th></tr>");
                        for (var i = 0; i < data.d.length; i++) {

                            $("#cpFormBody_ContractualGridView").append("<tr><td>" +
                            data.d[i].EmpMasterCode + "</td> <td>" +
                            data.d[i].EmpName + "</td> <td>" +
                            data.d[i].DepartmentName + "</td> <td>" +
                            data.d[i].Designation + "</td> <td>" +
                            data.d[i].ContractEndDate + "</td> <td>" +

                            data.d[i].DateOfJoin + "</td></tr>");
                        }
                    }
                },
                error: function (result) {


                }


            });
        });


        $('#ddlDivisionContractualEmployee').on('change',
       function () {

           var comId = $('#ddlCompany').val();
           var selecteddiv = $('#ddlDivisionContractualEmployee').val();
           $.ajax({
               type: "POST",
               async: false,
               dataType: "JSON",
               url: "ChartNew.aspx/EmployeeProbationContractualInfoFordiv",

               contentType: "application/json;charset=utf-8",
               data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),

               success: function (data) {

                   $("#cpFormBody_ContractualGridView").empty();

                   if (data.d.length > 0) {

                       $("#cpFormBody_ContractualGridView").append("<tr><th>Employee ID</th> <th>Employee Name</th> <th>Department</th>  <th>Designation</th>  <th>Contractual End Date</th>   <th>Joining Date</th></tr>");
                       for (var i = 0; i < data.d.length; i++) {

                           $("#cpFormBody_ContractualGridView").append("<tr><td>" +
                           data.d[i].EmpMasterCode + "</td> <td>" +
                           data.d[i].EmpName + "</td> <td>" +
                           data.d[i].DepartmentName + "</td> <td>" +
                           data.d[i].Designation + "</td> <td>" +
                           data.d[i].ContractEndDate + "</td> <td>" +

                           data.d[i].DateOfJoin + "</td></tr>");
                       }
                   }
               },
               error: function (result) {


               }


           });
       });

        $('#ddlDeptContractualEmployee').on('change',
      function () {

          var comId = $('#ddlCompany').val();
          var selecteddiv = $('#ddlDivisionContractualEmployee').val();
          var selectedDept = $('#ddlDeptContractualEmployee').val();
          $.ajax({
              type: "POST",
              async: false,
              dataType: "JSON",
              url: "ChartNew.aspx/EmployeeProbationContractualInfoForDpt",

              contentType: "application/json;charset=utf-8",
              data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv, "selectedDept": selectedDept }),

              success: function (data) {

                  $("#cpFormBody_ContractualGridView").empty();

                  if (data.d.length > 0) {

                      $("#cpFormBody_ContractualGridView").append("<tr><th>Employee ID</th> <th>Employee Name</th> <th>Department</th>  <th>Designation</th>  <th>Contractual End Date</th>   <th>Joining Date</th></tr>");
                      for (var i = 0; i < data.d.length; i++) {

                          $("#cpFormBody_ContractualGridView").append("<tr><td>" +
                          data.d[i].EmpMasterCode + "</td> <td>" +
                          data.d[i].EmpName + "</td> <td>" +
                          data.d[i].DepartmentName + "</td> <td>" +
                          data.d[i].Designation + "</td> <td>" +
                          data.d[i].ContractEndDate + "</td> <td>" +

                          data.d[i].DateOfJoin + "</td></tr>");
                      }
                  }
              },
              error: function (result) {


              }


          });
      });



        $('#ddlCompany').on('change',
      function () {

          var comId = $('#ddlCompany').val();
          var selecteddiv = $('#ddlDivisionRetirementEmployee').val();
          $.ajax({
              type: "POST",
              async: false,
              dataType: "JSON",
              url: "ChartNew.aspx/LoadRetirementInfo",

              contentType: "application/json;charset=utf-8",
              data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),

              success: function (data) {

                  $("#cpFormBody_RetirementGridView").empty();

                  if (data.d.length > 0) {

                      $("#cpFormBody_RetirementGridView").append("<tr><th>Employee ID</th> <th>Employee Name</th> <th>Department</th>  <th>Designation</th>  <th>Date Of Retirement</th>   <th>Joining Date</th></tr>");
                      for (var i = 0; i < data.d.length; i++) {

                          $("#cpFormBody_RetirementGridView").append("<tr><td>" +
                          data.d[i].EmpMasterCode + "</td> <td>" +
                          data.d[i].EmpName + "</td> <td>" +
                          data.d[i].DepartmentName + "</td> <td>" +
                          data.d[i].Designation + "</td> <td>" +
                          data.d[i].ContractEndDate + "</td> <td>" +

                          data.d[i].DateOfJoin + "</td></tr>");
                      }
                  }
              },
              error: function (result) {


              }


          });
      });



        $('#ddlDivisionRetirementEmployee').on('change',
          function () {

              var comId = $('#ddlCompany').val();
              var selecteddiv = $('#ddlDivisionRetirementEmployee').val();
              $.ajax({
                  type: "POST",
                  async: false,
                  dataType: "JSON",
                  url: "ChartNew.aspx/LoadRetirementInfo",

                  contentType: "application/json;charset=utf-8",
                  data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),

                  success: function (data) {

                      $("#cpFormBody_RetirementGridView").empty();

                      if (data.d.length > 0) {

                          $("#cpFormBody_RetirementGridView").append("<tr><th>Employee ID</th> <th>Employee Name</th> <th>Department</th>  <th>Designation</th>  <th>Date Of Retirement</th>   <th>Joining Date</th></tr>");
                          for (var i = 0; i < data.d.length; i++) {

                              $("#cpFormBody_RetirementGridView").append("<tr><td>" +
                              data.d[i].EmpMasterCode + "</td> <td>" +
                              data.d[i].EmpName + "</td> <td>" +
                              data.d[i].DepartmentName + "</td> <td>" +
                              data.d[i].Designation + "</td> <td>" +
                              data.d[i].DateOfRetirement + "</td> <td>" +

                              data.d[i].DateOfJoin + "</td></tr>");
                          }
                      }
                  },
                  error: function (result) {


                  }


              });
          });

        $('#ddlDeptRetirementEmployee').on('change',
    function () {

        var comId = $('#ddlCompany').val();
        var selecteddiv = $('#ddlDivisionRetirementEmployee').val();
        var selectedDept = $('#ddlDeptRetirementEmployee').val();
        $.ajax({
            type: "POST",
            async: false,
            dataType: "JSON",
            url: "ChartNew.aspx/LoadRetirementInfoDtp",

            contentType: "application/json;charset=utf-8",
            data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv, "selectedDept": selectedDept }),

            success: function (data) {

                $("#cpFormBody_RetirementGridView").empty();

                if (data.d.length > 0) {

                    $("#cpFormBody_RetirementGridView").append("<tr><th>Employee ID</th> <th>Employee Name</th> <th>Department</th>  <th>Designation</th>  <th>Date Of Retirement</th>   <th>Joining Date</th></tr>");
                    for (var i = 0; i < data.d.length; i++) {

                        $("#cpFormBody_RetirementGridView").append("<tr><td>" +
                        data.d[i].EmpMasterCode + "</td> <td>" +
                        data.d[i].EmpName + "</td> <td>" +
                        data.d[i].DepartmentName + "</td> <td>" +
                        data.d[i].Designation + "</td> <td>" +
                        data.d[i].DateOfRetirement + "</td> <td>" +

                        data.d[i].DateOfJoin + "</td></tr>");
                    }
                }
            },
            error: function (result) {


            }


        });
    });



        $(document).ready(function () {

            $("#ddlDivision")[0].selectedIndex = 1;
            var selecteddiv = $('#ddlDivision').val();
            $('#ddlDivision').val(selecteddiv).trigger('change');





        });


        $(document).ready(function () {

            $("#ddlDivisionETR")[0].selectedIndex = 1;
            var selecteddiv = $('#ddlDivisionETR').val();
            $('#ddlDivisionETR').val(selecteddiv).trigger('change');





        });


        $(document).ready(function () {

            $("#ddlDivisionAgeGroup")[0].selectedIndex = 1;
            var selecteddiv = $('#ddlDivisionAgeGroup').val();
            $('#ddlDivisionAgeGroup').val(selecteddiv).trigger('change');





        });



        $(document).ready(function () {

            $("#ddlYearWiseSeperationDivision")[0].selectedIndex = 1;
            var selecteddiv = $('#ddlYearWiseSeperationDivision').val();
            $('#ddlYearWiseSeperationDivision').val(selecteddiv).trigger('change');

        });

        $(document).ready(function () {

            $("#ddlDivisionGWE")[0].selectedIndex = 1;
            var selecteddiv = $('#ddlDivisionGWE').val();
            $('#ddlDivisionGWE').val(selecteddiv).trigger('change');

        });


        $(document).ready(function () {

            $("#ddlDivLengthofService")[0].selectedIndex = 1;
            var selecteddiv = $('#ddlDivLengthofService').val();
            $('#ddlDivLengthofService').val(selecteddiv).trigger('change');

        });



        $(document).ready(function () {

            $("#ddlDivisionProbationEmployee")[0].selectedIndex = 1;
            var selecteddiv = $('#ddlDivisionProbationEmployee').val();
            $('#ddlDivisionProbationEmployee').val(selecteddiv).trigger('change');

        });


        $(document).ready(function () {

            $("#ddlDivisionContractualEmployee")[0].selectedIndex = 1;
            var selecteddiv = $('#ddlDivisionContractualEmployee').val();
            $('#ddlDivisionContractualEmployee').val(selecteddiv).trigger('change');

        });


        $(document).ready(function () {

            $("#ddlDivisionRetirementEmployee")[0].selectedIndex = 1;
            var selecteddiv = $('#ddlDivisionRetirementEmployee').val();
            $('#ddlDivisionRetirementEmployee').val(selecteddiv).trigger('change');

        });





        $('#ddlDivisionContractualEmployee').on('change',
          function () {

              var selecteddiv = $('#ddlDivisionContractualEmployee').val();

              $.ajax({
                  type: "POST",
                  async: false,
                  dataType: "JSON",
                  url: "ChartNew.aspx/LoadDept",

                  contentType: "application/json;charset=utf-8",
                  data: JSON.stringify({ "selecteddiv": selecteddiv }),

                  success: function (data) {
                      $('#ddlDeptContractualEmployee').empty();
                      var result = data.d;

                      var selectOptions = '';
                      for (var i in result) {
                          selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
                      }
                      $('#ddlDeptContractualEmployee').append(selectOptions);
                      //$("#ddlDept")[0].selectedIndex = 1;

                  },
                  error: function (err) {
                      alert(err);
                  }


              });
          });



        $('#ddlDivisionRetirementEmployee').on('change',
          function () {

              var selecteddiv = $('#ddlDivisionRetirementEmployee').val();

              $.ajax({
                  type: "POST",
                  async: false,
                  dataType: "JSON",
                  url: "ChartNew.aspx/LoadDept",

                  contentType: "application/json;charset=utf-8",
                  data: JSON.stringify({ "selecteddiv": selecteddiv }),

                  success: function (data) {
                      $('#ddlDeptRetirementEmployee').empty();
                      var result = data.d;

                      var selectOptions = '';
                      for (var i in result) {
                          selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
                      }
                      $('#ddlDeptRetirementEmployee').append(selectOptions);
                      //$("#ddlDept")[0].selectedIndex = 1;

                  },
                  error: function (err) {
                      alert(err);
                  }


              });
          });



        $('#ddlDivision').on('change',
            function () {

                var selecteddiv = $('#ddlDivision').val();

                $.ajax({
                    type: "POST",
                    async: false,
                    dataType: "JSON",
                    url: "ChartNew.aspx/LoadDept",

                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "selecteddiv": selecteddiv }),

                    success: function (data) {
                        $('#ddlDept').empty();
                        var result = data.d;

                        var selectOptions = '';
                        for (var i in result) {
                            selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
                        }
                        $('#ddlDept').append(selectOptions);
                        //$("#ddlDept")[0].selectedIndex = 1;

                    },
                    error: function (err) {
                        alert(err);
                    }


                });
            });

        $('#ddlYearWiseSeperationDivision').on('change',
            function () {

                var selecteddiv = $('#ddlYearWiseSeperationDivision').val();

                $.ajax({
                    type: "POST",
                    async: false,
                    dataType: "JSON",
                    url: "ChartNew.aspx/LoadDept",

                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "selecteddiv": selecteddiv }),

                    success: function (data) {
                        $('#ddlYearWiseSeperationDept').empty();
                        var result = data.d;

                        var selectOptions = '';
                        for (var i in result) {
                            selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
                        }
                        $('#ddlYearWiseSeperationDept').append(selectOptions);
                        //$("#ddlDept")[0].selectedIndex = 1;

                    },
                    error: function (err) {
                        alert(err);
                    }


                });
            });


        $('#ddlDivisionETR').on('change',
            function () {

                var selecteddiv = $('#ddlDivisionETR').val();

                $.ajax({
                    type: "POST",
                    async: false,
                    dataType: "JSON",
                    url: "ChartNew.aspx/LoadDept",

                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "selecteddiv": selecteddiv }),

                    success: function (data) {
                        $('#ddlDeptETR').empty();
                        var result = data.d;

                        var selectOptions = '';
                        for (var i in result) {
                            selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
                        }
                        $('#ddlDeptETR').append(selectOptions);
                        //$("#ddlDept")[0].selectedIndex = 1;

                    },
                    error: function (err) {
                        alert(err);
                    }


                });
            });

        $('#ddlDivisionAgeGroup').on('change',
            function () {

                var selecteddiv = $('#ddlDivisionAgeGroup').val();

                $.ajax({
                    type: "POST",
                    async: false,
                    dataType: "JSON",
                    url: "ChartNew.aspx/LoadDept",

                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "selecteddiv": selecteddiv }),

                    success: function (data) {
                        $('#ddlDeptAgeGroup').empty();
                        var result = data.d;

                        var selectOptions = '';
                        for (var i in result) {
                            selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
                        }
                        $('#ddlDeptAgeGroup').append(selectOptions);
                        //$("#ddlDept")[0].selectedIndex = 1;

                    },
                    error: function (err) {
                        alert(err);
                    }


                });
            });


        $('#ddlDivisionGWE').on('change',
            function () {

                var selecteddiv = $('#ddlDivisionGWE').val();

                $.ajax({
                    type: "POST",
                    async: false,
                    dataType: "JSON",
                    url: "ChartNew.aspx/LoadDept",

                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "selecteddiv": selecteddiv }),

                    success: function (data) {
                        $('#ddlDeptGWE').empty();
                        var result = data.d;

                        var selectOptions = '';
                        for (var i in result) {
                            selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
                        }
                        $('#ddlDeptGWE').append(selectOptions);
                        //$("#ddlDept")[0].selectedIndex = 1;

                    },
                    error: function (err) {
                        alert(err);
                    }


                });
            });



        $('#ddlDivLengthofService').on('change',
          function () {

              var selecteddiv = $('#ddlDivLengthofService').val();

              $.ajax({
                  type: "POST",
                  async: false,
                  dataType: "JSON",
                  url: "ChartNew.aspx/LoadDept",

                  contentType: "application/json;charset=utf-8",
                  data: JSON.stringify({ "selecteddiv": selecteddiv }),

                  success: function (data) {
                      $('#ddlDeptLengthofService').empty();
                      var result = data.d;

                      var selectOptions = '';
                      for (var i in result) {
                          selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
                      }
                      $('#ddlDeptLengthofService').append(selectOptions);
                      //$("#ddlDept")[0].selectedIndex = 1;

                  },
                  error: function (err) {
                      alert(err);
                  }


              });
          });




        $('#ddlDivisionProbationEmployee').on('change',
         function () {

             var selecteddiv = $('#ddlDivisionProbationEmployee').val();

             $.ajax({
                 type: "POST",
                 async: false,
                 dataType: "JSON",
                 url: "ChartNew.aspx/LoadDept",

                 contentType: "application/json;charset=utf-8",
                 data: JSON.stringify({ "selecteddiv": selecteddiv }),

                 success: function (data) {
                     $('#ddlDeptProbationEmployee').empty();
                     var result = data.d;

                     var selectOptions = '';
                     for (var i in result) {
                         selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
                     }
                     $('#ddlDeptProbationEmployee').append(selectOptions);
                     //$("#ddlDept")[0].selectedIndex = 1;

                 },
                 error: function (err) {
                     alert(err);
                 }


             });
         });

        //Dept 
        //function LoadDept() {
        //    $.ajax({
        //        type: "POST",
        //        async:false,
        //        url: "ChartNew.aspx/LoadDept",
        //        dataType: "JSON",
        //        contentType: "application/json;charset=utf-8",
        //        success: function (data) {
        //            var result = data.d;
        //            var selectOptions = '';
        //            for (var i in result) {
        //                selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
        //            }
        //            $('#ddlDept').append(selectOptions);

        //            $('#ddlYearWiseSeperationDept').append(selectOptions);
        //            $("#ddlYearWiseSeperationDept")[0].selectedIndex = 1;
        //            $('#ddlDeptAgeGroup').append(selectOptions);
        //            $("#ddlDeptAgeGroup")[0].selectedIndex = 1;
        //            $('#ddlDeptLengthofService').append(selectOptions);
        //            $("#ddlDeptLengthofService")[0].selectedIndex = 1;

        //            $('#ddlDeptProbationEmployee').append(selectOptions);
        //            $("#ddlDeptProbationEmployee")[0].selectedIndex = 1;

        //            $('#ddlDeptRetirementEmployee').append(selectOptions);
        //            $("#ddlDeptRetirementEmployee")[0].selectedIndex = 1;

        //            $('#ddlDeptContractualEmployee').append(selectOptions);
        //            $("#ddlDeptContractualEmployee")[0].selectedIndex = 1;


        //        },
        //        error: function (err) {
        //            alert(err);
        //        }
        //    });
        //}


        $('#ddlCompany').on('change',
            function () {
                 
                var comId = $('#ddlCompany').val();
                var selecteddiv = $('#ddlDivision').val();



                $.ajax({
                    async: false,
                    type: "POST",
                    url: "ChartNew.aspx/GetEmpCountForDept",
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),
                    success: function (data) {
                        $('#genderPieChartByDept').empty();
                        var result = data.d;
                        var piChartData = [];
                        var piChartLabel = [];
                        for (var i in result) {
                            piChartData.push(result[i].Value);
                            piChartLabel.push(result[i].TextField);
                        }
                        data = {
                            datasets: [
                                {
                                    data: piChartData,
                                    backgroundColor: ['rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)'],
                                        hoverBackgroundColor: [
                                            "#61b0ff" 
                                        ],
                                    labels: piChartLabel
                                }
                            ],
                            // These labels appear in the legend and in the tooltips when hovering different arcs
                            labels: piChartLabel
                        };
                        
                        var ctxPie = document.getElementById('genderPieChartByDept').getContext('2d');
                        var myPieChart = new Chart(ctxPie, {
                            type: 'pie',
                            data: data, animationEnabled: true,
                            legend: {
                                verticalAlign: "center",
                                horizontalAlign: "left",
                                fontSize: 20,
                                fontFamily: "Helvetica"
                            },
                            theme: "light2",
                           
                                title: {
                            display: true 
                          
                    },
                    animation: {
                        animateScale: true,
                        animateRotate: true
                    },
                            options: {
                               
                                showAllTooltips: true

                            }
                        });
                        if (window.myLine != undefined) {
                            window.myLine.destroy();
                        }

                        window.myLine = myPieChart;

                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });



        $('#ddlDivision').on('change',
           function () {
                
               var comId = $('#ddlCompany').val();
               var selecteddiv = $('#ddlDivision').val();



               $.ajax({
                   async: false,
                   type: "POST",
                   url: "ChartNew.aspx/GetEmpCountForDept",
                   dataType: "JSON",
                   contentType: "application/json;charset=utf-8",
                   data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),
                   success: function (data) {

                       var result = data.d;
                       var piChartData = [];
                       var piChartLabel = [];
                       for (var i in result) {
                           piChartData.push(result[i].Value);
                           piChartLabel.push(result[i].TextField);
                       }
                       data = {
                           datasets: [
                               {
                                   data: piChartData,
                                   backgroundColor: ['rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)'],
                                   hoverBackgroundColor: [
                                        "#61b0ff"
                                   ],
                                   labels: piChartLabel
                               }
                           ],
                           // These labels appear in the legend and in the tooltips when hovering different arcs
                           labels: piChartLabel
                       };
                       var ctxPie = document.getElementById('genderPieChartByDept').getContext('2d');
                       if (window.myLine != undefined) {
                           window.myLine.destroy();
                       }

                       var myPieChart = new Chart(ctxPie, {
                           type: 'pie',
                           data: data, animationEnabled: true,
                           legend: {
                               verticalAlign: "center",
                               horizontalAlign: "left",
                               fontSize: 20,
                               fontFamily: "Helvetica"
                           },
                           theme: "light2",

                           title: {
                               display: true
                              
                           },
                           animation: {
                               animateScale: true,
                               animateRotate: true
                           },
                           options: {
                               showAllTooltips: true

                           }
                       });


                       window.myLine = myPieChart;
                   },
                   error: function (err) {
                       alert(err);
                   }
               });
           });




        $('#ddlDept').on('change',
            function () {
                 
                var comId = $('#ddlCompany').val();
                var selecteddiv = $('#ddlDivision').val();
                var selectedDept = $('#ddlDept').val();


                $.ajax({
                    async: false,
                    type: "POST",
                    url: "ChartNew.aspx/GetEmpCountForDeptByDepttt",
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv, "selectedDept": selectedDept }),
                    success: function (data) {
                        var result = data.d;
                        var piChartData = [];
                        var piChartLabel = [];
                        for (var i in result) {
                            piChartData.push(result[i].Value);
                            piChartLabel.push(result[i].TextField);
                        }
                        data = {
                            datasets: [
                                {
                                    data: piChartData,
                                    backgroundColor: ['rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)'],
                                    hoverBackgroundColor: [
                                        "#61b0ff"
                                    ],
                                    labels: piChartLabel
                                }
                            ],
                            // These labels appear in the legend and in the tooltips when hovering different arcs

                            //labels: piChartLabel
                            
                        };
                        var ctxPie = document.getElementById('genderPieChartByDept').getContext('2d');

                        if (window.myLine != undefined) {
                            window.myLine.destroy();
                        }

                        var myPieChart = new Chart(ctxPie, {
                            type: 'pie',
                            data: data,
                            animationEnabled: true,
                            legend: {
                                verticalAlign: "center",
                                horizontalAlign: "left",
                                fontSize: 20,
                                fontFamily: "Helvetica"
                            },
                            theme: "light2",

                            title: {
                                display: true 
                            },
                            animation: {
                                animateScale: true,
                                animateRotate: true
                            },
                            options: {
                               
                                showAllTooltips: true

                            }
                        });

                       
                        window.myLine = myPieChart;
                    },
                    error: function (err) {
                        alert(err);
                    }


                });
                
            });





        //Employee Type Ratio
        $('#ddlCompany').on('change',
            function () {
                 
                var comId = $('#ddlCompany').val();
                var selecteddiv = $('#ddlDivisionETR').val();

                $.ajax({
                    type: "POST",
                    url: "ChartNew.aspx/GetEmployeeType",
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),
                    success: function (data) {
                        var result = data.d;
                        var piChartData = [];
                        var piChartLabel = [];
                        for (var i in result) {


                            piChartData.push(result[i].Value);
                            piChartLabel.push(result[i].TextField);
                        }
                        data = {
                            datasets: [
                                {
                                    data: piChartData,
                                    backgroundColor: ['rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)', 'rgba(255, 61, 103, 0.7)'] 
                                }
                            ],
                            // These labels appear in the legend and in the tooltips when hovering different arcs
                            labels: piChartLabel
                        };


                        var ctxDoughnut = document.getElementById('genderDoughnutChartByDept').getContext('2d');

                        if (window.doughnut != undefined) {
                            window.doughnut.destroy();
                        }
                        var myDoughnutChart = new Chart(ctxDoughnut, {
                            type: 'polarArea',
                            data: data,
                            options: {
                                cutoutPercentage: 50
                            }
                        });

                        window.doughnut = myDoughnutChart;
 
                    },
                    error: function (err) {
                        alert(err);
                    }
                });

            });


        $('#ddlDivisionETR').on('change',
           function () {
                
               var comId = $('#ddlCompany').val();
               var selecteddiv = $('#ddlDivisionETR').val();

               $.ajax({
                   type: "POST",
                   url: "ChartNew.aspx/GetEmployeeType",
                   dataType: "JSON",
                   contentType: "application/json;charset=utf-8",
                   data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),
                   success: function (data) {
                       var result = data.d;
                       var piChartData = [];
                       var piChartLabel = [];
                       for (var i in result) {


                           piChartData.push(result[i].Value);
                           piChartLabel.push(result[i].TextField);
                       }
                       data = {
                           datasets: [
                               {
                                   data: piChartData,
                                   backgroundColor: ['rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)', 'rgba(255, 61, 103, 0.7)'],
                                   showInLegend: true,
                                   toolTipContent: "{label} <br/> {y} ",
                                   indexLabel: "{y} %",
                                   dataPoints: piChartLabel
                               }
                           ],
                           // These labels appear in the legend and in the tooltips when hovering different arcs
                           labels: piChartLabel
                       };


                       var ctxDoughnut = document.getElementById('genderDoughnutChartByDept').getContext('2d');
                       if (window.doughnut != undefined) {
                           window.doughnut.destroy();
                       }
                       var myDoughnutChart = new Chart(ctxDoughnut, {
                           type: 'polarArea',
                           data: data,
                           options: {
                               cutoutPercentage: 50
                           }
                       });

                       window.doughnut = myDoughnutChart;
                   },
                   error: function (err) {
                       alert(err);
                   }
               });

           });




        $('#ddlDeptETR').on('change',
            function () {
                 
                var comId = $('#ddlCompany').val();
                var selecteddiv = $('#ddlDivision').val();
                var selectedDept = $('#ddlDeptETR').val();
                $.ajax({
                    type: "POST",
                    url: "ChartNew.aspx/GetEmployeeTypeByDpt",
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv, "selectedDept": selectedDept }),
                    success: function (data) {
                        var result = data.d;
                        var piChartData = [];
                        var piChartLabel = [];
                        for (var i in result) {


                            piChartData.push(result[i].Value);
                            piChartLabel.push(result[i].TextField);
                        }
                        data = {
                            datasets: [
                                {
                                    data: piChartData,
                                    backgroundColor: ['rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)', 'rgba(255, 61, 103, 0.7)']
                                }
                            ],
                            // These labels appear in the legend and in the tooltips when hovering different arcs
                            labels: piChartLabel
                        };


                        var ctxDoughnut = document.getElementById('genderDoughnutChartByDept').getContext('2d');
                        var myDoughnutChart = new Chart(ctxDoughnut, {
                            type: 'polarArea',
                            data: data,
                            options: {
                                cutoutPercentage: 50
                            }
                        });
                        if (window.doughnut != undefined) {
                            window.doughnut.destroy();
                        }

                        window.doughnut = myDoughnutChart;
                    },
                    error: function (err) {
                        alert(err);
                    }
                });

            });




        //Year Wise Seperation

        $('#ddlCompany').on('change',
            function () {
                 
                var comId = $('#ddlCompany').val();
                var selecteddiv = $('#ddlYearWiseSeperationDivision').val();
                $.ajax({
                    type: "POST",
                    url: "ChartNew.aspx/GetYearWiseSeperation",
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),
                    success: function (data) {

                        var result = data.d;
                        var piChartData = [];
                        var piChartLabel = [];
                        for (var i in result) {
                            piChartData.push(result[i].Value);
                            piChartLabel.push(result[i].TextField);
                        }
                        data = {

                            labels: piChartLabel,
                            datasets: [
                                {

                                    data: piChartData,


                                    backgroundColor: "blue",
                                    borderColor: "lightblue",
                                    fill: false,
                                    lineTension: 0,
                                    radius: 5,

                                    strokeColor: "rgba(220,180,0,1)",
                                    pointColor: "rgba(220,180,0,1)",

                                }
                            ]

                            // These labels appear in the legend and in the tooltips when hovering different arcs
                            // labels: piChartLabel,

                            // String - A legend template
                            //  legendTemplate: piChartLabel,
                            // String - A tooltip template
                            // tooltipTemplate: piChartData

                        };


                        var ctxDoughnut = document.getElementById('BubbleChartYearWiseSeperationByDept').getContext('2d');
                        var myDoughnutChart = new Chart(ctxDoughnut, {
                            type: 'line',
                            data: data,
                            options: {
                                legend: {
                                    display: false
                                },
                                pointDotRadius: 10,
                                bezierCurve: false,
                                scaleShowVerticalLines: false,
                                scaleGridLineColor: "black"

                                
                            }

                        });

                        if (window.bar != undefined) {
                            window.bar.destroy();
                        }

                        window.bar = myDoughnutChart;
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });



        $('#ddlYearWiseSeperationDivision').on('change',
          function () {
               
              var comId = $('#ddlCompany').val();
              var selecteddiv = $('#ddlYearWiseSeperationDivision').val();
              $.ajax({
                  type: "POST",
                  url: "ChartNew.aspx/GetYearWiseSeperation",
                  dataType: "JSON",
                  contentType: "application/json;charset=utf-8",
                  data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),
                  success: function (data) {

                      var result = data.d;
                      var piChartData = [];
                      var piChartLabel = [];
                      for (var i in result) {
                          piChartData.push(result[i].Value);
                          piChartLabel.push(result[i].TextField);
                      }
                      data = {

                          labels: piChartLabel,
                          datasets: [
                              {

                                  data: piChartData,

                                  backgroundColor: "blue",
                                  borderColor: "lightblue",
                                  fill: false,
                                  lineTension: 0,
                                  radius: 5,

                                  strokeColor: "rgba(220,180,0,1)",
                                  pointColor: "rgba(220,180,0,1)",
                              }
                          ]

                          // These labels appear in the legend and in the tooltips when hovering different arcs
                          // labels: piChartLabel,

                          // String - A legend template
                          //  legendTemplate: piChartLabel,
                          // String - A tooltip template
                          // tooltipTemplate: piChartData

                      };


                      var ctxDoughnut = document.getElementById('BubbleChartYearWiseSeperationByDept').getContext('2d');
                      var myDoughnutChart = new Chart(ctxDoughnut, {
                          type: 'line',
                          data: data,
                          options: {
                              legend: {
                                  display: false
                              },
                              pointDotRadius: 10,
                              bezierCurve: false,
                              scaleShowVerticalLines: false,
                              scaleGridLineColor: "black"

                                
                           
                          }
                      });

                      if (window.bar != undefined) {
                          window.bar.destroy();
                      }

                      window.bar = myDoughnutChart;
                  },
                  error: function (err) {
                      alert(err);
                  }
              });
          });



        $('#ddlYearWiseSeperationDept').on('change',
            function () {
                 
                var comId = $('#ddlCompany').val();
                var selecteddiv = $('#ddlYearWiseSeperationDivision').val();
                var selectedDept = $('#ddlYearWiseSeperationDept').val();
                $.ajax({
                    type: "POST",
                    url: "ChartNew.aspx/GetYearWiseSeperationDept",
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv, "selectedDept": selectedDept }),
                    success: function (data) {

                        var result = data.d;
                        var piChartData = [];
                        var piChartLabel = [];
                        for (var i in result) {
                            piChartData.push(result[i].Value);
                            piChartLabel.push(result[i].TextField);
                        }
                        data = {

                            labels: piChartLabel,
                            datasets: [
                                {

                                    data: piChartData,
                                    backgroundColor: "blue",
                                    borderColor: "lightblue",
                                    fill: false,
                                    lineTension: 0,
                                    radius: 5,
                                 
                                    strokeColor: "rgba(220,180,0,1)",
                                    pointColor: "rgba(220,180,0,1)",

                                }
                            ]

                            // These labels appear in the legend and in the tooltips when hovering different arcs
                            // labels: piChartLabel,

                            // String - A legend template
                            //  legendTemplate: piChartLabel,
                            // String - A tooltip template
                            // tooltipTemplate: piChartData

                        };


                        var ctxDoughnut = document.getElementById('BubbleChartYearWiseSeperationByDept').getContext('2d');
                        var myDoughnutChart = new Chart(ctxDoughnut, {
                            type: 'line',
                            data: data,
                            options: {
                                legend: {
                                    display: false
                                },
                                pointDotRadius: 10,
                                bezierCurve: false,
                                scaleShowVerticalLines: false,
                                scaleGridLineColor: "black"

                                
                            }
                             
                        });
                        if (window.bar != undefined) {
                            window.bar.destroy();
                        }

                        window.bar = myDoughnutChart;
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });

        //Age Group




        $('#ddlCompany').on('change',
            function () {
                 
                var comId = $('#ddlCompany').val();
                var selecteddiv = $('#ddlDivisionAgeGroup').val();
              
                s2.innerHTML = "";
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "ChartNew.aspx/GetAgeGroup",
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),
                   
                    success: function (data) {
                        $('#BubbleChartAgeGroupByDept').empty();
                        //$('#BubbleChartAgeGroupByDept').clear();
                        //$('#BubbleChartAgeGroupByDept').destroy();
                        var result = data.d;
                        var piChartData = [];
                        var piChartLabel = [];
                        for (var i in result) {
                            piChartData.push(result[i].Value);
                            piChartLabel.push(result[i].TextField);
                        }
                        data = {

                            labels: piChartLabel,
                            datasets: [
                                {
                                     
                                    data: piChartData,

                                    backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", 'rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)', 'rgba(255, 61, 103, 0.7)', "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#E6D9FF", "#750075"]
                                    ,
                                    borderColor: [
                                      'rgba(255,99,132,1)',
                                      'rgba(54, 162, 235, 1)',
                                      'rgba(255, 206, 86, 1)',
                                      'rgba(75, 192, 192, 1)',
                                      'rgba(153, 102, 255, 1)',
                                      'rgba(255, 159, 64, 1)',
                                      'rgba(255,99,132,1)',
                                      'rgba(54, 162, 235, 1)',
                                      'rgba(255, 206, 86, 1)',
                                      'rgba(75, 192, 192, 1)',
                                      'rgba(153, 102, 255, 1)',
                                      'rgba(255, 159, 64, 1)'
                                    ],
                                    borderWidth: 2
                                    
                                }
                            ]

                            // These labels appear in the legend and in the tooltips when hovering different arcs
                           // labels: piChartLabel,
                         
                            // String - A legend template
                          //  legendTemplate: piChartLabel,
                            // String - A tooltip template
                           // tooltipTemplate: piChartData

                        };


                        var ctxDoughnut = document.getElementById('BubbleChartAgeGroupByDept').getContext('2d');

                        
                         ctxDoughnut.innerHTML = "";

                        //if (document.getElementById('BubbleChartAgeGroupByDept')) {
                        //    document.getElementById('BubbleChartAgeGroupByDept').destroy();
                        //}


                        var myDoughnutChart = new Chart(ctxDoughnut, {
                            type: 'bar',
                            data: data,
                            maintainAspectRatio: false,
                            options: {
                               
                                legend: {
                                    display: false
                                },
                                //title: {
                                //    display: true,
                                //    text: 'Employee Age Group',
                                //    position: 'bottom'
                                //},
                               
                                
                                scales: {
                                    xAxes: [{
                                        barPercentage: 0.4,
                                        barThickness: 25,
                                        maxBarThickness: 30,
                                        minBarLength: 7,
                                        ticks: {
                                            beginAtZero: true
                                        },
                                        gridLines: {
                                            offsetGridLines: true
                                        }
                                    }],
                                    yAxes: [{
                                       
                                        ticks: {
                                            beginAtZero: true,
                                            stacked: true
                                            //min: 0,
                                            //max: 100
                                        }
                                    }]
                                }
                            }
                        });

                        if (window.pipe != undefined) {
                            window.pipe.destroy();
                        }

                        window.pipe = myDoughnutChart;

                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });




        $('#ddlDivisionAgeGroup').on('change',
            function () {
                 
                var comId = $('#ddlCompany').val();
                var selecteddiv = $('#ddlDivisionAgeGroup').val();
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "ChartNew.aspx/GetAgeGroup",
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),
                    success: function (data) {
                        $('#BubbleChartAgeGroupByDept').empty();
                        //$('#BubbleChartAgeGroupByDept').clear();
                        //$('#BubbleChartAgeGroupByDept').destroy();
                        var result = data.d;
                        var piChartData = [];
                        var piChartLabel = [];
                        for (var i in result) {
                            piChartData.push(result[i].Value);
                            piChartLabel.push(result[i].TextField);
                        }
                        data = {

                            labels: piChartLabel,
                            datasets: [
                                {
                                    
                                    data: piChartData,

                                    backgroundColor: [ 'rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)', 'rgba(255, 61, 103, 0.7)', "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#E6D9FF", "#750075"]
                                    ,
                                    borderColor: [
                                      'rgba(255,99,132,1)',
                                      'rgba(54, 162, 235, 1)',
                                      'rgba(255, 206, 86, 1)',
                                      'rgba(75, 192, 192, 1)',
                                      'rgba(153, 102, 255, 1)',
                                      'rgba(255, 159, 64, 1)',
                                      'rgba(255,99,132,1)',
                                      'rgba(54, 162, 235, 1)',
                                      'rgba(255, 206, 86, 1)',
                                      'rgba(75, 192, 192, 1)',
                                      'rgba(153, 102, 255, 1)',
                                      'rgba(255, 159, 64, 1)'
                                    ],
                                    borderWidth: 2

                                }
                            ]

                            // These labels appear in the legend and in the tooltips when hovering different arcs
                            // labels: piChartLabel,

                            // String - A legend template
                            //  legendTemplate: piChartLabel,
                            // String - A tooltip template
                            // tooltipTemplate: piChartData

                        };


                        var ctxDoughnut = document.getElementById('BubbleChartAgeGroupByDept').getContext('2d');
                    

                         ctxDoughnut.innerHTML = "";

                        //if (document.getElementById('BubbleChartAgeGroupByDept')) {
                        //    document.getElementById('BubbleChartAgeGroupByDept').destroy();
                        //}


                        var myDoughnutChart = new Chart(ctxDoughnut, {
                            type: 'bar',
                            data: data,
                            maintainAspectRatio: false,
                            options: {
                             
                                legend: {
                                    display: false
                                },
                                //title: {
                                //    display: true,
                                //    text: 'Employee Age Group',
                                //    position: 'bottom'
                                //},
                               

                                scales: {
                                    xAxes: [{
                                        barPercentage: 0.4,
                                        barThickness: 25,
                                        maxBarThickness: 30,
                                        minBarLength: 7,
                                        
                                        ticks: {
                                            beginAtZero: true
                                        },
                                        gridLines: {
                                            offsetGridLines: true
                                        }
                                    }],
                                    yAxes: [{
                                       
                                        ticks: {
                                            beginAtZero: true,
                                            stacked: true
                                            //min: 0,
                                            //max: 100
                                        }
                                    }]
                                }
                            }
                        });

                        if (window.pipe != undefined) {
                            window.pipe.destroy();
                        }

                        window.pipe = myDoughnutChart;
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });


        $('#ddlDeptAgeGroup').on('change',
            function () {
                var comId = $('#ddlCompany').val();
                var selecteddiv = $('#ddlDivisionAgeGroup').val();
                var selectedDept = $('#ddlDeptAgeGroup').val();
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "ChartNew.aspx/GetAgeGroupDept",
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv, "selectedDept": selectedDept }),
                    success: function (data) {
                        $('#BubbleChartAgeGroupByDept').empty();
                        //$('#BubbleChartAgeGroupByDept').clear();
                        //$('#BubbleChartAgeGroupByDept').destroy();
                        var result = data.d;
                        var piChartData = [];
                        var piChartLabel = [];
                        for (var i in result) {
                            piChartData.push(result[i].Value);
                            piChartLabel.push(result[i].TextField);
                        }
                        data = {

                            labels: piChartLabel,
                            datasets: [
                                {
                                   
                                    data: piChartData,

                                    backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", 'rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)', 'rgba(255, 61, 103, 0.7)', "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#E6D9FF", "#750075"]
                                    ,
                                    borderColor: [
                                      'rgba(255,99,132,1)',
                                      'rgba(54, 162, 235, 1)',
                                      'rgba(255, 206, 86, 1)',
                                      'rgba(75, 192, 192, 1)',
                                      'rgba(153, 102, 255, 1)',
                                      'rgba(255, 159, 64, 1)',
                                      'rgba(255,99,132,1)',
                                      'rgba(54, 162, 235, 1)',
                                      'rgba(255, 206, 86, 1)',
                                      'rgba(75, 192, 192, 1)',
                                      'rgba(153, 102, 255, 1)',
                                      'rgba(255, 159, 64, 1)'
                                    ],
                                    borderWidth: 2

                                }
                            ]

                            // These labels appear in the legend and in the tooltips when hovering different arcs
                            // labels: piChartLabel,

                            // String - A legend template
                            //  legendTemplate: piChartLabel,
                            // String - A tooltip template
                            // tooltipTemplate: piChartData

                        };


                        var ctxDoughnut = document.getElementById('BubbleChartAgeGroupByDept').getContext('2d');
                        ctxDoughnut.innerHTML = "";

                        //if (document.getElementById('BubbleChartAgeGroupByDept')) {
                        //    document.getElementById('BubbleChartAgeGroupByDept').destroy();
                        //}

                        var myDoughnutChart = new Chart(ctxDoughnut, {
                            type: 'bar',
                            data: data,
                            maintainAspectRatio: false,
                            options: {

                                legend: {
                                    display: false
                                },
                                //title: {
                                //    display: true,
                                //    text: 'Employee Age Group',
                                //    position: 'bottom'
                                //},

                              

                                scales: {
                                    xAxes: [{
                                        barPercentage: 0.4,
                                        barThickness: 25,
                                        maxBarThickness: 30,
                                        minBarLength: 7,
                                        ticks: {
                                            beginAtZero: true
                                        },
                                        gridLines: {
                                            offsetGridLines: true
                                        }
                                    }],
                                    yAxes: [{
                                      
                                        ticks: {
                                            beginAtZero: true,
                                          
                                            stacked: true
                                            //max:100
                                        }
                                    }]
                                }
                            }
                        });

                        if (window.pipe != undefined) {
                            window.pipe.destroy();
                        }

                        window.pipe = myDoughnutChart;
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });





        //Grade wise Employee



        $('#ddlCompany').on('change',
            function () {
                var comId = $('#ddlCompany').val();
                var selecteddiv = $('#ddlDivisionGWE').val();
                $.ajax({
                    type: "POST",
                    url: "ChartNew.aspx/GradewiseEmployeeByDept",
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),
                    success: function (data) {
                        $('#horizontalBarGradewiseEmployeeByDept').empty();
                        var result = data.d;
                        var piChartData = [];
                        var piChartLabel = [];
                        for (var i in result) {
                            piChartData.push(result[i].Value);
                            piChartLabel.push(result[i].TextField);
                        }
                        data = {
                            datasets: [
                                {
                                    data: piChartData,

                                    backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", 'rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)', 'rgba(255, 61, 103, 0.7)', "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#E6D9FF", "#750075", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                                    borderColor: "#3e95cd",
                                }
                            ],
                            // These labels appear in the legend and in the tooltips when hovering different arcs
                            labels: piChartLabel

                        };


                        var ctxDoughnut = document.getElementById('horizontalBarGradewiseEmployeeByDept').getContext('2d');
                        var myDoughnutChart = new Chart(ctxDoughnut, {
                            type: 'horizontalBar',
                            data: data, maintainAspectRatio: false,
                            options: {
                                legend: {
                                    display: false
                                },
                                scales: {
                                    xAxes: [{
                                        barPercentage: 5,
                                        barThickness: 25,
                                        maxBarThickness: 30,
                                        minBarLength: 15,
                                        gridLines: {
                                            offsetGridLines: true
                                        }
                                    }]
                                }
                            }
                        });
                        if (window.s != undefined) {
                            window.s.destroy();
                        }

                        window.s = myDoughnutChart;
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });


        $('#ddlDivisionGWE').on('change',
         function () {
             var comId = $('#ddlCompany').val();
             var selecteddiv = $('#ddlDivisionGWE').val();
             $.ajax({
                 type: "POST",
                 url: "ChartNew.aspx/GradewiseEmployeeByDept",
                 dataType: "JSON",
                 contentType: "application/json;charset=utf-8",
                 data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv }),
                 success: function (data) {
                     $('#horizontalBarGradewiseEmployeeByDept').empty();
                     var result = data.d;
                     var piChartData = [];
                     var piChartLabel = [];
                     for (var i in result) {
                         piChartData.push(result[i].Value);
                         piChartLabel.push(result[i].TextField);
                     }
                     data = {
                         datasets: [
                             {
                                 data: piChartData,

                                 backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", 'rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)', 'rgba(255, 61, 103, 0.7)', "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#E6D9FF", "#750075", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                                 borderColor: "#3e95cd",
                             }
                         ],
                         // These labels appear in the legend and in the tooltips when hovering different arcs
                         labels: piChartLabel

                     };


                     var ctxDoughnut = document.getElementById('horizontalBarGradewiseEmployeeByDept').getContext('2d');
                     var myDoughnutChart = new Chart(ctxDoughnut, {
                         type: 'horizontalBar',
                         data: data,
                         options: {
                             legend: {
                                 display: false
                             },
                             scales: {
                                 xAxes: [{
                                     barPercentage: 5,
                                     barThickness: 25,
                                     maxBarThickness: 30,
                                     minBarLength: 15,
                                     gridLines: {
                                         offsetGridLines: true
                                     }
                                 }]
                             }
                         }
                     });
                     if (window.s != undefined) {
                         window.s.destroy();
                     }

                     window.s = myDoughnutChart;
                 },
                 error: function (err) {
                     alert(err);
                 }
             });
         });




        $('#ddlDeptGWE').on('change',
       function () {
           var comId = $('#ddlCompany').val();
           var selecteddiv = $('#ddlDivisionGWE').val();
           var selectedDept = $('#ddlDeptGWE').val();
           $.ajax({
               type: "POST",
               url: "ChartNew.aspx/GradewiseEmployeeByDeptForDep",
               dataType: "JSON",
               contentType: "application/json;charset=utf-8",
               data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv,   "selectedDept": selectedDept }),
               success: function (data) {
                   $('#horizontalBarGradewiseEmployeeByDept').empty();
                   var result = data.d;
                   var piChartData = [];
                   var piChartLabel = [];
                   for (var i in result) {
                       piChartData.push(result[i].Value);
                       piChartLabel.push(result[i].TextField);
                   }
                   data = {
                       datasets: [
                           {
                               data: piChartData,

                               backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", 'rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)', 'rgba(255, 61, 103, 0.7)', "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#E6D9FF", "#750075", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                               borderColor: "#3e95cd",
                           }
                       ],
                       // These labels appear in the legend and in the tooltips when hovering different arcs
                       labels: piChartLabel

                   };


                   var ctxDoughnut = document.getElementById('horizontalBarGradewiseEmployeeByDept').getContext('2d');
                   var myDoughnutChart = new Chart(ctxDoughnut, {
                       type: 'horizontalBar',
                       data: data,
                       options: {
                           legend: {
                               display: false
                           },
                           scales: {
                               xAxes: [{
                                   barPercentage: 5,
                                   barThickness: 25,
                                   maxBarThickness: 30,
                                   minBarLength: 15,
                                   gridLines: {
                                       offsetGridLines: true
                                   }
                               }]
                           }
                       }
                   });
                   if (window.s != undefined) {
                       window.s.destroy();
                   }

                   window.s = myDoughnutChart;
               },
               error: function (err) {
                   alert(err);
               }
           });
       });




        //Length of Service



        $('#ddlCompany').on('change',
            function () {
             
                var comId = $('#ddlCompany').val();
                var selecteddiv = $('#ddlDivLengthofService').val();
                var dDate = $("#refDate").val();
                $.ajax({
                    type: "POST",
                    url: "ChartNew.aspx/GetLengthofService",
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId":comId, "selecteddiv": selecteddiv, "dDate": dDate }),
                    success: function (data) {
                        var result = data.d;
                        var piChartData = [];
                        var piChartLabel = [];
                        for (var i in result) {
                            piChartData.push(result[i].Value);
                            piChartLabel.push(result[i].TextField);
                        }
                        data = {
                            datasets: [
                                {
                                    data: piChartData,

                                    backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", 'rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)', 'rgba(255, 61, 103, 0.7)', "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#E6D9FF", "#750075"],
                                    borderColor: "#3e95cd",
                                }
                            ],
                            // These labels appear in the legend and in the tooltips when hovering different arcs
                            labels: piChartLabel

                        };


                        var ctxDoughnut = document.getElementById('BarChartLengthofServiceByDept').getContext('2d');
                        var myDoughnutChart = new Chart(ctxDoughnut, {
                            type: 'bar',
                            data: data,
                            options: {
                                legend: {
                                    display: false
                                },
                                scales: {
                                    xAxes: [{
                                        barPercentage: 2,
                                        barThickness: 25,
                                        maxBarThickness: 30,
                                        minBarLength: 10,
                                        gridLines: {
                                            offsetGridLines: true
                                        }
                                    }]
                                }
                            }
                        });
                        if (window.g != undefined) {
                            window.g.destroy();
                        }

                        window.g = myDoughnutChart;
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });



        $('#ddlDivLengthofService').on('change',
           function () {
             
               var comId = $('#ddlCompany').val();
               var selecteddiv = $('#ddlDivLengthofService').val();
               var dDate = $("#refDate").val();
               $.ajax({
                   type: "POST",
                   url: "ChartNew.aspx/GetLengthofService",
                   dataType: "JSON",
                   contentType: "application/json;charset=utf-8",
                   data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv, "dDate": dDate }),
                   success: function (data) {
                       var result = data.d;
                       var piChartData = [];
                       var piChartLabel = [];
                       for (var i in result) {
                           piChartData.push(result[i].Value);
                           piChartLabel.push(result[i].TextField);
                       }
                       data = {
                           datasets: [
                               {
                                   data: piChartData,

                                   backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", 'rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)', 'rgba(255, 61, 103, 0.7)', "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#E6D9FF", "#750075"],
                                   borderColor: "#3e95cd",
                               }
                           ],
                           // These labels appear in the legend and in the tooltips when hovering different arcs
                           labels: piChartLabel

                       };


                       var ctxDoughnut = document.getElementById('BarChartLengthofServiceByDept').getContext('2d');
                       var myDoughnutChart = new Chart(ctxDoughnut, {
                           type: 'bar',
                           data: data,
                           options: {
                               legend: {
                                   display: false
                               },
                               scales: {
                                   xAxes: [{
                                       barPercentage: 2,
                                       barThickness: 25,
                                       maxBarThickness: 30,
                                       minBarLength: 10,
                                       gridLines: {
                                           offsetGridLines: true
                                       }
                                   }]
                               }
                           }
                       });
                       if (window.g != undefined) {
                           window.g.destroy();
                       }

                       window.g = myDoughnutChart;
                   },
                   error: function (err) {
                       alert(err);
                   }
               });
           });



        $('#ddlDeptLengthofService').on('change',
         function () {
             
             var comId = $('#ddlCompany').val();
             var selecteddiv = $('#ddlDivLengthofService').val();
             var selectedDept   = $('#ddlDeptLengthofService').val();
             var dDate = $("#refDate").val();
             $.ajax({
                 type: "POST",
                 url: "ChartNew.aspx/GetLengthofServiceFroDept",
                 dataType: "JSON",
                 contentType: "application/json;charset=utf-8",
                 data: JSON.stringify({ "comId": comId, "selecteddiv": selecteddiv, "selectedDept":selectedDept, "dDate": dDate }),
                 success: function (data) {
                     var result = data.d;
                     var piChartData = [];
                     var piChartLabel = [];
                     for (var i in result) {
                         piChartData.push(result[i].Value);
                         piChartLabel.push(result[i].TextField);
                     }
                     data = {
                         datasets: [
                             {
                                 data: piChartData,

                                 backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850", 'rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)', 'rgba(255, 61, 103, 0.7)', "#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#E6D9FF", "#750075"],
                                 borderColor: "#3e95cd",
                             }
                         ],
                         // These labels appear in the legend and in the tooltips when hovering different arcs
                         labels: piChartLabel

                     };


                     var ctxDoughnut = document.getElementById('BarChartLengthofServiceByDept').getContext('2d');
                     var myDoughnutChart = new Chart(ctxDoughnut, {
                         type: 'bar',
                         data: data,
                         options: {
                             legend: {
                                 display: false
                             },
                             scales: {
                                 xAxes: [{
                                     barPercentage: 2,
                                     barThickness: 25,
                                     maxBarThickness: 30,
                                     minBarLength: 10,
                                     gridLines: {
                                         offsetGridLines: true
                                     }
                                 }]
                             }
                         }
                     });
                     if (window.g != undefined) {
                         window.g.destroy();
                     }

                     window.g = myDoughnutChart;
                 },
                 error: function (err) {
                     alert(err);
                 }
             });
         });
    </script>





    <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=GridView1]").table2excel({
                filename: "Employee_Join_left_Data_Info.xls"
            });
        });








    </script>
    <script type="text/javascript">
        $("body").on("click", "#btnExportProbition", function () {
            $("[id*=ProbationGridView]").table2excel({
                filename: "Probation_Employee_List_Info.xls"
            });
        });








    </script>


    <script type="text/javascript">
        $("body").on("click", "#btnContractual", function () {
            $("[id*=ContractualGridView]").table2excel({
                filename: "Contractual_Employee_List_Info.xls"
            });
        });








    </script>




    <script type="text/javascript">
        $("body").on("click", "#btnloadGridView", function () {
            $("[id*=loadGridView]").table2excel({
                filename: "UpComing_Trainging_List_Info.xls"
            });
        });








    </script>


    <script type="text/javascript">
        $("body").on("click", "#btnRetirement", function () {
            $("[id*=RetirementGridView]").table2excel({
                filename: "Retirement_Employee_List_Info.xls"
            });
        });








    </script>

    <%-- <script type="text/javascript">
          $(function () {
              $.ajax({
                  type: "POST",
                  url: "ChartNew.aspx/GetProbationEmployeeList",
                  data: '{}',
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: OnSuccess,
                  failure: function (r) {
                      alert(r.d);
                  },
                  error: function (response) {
                      alert(r.d);
                  }
              });
          });

          function OnSuccess(r) {
              //Parse the XML and extract the records.
              var dd = $($.parseXML(r.d)).find("Table");

              //Reference GridView Table.
              var table = $("[id*=ProbationGridView]");

              //Reference the Dummy Row.
              var row = table.find("tr:last-child").clone(true);

              //Remove the Dummy Row.
              $("tr", table).not($("tr:first-child", table)).remove();

              //Loop through the XML and add Rows to the Table.
              $.each(dd, function () {
                  var customer = $(this);
                  $("td", row).eq(0).html($(this).find("EmpMasterCode").text());
                  $("td", row).eq(1).html($(this).find("EmpName").text());
                  $("td", row).eq(2).html($(this).find("DepartmentName").text());
                  $("td", row).eq(3).html($(this).find("Designation").text());
                  $("td", row).eq(4).html($(this).find("ProbationEndDate").text());
                  $("td", row).eq(5).html($(this).find("DateOfJoin").text());
                 
                  table.append(row);
                  row = table.find("tr:last-child").clone(true);
              });
          }
    </script>--%>

    <script src="../Assets/table2excel.js"></script>
</asp:Content>

