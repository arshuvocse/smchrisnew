<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ContractualEmpManagement.aspx.cs" Inherits="ContractualEmployeeManagement_UI_ContractualEmpManagement" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>


    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <style type="text/css">
        .SelectchkChoice label {
            padding-left: 6px;
            font-size: 14px;
            color: red;
            font-weight: bold;
        }
    </style>
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


        #cpFormBody_GVExistingProject > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

        #cpFormBody_GVExistingProject > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }
    </style>


    <div class="content" id="content">
        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
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
                    <img src="../Report_Pages/app.png" width="20px" />
                    Employee State Change</h1>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
                <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                <asp:Button ID="detailsViewButton" Text="Back to List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
            </div>

        </div>
        <!-- //END PAGE HEADING -->

        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <asp:UpdatePanel runat="server" ID="kskdfs">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company Name </label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                    <asp:HiddenField runat="server" ID="HFDesgId" />


                                    <asp:HiddenField runat="server" ID="hfPreviousPreoject" />
                                    <asp:HiddenField runat="server" ID="HFDivID" />
                                    <asp:HiddenField runat="server" ID="HFDivWingId" />
                                    <asp:HiddenField runat="server" ID="HFDeptID" />
                                    <asp:HiddenField runat="server" ID="HFSecID" />
                                    <asp:HiddenField runat="server" ID="HFSubSecID" />


                                    <asp:HiddenField runat="server" ID="HFEmpCode" />
                                    <asp:HiddenField runat="server" ID="HFEmpTypeID" />

                                    <asp:HiddenField runat="server" ID="HFSalLocID" />
                                    <asp:HiddenField runat="server" ID="ContractPeriodHF" />
                                    <asp:HiddenField runat="server" ID="ContactualEndDateHiddenField" />

                                    <asp:HiddenField runat="server" ID="HFJobLocID" />



                                    <asp:HiddenField runat="server" ID="SGradeFF" />
                                    <asp:HiddenField runat="server" ID="SStepHF" />

                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Search Employee: </label>
                                        &nbsp;<label style="color: #a52a2a">*</label>

                                        <asp:DropDownList runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpInfo_OnSelectedIndexChanged" ID="ddlEmpInfo" class="form-control form-control-sm" />
                                        <script type="text/javascript">
                                            function pageLoad() {
                                                $('#<%=ddlEmpInfo.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                $('.selectMee').chosen({ disable_search_threshold: 5, search_contains: true });
                                                $('.selectMee2').chosen({ disable_search_threshold: 5, search_contains: true });

                                                        }
                                        </script>

                                        <asp:TextBox ID="SearchEmployeeNameTextBoxTextBox" Visible="False" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetCompanyWiseEmployeeInfo" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxTextBox"
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
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Effective Date: </label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="txtEffectiveDate" AutoCompleteType="Disabled" runat="server" OnTextChanged="txtEffectiveDate_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            TargetControlID="txtEffectiveDate"
                                            Format="dd MMM yyyy" CssClass="MyCalendar" PopupPosition="TopLeft" />
                                        <style>
                                                      
                                                        </style>
                                    </div>
                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:Panel runat="server" ID="ShowPanel">

                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-4">
                                        <fieldset class="for-panel">
                                            <legend>Basic Information </legend>
                                            <div class="row">

                                                <div class="col-md-12">



                                                    <div class="form-group" style="display: none">

                                                        <label>Company Name: </label>
                                                        <asp:Label ID="lblComName" runat="server" Text=""></asp:Label>

                                                    </div>

                                                    <div class="form-group">

                                                        <label>Employee Name: </label>
                                                        <asp:Label ID="lblEmp" runat="server" Text=""></asp:Label>

                                                    </div>
                                                    <div class="form-group">
                                                        <label>Employee Code: </label>
                                                        <asp:Label ID="lblEmployeeCode" runat="server" Text=""></asp:Label>
                                                    </div>

                                                    <div class="form-group">
                                                        <label>Joining Date: </label>
                                                        <asp:Label ID="lblJdate" runat="server" Text=""></asp:Label>
                                                    </div>

                                                    <div class="form-group">
                                                        <label>Designation: </label>
                                                        <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                                                    </div>


                                                    <div class="form-group">
                                                        <label>Salary Grade: </label>
                                                        <asp:Label ID="lblSalaryGrade" runat="server" Text=""></asp:Label>
                                                    </div>


                                                    <div class="form-group">
                                                        <label>Employee Type: </label>
                                                        <asp:Label ID="lblEmpType" runat="server" Text=""></asp:Label>
                                                    </div>


                                                    <div class="form-group">
                                                        <label>Contract End Date: </label>
                                                        <asp:Label ID="lblContractEndDate" runat="server" Text=""></asp:Label>
                                                    </div>

                                                    <div class="form-group" runat="server" visible="False">
                                                        <label>Project: </label>
                                                        <asp:Label ID="lblProject" runat="server" Text=""></asp:Label>
                                                    </div>

                                                </div>

                                            </div>
                                        </fieldset>
                                    </div>



                                    <div class="col-md-4">
                                        <fieldset class="for-panel">
                                            <legend>Functional Structure</legend>
                                            <div class="row">

                                                <div class="col-md-12">

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


                                    <div class="col-md-4">

                                        <legend>Project Information </legend>
                                        <div class="row">

                                            <asp:GridView ID="loadGridView" Visible="False"  runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered  text-center thead-dark" DataKeyNames="EmpInfoId">
                                                <Columns>


                                                    <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />




                                                </Columns>
                                            </asp:GridView>

                                            <div class="col-md-12">




                                                <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                                    <asp:GridView ID="GVExistingProject" runat="server" AutoGenerateColumns="False"
                                                        CssClass="table table-bordered text-center thead-dark" DataKeyNames="ProjectId"
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




                                                            <%--<asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="deleteImageButtonPro" runat="server" OnClick="deleteImageButtonPro_OnClick"
                                                                    ImageUrl="~/Assets/img/delete.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <asp:UpdatePanel runat="server" ID="ss">
                        <ContentTemplate>
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="row">
                                        <asp:HiddenField runat="server" ID="ContractualEmpManageIdHiddenField" />
                                        <div class="col-md-9">
                                            <div class="form-group">
                                                <asp:RadioButtonList ID="ExtentionRenewRadioButtonList" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ExtentionRenewRadioButtonList_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem>&nbsp; &nbsp;Extension&nbsp; &nbsp;</asp:ListItem>
                                                    <asp:ListItem>&nbsp; &nbsp;Renew&nbsp; &nbsp;</asp:ListItem>
                                                    <asp:ListItem>&nbsp; &nbsp;Permanent to Contractual&nbsp; &nbsp;</asp:ListItem>
                                                    <asp:ListItem>&nbsp; &nbsp;Contractual to Permanent&nbsp; &nbsp;</asp:ListItem>
                                                    <asp:ListItem>&nbsp; &nbsp;SMC Funded Projects to SMC Contract&nbsp; &nbsp;</asp:ListItem>

                                                    <asp:ListItem>&nbsp; &nbsp;SMC Contract to SMC Funded Projects&nbsp; &nbsp;</asp:ListItem>
                                                    <asp:ListItem>&nbsp; &nbsp;Project Change&nbsp; &nbsp;</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="divReappointment" visible="False">
                                            <div class="form-group">
                                                <asp:CheckBox runat="server" ID="chkReappointment" Text="Emp. Information Update" AutoPostBack="True" OnCheckedChanged="chkReappointment_OnCheckedChanged" />
                                            </div>
                                        </div>

                                        <div class="col-md-2" runat="server" id="divRedesignation" visible="False">
                                            <div class="form-group">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:Panel runat="server" ID="ExtensionPanelView" Visible="False">
                                                <div class="row">

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Extension From Date: </label>
                                                            &nbsp;<label style="color: #a52a2a">*</label>
                                                            <asp:TextBox ID="ExtensionFromDateTextBox" AutoCompleteType="Disabled" runat="server" OnTextChanged="ExtensionFromDateTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="Calendar1" runat="server"
                                                                TargetControlID="ExtensionFromDateTextBox"
                                                                Format="dd MMM yyyy" CssClass="MyCalendar" PopupPosition="TopLeft" />
                                                            <style>
                                                      
                                                        </style>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Extension To Date: </label>
                                                            &nbsp;<label style="color: #a52a2a">*</label>
                                                            <asp:TextBox ID="ExtensionToDateTextBox" AutoPostBack="True" AutoCompleteType="Disabled" runat="server" OnTextChanged="ExtensionToDateTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                                Format="dd-MMM-yyyy" PopupButtonID="ExtensionToDateTextBox" CssClass="MyCalendar"
                                                                TargetControlID="ExtensionToDateTextBox" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel runat="server" ID="RenewPanelView" Visible="False">
                                                <div class="row">

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Renew Start Date: </label>
                                                            &nbsp;<label style="color: #a52a2a">*</label>
                                                            <asp:TextBox ID="RenewStartDateTextBox" AutoPostBack="True" AutoCompleteType="Disabled" runat="server" OnTextChanged="RenewStartDateTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupPosition="TopLeft"
                                                                Format="dd-MMM-yyyy" PopupButtonID="RenewStartDateTextBox" CssClass="MyCalendar"
                                                                TargetControlID="RenewStartDateTextBox" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Renew To Date: </label>
                                                            &nbsp;<label style="color: #a52a2a">*</label>
                                                            <asp:TextBox ID="RenewToDateTextBox" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" OnTextChanged="RenewToDateTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupPosition="TopLeft"
                                                                Format="dd-MMM-yyyy" PopupButtonID="RenewToDateTextBox" CssClass="MyCalendar"
                                                                TargetControlID="RenewToDateTextBox" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>



                                            <asp:Panel runat="server" ID="PermanentToContractualPanelView" Visible="False">
                                                <div class="row">

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Start Date: </label>
                                                            &nbsp;<label style="color: #a52a2a">*</label>
                                                            <asp:TextBox ID="PermanentToContractualEffectiveDaeTextBox" AutoCompleteType="Disabled" runat="server" OnTextChanged="PermanentToContractualEffectiveDaeTextBox_TextChanged" CausesValidation="true" AutoPostBack="True" class="form-control form-control-sm"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" PopupPosition="TopLeft"
                                                                Format="dd-MMM-yyyy" PopupButtonID="PermanentToContractualEffectiveDaeTextBox" CssClass="MyCalendar"
                                                                TargetControlID="PermanentToContractualEffectiveDaeTextBox" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>End Date: </label>
                                                            &nbsp;<label style="color: #a52a2a">*</label>
                                                            <asp:TextBox ID="PermanentToContractualEndDateTextBox" AutoCompleteType="Disabled" runat="server" OnTextChanged="PermanentToContractualEffectiveDaeTextBox_TextChanged" CausesValidation="true" AutoPostBack="True" class="form-control form-control-sm"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" PopupPosition="TopLeft"
                                                                Format="dd-MMM-yyyy" PopupButtonID="PermanentToContractualEndDateTextBox" CssClass="MyCalendar"
                                                                TargetControlID="PermanentToContractualEndDateTextBox" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>


                                            <asp:Panel runat="server" ID="rbOther" Visible="False">
                                                <div class="row">
                                                    <asp:HiddenField runat="server" ID="hfIsProgramContractualOP" />
                                                    <asp:HiddenField runat="server" ID="hfIsSMCFundedProjects" />
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Project Name: </label>
                                                            &nbsp;<label style="color: #a52a2a">*</label>
                                                            <asp:DropDownList runat="server" ID="ddlProject" class="form-control form-control-sm">
                                                                <asp:ListItem Value="0">Select One.....</asp:ListItem>
                                                                <asp:ListItem Value="1">SMC Funded Projects</asp:ListItem>
                                                                <asp:ListItem Value="2">SMC Contract</asp:ListItem>
                                                                <asp:ListItem Value="3">Other Projects</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                </div>
                                            </asp:Panel>


                                            <asp:Panel runat="server" ID="ContractualToPermanentPanelView" Visible="False">
                                                <div class="row" runat="server" visible="False">

                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Effective Date: </label>
                                                            &nbsp;<label style="color: #a52a2a">*</label>
                                                            <asp:TextBox ID="ContractualToPermanentDateTextBox" AutoCompleteType="Disabled" runat="server" OnTextChanged="ContractualToPermanentTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" PopupPosition="TopLeft"
                                                                Format="dd-MMM-yyyy" PopupButtonID="ContractualToPermanentDateTextBox" CssClass="MyCalendar"
                                                                TargetControlID="ContractualToPermanentDateTextBox" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-md-2" runat="server" id="ContractPreiod">
                                            <div class="form-group">
                                                <label>Contract Preiod</label>
                                                <asp:TextBox runat="server" ID="txtContractualPreiod" ReadOnly="True" CssClass="form-control form-control-sm" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:RadioButtonList ID="SalaryIncrementRadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem>&nbsp;  Salary Increment &nbsp; &nbsp;</asp:ListItem>
                                                    <asp:ListItem>&nbsp;  No Increment &nbsp; &nbsp;</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:RadioButtonList ID="FacilityRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem>&nbsp;  Facility Included &nbsp; &nbsp;</asp:ListItem>
                                                    <asp:ListItem> &nbsp;  No Facility &nbsp; &nbsp;</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>



                                    <div runat="server" id="evgrid" visible="False">
                                        <div style="text-align: center;">
                                            Key Rating Criterions to be evaluated
                                        <br />
                                            <br />
                                            (Please tick (√) your actual rating in the appropriate box)
                                    <br />
                                            <br />
                                        </div>
                                        <asp:GridView Width="100%" ID="gv_ProbationEvaluation" runat="server"
                                            AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("ValueField") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Key Rating Criterions">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txt_RatingCriterions" runat="server" Text='<%#Eval("TextField") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rating Scale">
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList runat="server" ID="rad_RatingScale" RepeatDirection="Horizontal" BorderStyle="None">
                                                            <asp:ListItem Value="4" Text="Excellent" runat="server"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Good" runat="server"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Satisfactory" runat="server"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Not Satisfactory" runat="server"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>


                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Remarks: </label>
                                                <asp:TextBox ID="RemarksTextBox" Rows="2" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-12 ">
                                            <asp:CheckBox ID="manualUpdateCheckBox" Checked="True" Enabled="False" Text="&nbsp; Manually Update to Employee Information" runat="server" CssClass="SelectchkChoice" />

                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <asp:Button ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                        <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:Button ID="cancelButton" Text="Back to List" CssClass="btn btn-sm warning" runat="server" Visible="False" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
                                        &nbsp; &nbsp;  &nbsp;<asp:HyperLink ID="hyperlink" Visible="False" NavigateUrl="ContractualEmpList.aspx" CssClass="btn btn-sm text-info" Text="&lt; &lt; &lt; &lt; Back to List" runat="server" />




                                    </div>
                                </div>



                            </div>


                            <div class="form-row">
                                <div class="col-md-12">
                                    <asp:ModalPopupExtender runat="server" TargetControlID="test" BackgroundCssClass="modalBackground" PopupControlID="panal" ID="mp1" />
                                    <asp:HiddenField ID="test" runat="server"></asp:HiddenField>
                                    <asp:Panel ID="panal" runat="server" Style="display: none; padding: 10px;" Height="350px" Width="30%" CssClass="modalPopup">



                                        <br />
                                        <div class="page-header text-center">
                                            <h1>Confirmation Message   </h1>
                                        </div>
                                        <hr />









                                        <div class="row">
                                            <div class="col-12">
                                                <div class="form-group">
                                                    <label class="alert alert-warning" style="font-size: 13px;">
                                                        <p>this employee's Employee ID will be changed. Remarks Field is Mandatory.</p>
                                                        <p>Are you sure want to do this?</p>
                                                    </label>
                                                    <br />
                                                    <asp:RadioButtonList runat="server" ID="isAceptTerm" CssClass="chkChoiceDesignation">

                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                        </div>
                                        <hr />

                                        <div class="row" style="padding-right: 20px; padding-left: 20px;">
                                            <hr />
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <asp:LinkButton runat="server" ID="btnYes" OnClick="btnYes_OnClick" CssClass="btn btn-sm btn-danger pull-right"> <span aria-hidden="true" class="fa fa-times"></span>  &nbsp;Close</asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>


                                        <%--<asp:Button ID="btnNo" Text="Cancel" OnClick="btnNo_Click" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
                                    </asp:Panel>
                                </div>
                            </div>



                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:LinkButton runat="server" CssClass="btn btn-info btn-sm" BackColor="#0A9944" ToolTip="Click To View Note" BorderStyle="None" OnClick="ShowPopup" ID="btnStatus">View Note</asp:LinkButton>
                    <%--  </ContentTemplate>
             <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="btnStatus" EventName="Click" />
               </Triggers>
        </asp:UpdatePanel>--%>

                    <!-- Modal Popup -->
                    <script type="text/javascript">
                        function ShowPopup() {

                            $("#exampleModal").modal("show");
                        }
                    </script>
                    <style>
                        .w3-tag {
                            background-color: #FF9800;
                            color: #fff;
                            padding: 4px;
                            border-radius: 10%;
                        }

                        .w3-green, .w3-hover-green:hover {
                            color: #fff !important;
                            background-color: #4CAF50;
                        }
                    </style>


                    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h3 class="modal-title" id="exampleModalLabel" style="color: #FF9800;">ID Change Policy</h3>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">


                                    <img src="../RecruitmentManagement_UI/idchangepoilicy.png" width="480px" />

                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>


    <div>
        <ajaxToolkit:ModalPopupExtender ID="MPBehavioral" runat="server" TargetControlID="Behavioral_Test" PopupControlID="pnl_Behavioral"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="Behavioral_Test" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnl_Behavioral" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="600px" Width="90%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>

                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Image ID="imgWaeeeewit" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <div class="row">
                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                            <div class="text-left">
                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                    <img src="../Report_Pages/app.png" width="20px" />
                                    Employment Information</h1>
                            </div>
                        </div>

                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                            <asp:LinkButton ID="btnBehavioralClose" OnClick="btnBehavioralClose_Click" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server"><i style="font-size: 16px" class="fa fa-times" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <hr />
                    <div class="row">
                        <style>
                            .tblTHColorChang {
                                background-color: #EDF2F5 !important;
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


                            div#cpFormBody_ddlDivision_chosen {
                                width: 270px !important;
                            }

                            div#cpFormBody_ddlDivision_chosen {
                                width: 270px !important;
                            }


                            div#cpFormBody_ddlWing_chosen {
                                width: 270px !important;
                            }





                            div#cpFormBody_ddlDepartment_chosen {
                                width: 270px !important;
                            }


                           . {
                                width: 270px !important;
                            }

                                div#cpFormBody_ddlSection_chosen {
                                width: 270px !important;
                            }

                            div#cpFormBody_ddlSubSection_chosen {
                                width: 270px !important;
                            }



                            div#cpFormBody_ddlEmpCategory_chosen {
                                width: 270px !important;
                            }



                            div#cpFormBody_ddlSalaryGrade_chosen {
                                width: 270px !important;
                            }



                            div#cpFormBody_ddlSalaryStep_chosen {
                                width: 270px !important;
                            }



                            div#cpFormBody_ddlDesignation_chosen {
                                width: 270px !important;
                            }



                            div#cpFormBody_ddlDesignationType_chosen {
                                width: 270px !important;
                            }




                            div#cpFormBody_ddlSalaryLocation_chosen {
                                width: 270px !important;
                            }



                            div#cpFormBody_ddlJobLocation_chosen {
                                width: 270px !important;
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

                            .SelectchkChoiceDsss label {
                                font-size: 16px !important;
                                color: darkred !important;
                                padding: 2px;
                            }
                        </style>


                        <div class="col-md-12">
                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Separation for Previous Employee ID</h2>
                            <div class="row">
                                <div class="col-2">
                                    <div class="form-group">
                                        <asp:CheckBox runat="server" ID="chkIsSeparation" CssClass="SelectchkChoiceDsss" AutoPostBack="True" Text="Separation" OnCheckedChanged="chkIsSeparation_OnCheckedChanged" />

                                    </div>
                                </div>
                                <div class="col-md-5">

                                    <asp:GridView ID="gv_NewSupp" runat="server" AutoGenerateColumns="False"
                                        CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="EmpInfoId,PrevEmpReportingBodyId">
                                        <Columns>


                                            <asp:BoundField DataField="EmpName" HeaderText="Directly Supervised Employee List" />


                                                         <asp:TemplateField HeaderText="Is Board Member">
                                            <ItemTemplate>
                                                                        
                                                                         <asp:CheckBox runat="server" ID="chkIsB_ReportingBody" AutoPostBack="True" OnCheckedChanged="chkIsB_ReportingBody_OnCheckedChanged"  />
                                                                          </ItemTemplate>
                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="Superised selection">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlEmpInfoList" runat="server" CssClass="form-control form-control-sm selectMee2"    Width="320px"
                                                    >
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div class="col-md-2" runat="server" visible="True" id="septype">
                                    <div class="form-group">
                                        <label>Job Left Type: </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="JobLeftTypeDropDownList" OnSelectedIndexChanged="JobLeftTypeDropDownList_OnSelectedIndexChanged" AutoPostBack="True" class="form-control form-control-sm" runat="server"></asp:DropDownList>
                                        <asp:CheckBox ID="chkIsSubmissionDate" Visible="False" Text="Is Submission Date" CssClass="checkbox margin-right" runat="server" />
                                    </div>
                                </div>


                                <div class="col-md-2" runat="server" visible="True" id="sepDate">
                                    <div class="form-group">
                                        <label>Separation Date: </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:TextBox ID="JobLeftDateTextBox" AutoCompleteType="Disabled" runat="server" AutoPostBack="True" OnTextChanged="JobLeftDateTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupPosition="TopLeft"
                                            TargetControlID="JobLeftDateTextBox" />
                                    </div>
                                </div>
                            </div>


                            <br />
                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Separation for New Employee ID</h2>
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList runat="server" Enabled="False" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Division</label>
                                        <label style="color: #a52a2a">*</label>
                                        <br />
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />



                                    </div>
                                </div>
                                <div class="col-3" runat="server" id="wing">
                                    <div class="form-group">
                                        <label>Wing</label>
                                        <br />
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />

                                    </div>
                                </div>
                                <div class="col-3" runat="server" id="dept">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <label style="color: #a52a2a">*</label>
                                        <br />
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />

                                    </div>
                                </div>
                                <div class="col-3" runat="server" id="sec">
                                    <div class="form-group">
                                        <label>Section</label>
                                        <br />
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSection" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged" />

                                    </div>
                                </div>
                                <div class="col-3" runat="server" id="subsec">
                                    <div class="form-group">
                                        <label>Sub Section</label>
                                        <br />
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSubSection" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlSubSection_OnSelectedIndexChanged" />

                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Category</label>
                                        <label style="color: #a52a2a">*</label>
                                        <br />
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpCategory" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlEmpCategory_OnSelectedIndexChanged" />

                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Salary Grade</label><label style="color: #a52a2a">*</label>
                                        <br />
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSalaryGrade" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlSalaryGrade_OnSelectedIndexChanged" />

                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Salary Step</label><label style="color: #a52a2a">*</label>
                                        <br />
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSalaryStep" class="selectMee form-control form-control-sm" />

                                    </div>
                                </div>


                                <div class="col-3">
                                    <div class="form-group">
                                        <label>
                                            <asp:CheckBox runat="server" ID="chkRedesignation" Text="Re-Designation" AutoPostBack="True" OnCheckedChanged="chkRedesignation_OnCheckedChanged" /></label><label style="color: #a52a2a">*</label>
                                        <br />
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignation" Enabled="False" CssClass="selectMee form-control form-control-sm" />

                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Designation Type</label>
                                        <br />
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignationType" class="selectMee form-control form-control-sm" />

                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Office</label><label style="color: #a52a2a">*</label>
                                        <br />
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSalaryLocation" OnSelectedIndexChanged="ddlSalaryLocation_OnSelectedIndexChanged" class="selectMee form-control form-control-sm" />

                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Place</label>
                                        <br />
                                        <asp:DropDownList runat="server" ID="ddlJobLocation" class="selectMee form-control form-control-sm" />

                                    </div>
                                </div>


                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Floor</label>
                                        <asp:TextBox runat="server" ID="txtFloor" class="form-control form-control-sm" />

                                    </div>
                                </div>

                            </div>

                            <br />

                        </div>



                    </div>




                    <hr />



                    <%--<asp:Button runat="server" ID="btnBehave" OnClick="btnBehave_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />--%>
                    <%--   <asp:Button ID="btnFunctionalCancel" Text="Close" OnClick="btnFunctionalCancel_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
                    <br />
                    <br />
                    </div>
                
                    </div>
                </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>


    <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender14" runat="server" TargetControlID="re_Test" PopupControlID="Panel1"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="re_Test" runat="server"></asp:HiddenField>
        <asp:Panel ID="Panel1" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="600px" Width="60%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>

                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Image ID="imgWaeeeezxczxcwit" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <div class="row">
                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                            <div class="text-left">
                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                    <img src="../Report_Pages/app.png" width="20px" />
                                    Employee Re-Designation Information</h1>
                            </div>
                        </div>

                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                            <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server"><i style="font-size: 16px" class="fa fa-times" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <hr />
                    <div class="row">
                        <style>
                            .tblTHColorChang {
                                background-color: #EDF2F5 !important;
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


                            div#cpFormBody_ddlDivision_chosen {
                                width: 270px !important;
                            }

                            div#cpFormBody_ddlDivision_chosen {
                                width: 270px !important;
                            }


                            div#cpFormBody_ddlWing_chosen {
                                width: 270px !important;
                            }





                            div#cpFormBody_ddlDepartment_chosen {
                                width: 270px !important;
                            }


                            div#cpFormBody_ddlSection_chosen {
                                width: 270px !important;
                            }



                            div#cpFormBody_ddlSubSection_chosen {
                                width: 270px !important;
                            }

                            div#cpFormBody_gv_NewSupp_ddlEmpInfoList_0_chosen
                              {
                                width: 320px !important;
                            }

                             div#cpFormBody_gv_NewSupp_ddlEmpInfoList_1_chosen
                              {
                                width: 320px !important;
                            }

                              div#cpFormBody_gv_NewSupp_ddlEmpInfoList_2_chosen
                              {
                                width: 320px !important;
                            }


                               div#cpFormBody_gv_NewSupp_ddlEmpInfoList_3_chosen
                              {
                                width: 320px !important;
                            }


                                div#cpFormBody_gv_NewSupp_ddlEmpInfoList_4_chosen
                              {
                                width: 320px !important;
                            }

                                 div#cpFormBody_gv_NewSupp_ddlEmpInfoList_5_chosen
                              {
                                width: 320px !important;
                            }

                                    div#cpFormBody_gv_NewSupp_ddlEmpInfoList_6_chosen
                              {
                                width: 320px !important;
                            }


                                       div#cpFormBody_gv_NewSupp_ddlEmpInfoList_7_chosen
                              {
                                width: 320px !important;
                            }


                                          div#cpFormBody_gv_NewSupp_ddlEmpInfoList_8_chosen
                              {
                                width: 320px !important;
                            }


                                             div#cpFormBody_gv_NewSupp_ddlEmpInfoList_9_chosen
                              {
                                width: 320px !important;
                            }

                                                div#cpFormBody_gv_NewSupp_ddlEmpInfoList_10_chosen
                              {
                                width: 320px !important;
                            }

                                                   div#cpFormBody_gv_NewSupp_ddlEmpInfoList_11_chosen
                              {
                                width: 320px !important;
                            }


                                                      div#cpFormBody_gv_NewSupp_ddlEmpInfoList_12_chosen
                              {
                                width: 320px !important;
                            }


                                                         div#cpFormBody_gv_NewSupp_ddlEmpInfoList_13_chosen
                              {
                                width: 320px !important;
                            }

                                                            div#cpFormBody_gv_NewSupp_ddlEmpInfoList_14_chosen
                              {
                                width: 320px !important;
                            }


                                                               div#cpFormBody_gv_NewSupp_ddlEmpInfoList_15_chosen
                              {
                                width: 320px !important;
                            }


                                                                  div#cpFormBody_gv_NewSupp_ddlEmpInfoList_16_chosen
                              {
                                width: 320px !important;
                            }


                                                                     div#cpFormBody_gv_NewSupp_ddlEmpInfoList_17_chosen
                              {
                                width: 320px !important;
                            }

                                                                        div#cpFormBody_gv_NewSupp_ddlEmpInfoList_18_chosen
                              {
                                width: 320px !important;
                            }

                                                                           div#cpFormBody_gv_NewSupp_ddlEmpInfoList_19_chosen
                              {
                                width: 320px !important;
                            }

                                                                              div#cpFormBody_gv_NewSupp_ddlEmpInfoList_20_chosen
                              {
                                width: 320px !important;
                            }

                            div#cpFormBody_ddlEmpCategory_chosen {
                                width: 270px !important;
                            }



                            div#cpFormBody_ddlSalaryGrade_chosen {
                                width: 270px !important;
                            }



                            div#cpFormBody_ddlSalaryStep_chosen {
                                width: 270px !important;
                            }



                            div#cpFormBody_ddlDesignation_chosen {
                                width: 270px !important;
                            }



                            div#cpFormBody_ddlDesignationType_chosen {
                                width: 270px !important;
                            }




                            div#cpFormBody_ddlSalaryLocation_chosen {
                                width: 270px !important;
                            }



                            div#cpFormBody_ddlJobLocation_chosen {
                                width: 270px !important;
                            }
                        </style>


                        <div class="col-md-3">




                            <br />
                            <div class="form-group">
                                <asp:Label runat="server" ID="Label6">New Designation </asp:Label>
                                <span style="color: red">&nbsp;*</span>
                                <asp:DropDownList ID="NewDesignationDropDownList" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                            </div>

                            <br />

                        </div>



                    </div>




                    <hr />



                    <%--<asp:Button runat="server" ID="btnBehave" OnClick="btnBehave_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />--%>
                    <%--   <asp:Button ID="btnFunctionalCancel" Text="Close" OnClick="btnFunctionalCancel_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
                    <br />
                    <br />
                    </div>
                
                    </div>
                </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>

