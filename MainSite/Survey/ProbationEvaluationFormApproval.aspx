<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="ProbationEvaluationFormApproval.aspx.cs" Inherits="Survey_ProbationEvaluationForm" %>

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

        .chkChoiceHeader label {
            padding-left: 2px;
            padding-right: 10px;
            font-size: 13px;
        }


        #cpFormBody_AppLogCommentGridView td {
            border: 1px solid #ddd !important;
            padding: 8px;
        }


        #cpFormBody_presuperGridView td {
            border: 1px solid #ddd !important;
            padding: 8px;
        }


        #cpFormBody_loadGridView td {
            border: 1px solid #ddd !important;
            padding: 8px;
        }
             .chkChoiceHeader label {
            padding-left: 2px;
            padding-right: 10px;
            font-size: 13px;
                font-weight: bold;
        }
    </style>
    <style type="text/css">
        /*AutoComplete flyout */
    </style>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <div class="icon">
                                <img src="../Assets/2076-512.png" /></div>
                            <span>Approval Request for</span>
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Probation Period </h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">

                            <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                            <asp:Button ID="detailsViewButton" Text="&#8920; Back to List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick"   />

                            <%--  <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />--%>
                        </div>
                    </div>

                    <div class="card-body">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-1.5">
                                    <label style="font-weight: bold">Approval Status:&nbsp;<span style="color: #a52a2a">*</span></label>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">

                                        <asp:RadioButtonList ID="actionRadioButtonList" runat="server" CssClass="chkChoiceHeader" RepeatDirection="Horizontal" >
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                            </div>

                            <div class="form-row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label style="font-weight: bold">Comments</label>
                                        <asp:TextBox runat="server" ID="commentsTextBox" CssClass="form-control" ReadOnly="False" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-5">
                                </div>
                                <div class="col-4 ">
                                    <div class="form-group">
                                        <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                        <div class="ui-group-buttons">
                                            <asp:LinkButton ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="Button2_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit&nbsp; </asp:LinkButton>
                                            <asp:LinkButton ID="Button1" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="Button1_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>
                                          <%--  <div class="or or-sm" runat="server" id="orBTN"></div>--%>
                                           <%-- <asp:LinkButton ID="Button2a" Text="Cancel" runat="server" OnClick="Button2a_OnClick" OnClientClick="return confirm('Are you sure you want to Reject ?')" CssClass="btn btn-sm btn-danger"><i class="fa fa-times" aria-hidden="true"></i>
&nbsp; Reject </asp:LinkButton>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
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
                                </style>
                                <br>
                                 <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;"> EMPLOYEE INFORMATION :</h2>
                                 
                            <hr>
