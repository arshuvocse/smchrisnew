<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="JobRequisitionForm.aspx.cs" Inherits="MasterSetup_UI_JobRequisitionForm" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        /*AutoComplete flyout */
        .autocomplete_completionListElement {
            margin: 0px !important;
            background-color: White;
            color: windowtext;
            border: buttonshadow;
            border-width: 1px;
            border-style: solid;
            cursor: 'default';
            overflow: auto;
            font-family: Calibri;
            font-size: 12px;
            text-align: left;
            list-style-type: none;
            margin-left: 0px;
            padding-left: 0px;
            max-height: 350px;
            width: 130% !important;
        }

        /* AutoComplete highlighted item */

        .autocomplete_highlightedListItem {
            background-color: yellow;
            color: black;
            padding: 1px;
        }

        /* AutoComplete item */

        .autocomplete_listItem {
            background-color: white;
            color: blue;
            padding: 0px;
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
    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"><span class="li-register"></span></div>
                        <span></span>
                        <h1 class="title" style="font-size: 16px; padding-top: 1px;">Employee Requisition Form </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="ViewListButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="ViewListButton_OnClick" />
                        <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->
                <asp:HiddenField ID="empIdHiddenField" runat="server" />

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Company Name </label>
                                                <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <br />
                                                <label>Is this Budgeted </label>
                                                <asp:CheckBox ID="isBudgetedCheckBox" AutoPostBack="true" CssClass="checkbox margin-right" runat="server" OnCheckedChanged="isBudgetedCheckBox_CheckedChanged" />
                                            </div>
                                        </div>

                                        <div class="col-md-7" id="ShowBudgetCodeDiv" visible="False" runat="server">

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Financial Year</label>
                                                        <asp:DropDownList ID="financialYearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True" OnTextChanged="financialYearDropDownList_OnTextChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Budget Code </label>
                                                        <asp:DropDownList ID="BudgetCodeDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Note </label>
                                                <asp:TextBox ID="NoteTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:TextBox ID="ReqCodetextBox" runat="server" Visible="False" class="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Requisition Date</label>
                                                <asp:TextBox ID="reqDateTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="reqDateTextBox" />
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Financial Year</label>
                                                <asp:DropDownList ID="mainFinyearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <fieldset class="for-panel">
                                        <legend>Justification / Job Summery </legend>


                                        <div class="row">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label>Description </label>
                                                    <asp:TextBox ID="descriptionTextBox" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="col-md-7">

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label></label>
                                                            <asp:RadioButtonList ID="jstRadioButtonList" RepeatDirection="Horizontal" runat="server" AutoPostBack="True" OnTextChanged="jstRadioButtonList_OnTextChanged">
                                                                <asp:ListItem> &nbsp; New &nbsp; </asp:ListItem>
                                                                <asp:ListItem> &nbsp; Replacement &nbsp; </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <hr />
                                                    </div>
                                                </div>

                                                <div runat="server" visible="False" id="detail">

                                                    <div class="row">
                                                        <div class="col-md-5">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblEmpName" runat="server" Visible="False">
                                                            <Label> Search Employee Name </Label>
                                                                </asp:Label>
                                                                <div class="form-group">
                                                                    <%--<asp:DropDownList ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" ></asp:DropDownList>--%>
                                                                    <asp:TextBox ID="EmployeeNameTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="EmployeeNameTextBox_OnTextChanged"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                        ServiceMethod="GetCompanyWiseEmployeeInfo" ServicePath="~/WebService.asmx" TargetControlID="EmployeeNameTextBox"
                                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                                    </cc1:AutoCompleteExtender>

                                                                    <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />
                                                                    <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-5">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Label ID="lblDatSep" runat="server" Visible="False"> Date Of Seperation </asp:Label>
                                                                </label>
                                                                <asp:TextBox ID="DateOfSeperationTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="custom"
                                                                    TargetControlID="DateOfSeperationTextBox" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-2" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <label style="color: grey">Code </label>
                                                                <br />
                                                                <asp:Label ID="codeLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">

                                                                <label style="color: grey">Name </label>
                                                                <br />
                                                                <asp:Label ID="nameLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">

                                                                <label style="color: grey">Designation </label>
                                                                <br />
                                                                <asp:Label ID="desigLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">

                                                                <label style="color: grey">Salary Grade </label>
                                                                <br />
                                                                <asp:Label ID="salgradLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">

                                                                <label style="color: grey">Division </label>
                                                                <br />
                                                                <asp:Label ID="divLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row" runat="server" visible="False">

                                                        <div class="col-md-2">
                                                            <div class="form-group">

                                                                <label style="color: grey">Wing </label>
                                                                <br />
                                                                <asp:Label ID="wingLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="form-group">

                                                                <label style="color: grey">Department </label>
                                                                <br />
                                                                <asp:Label ID="deptLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="form-group">

                                                                <label style="color: grey">Section </label>
                                                                <br />
                                                                <asp:Label ID="secLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="form-group">

                                                                <label style="color: grey">Sub Section </label>
                                                                <br />
                                                                <asp:Label ID="subsecLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </fieldset>


                                    <fieldset class="for-panel">
                                        <legend>Position Description</legend>
                                        <div class="row">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Job Title</label>
                                                    <asp:TextBox ID="jobTitleTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Grade </label>
                                                    <asp:DropDownList ID="gradeDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Total Vacency </label>

                                                    <asp:TextBox ID="nosTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">

                                                <div class="form-group">
                                                    <label>Employee Type  </label>
                                                    <asp:RadioButtonList ID="typeOfPosRadioButtonList" OnSelectedIndexChanged="typeOfPosRadioButtonList_OnSelectedIndexChanged" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>

                                                    <%--   <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                            <asp:ListItem><span style="font-size:12px">Permanent</span>&nbsp; </asp:ListItem>
                                                            <asp:ListItem> <span style="font-size:12px">Contractual</span> &nbsp;</asp:ListItem>
                                                            <asp:ListItem><span style="font-size:12px">Casual</span> &nbsp;</asp:ListItem>
                                                            <asp:ListItem><span style="font-size:12px">Other</span>&nbsp;</asp:ListItem>
                                                        </asp:RadioButtonList>--%>
                                                </div>
                                            </div>
                                            <div class="col-md-3" style="display: none">
                                                <div class="form-group">
                                                    <label>Division </label>
                                                    <asp:DropDownList ID="divisionDropDownList" CssClass="form-control form-control-sm" AutoPostBack="True" runat="server" OnSelectedIndexChanged="divisionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3" style="display: none">
                                                <div class="form-group">
                                                    <label>Wing </label>
                                                    <asp:DropDownList ID="divWingDropDownList" CssClass="form-control form-control-sm" AutoPostBack="True" runat="server" OnSelectedIndexChanged="divWingDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Department </label>
                                                    <asp:DropDownList ID="deptDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Place of Posting </label>
                                                    <asp:TextBox ID="placeOfPostingTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetJobLocation" ServicePath="~/WebService.asmx" TargetControlID="placeOfPostingTextBox"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>

                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Expected Date of joining </label>
                                                    <asp:TextBox ID="expDtJoinTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                        Format="dd-MMM-yyyy" PopupButtonID="expDtJoinTextBox" CssClass="MyCalendar"
                                                        TargetControlID="expDtJoinTextBox" />
                                                </div>
                                            </div>

                                            <div class="col-md-3" id="project" runat="server" visible="False">
                                                <div class="form-group">
                                                    <label>Project </label>
                                                    <asp:DropDownList ID="projectDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>


                                        <div class="row">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Reporting to(Search Employee) </label>
                                                    <asp:TextBox ID="reportToTextBox" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" OnTextChanged="reportToTextBox_OnTextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender21" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetCompanyWiseEmployeeInfo" ServicePath="~/WebService.asmx" TargetControlID="reportToTextBox"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Directly Supervices </label>
                                                    <asp:DropDownList ID="jobtitleDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Internal Contacts</label>
                                                    <asp:TextBox ID="internalConTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>External Contacts</label>
                                                    <asp:TextBox ID="externalConTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Office </label>
                                                    <asp:DropDownList ID="officeDropDownList" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="officeDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Place </label>
                                                    <asp:DropDownList ID="placeDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>


                                    </fieldset>






                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-6">
                                    <fieldset class="for-panel">
                                        <legend>Key Responsibilities</legend>
                                        <div class="row">




                                            <div class="col-md-12">

                                                <div class="form-horizontal">

                                                    <div class="form-group">

                                                        <div class="row">

                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Division </label>
                                                                    <asp:DropDownList ID="dsnDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="dsnDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                        </div>


                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Search Employee </label>
                                                                    <asp:TextBox ID="keyEmpTextBox" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" OnTextChanged="keyEmpTextBox_OnTextChanged"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                        ServiceMethod="GetCmpWiseEmployeeInfo" ServicePath="~/WebService.asmx" TargetControlID="keyEmpTextBox"
                                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                                    </cc1:AutoCompleteExtender>
                                                                </div>
                                                            </div>

                                                        </div>


                                                        <div class="row">

                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Job Description </label>
                                                                    <asp:CheckBoxList ID="jdCheckBoxList" runat="server"></asp:CheckBoxList>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-2">
                                                                <asp:ImageButton ID="addImageButton" OnClick="addImageButton_OnClick" CssClass="btn btn-outline-info btn-xs" runat="server" ImageUrl="../Assets/img/add.png" />
                                                            </div>
                                                        </div>


                                                        <%--<label>Key Responsibilites  </label>
                                                        &nbsp;<label style="color: #a52a2a">*</label>
                                                        <div class="row">
                                                            <div class="col-md-10">
                                                                <asp:TextBox ID="KeyResponsibilitesDropDownList" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender254" runat="server" DelimiterCharacters=""
                                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                    ServiceMethod="GetKeyResponsibilites" ServicePath="~/WebService.asmx" TargetControlID="KeyResponsibilitesDropDownList"
                                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                                </cc1:AutoCompleteExtender>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <asp:ImageButton ID="addImageButton" OnClick="addImageButton_OnClick" CssClass="btn btn-outline-info btn-xs" runat="server" ImageUrl="../Assets/img/add.png" />

                                                            </div>
                                                        </div>--%>
                                                    </div>


                                                    <div class="form-group">
                                                        <asp:GridView ID="KeyResponGridView" runat="server" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                                <asp:BoundField DataField="JobReqKeyResName" HeaderText="Key Responsibilite" HtmlEncode="False" />


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


                                                <div class="form-group">

                                                    <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                        CssClass="table table-bordered text-center thead-dark" Font-Size="11px" Visible="False">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="SL">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="keyresponsTextBox" Text='<%# Eval("JobReqKeyResName")%>' runat="server"></asp:TextBox>
                                                                    <%--<asp:TextBox ID="JobReqKeyResNameTextBox" Text='<%# Eval("JobReqKeyResName")%>' runat="server"></asp:TextBox>--%>

                                                                    <asp:AutoCompleteExtender ID="keyresponsTextBox_AutoCompleteExtender" runat="server"
                                                                        DelimiterCharacters="" EnableCaching="true"
                                                                        Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                        ServiceMethod="GetKeyResponse" ServicePath="WebService.asmx" TargetControlID="keyresponsTextBox"
                                                                        UseContextKey="True"
                                                                        CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem"
                                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                                    </asp:AutoCompleteExtender>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Add">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="adddImageButton" runat="server"
                                                                        CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                                                        ImageUrl="~/Assets/img/add.png" OnClick="adddImageButton_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="delImageButton" runat="server"
                                                                        CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData" OnClick="delImageButton_OnClick"
                                                                        ImageUrl="~/Assets/img/delete.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-md-6">

                                    <fieldset class="for-panel">
                                        <legend>Requirement</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label>Education  </label>
                                                        &nbsp;<label style="color: #a52a2a">*</label>
                                                        <div class="row">
                                                            <div class="col-md-10">
                                                                <asp:DropDownList ID="EducationRequirementDropDownList" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <asp:ImageButton ID="EducationRequirementImageButton" CssClass="btn btn-outline-info btn-xs" runat="server" ImageUrl="../Assets/img/add.png" OnClick="EducationRequirementImageButton_Click" />

                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="form-group">
                                                        <asp:GridView ID="EducationRequirementGridView" CssClass="table table-bordered text-center thead-dark" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ERID">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                                <asp:BoundField DataField="EducationRequirements" HeaderText="Education" HtmlEncode="False" />
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="deleteImageButtonEducationRequirement" runat="server" OnClick="deleteImageButtonEducationRequirement_OnClick"
                                                                            ImageUrl="~/Assets/img/delete.png" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>

                                                <div class="form-group">

                                                    <asp:GridView ID="educationGridView" runat="server" AutoGenerateColumns="False"
                                                        CssClass="table table-bordered text-center thead-dark" Font-Size="11px" Visible="False">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="SL">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="educationTextBox" Text='<%# Eval("Education")%>' runat="server"></asp:TextBox>
                                                                    <asp:AutoCompleteExtender ID="educationTextBox_AutoCompleteExtender" runat="server"
                                                                        DelimiterCharacters="" EnableCaching="true"
                                                                        Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                        ServiceMethod="GetEducation" ServicePath="WebService.asmx" TargetControlID="educationTextBox"
                                                                        UseContextKey="True"
                                                                        CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem"
                                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                                    </asp:AutoCompleteExtender>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Add">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="addeduImageButton" runat="server"
                                                                        CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData" OnClick="addeduImageButton_OnClick"
                                                                        ImageUrl="~/Assets/img/add.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="deleduImageButton" runat="server" OnClick="deleduImageButton_OnClick"
                                                                        CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                                                        ImageUrl="~/Assets/img/delete.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Professional Certification</label>
                                                    <asp:TextBox ID="profCertificationTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>

                            <div class="row">


                                <div class="col-md-12">


                                    <fieldset class="for-panel">
                                        <legend>Education Experiences</legend>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>
                                                        Experience
                                                    </label>
                                                    <asp:TextBox ID="experienceTextBox" CssClass="form-control " runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Skill / Specialization</label>

                                                    <asp:TextBox ID="skillTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Age</label>
                                                    <asp:TextBox ID="ageTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Computer Skill</label>
                                                    <asp:TextBox ID="cmpSkillsTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Other Requirements</label>
                                                    <asp:TextBox ID="othersTextBox" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>                                            
                                        </div>

                                    </fieldset>




                                    <fieldset class="for-panel col-md-5">
                                        <legend>Prefered Way To Circulate The Vacancey</legend>










                                        <div class="form-group">

                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                                                <%--<asp:ListItem>Internal Circulation</asp:ListItem>
                                                <asp:ListItem>Online Circulation</asp:ListItem>
                                                <asp:ListItem>SMC Webpage</asp:ListItem>
                                                <asp:ListItem>Newspaper(Bengali/English)</asp:ListItem>
                                                <asp:ListItem>Head Hunting Firm</asp:ListItem>--%>

                                            </asp:CheckBoxList>

                                            <asp:CheckBox ID="OtherCheckBox" AutoPostBack="true" CssClass="checkbox margin-right" runat="server" Text="Other" OnCheckedChanged="OtherCheckBox_CheckedChanged" />


                                        </div>



                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="LblOther" runat="server">Other</asp:Label></label>
                                            <asp:TextBox CssClass="form-control form-control-sm  col-md-7" ID="otherTextBox" runat="server"></asp:TextBox>
                                        </div>








                                    </fieldset>


                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:TextBox ID="RemarksTextBox" CssClass="form-control form-control-sm col-md-3" runat="server"></asp:TextBox>
                                    </div>



                                    <asp:Button ID="Button2" Text="Save" CssClass="btn btn-info btn-sm" runat="server" OnClick="Button2_OnClick" />
                                    <asp:Button ID="Button3" Text="Cancel" CssClass="btn btn-warning btn-sm" runat="server" />


                                </div>
                            </div>







                        </div>
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

