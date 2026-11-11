<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="EmployeePromotionReport.aspx.cs" Inherits="Report_Pages_EmployeePromotionReport" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <div class="content" id="content">
        <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                      <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px"
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
        <!-- PAGE HEADING -->
        <div class="page-heading">
            <div class="page-heading__container">
                <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                <span></span>
                <h1 class="title" style="font-size: 18px; padding-top: 9px;"> <img src="app.png" />Promotion  											
 Report </h1>
            </div>

            <div class="page-heading__container float-right d-none d-sm-block">
                <asp:Button ID="homeButton" Visible="False" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                <asp:Button ID="addNewButton" Visible="False" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
            </div>



        </div>
        <!-- //END PAGE HEADING -->
         <style>
        fieldset.for-panel {
            background-color: #fcfcfc;
            border: 1px solid #999;
            padding: 15px 10px;
            background-color: white;
            margin-bottom: 12px;
        }

            fieldset.for-panel legend {
                background-color: #fafafa;
                border: 1px solid #ddd;
                border-radius: 1px;
                font-size: 12px;
                font-weight: bold;
                line-height: 10px;
                margin: inherit;
                padding: 7px;
                width: auto;
                margin-bottom: 0;
                color: black;
            }
    </style>
    <style>
        .chkChoiceHeader label {
            padding-left: 10px;
            padding-right: 40px;
            font-size: 13px;
        }


        .Label_Title {
            background-color: #C7C7C7;
            width: 100%;
            text-align: center;
            margin: 0px;
            padding: 3px;
            text-align: center;
            color: #000;
            margin-right: 5%;
            font-weight: bold;
            font-size: 13px;
        }

        .chkChoice label {
            padding-left: 10px;
            padding-right: 30px;
        }
    </style>
        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                           <%-- <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaiting">
                                        <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                            <div class="row" runat="server" visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            
                             <div class="row">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Report Header</legend>

                                        <div class="row">


                                            <div class="col-md-12">
                                                 <asp:CheckBoxList ID="cblHeader" RepeatDirection="Vertical" OnSelectedIndexChanged="cblHeader_OnSelectedIndexChanged" RepeatColumns="6" CssClass="chkChoiceHeader" runat="server">
                                                    <asp:ListItem Selected="True">Company Name</asp:ListItem>
                                                    <asp:ListItem Selected="True">Employee ID</asp:ListItem>
                                                    <asp:ListItem>Employee Old ID</asp:ListItem>
                                                    <asp:ListItem Selected="True">Employee Name</asp:ListItem>
                                                    <asp:ListItem Selected="True">Designation</asp:ListItem>
                                                    <asp:ListItem Selected="True">Department</asp:ListItem>
                                                    <asp:ListItem>Division</asp:ListItem>
                                                    <asp:ListItem>Wing</asp:ListItem>
                                                    <asp:ListItem>Section</asp:ListItem>
                                                    <asp:ListItem>Sub-Section</asp:ListItem>
                                                    <asp:ListItem>Category</asp:ListItem>
                                                    <asp:ListItem>Grade</asp:ListItem>
                                                    <asp:ListItem>Step</asp:ListItem>
                                                    <asp:ListItem>Office</asp:ListItem>
                                                    <asp:ListItem>Place</asp:ListItem>
                                                    <asp:ListItem>Date of Birth</asp:ListItem>
                                                    <asp:ListItem>Nationality</asp:ListItem>
                                                    <asp:ListItem>National Id No</asp:ListItem>
                                                    <asp:ListItem>Passport</asp:ListItem>



                                                    <asp:ListItem>Blood Group</asp:ListItem>
                                                    <asp:ListItem>Gender</asp:ListItem>
                                                    <asp:ListItem>Religion</asp:ListItem>
                                                    <asp:ListItem>Place of Birth</asp:ListItem>
                                                    <asp:ListItem>Present Address</asp:ListItem>
                                                    <asp:ListItem>Permanent Address</asp:ListItem>
                                                    <asp:ListItem>Present Division</asp:ListItem>
                                                    <asp:ListItem>Permanent Division</asp:ListItem>

                                                    <asp:ListItem>Present District</asp:ListItem>
                                                    <asp:ListItem>Permanent District</asp:ListItem>

                                                    <asp:ListItem>Present Thana</asp:ListItem>
                                                    <asp:ListItem>Permanent Thana</asp:ListItem>
                                                    <asp:ListItem>Date of Joining</asp:ListItem>
                                                    <asp:ListItem>Confirmation Date</asp:ListItem>
                                                    <asp:ListItem>Service Length</asp:ListItem>
                                                    <asp:ListItem>Date of Retirement</asp:ListItem>
                                                    <asp:ListItem>Probition End Date</asp:ListItem>
                                                    <asp:ListItem>Contractual  End Date</asp:ListItem>



                                                </asp:CheckBoxList>

                                            </div>
                                        </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Promotion Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="reportDropDownList" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnSelectedIndexChanged="reportDropDownList_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Select One</asp:ListItem>
                                            <asp:ListItem Value="Inquiry">Promotion Information List </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server">
                                    <div class="form-group">
                                        <label>Financial Year </label>

                                        <asp:DropDownList ID="PromotionFinancialYearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label runat="server" id="Label4">Effective From Date </label>

                                        <asp:TextBox ID="PromotionEffectiveDateTextBox" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="PromotionEffectiveDateTextBox" CssClass="MyCalendar"
                                            TargetControlID="PromotionEffectiveDateTextBox" />
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label runat="server" id="Label1">Effective To Date </label>

                                        <asp:TextBox ID="PromotionEffectToDate" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="PromotionEffectToDate" CssClass="MyCalendar"
                                            TargetControlID="PromotionEffectToDate" />
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server">
                                    <div class="form-group">
                                        <label runat="server" id="Label90">Promotion Type </label>

                                        <asp:DropDownList ID="PromotionTypeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" visible="False">

                                    <div class="form-group">
                                        <label>
                                            <asp:CheckBox ID="manCheckBox" Text="Select/UnSelect All" runat="server" AutoPostBack="True" OnCheckedChanged="manCheckBox_OnCheckedChanged" />
                                        </label>
                                        <div style="max-height: 200px; overflow: scroll">
                                            <asp:CheckBoxList ID="managementCheckBoxList" runat="server"></asp:CheckBoxList>
                                        </div>


                                    </div>
                                </div>

                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">

                                       
                                        <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search"   CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>
                                        
                                          <asp:LinkButton runat="server" ID="lbReset" OnClick="lbReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">

                       
                              <div class="col-md-2" style="padding-left:20px">
               <label style="font-size: 18px;">  Promotion  List</label>
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
                           <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel pull-right" style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>


                            <asp:LinkButton ID="btnExportToPDF" runat="server" Visible="False" CssClass="btnPDF  pull-right" OnClick="btnExportToPDF_Click"><span aria-hidden="true" class="glyphicon glyphicon-apple"></span>Export To PDF</asp:LinkButton>

                        </div>

                    </div>
                    <br/>
                    <style>
                        .btnexcel {
                               background-color: white;
            border: none;
            color: black;
            padding: 8px 12px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            margin: 4px 2px;
            cursor: pointer;
            text-decoration: none;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35);
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
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div id="gridContainer1"  style="overflow: scroll; height: 500px;width: 100%">
                                <asp:GridView ID="PromotionloadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" 
                                   >
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                     
 <asp:BoundField DataField="ShortName" HeaderText="Company Name" />
                                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee New ID" />
                                                            <asp:BoundField DataField="SMCOldCode" HeaderText="Employee Old ID" />

                                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                                            <asp:BoundField DataField="Division" HeaderText="Division" />
                                                            <asp:BoundField DataField="Wing" HeaderText="Wing" />
                                                            <asp:BoundField DataField="Section" HeaderText="Section" />
                                                            <asp:BoundField DataField="SubSection" HeaderText="Sub-Section" />
                                                            <asp:BoundField DataField="Category" HeaderText="Category" />
                                                            <asp:BoundField DataField="Grade" HeaderText="Grade" />
                                                            <asp:BoundField DataField="Step" HeaderText="Step" />
                                                            <asp:BoundField DataField="Office" HeaderText="Office" />
                                                            <asp:BoundField DataField="Place" HeaderText="Place" />

                                                            <asp:BoundField DataField="DateofBirth" HeaderText="Date Of Birth" DataFormatString="{0:dd-MMM-yyyy}" />
                                                            <asp:BoundField DataField="Nationality" HeaderText="Nationality" />
                                                            <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
                                                            <asp:BoundField DataField="Passport" HeaderText="Passport" />
                                                            <asp:BoundField DataField="BloodGroup" HeaderText="Blood Group" />
                                                            <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                                            <asp:BoundField DataField="Religion" HeaderText="Religion" />
                                                            <asp:BoundField DataField="PlaceofBirth" HeaderText="Place of Birth" />
                                                            <asp:BoundField DataField="PresentAddress" HeaderText="Present Address" />
                                                            <asp:BoundField DataField="PermanentAddress" HeaderText="Permanent Address" />




                                                            <asp:BoundField DataField="PresentDivision" HeaderText="Present Division" />
                                                            <asp:BoundField DataField="PermanentDivision" HeaderText="Permanent Division" />

                                                            <asp:BoundField DataField="PresentDistrict" HeaderText="Present District" />
                                                            <asp:BoundField DataField="PermanentDistrict" HeaderText="Permanent District" />


                                                            <asp:BoundField DataField="PresentThana" HeaderText="Present Thana" />
                                                            <asp:BoundField DataField="PermanentThana" HeaderText="Permanent Thana" />



                                                            <asp:BoundField DataField="DateOfJoin" HeaderText="Joining Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                            <asp:BoundField DataField="DateOfConformation" HeaderText="Confirmation Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                            <asp:BoundField DataField="ServiceLength" HeaderText="Service Length" />

                                                            <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />

                                                            <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                            <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="PromotionTypeName" HeaderText="Type of Action" />

                                        <asp:BoundField DataField="Effectivedate" Visible="False" HeaderText="Effective Date" DataFormatString="{0:dd-MMM-yyyy}" />



                                     <%--   <asp:BoundField DataField="Designation" HeaderText="Position" />--%>
                                        <asp:BoundField DataField="GradeName" HeaderText="Grade" />
                                        <asp:BoundField DataField="SalaryStepName" HeaderText="Step" />
                                        <asp:BoundField DataField="GrossAmount" HeaderText="Gross Monthly Salary" />
                                        <asp:BoundField DataField="LengthServicewithSMC" HeaderText="Total Years with SMC" />
                                        
                                        
                                        
                                        
                                        <asp:BoundField DataField="" HeaderText="Last Promotion/Upgradation/Reappointment Date" />
                                        <asp:BoundField DataField="" HeaderText="Total Length from last promotion/upgradation/reappointment" />
                                        <asp:BoundField DataField="" HeaderText="Disciplinary Issues in last 2 years" />





                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
    <%--   </ContentTemplate>
        </asp:UpdatePanel>--%>
    <%--</div>--%>
</asp:Content>

