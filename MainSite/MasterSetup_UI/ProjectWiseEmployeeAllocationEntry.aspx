<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ProjectWiseEmployeeAllocationEntry.aspx.cs" Inherits="MasterSetup_UI_ProjectWiseEmployeeAllocationEntry" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
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


        .elegantshd2 {
            color: #131313;
            letter-spacing: .15em;
            text-shadow: 2px 2px 4px #000000;
            font-family: 'Kreon', serif;
            vertical-align: auto;
            text-decoration-style: wavy;
        }
</style>
   
    <style>
        
            #cpFormBody_GVExistingProject  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

       #cpFormBody_GVExistingProject > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }
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


    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                           <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px"
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Project Wise Employee Allocation Information </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="View List" Visible="False" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="row">

                                <div class="col-md-12">
                                    <div class="row">

                                        <asp:HiddenField runat="server" ID="HfMasterID"/>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Company Name </label>
                                                <span style="color: red">&nbsp;*</span>
                                                <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                
                                                 
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" visible="False">
                                            <div class="form-group">
                                                <label>Financial Year </label>
                                                <span style="color: red">&nbsp;*</span>
                                                <asp:DropDownList ID="FinancialYearDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="FinancialYearDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" visible="False">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Label4">Active Date </asp:Label>
                                                <span style="color: red">&nbsp;*</span>
                                                <asp:HiddenField ID="areaHiddenField" runat="server" />
                                                <asp:HiddenField ID="areaCodeHiddenField" runat="server" />
                                                <asp:TextBox ID="ActiveDateTextBox" AutoCompleteType="Disabled" OnTextChanged="ActiveDateTextBox_Changed" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                    Format="dd-MMM-yyyy" PopupButtonID="ActiveDateTextBox" CssClass="MyCalendar"
                                                    TargetControlID="ActiveDateTextBox" />
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <asp:HiddenField ID="EmployeePromotionEntryIdHiddenField" runat="server" />
                                            <div class="form-group">
                                                <label>Search Employee: </label>
                                                
                                                  <asp:DropDownList   runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpInfo_OnSelectedIndexChanged"  ID="ddlEmpInfo" class="form-control form-control-sm selectme" />
                                                 <script type="text/javascript">
                                                     function pageLoad() {
                                                         $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });



                                                     }
