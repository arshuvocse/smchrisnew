<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" ValidateRequest="false"   EnableEventValidation="false" CodeFile="TrainingEvaluationForm.aspx.cs" Inherits="Training_TrainingEvaluationForm" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-group.required .control-label:after {
            color: #d00;
            content: "*";
            position: absolute;
            margin-left: 4px;
            top: 4px;
            font-size: large;
        }
    </style>

    <style type="text/css">
        /*AutoComplete flyout */
       
    </style>
    
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.min.js"></script>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Training Effectiveness Evaluation Form</h1>
                        </div>
                        <%--                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
                    </div>
                    <div class="card">
                        <div class="card-body">

                            <div class="form-row" runat="server" visible="False">
                                <div class="col-2">
                                    <div class="form-group required">
                                        <label class="control-label">Company</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group required">
                                        <label class="control-label">Employee Name</label>
                                        <asp:TextBox ID="txt_EmpName" runat="server" AutoPostBack="True" class="form-control form-control-sm" OnTextChanged="txt_EmpName_OnTextChanged"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="MasterIDHF" />
                                        <asp:HiddenField runat="server" ID="hfEmpInfoId" />
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="txt_EmpName"
                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </cc1:AutoCompleteExtender>

                                    </div>
                                </div>

                            </div>
                            <div class="form-row">

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Employee ID</label>
                                        <asp:Label runat="server" ID="empCode" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="empIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Employee Name</label>
                                        <br />
                                        <asp:Label runat="server" ID="empName" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField" runat="server" />

                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Date of Joining</label>
                                        <asp:Label runat="server" ID="dtJoining" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Division</label>
                                        <asp:Label runat="server" ID="ddlDivision" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfDivision" runat="server" />

                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Designation</label>
                                        <asp:Label runat="server" ID="ddlDesignation" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfDesignation" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                            </div>
                            <div class="form-row">


                                <div class="col-3" style="display: none">
                                    <div class="form-group">
                                        <label>Slary Grade</label>
                                        <asp:Label runat="server" ID="ddlSalaryGrade" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfSalaryGrade" runat="server" />
                                    </div>
                                </div>

                                <div class="col-6" style="display: none">
                                    <div class="form-group">
                                        <label>Description </label>
                                        <asp:TextBox runat="server" ID="descriptionTextbox" TextMode="MultiLine" Rows="2" class="form-control" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-3" style="display: none">
                                <div class="form-group">
                                    <label class="control-label">Position Title</label>
                                    <asp:TextBox runat="server" ID="txt_PositionTitle" class="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3" style="display: none">
                                <div class="form-group required">
                                    <label class="control-label">Date Of Join</label>
                                    <asp:TextBox runat="server" ID="txt_DOJ" class="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3" style="display: none">
                                <div class="form-group">
                                    <label>Grade</label>
                                    <asp:TextBox runat="server" ID="txt_Grade" class="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3" style="display: none">
                                <div class="form-group">
                                    <label>Place Of Posting</label>
                                    <asp:TextBox runat="server" ID="txt_PlaceOfPosting" class="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-3" style="display: none">
                                <div class="form-group required">
                                    <label class="control-label">Department</label>
                                    <asp:TextBox runat="server" ID="txt_Department" class="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <%--                               <div class="form-row" runat="server" Visible="False">
                                <div class="col-3">
                                    <div class="form-group required">
                                        <label class="control-label">Probition From</label>
                                        <asp:TextBox runat="server" ID="txt_ProbitionFrom" class="form-control form-control-sm"></asp:TextBox>
                                          <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                    Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                    TargetControlID="txt_ProbitionFrom" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group required">
                                        <label class="control-label">Probition To</label>
                                        <asp:TextBox runat="server" ID="txt_ProbitionTo" class="form-control form-control-sm"></asp:TextBox>
                                         <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                    Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                    TargetControlID="txt_ProbitionTo" />
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group required">
                                        <label class="control-label">Due Date Of Confirmation</label>
                                        <asp:TextBox runat="server" ID="txt_DueDateOfConfirmation" class="form-control form-control-sm"></asp:TextBox>
                                           <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupPosition="TopLeft"
                                                    Format="dd-MMM-yyyy" PopupButtonID="txt_DueDateOfConfirmation" CssClass="MyCalendar"
                                                    TargetControlID="txt_DueDateOfConfirmation" />
                                    </div>
                                </div>
                                <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <br/>
                                        <label>IsActive</label>
                                        <asp:CheckBox runat="server" ID="chk_IsActive" Checked="True"></asp:CheckBox>
                                    </div>
                                </div>


                            </div>--%>
                            <div>
                                <div style="text-align: center;">
                                    Key Rating Criteria to be evaluated
                                        <br />
                                    <br />
                                    (Please use this form to evaluate the effectiveness of imparted training and return to HR department with comments tick (√) marks against each of the following boxes as felt appropriate to you)
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
                                        <asp:TemplateField HeaderText="Key Rating Criteria">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_RatingCriterions" runat="server" Text='<%#Eval("TextField") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rating Scale">
                                            <ItemTemplate>
                                                <asp:RadioButtonList runat="server" ID="rad_RatingScale" RepeatDirection="Horizontal" BorderStyle="None" CellPadding="1" CellSpacing="1" Font-Size="14px">
                                                    <asp:ListItem Value="1" Text="No change (0)" runat="server"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Minor Change (1)" runat="server"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Reasonable  Change (2)" runat="server"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Significant  Change (3)" runat="server"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="Not applicable (0)" runat="server"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label class="control-label">Comments</label>
                                        <asp:TextBox runat="server" ID="txt_SupervisorObservation" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label class="control-label">Department Head Observation</label>
                                        <asp:TextBox runat="server" ID="txt_DepartmentHeadObservation" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2"  runat="server" Visible="False">
                                    <div class="form-group">
                                        <label class="control-label">Division Head Observation</label>
                                        <asp:TextBox runat="server" ID="txt_DivisionHeadObservation" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2"  runat="server" Visible="False">
                                   <%-- <asp:RadioButtonList ID="RBConSep" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RBConSep_OnSelectedIndexChanged">
                                        <asp:ListItem>Extend probation period</asp:ListItem>
                                        <asp:ListItem>Probation End Request</asp:ListItem>
                                    </asp:RadioButtonList>--%>
                                     <div class="form-group">
                                     <label class="control-label">Action Type</label>
                                      <asp:DropDownList ID="ExtendProbationDropDownList" AutoPostBack="True" class="form-control form-control-sm" runat="server" OnSelectedIndexChanged="ExtendProbationDropDownList_OnSelectedIndexChanged">
                                            <asp:ListItem Value="0">Select One...</asp:ListItem>
                                            <asp:ListItem Value="1">Extend probation period</asp:ListItem>
                                            <asp:ListItem Value="2">Probation End Request</asp:ListItem>
                                        </asp:DropDownList>
                                         </div>
                                </div>
                                <asp:Panel class="col-2" runat="server" visible="False" id="probendreason">
                                    <div class="form-group">
                                        <label class="control-label">Extended Probation </label>
                                        <asp:DropDownList ID="ddlreason" AutoPostBack="True" class="form-control form-control-sm" runat="server" OnSelectedIndexChanged="ddlreason_OnSelectedIndexChanged">
                                            <asp:ListItem Value="1">Seperation</asp:ListItem>
                                            <asp:ListItem Value="2">Confirmed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </asp:Panel>
                                <asp:Panel class="col-2" runat="server" visible="False" id="exppr">
                                    <div class="form-group">
                                        <label class="control-label">
                                            <asp:Label runat="server" Text="Date" ID="dateprob"></asp:Label></label>
                                        <asp:TextBox runat="server" ID="exProDate" class="form-control form-control-sm"  ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="exProDate" PopupPosition="TopLeft" />
                                    </div>
                                </asp:Panel>

                            </div>

                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-12 ">
                                    <asp:CheckBox ID="manualUpdateCheckBox" runat="server" />
                                    <span>&nbsp;  Manually Update to Employee Information </span>
                                </div>
                            </div>
                            <br />

                            <div>
                                <asp:HiddenField runat="server" ID="hdpk" />
                                <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                                <asp:Button ID="cancelButton" Text="Cancel" Visible="False" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                    <asp:Button ID="detailsViewButton" Text="Back to List" CssClass="btn btn-sm text-info" runat="server" OnClick="detailsViewButton_OnClick" />
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />

                        </div>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


