<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="JobRequisitionFormApprovalView2.aspx.cs" Inherits="MasterSetup_UI_JobRequisitionForm" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        /*AutoComplete flyout */
     
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
                      <div class="icon"> <img src="../Assets/2076-512.png" /></div>
                                <span>Approval Request</span>
                        <h1 class="title" style="font-size: 18px; padding-top: 1px;">Employee Requisition Form Approval </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:HyperLink ID="BacktoList" Text="<<< Back to List" OnClick="BacktoList_OnClick" NavigateUrl="~/RecruitmentManagement_UI/JobRequisitionFormAproval.aspx" CssClass="btn btn-sm btn-default" runat="server" ForeColor="black" />
                         <%--<asp:Button ID="ViewListButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="ViewListButton_OnClick" />--%>
                        <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>

                    <div class="page-heading__container float-right d-none d-sm-block" runat="server" Visible="False">
                        <asp:Button ID="homeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="ViewListButton" Visible="True" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="ViewListButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->
                <asp:HiddenField ID="empIdHiddenField" runat="server" />

                <div class="container-fluid">
                    
                        <div class="card-body">
                            <div class="card" style="padding: 10px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Company Name </label>
                                                <br/>
                                                <hr/>
                                                <asp:Label runat="server" ID="lblCompany"></asp:Label>
                                                <asp:DropDownList ID="companyDropDownList" Visible="False" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                          <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Financial Year</label>
                                                 <br/>
                                                <hr/>
                                                <asp:Label runat="server" ID="lblFinYear"></asp:Label>
                                                <asp:DropDownList ID="financialYearDropDownList" Visible="False"  class="form-control form-control-sm" runat="server" AutoPostBack="True" OnTextChanged="financialYearDropDownList_OnTextChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Department </label>
                                              <br/>
                                                <hr/>
                                                <asp:Label runat="server" ID="lblDepartment"></asp:Label>
                                                <asp:DropDownList ID="deptDropDownList" Visible="False"  AutoPostBack="True" CssClass="form-control form-control-sm" runat="server" OnSelectedIndexChanged="deptDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                        </div>
                                      

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Is Management Approved</label>
                                                  <br/>
                                                <hr/>
                                                <asp:Label runat="server" ID="lblManagementApproved"></asp:Label>
                                                <asp:RadioButtonList Visible="False" ID="isBudgetedCheckBox" RepeatDirection="Horizontal" runat="server" AutoPostBack="True" OnTextChanged="isBudgetedCheckBox_CheckedChanged">
                                                    <asp:ListItem> &nbsp; Yes &nbsp; </asp:ListItem>
                                                    <asp:ListItem> &nbsp; No &nbsp; </asp:ListItem>
                                                </asp:RadioButtonList>

                                            </div>
                                        </div>




                                        <div class="col-md-3" runat="server" visible="False">
                                            <div class="form-group">
                                                <label>Financial Year</label>
                                                &nbsp;<label style="color: #a52a2a">*</label>
                                                <asp:DropDownList ID="mainFinyearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="col-md-2" id="ShowBudgetInfo" visible="False" runat="server">
                                            <div class="form-group">
                                                <label>Budgeted Position </label>
                                                  <br/>
                                                <hr/>
                                                <asp:Label runat="server" ID="lblBudgetedPosition"></asp:Label>
                                                <asp:DropDownList ID="BudgetCodeDropDownList" Visible="False" class="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="BudgetCodeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>

                                         <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Requisition Date</label>
                                                  <br/>
                                                <hr/>
                                                <asp:Label runat="server" ID="lblreqDate"></asp:Label>
                                                <asp:TextBox ID="reqDateTextBox"  Visible="False"  CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="reqDateTextBox" />
                                            </div>
                                        </div>
                                       



                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Note </label>
                                                 <br/>
                                                <hr/>
                                                  <asp:Label runat="server" ID="lblNote"></asp:Label>
                                                <asp:TextBox ID="NoteTextBox" Visible="False" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:TextBox ID="ReqCodetextBox" runat="server" Visible="False" class="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                        </div>

                                       


                                    </div>

                                    <fieldset class="for-panel">
                                        <legend>Justification & Job Summary </legend>


                                        <div class="row">
                                            <div class="col-md-4">
                                                
                                                    <div class="row">
                                                      <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Justification </label>
                                                    <br/>
                                                <hr/>
                                                  <asp:Label runat="server" ID="lblJustification"></asp:Label>
                                                    <asp:TextBox ID="JustificationTextBox" Visible="False" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                                </div>
                                               <div class="row">
                                                    <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Job Summary </label>   <br/>
                                                <hr/>
                                                  <asp:Label runat="server" ID="lblJobSummary"></asp:Label>
                                                    <asp:TextBox ID="descriptionTextBox" Visible="False" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                           
                                               </div>
                                            
                                            </div>


                                            <div class="col-md-4">

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Employee Information</label>
                                                            <br/>
                                                            <hr/>
                                                            <asp:Label runat="server" ID="lblEMpJst"></asp:Label>
                                                            <asp:RadioButtonList ID="jstRadioButtonList" Visible="False" RepeatDirection="Horizontal" runat="server" AutoPostBack="True" OnTextChanged="jstRadioButtonList_OnTextChanged">
                                                                <asp:ListItem> &nbsp; New &nbsp; </asp:ListItem>
                                                                <asp:ListItem> &nbsp; Replacement &nbsp; </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        
                                                    </div>
                                                </div>

                                                <div runat="server" visible="False" id="detail">

                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblEmpName" runat="server" Visible="False">
                                                            <Label>Employee Name </Label>
                                                                </asp:Label>
                                                                <br/>
                                                                <hr/>
                                                                  <asp:Label ID="nameLabel" runat="server"></asp:Label>
                                                                <div class="form-group">
                                                                    <%--<asp:DropDownList ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" ></asp:DropDownList>--%>
                                                                    <asp:TextBox Visible="False" ID="EmployeeNameTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="EmployeeNameTextBox_OnTextChanged"></asp:TextBox>
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

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Label ID="lblDatSep" runat="server" Visible="False"> Date Of Seperation </asp:Label>
                                                                </label>
                                                                <br/>
                                                                <hr/>
                                                                <asp:Label ID="DateOfSeperationTextBox"    runat="server"></asp:Label>
                                                               
                                                            </div>
                                                        </div>
                                                    </div>

                                                
                                                </div>
                                            </div>
                                           
                                            <div class="col-md-4" style="margin-top: 50px; font-style: italic;" id="Showme" runat="server" Visible="False">
                                                    <div class="row">
                                                        <div class="col-md-12" runat="server"  >
                                                            <div class="form-group">
                                                              <label>Employee Code: </label>
                                                              
                                                                <asp:Label ID="codeLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="form-group">

                                                                <label >Employee Name: </label>
                                                              
                                                             
                                                              
                                                            </div>
                                                        </div>
                                                             <div class="col-md-12">
                                                            <div class="form-group">

                                                                <label >Department </label>
                                                                
                                                                <asp:Label ID="deptLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="form-group">

                                                                <label>Designation: </label>
                                                          
                                                                <asp:Label ID="desigLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12" runat="server" Visible="False">
                                                            <div class="form-group">

                                                                <label >Salary Grade </label>
                                                               
                                                                <asp:Label ID="salgradLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12" runat="server" Visible="False">
                                                            <div class="form-group">

                                                                <%--<label style="color: grey">Division </label>--%>
                                                              
                                                                <asp:Label ID="divLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                            
                                                    </div>
                                                    <div class="row" runat="server"  >
                                                
                                                    <div class="col-md-12">
                                                            <div class="form-group">

                                                                <label  >Employeement Type: </label>
                                                                
                                                                <asp:Label ID="wingLabel"  runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                   
                                                        <div class="col-md-2" runat="server" Visible="False">
                                                            <div class="form-group">

                                                               <%-- <label style="color: grey">Section </label>--%>
                                                                
                                                                <asp:Label ID="secLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2" runat="server" Visible="False">
                                                            <div class="form-group">

                                                              <%--  <label style="color: grey">Sub Section </label>--%>
                                                                
                                                                <asp:Label ID="subsecLabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>

                                                    </div>
                                            </div>

                                        </div>

                                    </fieldset>


                                    <fieldset class="for-panel">
                                        <legend>Position Description</legend>
                                        <div class="row">

                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Job Title</label>
                                                       <br/>
                                                <hr/>
                                                    <asp:Label ID="jobTitleTextBox"   runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Grade </label>
                                                      <br/>
                                                <hr/>
                                                    <asp:Label ID="lblgrade"   runat="server"></asp:Label>
                                                    <asp:DropDownList ID="gradeDropDownList" Visible="False" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Total Vacancy </label>
                                                     <br/>
                                                <hr/>
                                                    <asp:Label ID="nosTextBox"  runat="server"></asp:Label>

                                                </div>
                                            </div>
                                             <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Expected Date of joining </label>
                                                       <br/>
                                                <hr/>
                                                    <asp:Label ID="expDtJoinTextBox"   runat="server"></asp:Label>
                                                     
                                                </div>
                                            </div>
                                            <div class="col-md-3">

                                                <div class="form-group">
                                                    <label>Employee Type  </label>
                                                      <br/>
                                                <hr/>
                                                    <asp:Label ID="lblEmpType"   runat="server"></asp:Label>
                                                    <asp:RadioButtonList  ID="typeOfPosRadioButtonList" OnSelectedIndexChanged="typeOfPosRadioButtonList_OnSelectedIndexChanged" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>

                                                    <%--   <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                            <asp:ListItem><span style="font-size:12px">Permanent</span>&nbsp; </asp:ListItem>
                                                            <asp:ListItem> <span style="font-size:12px">Contractual</span> &nbsp;</asp:ListItem>
                                                            <asp:ListItem><span style="font-size:12px">Casual</span> &nbsp;</asp:ListItem>
                                                            <asp:ListItem><span style="font-size:12px">Other</span>&nbsp;</asp:ListItem>
                                                        </asp:RadioButtonList>--%>
                                                </div>
                                            </div>
                                           
                                            <div class="col-md-2" id="project" runat="server" visible="False">
                                                <div class="form-group">
                                                    <label>Project </label>
                                                        <br/>
                                                <hr/>
                                                    <asp:Label ID="lblProject"  runat="server"></asp:Label>
                                                    <asp:DropDownList Visible="False" ID="projectDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3" style="display: none">
                                                <div class="form-group">
                                                    <label>Division </label>
                                                    <asp:DropDownList ID="divisionDropDownList" CssClass="form-control form-control-sm" AutoPostBack="True" runat="server" OnSelectedIndexChanged="divisionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3" style="display: none">
                                                <div class="form-group">
                                                    <label>Employement Type </label>
                                                    <asp:DropDownList ID="divWingDropDownList" CssClass="form-control form-control-sm" AutoPostBack="True" runat="server" OnSelectedIndexChanged="divWingDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-3" runat="server" visible="False">
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


                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Office </label>
                                                  <br/>
                                                <hr/>
                                                    <asp:Label ID="lblOffice"  runat="server"></asp:Label>
                                                    <asp:DropDownList ID="officeDropDownList" Visible="False" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="officeDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Job Location  </label>
                                                     <br/>
                                                <hr/>
                                                    <asp:Label ID="lblJobLocation"  runat="server"></asp:Label>
                                                    <asp:DropDownList ID="placeDropDownList"  Visible="False" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2.2">
                                                <div class="form-group">
                                                    <label>Reporting to (Search Employee)  </label>
                                                   <br/>
                                                <hr/>
                                                    <asp:Label ID="lblreportTo"  runat="server"></asp:Label>
                                                    <asp:TextBox ID="reportToTextBox" Visible="False" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" OnTextChanged="reportToTextBox_OnTextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender21" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetCompanyWiseEmployeeInfo" ServicePath="~/WebService.asmx" TargetControlID="reportToTextBox"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>
                                                </div>
                                            </div>



                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Internal Contacts</label>
                                                     <br/>
                                                <hr/>
                                                    <asp:Label ID="lblinternalCon"  runat="server"></asp:Label>
                                                    <asp:TextBox ID="internalConTextBox" Visible="False" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>External Contacts</label>
                                                     <br/>
                                                <hr/>
                                                    <asp:Label ID="lblexternalCon"  runat="server"></asp:Label>
                                                    <asp:TextBox ID="externalConTextBox" Visible="False" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row" runat="server" Visible="False">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Directly Supervised </label>
                                                    <asp:DropDownList ID="jobtitleDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-1.5" style="margin-top: 19px;">
                                                <asp:Button ID="DirectlySupervicesButton" Text="Add To List" OnClick="DirectlySupervicesButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm"  runat="server" /></div>
                                        </div>
                                    <%--     <div style="max-height: 150px; overflow: scroll">--%>
                                        <div class="row">
                                            <div class="col-5">
                                                 <div style="max-height: 150px; overflow: scroll">
                                                <asp:GridView ID="DirectlySupervicesGridView" CssClass="table table-bordered text-center thead-dark" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="DesignationId">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                        <asp:BoundField DataField="Designation" HeaderText="Directly Supervice" HtmlEncode="False" />
                                                        <asp:TemplateField HeaderText="Delete" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="deleteImageButtonDirectlySupervices" runat="server" OnClick="deleteImageButtonDirectlySupervices_OnClick"
                                                                    ImageUrl="~/Assets/img/delete.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                          
                                    </fieldset>
                                </div>
                            </div>
                            </div>
                        </div>
                           
                        <div class="card-body"> <div class="card" style="padding: 10px">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Key Responsibilities</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <div class="row" runat="server" Visible="False">
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Division </label>
                                                                    <asp:DropDownList ID="dsnDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="dsnDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
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
                                                        </div>
                                                        <div class="row" runat="server" Visible="False">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                   
                                                                     <div style="max-height: 150px; overflow: scroll">
                                                                         <asp:CheckBox runat="server" Text="Select/Unselect All" AutoPostBack="True" ID="SelectAll" OnCheckedChanged="SelectAll_Checked"/>
                                                                    <asp:CheckBoxList ID="jdCheckBoxList" runat="server"></asp:CheckBoxList>
                                                                         </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" runat="server" Visible="False">
                                                            <div class="form-group">
                                                                <div class="col-md-12">
                                                            
                                                                <asp:Button ID="addImageButton" Text="Add Emp. JD" OnClick="addImageButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />
                                                            </div>
                                                                    </div>
                                                        </div>

                                                        <br />

                                                        <div class="row" runat="server" Visible="False">

                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>JD </label>
                                                                    <asp:TextBox ID="jdTextBox" CssClass="form-control form-control-sm"  runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row" runat="server" Visible="False">
                                                          <div class="form-group">
                                                                <div class="col-md-12">
                                                                <asp:Button ID="textButton" Text="Add Free Text" OnClick="textButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />
                                                            </div>
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

                                                                <asp:TemplateField HeaderText="Edit" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="addeduImageButton" runat="server" OnClick="editImageButton_OnClick"
                                                                            ImageUrl="~/Assets/img/rsz_edit.png" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Delete" Visible="False">
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

                            </div>
                            <div class="row">

                                <div class="col-md-12">

                                    <fieldset class="for-panel">
                                        <legend>Requirement</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">

                                                        <div class="row">
                                                            <div class="col-md-3" runat="server" Visible="False">
                                                                <label>Education  </label>
                                                                &nbsp;<label style="color: #a52a2a">*</label>
                                                                <asp:DropDownList ID="EducationRequirementDropDownList" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            </div>

                                                            <div class="col-md-1.5" style="margin-top: 18px" runat="server" Visible="False">
                                                               <div class="form-group">
                                                                <asp:Button ID="EducationRequirementImageButton" CssClass="btn btn-outline-success btn-block disabled btn-sm"  runat="server"  Text="Add To List"   OnClick="EducationRequirementImageButton_Click" />
                                                                   </div>
                                                            </div>
                                                            <div class="col-md-3">

                                                                <label>Professional Certification</label>
                                                                    <br/>
                                                <hr/>
                                                   
                                                                <asp:Label ID="profCertificationTextBox"   runat="server"></asp:Label>

                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="col-5">
                                                         <div style="max-height: 150px; overflow: scroll">
                                                        <asp:GridView ID="EducationRequirementGridView" CssClass="table table-bordered text-center thead-dark" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ERID">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                                <asp:BoundField DataField="EducationRequirements" HeaderText="Education" HtmlEncode="False" />
                                                                <asp:TemplateField HeaderText="Delete" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="deleteImageButtonEducationRequirement" runat="server" OnClick="deleteImageButtonEducationRequirement_OnClick"
                                                                            ImageUrl="~/Assets/img/delete.png" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
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
                                        </div>
                                    </fieldset>
                                </div>

                            </div>

                            <div class="row">


                                <div class="col-md-12">


                                    <fieldset class="for-panel">
                                        <legend>Experiences</legend>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>
                                                        Relevant Experience
                                                    </label>
                                                     <br/>
                                                <hr/>
                                                    <asp:Label ID="experienceTextBox"   runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Skill / Specialization</label>
                                                     <br/>
                                                <hr/>
                                                    <asp:Label ID="skillTextBox"   runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Age</label>
                                                     <br/>
                                                <hr/>
                                                    <asp:Label ID="ageTextBox"   runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Computer Skill</label>
                                                     <br/>
                                                <hr/>
                                                    <asp:Label ID="cmpSkillsTextBox"   runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" runat="server" Visible="False">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Other Requirements</label>
                                                   
                                                    <asp:TextBox ID="othersTextBox" CssClass="form-control  form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12" style="margin-top: 18px">
                                                      <div class="form-group">
                                                    <asp:Button ID="OtherRequirementsAddButton" Text="Add To List" OnClick="OtherRequirementsAddButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm"  runat="server" />
                                              
                                                          </div>  </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:GridView ID="OtherRequirementsGridView" runat="server" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                            <asp:BoundField DataField="OtherRequirementsName" HeaderText="Other Requirements" HtmlEncode="False" />

                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="addeduImageButton" runat="server" OnClick="editOtherRequirementsGridViewButton_OnClick"
                                                                        ImageUrl="~/Assets/img/rsz_edit.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteOtherRequirementsGridViewButton_OnClick"
                                                                        ImageUrl="~/Assets/img/delete.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                    </fieldset>




                                    <fieldset class="for-panel col-md-5">
                                        <legend>Prefered Way To Circulate The Vacancy</legend>
                                        <div class="form-group">

                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                                                <%--<asp:ListItem>Internal Circulation</asp:ListItem>
                                                <asp:ListItem>Online Circulation</asp:ListItem>
                                                <asp:ListItem>SMC Webpage</asp:ListItem>
                                                <asp:ListItem>Newspaper(Bengali/English)</asp:ListItem>
                                                <asp:ListItem>Head Hunting Firm</asp:ListItem>--%>
                                            </asp:CheckBoxList>

                                            <asp:CheckBox ID="OtherCheckBox" Visible="False" AutoPostBack="true" CssClass="checkbox margin-right" runat="server" Text="Other" OnCheckedChanged="OtherCheckBox_CheckedChanged" />


                                        </div>



                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="LblOther" runat="server">Other</asp:Label></label>
                                            <asp:TextBox CssClass="form-control form-control-sm  col-md-7" ID="otherTextBox" runat="server"></asp:TextBox>
                                        </div>

                                    </fieldset>


                                    <div class="form-group">
                                        <label>Remarks</label>
                                         <br/>
                                                <hr/>
                                        <asp:Label ID="RemarksTextBox"   runat="server"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <label>Approval Status </label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:RadioButtonList ID="jobreqRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                    
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group">
                                            <label class="font-weight-bold">Comments</label>
                                            
                                         
                                            
                                            <asp:TextBox runat="server"   ID="txtComment" TextMode="MultiLine" Rows="2" class="form-control" />
                                        </div>
                                    <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="Button2_OnClick" />
                                    <%--<asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                    <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />--%>

                                    <%--<asp:Button ID="Button2" Text="Save" CssClass="btn btn-info btn-sm" runat="server" OnClick="Button2_OnClick" />--%>
                                    <%--<asp:Button ID="Button3" Text="Cancel" CssClass="btn btn-warning btn-sm" runat="server" Visible="False" />--%>


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