</script>
                                                <%--<asp:TextBox ID="SearchEmployeeNameTextBoxTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxTextBox"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>

                                                <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />--%>

                                                <%--<asp:AutoCompleteExtender
                                                            ID="at_txt_JobCirculation"
                                                            TargetControlID="EmployeeNameTextBox"
                                                            runat="server"
                                                            ServiceMethod="GetParticipantList"
                                                            ServicePath="~/WebService.asmx"
                                                            MinimumPrefixLength="2"
                                                            CompletionInterval="10"
                                                            EnableCaching="false"
                                                            CompletionSetCount="1"
                                                            FirstRowSelected="false">
                                                        </asp:AutoCompleteExtender>--%>
                                                <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                            </div>

                                        </div>

                                    </div>


                                    <div class="row">
                                    </div>
                                    <div class="row">

                                        <div class="col-md-5">
                                            <asp:Panel runat="server" ID="SearchViewPanel" Visible="False">
                                                <div class="row">


                                                    <div class="col-md-12">
                                                        <fieldset class="for-panel">
                                                            <legend>Employee Information</legend>
                                                            <div class="row">

                                                                <div class="col-md-6">


                                                                    <div class="form-group">
                                                                        <label>Employee Code: </label>
                                                                        <asp:Label ID="lblEmployeeCode" runat="server" Text=""></asp:Label>
                                                                    </div>


                                                                    <div class="form-group">

                                                                        <label>Employee Name: </label>
                                                                        <asp:Label ID="lblEmp" runat="server" Text=""></asp:Label>

                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label>Joining Date: </label>
                                                                        <asp:Label ID="lblJdate" runat="server" Text=""></asp:Label>
                                                                    </div>

                                                                    <div class="form-group" style="display: none">
                                                                        <label>Designation: </label>
                                                                        <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                                                                    </div>


                                                                    <div class="form-group" runat="server" visible="False">
                                                                        <label>Salary Grade: </label>
                                                                        <asp:Label ID="lblSalaryGrade" runat="server" Text=""></asp:Label>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label>Office: </label>
                                                                        <asp:Label ID="lblOffice" runat="server" Text=""></asp:Label>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label>Place: </label>
                                                                        <asp:Label ID="lblPlace" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">

                                                                    <div class="form-group">

                                                                        <label>Division: </label>
                                                                        <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label>

                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>Wing: </label>
                                                                        <asp:Label ID="lblWing" runat="server" Text=""></asp:Label>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label>Department: </label>
                                                                        <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label>Section: </label>
                                                                        <asp:Label ID="lblSection" runat="server" Text=""></asp:Label>
                                                                    </div>


                                                                    <div class="form-group">
                                                                        <label>Sub-Section: </label>
                                                                        <asp:Label ID="lblSubSection" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </fieldset>
                                                    </div>




                                                </div>
                                            </asp:Panel>
                                        </div>

                                        <div class="col-md-7" runat="server" visible="False">
                                            <asp:Panel runat="server" ID="ShowExistingAndNew" Visible="False">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <fieldset class="for-panel">
                                                            <legend>Promotion To</legend>
                                                            <div class="row">

                                                                <div class="col-md-6">
                                                                    <div class="form-horizontal">


                                                                        <div class="form-group">
                                                                            <asp:Label runat="server" ID="Label2">Previous Salary Grade  </asp:Label>
                                                                            <asp:DropDownList ID="PreviouSalaryGradeDropDownList" Enabled="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                        </div>



                                                                        <div class="form-group">
                                                                            <asp:Label runat="server" ID="Label1">Previous Designation </asp:Label>
                                                                            <asp:DropDownList ID="PreviousDesignationDropDownList" Enabled="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                        </div>




                                                                        <div class="form-group">
                                                                            <asp:Label runat="server" ID="Label11">Previous Reporting Body  </asp:Label>
                                                                            <asp:DropDownList ID="PReportingBodyDropDownList" runat="server" AutoPostBack="True" Enabled="False" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <asp:GridView ID="presuperGridView" runat="server" AutoGenerateColumns="False"
                                                                                CssClass="table table-bordered  text-center thead-dark" DataKeyNames="EmpInfoId">
                                                                                <Columns>


                                                                                    <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />


                                                                                    <%-- <asp:TemplateField HeaderText="Delete"  >
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteImageButton_OnClick"
                                                                                    ImageUrl="~/Assets/img/delete.png" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>


                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">

                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label7">New Salary Grade </asp:Label>
                                                                        <span style="color: red">&nbsp;*</span>
                                                                        <asp:DropDownList ID="NewSalaryGradeDropDownList" OnTextChanged="NewSalaryGradeDropDownList_Changed" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                    </div>


                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label6">New Designation </asp:Label>
                                                                        <span style="color: red">&nbsp;*</span>
                                                                        <asp:DropDownList ID="NewDesignationDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                    </div>



                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label9">New Reporting Body </asp:Label>
                                                                        <span style="color: red">&nbsp;*</span>


                                                                        <asp:TextBox ID="NewReportingBodyTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="NewReportingBodyTextBox_OnTextChanged"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                            ServiceMethod="GetCompanyWiseEmployeeInfo" ServicePath="~/WebService.asmx" TargetControlID="NewReportingBodyTextBox"
                                                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                                                        </cc1:AutoCompleteExtender>

                                                                        <asp:HiddenField ID="HiddenFieldNewReportingBody" runat="server" />

                                                                        <%--<asp:AutoCompleteExtender
                                                            ID="at_txt_JobCirculation"
                                                            TargetControlID="EmployeeNameTextBox"
                                                            runat="server"
                                                            ServiceMethod="GetParticipantList"
                                                            ServicePath="~/WebService.asmx"
                                                            MinimumPrefixLength="2"
                                                            CompletionInterval="10"
                                                            EnableCaching="false"
                                                            CompletionSetCount="1"
                                                            FirstRowSelected="false">
                                                        </asp:AutoCompleteExtender>--%>
                                                                        <asp:HiddenField ID="HiddenField2" runat="server" />

                                                                        <%--<asp:DropDownList ID="NewReportingBodyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>--%>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label5">Directly Supervisor </asp:Label>



                                                                        <asp:TextBox ID="directlySuperTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="directlySuperTextBox_OnTextChanged"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                                                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                            ServiceMethod="GetCompanyWiseEmployeeInfo" ServicePath="~/WebService.asmx" TargetControlID="directlySuperTextBox"
                                                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                                                        </cc1:AutoCompleteExtender>

                                                                        <asp:HiddenField ID="directlyEmpIdHiddenField" runat="server" />
                                                                        <asp:Button ID="Button1" Text="Add" CssClass="btn btn-sm btn-info" runat="server" OnClick="Button1_OnClick" />
                                                                        <%--<asp:AutoCompleteExtender
                                                            ID="at_txt_JobCirculation"
                                                            TargetControlID="EmployeeNameTextBox"
                                                            runat="server"
                                                            ServiceMethod="GetParticipantList"
                                                            ServicePath="~/WebService.asmx"
                                                            MinimumPrefixLength="2"
                                                            CompletionInterval="10"
                                                            EnableCaching="false"
                                                            CompletionSetCount="1"
                                                            FirstRowSelected="false">
                                                        </asp:AutoCompleteExtender>--%>
                                                                        <asp:HiddenField ID="HiddenField4" runat="server" />

                                                                        <%--<asp:DropDownList ID="NewReportingBodyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>--%>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="table table-bordered  text-center thead-dark" DataKeyNames="EmpInfoId">
                                                                            <Columns>


                                                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />


                                                                                <asp:TemplateField HeaderText="Delete">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteImageButton_OnClick"
                                                                                            ImageUrl="~/Assets/img/delete.png" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>



                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label90">Promotion Type </asp:Label>
                                                                        <span style="color: red">&nbsp;*</span>
                                                                        <asp:DropDownList ID="PromotionTypeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group" runat="server" visible="False">
                                                                        <asp:Label runat="server" ID="Label3">Entry Date </asp:Label>
                                                                        <asp:TextBox ID="EntryDateTextBox" runat="server" OnTextChanged="EntryDateTextBox_Changed" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupPosition="TopLeft"
                                                                            Format="dd-MMM-yyyy" PopupButtonID="EntryDateTextBox" CssClass="MyCalendar"
                                                                            TargetControlID="EntryDateTextBox" />
                                                                    </div>


                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label13">Remarks </asp:Label>
                                                                        <asp:TextBox ID="OtherRemarksTextBox" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                                    </div>


                                                                </div>
                                                            </div>
                                                        </fieldset>






                                                    </div>





                                                </div>
                                            </asp:Panel>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">

                                            <div class="form-group">
                                                <label>Select a Project </label>
                                                <%--<span style="color:red">&nbsp;*</span>--%>
                                                <asp:DropDownList ID="ProjectDropDownList" class="form-control form-control-sm selectme" AutoPostBack="True" OnSelectedIndexChanged="Project_Sel" runat="server"></asp:DropDownList>
                                            </div>

                                        </div>

                                        <div class="col-md-2" style="margin-top: 17px;">
                                            <div class="form-group">
                                                <asp:Button runat="server" ID="btnAddTolist" Text="Add to List" CssClass="btn btn-outline-success btn-sm" OnClick="btnAddTolist_OnClick" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-8">
                                              <h3 class="elegantshd">
                                            <label style="font-size: 15px;">Existing Projects & New Project</label>
                                                  </h3>
                                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                                <asp:GridView ID="GVExistingProject" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="EmpWiseProjectDetailID, ProjectId"
                                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--  <asp:TemplateField HeaderText="Report">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmployeePromotionEntryId") %>'
                                                        CommandName="ViewReport" ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>



                                                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" />
                                                        <asp:BoundField DataField="ProjectStartDate" HeaderText="Project Start Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                        <asp:BoundField DataField="ProjectEndDate" HeaderText="Project End Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                                         <asp:TemplateField   >
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="txt_checkAll" OnCheckedChanged="txt_checkAll_OnCheckedChanged" AutoPostBack="True" Text=" Is Active" runat="server"></asp:CheckBox>
                                        <asp:Label ID="txt_selectAll" runat="server"  ></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="txt_check" runat="server"></asp:CheckBox>
                                    </ItemTemplate>


                                </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="deleteImageButtonPro" runat="server" OnClick="deleteImageButtonPro_OnClick"
                                                                    ImageUrl="~/Assets/img/delete.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div class="col-md-4">

                                            <asp:TextBox runat="server" Visible="False" ID="ProStartDate"></asp:TextBox>
                                            <asp:TextBox runat="server" Visible="False" ID="ProStartEnd"></asp:TextBox>


                                        </div>
                                    </div>
                                </div>


                                <br/>
                              
                                <div class="col-md-6">

                                     <br/>
                               
                              

                                    <div class="form-group">
                                        <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_Click" />
                                        <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" Visible="False" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />

                                    </div>


                                </div>

                            </div>

                                      <br/>          <br/>          <br/>          <br/>          <br/>          <br/>          <br/>          <br/>
                        </div>

                    </div>
                </div>
                </div>
                </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


</asp:Content>

