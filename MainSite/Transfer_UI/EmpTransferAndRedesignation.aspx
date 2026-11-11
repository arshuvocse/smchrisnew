<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpTransferAndRedesignation.aspx.cs" Inherits="Transfer_UI_EmpTransferAndRedesignation" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>


    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        /*AutoComplete flyout */
    </style>
    <style>
        .SelectchkChoice label {
            padding-left: 6px;
            font-size: 14px;
            color: red;
            font-weight: bold;
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


        .SelectchkChoice22 label {
            padding-left: 6px;
            font-weight: bold;
            color: black !important;
        }

        .SelectchkChoice22New label {
            padding-left: 6px;
            font-weight: bold;
            font-size: 20px;
            color: black !important;
        }
    </style>


    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">

                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">
                            <img src="../Report_Pages/app.png" width="20px" />&nbsp;Employee Transfer Information </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <div class="col-md-12">
                                <div class="row">

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Company Name </label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <label>Financial Year </label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:DropDownList ID="FinancialYearDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="FinancialYearDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <label>Effective Date </label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox runat="server" OnTextChanged="EfectiveDate_TextChanged" AutoPostBack="True" CausesValidation="true" AutoCompleteType="Disabled" class="form-control form-control-sm" ID="EfectiveDate"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EfectiveDate" CssClass="MyCalendar"
                                                TargetControlID="EfectiveDate" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">

                                        <asp:HiddenField ID="EmpTransferAndRedesignationIdHiddenField" runat="server" />

                                        <div class="form-group">
                                            <label>Search Employee </label>
                                            <span style="color: red">&nbsp;*</span>

                                            <asp:DropDownList runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpInfo_OnSelectedIndexChanged" ID="ddlEmpInfo" CssClass="form-control form-control-sm" />
                                            <script type="text/javascript">
                                                        function pageLoad() {
                                                            $('#<%=ddlEmpInfo.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                            $('#<%=ddldirectlySuper.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });


                                                            $('#<%=ddlReportingBody.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

       $('#<%=ddlInterNewEmpBody.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

       $('.selectMe').chosen({ disable_search_threshold: 5, search_contains: true });
   }
   </script>
                                            <%--<asp:DropDownList ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" ></asp:DropDownList>--%>
                                            <asp:TextBox ID="SearchEmployeeNameTextBoxTextBox" Visible="False" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxTextBox"
                                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                ShowOnlyCurrentWordInCompletionListItem="true">
                                            </cc1:AutoCompleteExtender>

                                            <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />

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

                                        <div class="form-group">
                                            <label>Designation: </label>
                                            <asp:TextBox ID="DesignationTextBox" runat="server" ReadOnly="True" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <label>Joining Date: </label>
                                            <asp:TextBox ID="JoiningDateTextBox" runat="server" ReadOnly="True" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">

                                        <div class="form-group">
                                            <label>Transfer Type: </label>

                                            <br />
                                            <asp:RadioButtonList ID="rbTransferType" CssClass="SelectchkChoice22" runat="server" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rbTransferType_OnSelectedIndexChanged">
                                                <asp:ListItem>Special Transfer</asp:ListItem>
                                                <asp:ListItem>Regular Transfer</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>


                                        <div class="form-group" runat="server" visible="False" id="DIVTransferCat">
                                            <label>Transfer Category: </label>
                                            <br />
                                            <asp:RadioButtonList ID="rbTransferCategory" CssClass="SelectchkChoice22" runat="server" RepeatLayout="Flow">
                                                <asp:ListItem>Full Transfer</asp:ListItem>
                                                <asp:ListItem>Salary Transfer</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>


                                        <div class="form-group" runat="server" visible="False">
                                            <label>Record Update Type: </label>
                                            <br />
                                            <asp:RadioButtonList ID="rbRecordUpdateType" CssClass="SelectchkChoice22" runat="server" RepeatLayout="Flow">
                                                <asp:ListItem Selected="True">Salary Transfer</asp:ListItem>

                                            </asp:RadioButtonList>
                                        </div>


                                        <div class="form-group" runat="server" visible="False" id="DIVRecordUpdate">
                                            <label>Record View : </label>
                                            <br />
                                            <asp:RadioButtonList ID="rbRecordViewType" Visible="False" CssClass="SelectchkChoice22" runat="server" RepeatLayout="Flow">
                                                <asp:ListItem Selected="True">Only View</asp:ListItem>
                                                <asp:ListItem>Edit & View </asp:ListItem>
                                            </asp:RadioButtonList>

                                            <asp:RadioButtonList ID="rbSMCSMCEL" CssClass="SelectchkChoice22" runat="server" RepeatLayout="Flow">
                                                <asp:ListItem>SMC</asp:ListItem>
                                                <asp:ListItem>SMC EL </asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>
                                </div>



                                <div class="row">




                                    <div class="col-md-3">


                                        <%-- <div class="form-group">
                                        <label>Search Employee: </label>
                                        <asp:DropDownList ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="EmployeeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                    </div>--%>
                                    </div>

                                    <div class="col-md-2" runat="server" visible="False">

                                        <div class="form-group">
                                            <label>Employee ID: </label>
                                            <asp:TextBox ID="txtEmpId" runat="server" ReadOnly="True" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="HiddenField1" />
                                        </div>
                                    </div>
                                    <div class="col-md-2" runat="server" visible="False">

                                        <div class="form-group">
                                            <label>Employee Name: </label>
                                            <asp:TextBox ID="EmployeeNameTextBox" runat="server" ReadOnly="True" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="EmpTypeId" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2" runat="server" visible="False">

                                        <div class="form-group" runat="server" visible="False">
                                            <label>Salary Grade: </label>
                                            <asp:TextBox ID="SalaryGradeTextBox" runat="server" ReadOnly="True" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>




                                </div>





                                <div class="row">
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-9">
                                        <div class="form-group" style="text-align: center">
                                            <asp:RadioButtonList CssClass="SelectchkChoice22New" ID="TransferRadioButtonList" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="TransferRadioButtonList_SelectedIndexChanged">
                                                <asp:ListItem Value="&nbsp;Only Transfer&nbsp;">&nbsp;Company To Company Transfer&nbsp;</asp:ListItem>
                                                <asp:ListItem Value=" Only Re-designation&nbsp;"> Only Re-designation&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="&nbsp;Transfer With Re-designation&nbsp;">&nbsp;Transfer With Re-designation&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="&nbsp;Inter Company Transfer &nbsp;">&nbsp;Inter Company Transfer&nbsp;</asp:ListItem>

                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel runat="server" ID="Panel1" Visible="False">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblNewDesignationShow">New Designation </asp:Label>
                                                <asp:DropDownList ID="NewdesignationDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>New Reporting Body </label>
                                                <asp:DropDownList runat="server" ID="ddlReportingBody" class="form-control form-control-sm" />
                                                <asp:TextBox Visible="False" ID="NewReportingBodyTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="NewReportingBodyTextBox_OnTextChanged"></asp:TextBox>
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
                                        </div>
                                    </div>
                                </asp:Panel>



                                <asp:Panel runat="server" ID="ShowExistingAndNew" Visible="False">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <fieldset class="for-panel">
                                                <legend>Existing</legend>
                                                <div class="row">

                                                    <div class="col-md-12">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <label>Company Name: </label>
                                                                <asp:DropDownList ID="OldCompanyDropDownList" Enabled="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Office: </label>
                                                                <asp:DropDownList ID="OldSalaryLocationDropDownList" Enabled="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Place: </label>
                                                                <asp:DropDownList ID="OldJobLocationDropDownList" Enabled="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Division: </label>
                                                                <asp:DropDownList ID="OldDivisionDropDownList" Enabled="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            </div>



                                                            <div class="form-group">
                                                                <label>Wing: </label>
                                                                <asp:DropDownList ID="OldUnitDropDownList" Enabled="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            </div>

                                                            <div class="form-group">
                                                                <label>Department: </label>
                                                                <asp:DropDownList ID="OldDepartmentDropDownList" Enabled="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Section: </label>
                                                                <asp:DropDownList ID="OldSectionDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" Enabled="False">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Sub-Section:   </label>


                                                                <asp:DropDownList ID="OldSubSectionDropDownList" Enabled="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            </div>


                                                            <div class="form-group">
                                                                <label>Old Reporting Body: </label>
                                                                <asp:DropDownList ID="OldReportingBodyDropDownList" Enabled="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:GridView ID="presuperGridView" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="table table-bordered  text-center thead-dark" DataKeyNames="EmpInfoId">
                                                                    <Columns>


                                                                        <asp:BoundField DataField="EmpName" HeaderText="Directly Supervised Employee List	" />


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

                                                </div>
                                            </fieldset>
                                            <br />
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Label13">Remarks </asp:Label>
                                                <asp:TextBox ID="OtherRemarksTextBox" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <fieldset class="for-panel">
                                                <legend>New</legend>
                                                <div class="row">




                                                    <div class="col-md-12">



                                                        <div class="form-horizontal">

                                                            <div class="form-group">
                                                                <label>Company Name: </label>
                                                                <span style="color: red">&nbsp;*</span>
                                                                <asp:DropDownList ID="NewCompanyDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="NewCompanyDropDownList_SelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Office: </label>

                                                                <asp:DropDownList ID="NewSalaryLocationDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectMe" OnSelectedIndexChanged="NewSalaryLocationDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Place: </label>

                                                                <asp:DropDownList ID="NewJobLocationDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectMe"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Division: </label>

                                                                <asp:DropDownList ID="NewDivisionDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectMe" OnSelectedIndexChanged="NewDivisionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>



                                                            <div class="form-group" runat="server" id="wing">
                                                                <label>Wing: </label>
                                                                <asp:DropDownList ID="NewWingDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectMe" OnSelectedIndexChanged="NewWingDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>

                                                            <div class="form-group" runat="server" id="dept">
                                                                <label>Department: </label>

                                                                <asp:DropDownList ID="NewDepartmentDropDownList1" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectMe" OnSelectedIndexChanged="NewDepartmentDropDownList1_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group" runat="server" id="sec">
                                                                <label>Section: </label>
                                                                <asp:DropDownList ID="NewSectionDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectMe" OnSelectedIndexChanged="NewSectionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group" runat="server" id="subsec">
                                                                <label>Sub-Section: </label>
                                                                <asp:DropDownList ID="NewSubSectionDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm selectMe" OnSelectedIndexChanged="NewSubSectionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>

                                                            <div class="form-group">
                                                                <label>New Reporting Body </label>
                                                                <span style="color: red">&nbsp;*</span>
                                                                <asp:DropDownList runat="server" ID="ddlInterNewEmpBody" class="form-control form-control-sm" />

                                                                <asp:TextBox ID="NewEmpBodyTextBox" Visible="False" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="NewEmpBodyTextBox_OnTextChanged"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                    ServiceMethod="GetCompanyWiseEmployeeInfo" ServicePath="~/WebService.asmx" TargetControlID="NewEmpBodyTextBox"
                                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                                </cc1:AutoCompleteExtender>

                                                                <asp:HiddenField ID="NewEmpBodyTextBoxHiddenField" runat="server" />

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
                                                                <asp:HiddenField ID="HiddenField3" runat="server" />

                                                                <%--<asp:DropDownList ID="NewReportingBodyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>--%>
                                                            </div>
                                                            <div class="form-group">

                                                                <div class="row">
                                                                    <div class="col-md-9">
                                                                        <label>Directly Supervisor </label>

                                                                        <asp:DropDownList runat="server" ID="ddldirectlySuper" AutoPostBack="True" OnSelectedIndexChanged="ddldirectlySuper_OnSelectedIndexChanged" class="form-control form-control-sm" />

                                                                        <asp:TextBox ID="directlySuperTextBox" Visible="False" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="directlySuperTextBox_OnTextChanged"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                                                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                            ServiceMethod="GetCompanyWiseEmployeeInfo" ServicePath="~/WebService.asmx" TargetControlID="directlySuperTextBox"
                                                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                                                        </cc1:AutoCompleteExtender>

                                                                        <asp:HiddenField ID="directlyEmpIdHiddenField" runat="server" />
                                                                        <asp:HiddenField ID="rptHiddenField" runat="server" />

                                                                    </div>

                                                                    <div class="col-md-1" style="padding-top: 18px;">
                                                                        <asp:Button ID="Button1" Text="Add To List" CssClass="btn btn-sm btn-info" runat="server" OnClick="Button1_OnClick" />
                                                                    </div>
                                                                </div>
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
                                                                    CssClass="table table-bordered  text-center thead-dark" DataKeyNames="EmpInfoId,PrevEmpReportingBodyId">
                                                                    <Columns>


                                                                        <asp:BoundField DataField="EmpName" HeaderText="Directly Supervised Employee List" />


                                                                        <asp:TemplateField HeaderText="Delete">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteImageButton_OnClick"
                                                                                    ImageUrl="~/Assets/img/delete.png" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>



                                                        </div>

                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>

                                </asp:Panel>

                                <div class="form-row">
                                    <div class="col-12 ">
                                        <asp:CheckBox ID="manualUpdateCheckBox" CssClass="SelectchkChoice" Checked="True" runat="server" Text="Manually Update to Employee Information" />
                                        <%--    <span>&nbsp; Manually Update to Employee Information</span>--%>
                                    </div>
                                </div>

                                <br />


                                <div class="form-row">
                                    <div class="col-md-4" runat="server">
                                        <div class="form-group">
                                            <label class="font-weight-bold">Comments &nbsp;</label><label style="font-size: 10px; color: gray; font-style: italic"> (*Applicable for Final Submit)</label>



                                            <asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" Rows="2" class="form-control" />
                                        </div>
                                    </div>

                                    <div class="col-md-8" runat="server" id="divShow" visible="False">
                                        
                                          <asp:GridView   runat="server" AutoGenerateColumns="False" Width="100%"   ID="gv_Dependencies" CssClass="table table-bordered text-left thead-dark gridDatatable"  >
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       
                           
                                        <asp:BoundField DataField="Parti" HeaderText="Dependencies" />
                                        <asp:BoundField DataField="EmpCount" HeaderText="Count" />
                                    
                                    
                                  
                                    </Columns>
                                                     </asp:GridView>
                                        <label style="font-size: 10px; color: gray; font-style: italic"> (Note: Inter Company Transfer will not available, if any Approval Pending. (KPI and Appraisal will be Auto replace by New ID))</label>
                                        
                                        <table style="font-size: 12px; padding: 4px;display: none">
                                            <thead>
                                                <tr>
                                                    <th>Dependencies</th>
                                                    <th>Count</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>KPI Pending for Setup</td>
                                                    <td>
                                                        <asp:Label Text="0" runat="server" ID="lblKPIPendingforSetup"></asp:Label></td>
                                                </tr>


                                                <tr>
                                                    <td>KPI Pending for Approval</td>
                                                    <td>
                                                        <asp:Label Text="0" runat="server" ID="lblKPIPending"></asp:Label>
                                                    </td>
                                                </tr>



                                                <tr>
                                                    <td>Appraisal Pending for Setup</td>
                                                    <td>
                                                        <asp:Label Text="0" runat="server" ID="lblAppPendingforSetup"></asp:Label>
                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td>Appraisal Pending for Setup</td>
                                                    <td>
                                                        <asp:Label Text="0" runat="server" ID="lblAppPending"></asp:Label>
                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td>Manpower Budget Pending</td>
                                                    <td>
                                                        <asp:Label Text="0" runat="server" ID="lblManpowerBudgetPending"></asp:Label>
                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td>Requisition Pending</td>
                                                    <td>
                                                        <asp:Label Text="0" runat="server" ID="lblRequisitionPen"></asp:Label>
                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td>Recruitment Pending</td>
                                                    <td>
                                                        <asp:Label Text="0" runat="server" ID="lblRecruitmentPending"></asp:Label>
                                                    </td>
                                                </tr>



                                                <tr>
                                                    <td>Clearence Form Pending</td>
                                                    <td>
                                                        <asp:Label Text="0" runat="server" ID="lblClearenceFormApproval"></asp:Label>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>Document Approval Pending</td>
                                                    <td>
                                                        <asp:Label Text="0" runat="server" ID="lblMiscellaneousMeetingPending"></asp:Label>
                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td>Employee Probation Period</td>
                                                    <td>
                                                        <asp:Label Text="0" runat="server" ID="lblEmployeeProbationPeriod"></asp:Label>
                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td>Employemnt Type Change Approval</td>
                                                    <td>
                                                        <asp:Label Text="0" runat="server" ID="lblEmployeeStateChangeApproval"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>

                                    <div runat="server" visible="False">
                                        <asp:GridView Visible="False" runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_JdBoard" CssClass="table table-bordered text-center thead-dark gridDatatable" DataKeyNames="ActionStatus">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField ID="EmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>





                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="col-md-6" runat="server">
                                    </div>
                                </div>
                                <br />

                                <div class="form-row">
                                    <div class="form-group">

                                        <asp:CheckBox ID="Chkreappointment" Visible="False" CssClass="chkChoiceSigle" Text="Re-appointment" runat="server" />
                                        <asp:Button ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                        <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" Visible="False" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
                                    </div>
                                </div>
                            </div>


                            <div></div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                            </div>
                        </div>
                        <div class="col-md-3">
                        </div>

                        <div class="col-md-4">
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

