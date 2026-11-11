<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Chart.aspx.cs" Inherits="ChartTest_Chart" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <link href="../Assets/piechart.css" rel="stylesheet" />
    <link href="../Assets/barchart.css" rel="stylesheet" />

    <div class="content" id="content">




        <style>
            #cpFormBody_PieChart1 {
                border-color: white;
                border: none;
            }
    </style>
        <!-- PAGE HEADING -->

        <style>
            #cpFormBody_ProbationGridView > tbody > tr > th {
                padding: 9px 0;
                color: #fff;
                background-color: #5B799E;
                /*background-color: #98A9C0;*/
            }

            #cpFormBody_ProbationGridView > tbody > tr:not(th):nth-child(odd) {
                background-color: #DFDFDF;
            }




            #cpFormBody_ContractualGridView > tbody > tr > th {
                padding: 9px 0;
                color: #fff;
                background-color: #5B799E;
                /*background-color: #98A9C0;*/
            }

            #cpFormBody_ContractualGridView > tbody > tr:not(th):nth-child(odd) {
                background-color: #DFDFDF;
            }


            #cpFormBody_GridView1 > tbody > tr > th {
                padding: 9px 0;
                color: #fff;
                background-color: #5B799E;
                /*background-color: #98A9C0;*/
            }

            #cpFormBody_GridView1 > tbody > tr:not(th):nth-child(odd) {
                background-color: #DFDFDF;
            }

            #cpFormBody_loadGridView > tbody > tr > th {
                padding: 9px 0;
                color: #fff;
                background-color: #5B799E;
                /*background-color: #98A9C0;*/
            }

            #cpFormBody_loadGridView > tbody > tr:not(th):nth-child(odd) {
                background-color: #DFDFDF;
            }

            #cpFormBody_RetirementGridView > tbody > tr > th {
                padding: 9px 0;
                color: #fff;
                background-color: #5B799E;
                /*background-color: #98A9C0;*/
            }

            #cpFormBody_RetirementGridView > tbody > tr:not(th):nth-child(odd) {
                background-color: #DFDFDF;
            }
        </style>

        <div class="container-fluid" style="background-color: white">
            <div class="card" style="background-color: white">
                <div class="card-body" style="background-color: white">

                    <div class="row">

                        <div class="col-md-6" runat="server" visible="False">
                            <div class="form-group">
                                <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" OnSelectedIndexChanged="lchk_Company_OnSelectedIndexChanged" RepeatDirection="Horizontal" Enabled="True" />
                            </div>


                        </div>
                        <div class="col-md-2"></div>
                        <div class="col-md-2"></div>
                        <div class="col-md-2"></div>
                        <div class="col-md-2"></div>
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm" Enabled="True" />
                            </div>


                        </div>


                    </div>
                    <div class="row ">

                        <div class="col-md-12 card card-body" style="background-color: white">
                            <div class="row" runat="server">

                                <div class="col-md-2">
                                </div>


                                <div class="col-md-2">
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-3">
                                    <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click"><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>


                                    <asp:LinkButton ID="btnExportToPDF" runat="server" Visible="False" CssClass="btnPDF  pull-right"><span aria-hidden="true" class="glyphicon glyphicon-apple"></span>Export To PDF</asp:LinkButton>

                                </div>

                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                <ContentTemplate>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered text-center thead-dark" OnRowCreated="GridView1_OnRowCreated">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Month" HeaderText="Month" />

                                            <asp:BoundField DataField="PermJoin" HeaderText="Permanent" />
                                            <asp:BoundField DataField="ContJoin" HeaderText="Contractual" />
                                            <asp:BoundField DataField="PermContJoin" HeaderText="Program Contractual" />
                                            <asp:BoundField DataField="LeftPer" HeaderText="Permanent" />
                                            <asp:BoundField DataField="LeftCont" HeaderText="Contractual" />
                                            <asp:BoundField DataField="LeftPermCont" HeaderText="Program Contractual" />
                                            <asp:BoundField DataField="RetirePerm" HeaderText="Permanent" />
                                            <asp:BoundField DataField="RetireCont" HeaderText="Contractual" />


                                            <asp:BoundField DataField="RetirePermCont" HeaderText="Program Contractual" />
                                            <%--<asp:BoundField DataField="Total" HeaderText="Total"  />--%>
                                        </Columns>

                                    </asp:GridView>



                                    <style>
                                        .btnexcel {
                                            background-color: #4CAF50;
                                            border: none;
                                            color: white;
                                            padding: 8px 12px;
                                            text-align: center;
                                            text-decoration: none;
                                            display: inline-block;
                                            font-size: 12px;
                                            margin: 4px 2px;
                                            cursor: pointer;
                                        }

                                        .btnPDF {
                                            background-color: #008CBA;
                                            border: none;
                                            color: white;
                                            padding: 8px 12px;
                                            text-align: center;
                                            text-decoration: none;
                                            display: inline-block;
                                            font-size: 12px;
                                            margin: 4px 2px;
                                            cursor: pointer;
                                        }
                                    </style>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>

                        <div class="row">

                            <div class="col-md-6  card card-body" style="background-color: white">
                                <label style="font-size: 12px; text-align: center">Employee Gender Ratio</label>
                                <hr />
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">

                                    <ContentTemplate>
                                      <div class="row">
                                            <label style="padding: 10px;">Department: </label>
                                        <asp:DropDownList runat="server" ID="DeptDropDownList" CssClass="form-control form-control-sm" Width="200" AutoPostBack="True" OnSelectedIndexChanged="DeptDropDownList_OnSelectedIndexChanged" />
                                      </div>

                                        <div class="form-group">

                                            <div id="cpFormBody_PieChart1__ParentDiv" style="border-style: solid; border-width: 0px;">
                                                <cc1:PieChart ID="PieChart1"  EnableViewState="true" runat="server" ChartHeight="300" ChartWidth="620" BorderColor="White"
                                                    ChartType="Column" ChartTitleColor="#0E426C" BackColor="White" BorderWidth="0px" EnableTheming="True">
                                                </cc1:PieChart>
                                            </div>
                                             <div class="row" runat="server" >
                                                 <div class="col-md-4"></div>
                                                 <div class="col-md-4" style="text-align: center"> <asp:Label runat="server"    ID="lblToGender"></asp:Label> </div>
                                                 <div class="col-md-4"></div>
                                           
                                                 </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                            </div>
                            <div class="col-md-6  card card-body" style="background-color: white">

                                <label style="font-size: 12px; text-align: center">Employee Type Ratio</label>
                                <hr />
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">

                                    <ContentTemplate>
                                        <div class="form-group">
                                            <div id="cpFormBody_PieChart2__ParentDiv"  style="border-style: solid; border-width: 0px;">
                                                <cc1:PieChart ID="PieChart2"   EnableViewState="true" runat="server" ChartHeight="300" ChartWidth="620"
                                                    ChartType="Column">
                                                </cc1:PieChart>
                                            </div>
                                            <div class="row" runat="server" >
                                                 <div class="col-md-4"></div>
                                                 <div class="col-md-4" style="text-align: center"> <asp:Label   runat="server"  ID="lblEmpTypeRatio"></asp:Label></div>
                                                 <div class="col-md-4"></div>
                                           
                                                 </div>
                                               
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                        </div>
                        <%--<div class="row" runat="server" Visible="False">
                                 
                                
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <cc1:PieChart ID="PieChart3" runat="server" ChartHeight="300" ChartWidth = "300"
                                            ChartType="Column"  >
                                        </cc1:PieChart>
                                    </div>


                                </div>
                               
                            </div>--%>
                        <div class="row" runat="server" visible="false">
                            <div class="col-md-2"></div>
                            <div class="col-md-2"></div>
                            <div class="col-md-2"></div>
                            <div class="col-md-2"></div>
                            <div class="col-md-2"></div>
                            <div class="col-md-2  pull-right">
                                <div class="form-group">
                                    <label>Year </label>
                                    <asp:DropDownList runat="server" ID="ddlyear" CssClass="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlyear_OnSelectedIndexChanged" />
                                </div>


                            </div>


                        </div>
                        <div class="row" runat="server">

                            <div class="col-md-6" style="overflow: auto; width: 100%;" runat="server" visible="False">
                                <div class="form-group">
                                    <cc1:BarChart ID="BarChart1" runat="server" ChartHeight="300" ChartWidth="550"
                                        ChartType="Column" ChartTitleColor="#0E426C"
                                        CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">
                                    </cc1:BarChart>
                                </div>


                            </div>

                            <div class="col-md-6  card card-body" style="background-color: white">

                                 <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">

                                    <ContentTemplate>

                                <label style="font-size: 12px; text-align: center">Year Wise Seperation</label>
                                <hr />

                                <div class="row">

                                    <label style="padding: 10px;">Department: </label>
                                    <asp:DropDownList runat="server" ID="YearWiseDropDownList" CssClass="form-control form-control-sm" Width="150" AutoPostBack="True" OnSelectedIndexChanged="YearWiseDropDownList_OnSelectedIndexChanged" />

                                    <label style="padding: 10px;">From Year:</label>
                                    <asp:DropDownList runat="server" ID="FromYearDropDownList" CssClass="form-control form-control-sm" Width="100" AutoPostBack="False" OnSelectedIndexChanged="FromYearDropDownList_OnSelectedIndexChanged" />

                                    <label style="padding: 10px;">To Year:</label>
                                    <asp:DropDownList runat="server" ID="ToYearDropDownList" CssClass="form-control form-control-sm" Width="100" AutoPostBack="True" OnSelectedIndexChanged="ToYearDropDownList1_OnSelectedIndexChanged" />



                                </div>
                                <div class="form-group">
                                    <div id="cpFormBody_BarChart3__ParentDiv" style="border-style: none; border-width: 0px;">
                                        <cc1:LineChart ID="BarChart3" runat="server"  EnableViewState="true" ChartHeight="300" ChartWidth="540" ChartTitleColor="#0E426C"
                                            CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">
                                        </cc1:LineChart>
                                    </div>
                                </div>
                                         <div class="row" runat="server" >
                                                 <div class="col-md-4"></div>
                                                 <div class="col-md-4" style="text-align: center"> <asp:Label runat="server"    ID="lblYearWiseSeperation"></asp:Label> </div>
                                                 <div class="col-md-4"></div>
                                           
                                                 </div>

   </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            
                              <div class="col-md-6  card card-body" style="background-color: white">

                                <label style="font-size: 12px; text-align: center">Age Group</label>
                                <hr />
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">

                                    <ContentTemplate>
                                           <div class="row">
                                        <label style="padding: 10px;">Department: </label>
                                        <asp:DropDownList runat="server" ID="DptBarChart4DropDownList" CssClass="form-control form-control-sm" Width="200" AutoPostBack="True" OnSelectedIndexChanged="DptBarChart4DropDownList_OnSelectedIndexChanged" />
                                               </div>

                                        <div class="form-group">
                                            <br />
                                            <div id="cpFormBody_BarChart4__ParentDiv" style="border-style: solid; border-width: 0px;">
                                                <cc1:BarChart ID="BarChart4"  EnableViewState="true" runat="server" ChartHeight="300" ChartWidth="550"
                                                    ChartType="Column" ChartTitleColor="#0E426C"
                                                    CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">
                                                </cc1:BarChart>

                                            </div>
                                        </div>
                                        
                                         <div class="row" runat="server" >
                                                 <div class="col-md-4"></div>
                                                 <div class="col-md-4" style="text-align: center"> <asp:Label runat="server"    ID="lblAgeGroup"></asp:Label> </div>
                                                 <div class="col-md-4"></div>
                                           
                                                 </div>

                                    </ContentTemplate>

                                </asp:UpdatePanel>


                            </div>
                        </div>
                        <div class="row">
                        </div>
                        
                        
                             <div class="row">

                            <div class="col-md-12  card card-body" style="background-color: white">
                                <label style="font-size: 12px; text-align: center">Grade wise Employee</label>
                                <hr />


                                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">

                                    <ContentTemplate>
                                        <div style="overflow: auto; width: 100%;">
                                            <div id="cpFormBody_BarChart2__ParentDiv" style="border-style: solid; border-width: 0px;">
                                                <cc1:BarChart ID="BarChart2"  EnableViewState="true" runat="server"   ChartWidth="1100" ChartHeight="800"
                                                    ChartType="Bar" ChartTitleColor="#0E426C"
                                                    CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">
                                                </cc1:BarChart>
                                            </div>
                                        </div>
                                           <div class="row" runat="server" >
                                                 <div class="col-md-4"></div>
                                                 <div class="col-md-4" style="text-align: center"> <asp:Label runat="server"    ID="lbltoGradewiseEmployee"></asp:Label> </div>
                                                 <div class="col-md-4"></div>
                                           
                                                 </div>
                                    </ContentTemplate>

                                </asp:UpdatePanel>


                            </div>


                        </div>
                        <div class="row">

                          
                            <div class="col-md-6  card card-body" style="background-color: white">
                                <label style="font-size: 12px; text-align: center">Length of Service</label>
                                <hr />

                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">

                                    <ContentTemplate>

                                        <div class="row">

                                            <label style="padding: 10px;">Department: </label>
                                            <asp:DropDownList runat="server" ID="BarChart5DropDownList" CssClass="form-control form-control-sm" Width="150" AutoPostBack="True" OnSelectedIndexChanged="DptBarBarChart5DropDownList_OnSelectedIndexChanged" />

                                            <label style="padding: 10px;">Reference Date</label>

                                            <asp:TextBox ID="ReferenceDateTextBox" OnTextChanged="ReferenceDateTextBox_OnTextChanged" AutoPostBack="True" CssClass="form-control form-control-sm" Width="150" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                TargetControlID="ReferenceDateTextBox" />

                                        </div>
                                        <div class="form-group">

                                            <div id="cpFormBody_BarChart5__ParentDiv"  style="border-style: solid; border-width: 0px;">
                                                <cc1:BarChart ID="BarChart5"  EnableViewState="true" runat="server"  ChartHeight="300" ChartWidth="550"
                                                    ChartType="Column" ChartTitleColor="#0E426C"
                                                    CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">
                                                </cc1:BarChart>
                                            </div>
                                               <div class="row" runat="server" >
                                                 <div class="col-md-4"></div>
                                                 <div class="col-md-4" style="text-align: center"> <asp:Label runat="server"    ID="lblLengthofService"></asp:Label> </div>
                                                 <div class="col-md-4"></div>
                                           
                                                 </div>

                                        </div>

                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                            
                               <div class="col-md-6  card card-body" style="background-color: white">
                                <br />


                                <div class="row" runat="server">

                                    <div class="col-md-4">
                                        <h5>Upcomming Training List</h5>

                                    </div>


                                    
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-1">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:LinkButton ID="UpcommingTrainingLinkButton" runat="server" CssClass="btnexcel  pull-right" OnClick="btnUpcommingTrainingLinkButton_Click"><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>


                                        <asp:LinkButton ID="LinkButton2" runat="server" Visible="False" CssClass="btnPDF  pull-right"><span aria-hidden="true" class="glyphicon glyphicon-apple"></span>Export To PDF</asp:LinkButton>

                                    </div>

                                </div>
                                <hr />
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">

                                    <ContentTemplate>
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
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>


                        </div>
                        <div class="row">
                        </div>
                   

                        <div class="row">

                         

                        
                        </div>
                    </div>


                    <div class="row">

                        <div class="col-md-6  card card-body" style="background-color: white">
                       
                             <br />


                                <div class="row" runat="server">

                                    <div class="col-md-4">
                                        <h5>Probation Employee List</h5>

                                    </div>


                                    
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-1">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:LinkButton ID="ProbationLinkButton1" runat="server" CssClass="btnexcel  pull-right" OnClick="ProbationLinkButton_Click"><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>


                                        <asp:LinkButton ID="LinkButton4" runat="server" Visible="False" CssClass="btnPDF  pull-right"><span aria-hidden="true" class="glyphicon glyphicon-apple"></span>Export To PDF</asp:LinkButton>

                                    </div>

                                </div>
                            <hr />
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">

                                    <ContentTemplate>
                            <div class="row">
                                
                                            <label style="padding: 10px;">Department: </label>
                                            <asp:DropDownList runat="server" ID="DptProbationDropDownList" CssClass="form-control form-control-sm" Width="150" AutoPostBack="True" OnSelectedIndexChanged="DptProbationDropDownList_OnSelectedIndexChanged" />
                                        
                                        
                                </div> </ContentTemplate>

                                </asp:UpdatePanel>
                            <div id="gridContainer1" style="height: 300px; overflow: auto; width: auto;">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                                    <ContentTemplate>
                                        <asp:GridView ID="ProbationGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered  text-center thead-light" DataKeyNames="EmpInfoId">
                                            <Columns>
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

                                                
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>

                                </asp:UpdatePanel>

                            </div>
                        </div>
                        <div class="col-md-6  card card-body" style="background-color: white">
                            
                                      <div class="row" runat="server">

                                    <div class="col-md-4">
                                        <h5>Contractual Employee List</h5>

                                    </div>


                                    
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-1">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:LinkButton ID="ContractualLinkButton1" runat="server" CssClass="btnexcel  pull-right" OnClick="Contractual_Click"><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>


                                        <asp:LinkButton ID="LinkButton5" runat="server" Visible="False" CssClass="btnPDF  pull-right"><span aria-hidden="true" class="glyphicon glyphicon-apple"></span>Export To PDF</asp:LinkButton>

                                    </div>

                                </div>
                            <hr />
                             <asp:UpdatePanel ID="UpdatePanel12" runat="server">

                                    <ContentTemplate>
                            <div class="row">
                                 
                                            <label style="padding: 10px;">Department: </label>
                                            <asp:DropDownList runat="server" ID="DptContractualDropDownList" CssClass="form-control form-control-sm" Width="150" AutoPostBack="True" OnSelectedIndexChanged="DptContractualDropDownList_OnSelectedIndexChanged" />
                                        
                                </div>
                             </ContentTemplate>

                                </asp:UpdatePanel>
                              <br />


                      

                            <div id="gridContainer12" style="height: 300px; overflow: auto; width: auto;">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">

                                    <ContentTemplate>
                                        <asp:GridView ID="ContractualGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered  text-center thead-light" DataKeyNames="EmpInfoId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                               
                                                <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" />
                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                                                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                <asp:BoundField DataField="ContractEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                <asp:BoundField DataField="DateOfJoin" HeaderText="Joining Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                               
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>

                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        
                            <div class="col-md-6  card card-body" style="background-color: white">
                                
                                     <div class="row" runat="server">

                                    <div class="col-md-3">
                                        <h5>Retirement List</h5>

                                    </div>


                                    <div class="col-md-1">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-1">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:LinkButton ID="RetirementLinkButton1" runat="server" CssClass="btnexcel  pull-right" OnClick="Retirement_Click"><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>


                                        <asp:LinkButton ID="LinkButton3" runat="server" Visible="False" CssClass="btnPDF  pull-right"><span aria-hidden="true" class="glyphicon glyphicon-apple"></span>Export To PDF</asp:LinkButton>

                                    </div>

                                </div>

                                <hr />
  <asp:UpdatePanel ID="UpdatePanel13" runat="server">

                                    <ContentTemplate>
                                   <div class="row">
                                     
                                            <label style="padding: 10px;">Department: </label>
                                            <asp:DropDownList runat="server" ID="DptRetirementDropDownList" CssClass="form-control form-control-sm" Width="150" AutoPostBack="True" OnSelectedIndexChanged="DptRetirementDropDownList_OnSelectedIndexChanged" />
                                        
                                </div>
                             </ContentTemplate>

                                </asp:UpdatePanel>
                           
                                <div id="gridContainer188" style="height: 300px; overflow: auto; width: auto;">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">

                                        <ContentTemplate>
                                            <asp:GridView ID="RetirementGridView" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered  text-center thead-light" DataKeyNames="EmpInfoId">
                                                <Columns>
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

                                                  
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>

                                    </asp:UpdatePanel>

                                </div>
                            </div>
                    </div>



                </div>
            </div>
        </div>



    </div>

</asp:Content>