<br>
                                <table class="table table-bordered table-striped">
                                    <tr>
                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                        <td>
                                            <asp:Label runat="server" ID="empCode"></asp:Label></td>


                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                        <td>
                                            <asp:Label ID="ReportingLabel" runat="server"></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                        <td>
                                            <asp:Label runat="server" ID="empName"></asp:Label></td>



                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Department</td>
                                        <td>
                                            <asp:Label ID="deptNameLabel" runat="server"></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Designation</td>
                                        <td>
                                            <asp:Label ID="ddlDesignation" runat="server"></asp:Label></td>

                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Office</td>
                                        <td>
                                            <asp:Label ID="LocationLabel" runat="server"></asp:Label></td>

                                    </tr>






                                    <tr>
                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Date Of Joining</td>
                                        <td>
                                            <asp:Label ID="dtJoining" runat="server"></asp:Label></td>
                                        <td style="width: 20%; padding: 10px;" class="tblTHColorChang">Place</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                    </tr>





                                </table>
                            </div>

                              <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">  Action Type :</h2>
                           
                            <hr>
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

                            <div class="form-row" runat="server" visible="False">

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Code</label>
                                        <hr />

                                        <asp:HiddenField ID="empIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Name</label>
                                        <br />
                                        <hr />

                                        <asp:HiddenField ID="HiddenField" runat="server" />

                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Date of Joining</label>
                                        <br />
                                        <hr />

                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Division</label>
                                        <br />
                                        <hr />
                                        <asp:Label runat="server" ID="ddlDivision" />
                                        <asp:HiddenField ID="hfDivision" runat="server" />

                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Designation</label>
                                        <br />
                                        <hr />

                                        <asp:HiddenField ID="hfDesignation" runat="server" />
                                    </div>
                                </div>
                            </div>


                            <div class="form-row" runat="server" visible="False">


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
                            <div class="form-row">
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
                           <div class="col-3" runat="server"  >
                                <%-- <asp:RadioButtonList ID="RBConSep" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RBConSep_OnSelectedIndexChanged">
                                        <asp:ListItem>Extend probation period</asp:ListItem>
                                        <asp:ListItem>Probation End Request</asp:ListItem>
                                    </asp:RadioButtonList>--%>
                                <div class="form-group">
                                    <label class="control-label">Action Type</label>
                                    <asp:DropDownList ID="ExtendProbationDropDownList" CssClass="form-control form-control-sm" runat="server"  OnSelectedIndexChanged="ExtendProbationDropDownList_OnSelectedIndexChanged" AutoPostBack="True" Enabled="False">
                                        <asp:ListItem Value="0">Select One...</asp:ListItem>
                                        <asp:ListItem Value="1">Extend probation period</asp:ListItem>
                                        <asp:ListItem Value="2">Probation End Request</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                             <div class="col-3" runat="server" Visible="False">

                                    <asp:RadioButtonList ID="RadioButtonList1" AutoPostBack="True" Enabled="False"  OnSelectedIndexChanged="RadioButtonList1_OnSelectedIndexChanged" runat="server">
                                        <asp:ListItem>Extend probation period</asp:ListItem>
                                        <asp:ListItem Value="Probation End Request">Probation End Request</asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>
                                  <asp:Panel class="col-3" runat="server" Visible="False" ID="probendreason">
                                <div class="form-group">
                                    <label class="control-label">Probation End Status :</label>
                                    <asp:DropDownList ID="ddlreason" AutoPostBack="True" OnSelectedIndexChanged="ddlreason_OnSelectedIndexChanged" CssClass="form-control form-control-sm" runat="server" RepeatDirection=""  Enabled="False">
                                        <asp:ListItem Value="1">Seperation</asp:ListItem>
                                        <asp:ListItem Value="2">Confirmed</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </asp:Panel>
                                <div class="col-2" runat="server" visible="False" id="exppr">
                                    <div class="form-group">
                                        <label class="control-label">Date</label>
                                        <asp:TextBox runat="server" ID="exProDate" class="form-control form-control-sm" AutoPostBack="True" ReadOnly="True"></asp:TextBox>

                                    </div>
                                </div>
                        
                                 <asp:Panel class="col-2" runat="server" visible="False" id="Panel1">
                                    <div class="form-group">
                                        <label class="control-label">Extended Probation </label>
                                        <asp:DropDownList ID="DropDownList1" AutoPostBack="True" class="form-control form-control-sm" runat="server" OnSelectedIndexChanged="ddlreason_OnSelectedIndexChanged">
                                            <asp:ListItem Value="1">Seperation</asp:ListItem>
                                            <asp:ListItem Value="2">Confirmed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </asp:Panel>
                          

                               <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">  Supervisor Evaluation  :</h2>
                             
                            <hr>

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

                            <div class="form-row">
                                <div class="col-3" runat="server" visible="False">
                                    <div class="form-group required">
                                        <label class="control-label">Direct Supervisor Observation</label>
                                        <asp:TextBox runat="server" ID="txt_SupervisorObservation" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-3" runat="server" visible="False">
                                    <div class="form-group required">
                                        <label class="control-label">Department Head Observation</label>
                                        <asp:TextBox runat="server" ID="txt_DepartmentHeadObservation" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2" runat="server" visible="False">
                                    <div class="form-group required">
                                        <label class="control-label">Division Head Observation</label>
                                        <asp:TextBox runat="server" ID="txt_DivisionHeadObservation" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                               
                            </div>


                            <div class="form-row" runat="server">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Approval Status Information</legend>
                                        <div style="overflow: scroll">
                                            <asp:GridView ID="AppLogCommentGridView" CssClass="table table-bordered text-center thead-dark" runat="server" Width="100%" AutoGenerateColumns="False">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PreEmp" HeaderText="Approval Person" HtmlEncode="False" />
                                                      <asp:BoundField DataField="Comments" HeaderText="Comments" HtmlEncode="False" />
                                                 <%--   <asp:BoundField DataField="ForEmp" HeaderText="Approval Person" HtmlEncode="False" />--%>
                                                    <%--<asp:BoundField DataField="Version" HeaderText="Version" HtmlEncode="False" />--%>
                                                  
                                                <%--    <asp:BoundField DataField="ActionStatus" HeaderText="Action Status" HtmlEncode="False" />--%>


                                              <%--      <asp:BoundField DataField="ApproveBy" HeaderText="Approved By" HtmlEncode="False" />--%>
                                                    <asp:BoundField DataField="ApproveDate" HeaderText="Approved Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                </Columns>
                                            </asp:GridView>
                                    </fieldset>
                                </div>

                            </div>

                            <div>
                                <asp:HiddenField runat="server" ID="hdpk" />

                                <%--<asp:Button ID="detailsViewButton" Text="Back to List" CssClass="btn btn-sm text-info" runat="server" OnClick="detailsViewButton_OnClick" />--%>
                                <asp:HiddenField ID="entryempinfoIdHiddenField" runat="server" />
                                <asp:HiddenField ID="actionstatusHiddenField" runat="server" />
                                <%--<asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
                            </div>
                            <asp:HiddenField ID="masterHiddenField" runat="server" />

                        </div>


                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


