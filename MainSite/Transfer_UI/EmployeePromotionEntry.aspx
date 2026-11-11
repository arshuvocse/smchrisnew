<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeePromotionEntry.aspx.cs" Inherits="Transfer_UI_EmployeePromotionEntry" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style type="text/css">
        /*AutoComplete flyout */
        .chkChoiceDesignation label {
            padding-left:2px;
            padding-right: 7px;
            font-weight: bold;
        }

          .chkChoice label {
            padding-left: 2px;
            padding-right: 2px;
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

                    #cpFormBody_presuperGridView> tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


          #cpFormBody_presuperGridView  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
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
                        <div class="icon"><img src="../Report_Pages/app.png" width="20px"  /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Employee Promotion Information </h1>
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

                            <div class="row">

                                <div class="col-md-12">
                                    <div class="row">


                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Company Name </label>
                                                <span style="color: red">&nbsp;*</span>
                                                <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Financial Year </label>
                                     <%--        OnTextChanged="ActiveDateTextBox_Changed" AutoPostBack="True"   AutoPostBack="True" OnSelectedIndexChanged="FinancialYearDropDownList_OnSelectedIndexChanged"--%>
                                                <span style="color: red">&nbsp;*</span>
                                                <asp:DropDownList ID="FinancialYearDropDownList" class="form-control form-control-sm"  runat="server" ></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Label4">Effective Date </asp:Label>
                                                <span style="color: red">&nbsp;*</span>
                                                <asp:HiddenField ID="areaHiddenField" runat="server" />
                                                <asp:HiddenField ID="areaCodeHiddenField" runat="server" />
                                                <asp:TextBox ID="ActiveDateTextBox" AutoCompleteType="Disabled"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                    Format="dd-MMM-yyyy" PopupButtonID="ActiveDateTextBox" CssClass="MyCalendar"
                                                    TargetControlID="ActiveDateTextBox" />
                                            </div>
                                        </div>



                                    </div>
                                     <style>
                                                  .chkChoiceSigle label {
            padding-left: 8px;
            padding-right: 7px;
        }
                                            </style>

                                    <div class="row">
                                        <div class="col-md-6">
                                            
                                    
                                            <asp:HiddenField ID="EmployeePromotionEntryIdHiddenField" runat="server" />
                                            <div class="form-group">
                                                <label>Search Employee: </label>
                                                
                                                
                                                             <asp:DropDownList   runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpInfo_OnSelectedIndexChanged"  ID="ddlEmpInfo" class="form-control form-control-sm" />
                                                    <script type="text/javascript">
                                                        function pageLoad() {
                                                            $('#<%=ddlEmpInfo.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                            $('#<%=ddlReportingBody.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                                                            $('#<%=ddlDirectlySupervisor.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                                                            $('.SelecttoME').chosen({ disable_search_threshold: 5, search_contains: true });

                                                        }
</script>

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

                                        </div>

                                    </div>
                                    <div class="row">

                                        <div class="col-md-5">
                                            <asp:Panel runat="server" ID="SearchViewPanel" Visible="False">
                                                <div class="row">
                                                    <asp:HiddenField runat="server" ID="HFDesgId" />
                                                    <asp:HiddenField runat="server" ID="HFDivID" />
                                                    <asp:HiddenField runat="server" ID="HFDivWingId" />
                                                    <asp:HiddenField runat="server" ID="HFDeptID" />
                                                    <asp:HiddenField runat="server" ID="HFSecID" />
                                                    <asp:HiddenField runat="server" ID="HFSubSecID" />
                                                    <asp:HiddenField runat="server" ID="HFSalLocID" />
                                                    <asp:HiddenField runat="server" ID="HFJobLocID" />

                                                    <asp:HiddenField runat="server" ID="HFEmpCode" />
                                                    <asp:HiddenField runat="server" ID="HFEmpTypeID" />





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

                                        <div class="col-md-7">
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
                                                                            <asp:Label runat="server" ID="Label10">Previous Salary Step  </asp:Label>
                                                                            <asp:DropDownList ID="PreviouSalaryStepDropDownList" Enabled="False" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                        </div>



                                                                        <div class="form-group" runat="server">
                                                                            <asp:Label runat="server" ID="Label1">Previous Designation </asp:Label>
                                                                            <asp:DropDownList ID="PreviousDesignationDropDownList" Enabled="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                        </div>




                                                                        <div class="form-group">
                                                                            <asp:Label runat="server" ID="Label11">Previous Reporting Body  </asp:Label>
                                                                            <asp:DropDownList ID="PReportingBodyDropDownList" runat="server" AutoPostBack="True" Enabled="False" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <asp:GridView ID="presuperGridView" runat="server" AutoGenerateColumns="False"
                                                                                CssClass="table table-bordered text-center thead-dark table-hover table-striped" DataKeyNames="EmpInfoId">
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
                                                                        <asp:DropDownList ID="NewSalaryGradeDropDownList" OnTextChanged="NewSalaryGradeDropDownList_Changed" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm SelecttoME"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group" runat="server">
                                                                        <asp:Label runat="server" ID="Label8">New Salary Step </asp:Label>
                                                                        <span style="color: red">&nbsp;*</span>
                                                                        <asp:DropDownList ID="NstepDropDownList" runat="server" AutoPostBack="False" CssClass="form-control form-control-sm SelecttoME"></asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label6">New Designation </asp:Label>
                                                                        <span style="color: red">&nbsp;*</span>
                                                                        <asp:DropDownList ID="NewDesignationDropDownList" runat="server" CssClass="form-control form-control-sm SelecttoME"></asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label9">New Reporting Body </asp:Label>
                                                                        <span style="color: red">&nbsp;*</span> <asp:CheckBox runat="server" ID="chkIsB_ReportingBody" AutoPostBack="True" OnCheckedChanged="chkIsB_ReportingBody_OnCheckedChanged" CssClass="chkChoice" Text="Is Board Member"/>

                                                                           <asp:DropDownList   runat="server"   ID="ddlReportingBody" class="form-control form-control-sm" />
                                                                        <asp:TextBox ID="NewReportingBodyTextBox" Visible="False" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="NewReportingBodyTextBox_OnTextChanged"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                            ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="NewReportingBodyTextBox"
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
                                                                        
                                                                        <div class="row">
                                                                            <div class="col-md-9">
                                                                                
                                                                                  <asp:Label runat="server" ID="Label5">Directly Supervisor </asp:Label><asp:CheckBox runat="server" ID="chkIsB_DirectlySupervisor" AutoPostBack="True" OnCheckedChanged="chkIsB_DirectlySupervisor_OnCheckedChanged"  CssClass="chkChoice"  Text="Is Board Member"/>

                                                                                     <asp:DropDownList   runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDirectlySupervisor_OnSelectedIndexChanged"   ID="ddlDirectlySupervisor" class="form-control form-control-sm" />

                                                                        <asp:TextBox Visible="False" ID="directlySuperTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="directlySuperTextBox_OnTextChanged"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                                                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                            ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="directlySuperTextBox"
                                                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                                                        </cc1:AutoCompleteExtender>
                                                                            </div>
                                                                            <div class="col-md-2" style="margin-top: 13px;">
                                                                                   <asp:Button ID="Button1" Text="Add" CssClass="btn btn-sm btn-info" runat="server" OnClick="Button1_OnClick" />
                                                                            </div>
                                                                        </div>
                                                                      

                                                                        <asp:HiddenField ID="directlyEmpIdHiddenField" runat="server" />
                                                                        <asp:HiddenField ID="rptHiddenField" runat="server" />
                                                                     
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
                                                                            CssClass="table table-bordered text-center thead-dark table-hover table-striped" DataKeyNames="EmpInfoId,PrevEmpReportingBodyId">
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
                                                                         
                                                                        <asp:DropDownList ID="PromotionTypeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                    </div>
                                                                    
                                                                      <div class="form-group">
                                                                             
                                                                          <asp:CheckBox ID="Chkreappointment" CssClass="chkChoiceSigle" Text="Re-appointment" runat="server" />
                                                                        
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

                                    <div class="form-row">
                                        <div class="col-12 ">
                                            <asp:CheckBox ID="manualUpdateCheckBox" CssClass="chkChoiceDesignation" Text="  Manually Update to Employee Information"  runat="server" />
                                          
                                        </div>
                                    </div>
                                    <div class="form-row">
                                         <div class="col-md-6" runat="server">
                                        <div class="form-group">
                                            <label class="font-weight-bold">Comments &nbsp;</label><label style="font-size: 10px; color: gray; font-style: italic"> (*Applicable for Final Submit)</label>
                                            
                                         
                                            
                                            <asp:TextBox runat="server"   ID="txtComment" TextMode="MultiLine" Rows="2" class="form-control" />
                                        </div>
                                    </div>
                                    </div>
                                    <br />
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                            <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                            <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                            <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" Visible="False" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />

                                        </div>


                                    </div>

                                </div>


                            </div>

                        </div>
                    </div>
                </div>
                </div>
                    </div>
                
                
                             <asp:GridView ID="KeyResponGridView" runat="server" Visible="False" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="False">
                                                            <Columns>
                                                                                                                                 <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Particulars" runat="server" Text='<%#Eval("ParticularsName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Salary Breakdown">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_SalaryBreakUp" runat="server" Text='<%#Eval("SalaryBreakUp") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                           
                                                            </Columns>
                                                        </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


</asp:Content>

