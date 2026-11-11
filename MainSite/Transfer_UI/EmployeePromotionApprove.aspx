<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeePromotionApprove.aspx.cs" Inherits="Transfer_UI_EmployeePromotionApprove" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style type="text/css">
        /*AutoComplete flyout */
       .table {
           font-size: 13px !important;
       }


         .chkChoiceHeader label {
            padding-left: 2px;
            padding-right: 10px;
            font-size: 13px;
        }
    </style>
    <style>
.checkmark {
  position: absolute;
  top: 0;
  left: 0;
  height: 25px;
  width: 25px;
  background-color: #eee;
}

/* On mouse-over, add a grey background color */
.container:hover input ~ .checkmark {
  background-color: #ccc;
}

/* When the checkbox is checked, add a blue background */
.container input:checked ~ .checkmark {
  background-color: #2196F3;
}

/* Create the checkmark/indicator (hidden when not checked) */
.checkmark:after {
  content: "";
  position: absolute;
  display: none;
}

/* Show the checkmark when checked */
.container input:checked ~ .checkmark:after {
  display: block;
}

/* Style the checkmark/indicator */
.container .checkmark:after {
  left: 9px;
  top: 5px;
  width: 5px;
  height: 10px;
  border: solid white;
  border-width: 0 3px 3px 0;
  -webkit-transform: rotate(45deg);
  -ms-transform: rotate(45deg);
  transform: rotate(45deg);
}
        .chkChoice label {
            padding-left: 2px;
            padding-right: 5px;
        }

        fieldset.for-panel {
            background-color: #fcfcfc;
            border: 1px solid #999;
            padding: 15px 10px;
            background-color: white;
            margin-bottom: 12px;
        }
        #cpFormBody_AppLogCommentGridView  td {
            border: 1px solid #ddd;
            padding: 8px;
        }
        #cpFormBody_loadGridView  td{
             border: 1px solid #ddd;
            padding: 8px;
        }

            #cpFormBody_presuperGridView td {
            border: 1px solid #ddd;
            padding: 8px;
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
                        <div class="icon"><img src="../Report_Pages/app.png"  width="20px" /></div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Employee Promotion Approval </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="&#8920; Back to List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            
                                  
                            <div class="form-row">
                                
                                 <div class="col-md-4">
                               </div>
                           <div class="col-md-1.5">
                               <label style="font-weight: bold">Approval Status:&nbsp;<span style="color: #a52a2a">*</span></label>
                               </div>

                                
                                  <div class="col-md-4" >
                                    <div class="form-group" >
                                        <asp:RadioButtonList CssClass="chkChoice"  ID="actionRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                    
                                            
                                                    
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                </div>
                                    
                                    <div class="form-row">
                                          <div class="col-md-4">
                               </div>
                                          <div class="col-md-4">
                                    <div class="form-group">
                                       <label  style="font-weight: bold">Comments</label>
                                        <asp:TextBox runat="server" ID="commentsTextBox" CssClass="form-control" ReadOnly="False"  TextMode="MultiLine"></asp:TextBox>
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
                                        <asp:LinkButton ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="Button2_OnClick"  OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"> <i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>  
                                    <asp:LinkButton ID="LinkButton1" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="Button10_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"  ><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>  
                                            <div class="or or-sm" runat="server"   id="orBTN"></div>
                                    <asp:LinkButton ID="Button2a" Text="Cancel"  CssClass="btn btn-sm btn-danger"  OnClientClick="return confirm('Are you sure you want to Reject ?')" runat="server" OnClick="Button2a_OnClick" ><i class="fa fa-times" aria-hidden="true"></i>
&nbsp; Reject </asp:LinkButton> 
                                     <%--<asp:Button ID="Button2" Text="Back to List" CssClass="btn btn-sm text-info" runat="server" OnClick="detailsViewButton_OnClick" />--%>
                                    <asp:HiddenField ID="entryempinfoIdHiddenField" runat="server" />
                                    <asp:HiddenField ID="actionstatusHiddenField" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                     
                   

                            
                            <div class="row">
                                 <div class="col-md-12">
                                    <table class="table table-bordered table-striped">
                                        <tr>
                                            <td style="width: 20%; padding: 10px;">Company Name</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblCompany"></asp:Label></td>

                                        </tr>



                                        <tr>
                                            <td style="width: 20%; padding: 10px;">Financial Year</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblFinancialYearDesc"></asp:Label></td>

                                        </tr>
                                        
                                        
                                             <tr>
                                            <td style="width: 20%; padding: 10px;">Effective Date</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblEffDate"></asp:Label></td>

                                        </tr>
                                        
                                        
                                        
                                        
                                        <tr>
                                            <td style="width: 20%; padding: 10px;">Employee Information</td>
                                            <td>

                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td style="width: 30%; padding: 10px;">Employee ID</td>
                                                        <td> <asp:Label ID="lblEmployeeCode" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Employee Name</td>
                                                        <td> <asp:Label ID="lblEmp" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    
                                                       <tr>
                                                        <td style="width: 30%; padding: 10px;">Date Of Joining</td>
                                                        <td> <asp:Label ID="lblJdate" runat="server" Text=""></asp:Label></td>

                                                    </tr>

                                                    
                                                   
                                                    
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Office</td>
                                                        <td> <asp:Label ID="lblOffice" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Place</td>
                                                        <td> <asp:Label ID="lblPlace" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Division</td>
                                                        <td> <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Wing</td>
                                                        <td> <asp:Label ID="lblWing" runat="server" Text=""></asp:Label></td>

                                                    </tr>

                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Department</td>
                                                        <td> <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Section</td>
                                                        <td> <asp:Label ID="lblSection" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Sub-Section</td>
                                                        <td> <asp:Label ID="lblSubSection" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                     
                                                       
                                                         
                                                         
                                                    
                                                    
                                                    
                                                    
                                                  
                                           
                                                    
                                                      
                                                </table>

 

                                        </tr>
                                        
                                        
                                            <tr>
                                            <td style="width: 20%; padding: 10px;">Promotion To
</td>
                                            <td>

                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td style="width: 30%; padding: 10px;">Previous Salary Grade</td>
                                                        <td> <asp:Label ID="lblPSalaryGrade" runat="server" Text=""></asp:Label></td>
                                                         <td style="width: 30%; padding: 10px;">New Salary Grade</td>
                                                        <td> <asp:Label ID="lblNSalaryGrade" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                    
                                                        <tr>
                                                        <td style="width: 30%; padding: 10px;">Previous Salary Step</td>
                                                        <td> <asp:Label ID="lblPSalaryStep" runat="server" Text=""></asp:Label></td>
                                                         <td style="width: 30%; padding: 10px;">New Salary Step</td>
                                                        <td> <asp:Label ID="lblNSalaryStep" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                        <tr>
                                                        <td style="width: 30%; padding: 10px;">Previous Designation</td>
                                                        <td> <asp:Label ID="lblPDesignation" runat="server" Text=""></asp:Label></td>
                                                         <td style="width: 30%; padding: 10px;">New Designation</td>
                                                        <td> <asp:Label ID="lblNDesignation" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                        <tr>
                                                        <td style="width: 30%; padding: 10px;">Previous Reporting Body</td>
                                                        <td> <asp:Label ID="lblPReportingBody" runat="server" Text=""></asp:Label></td>
                                                         <td style="width: 30%; padding: 10px;">New  Reporting Body</td>
                                                        <td> <asp:Label ID="lblNReportingBody" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                    
                                                      <tr>
                                                       
                                                        <td colspan='2'>
                                                           
                                                            <asp:GridView ID="presuperGridView" runat="server" AutoGenerateColumns="False"
                                                                                CssClass="table table-bordered text-center thead-dark table-hover table-striped" DataKeyNames="EmpInfoId">
                                                                                <Columns>

  <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                                                    <asp:BoundField DataField="EmpName" HeaderText="Previous Directly Supervisor Employee List " />
                                                                                    </Columns>
                                                                </asp:GridView> 
                                                        </td>
                                                        
                                                        <td  colspan='2' >
                                                            
                                                                   <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="table table-bordered text-center thead-dark table-hover table-striped" DataKeyNames="EmpInfoId,PrevEmpReportingBodyId">
                                                                            <Columns>

  <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                                                <asp:BoundField DataField="EmpName" HeaderText="New Directly Supervisor Employee List" />


                                                                                 

                                                                            </Columns>
                                                                        </asp:GridView>
                                                             
                                                        </td>
                                                    </tr>
                                                    
                                                    
                                                      
                                                    
                                                    
                                                       <tr>
                                                        <td style="width: 30%; padding: 10px;"> </td>
                                                        <td>  </td>
                                                         <td style="width: 30%; padding: 10px;">Promotion Type</td>
                                                        <td> <asp:Label ID="lblPromotionType" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                      
                                                       <tr>
                                                        <td style="width: 30%; padding: 10px;"> </td>
                                                        <td>  </td>
                                                         <td style="width: 30%; padding: 10px;">Is Reappointment</td>
                                                        <td> <asp:Label ID="lblReappointment" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                         <tr>
                                                        <td style="width: 30%; padding: 10px;"> </td>
                                                        <td>  </td>
                                                         <td style="width: 30%; padding: 10px;">Remarks</td>
                                                        <td> <asp:Label ID="lblRemarks" runat="server" Text=""></asp:Label></td>
                                                    </tr>

                                                    </table>
                                                </td>


                                                </tr>
                                        

                                        </table>


    <div class="form-row">
                              
                                              <div class="col-md-12">
                                                    <fieldset class="for-panel">
                                                            <legend>Approval Status List</legend>
                                                 <div style="overflow: scroll">
                                                <asp:GridView ID="AppLogCommentGridView" CssClass="table table-bordered text-center thead-dark" runat="server" Width="100%" AutoGenerateColumns="False"  >
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
                                            </div>
                                                        </fieldset>
                                        </div>
                            </div>
                            </div>

                                <div runat="server" Visible="False">
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
                                                <span style="color: red">&nbsp;*</span>
                                                <asp:DropDownList ID="FinancialYearDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="FinancialYearDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Label4">Effective Date </asp:Label>
                                                <span style="color: red">&nbsp;*</span>
                                                <asp:HiddenField ID="areaHiddenField" runat="server" />
                                                <asp:HiddenField ID="areaCodeHiddenField" runat="server" />
                                                <asp:TextBox ID="ActiveDateTextBox" AutoCompleteType="Disabled" OnTextChanged="ActiveDateTextBox_Changed" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

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
                                        <div class="col-md-3">
                                            <asp:HiddenField ID="EmployeePromotionEntryIdHiddenField" runat="server" />
                                            <div class="form-group">
                                                <label>Search Employee: </label>
                                                <asp:TextBox ID="SearchEmployeeNameTextBoxTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
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
                                                                        
                                                                    </div>


                                                                    <div class="form-group">

                                                                        <label>Employee Name: </label>
                                                                        

                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label>Joining Date: </label>
                                                                        
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
                                                                        
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label>Place: </label>
                                                                        
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">

                                                                    <div class="form-group">

                                                                        <label>Division: </label>
                                                                        

                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>Wing: </label>
                                                                        
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label>Department: </label>
                                                                        
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label>Section: </label>
                                                                        
                                                                    </div>


                                                                    <div class="form-group">
                                                                        <label>Sub-Section: </label>
                                                                        
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



                                                                        <div class="form-group">
                                                                            <asp:Label runat="server" ID="Label1">Previous Designation </asp:Label>
                                                                            <asp:DropDownList ID="PreviousDesignationDropDownList" Enabled="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                        </div>




                                                                        <div class="form-group">
                                                                            <asp:Label runat="server" ID="Label11">Previous Reporting Body  </asp:Label>
                                                                            <asp:DropDownList ID="PReportingBodyDropDownList" runat="server" AutoPostBack="True" Enabled="False" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            


                                                                                    <%-- <asp:TemplateField HeaderText="Delete"  >
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteImageButton_OnClick"
                                                                                    ImageUrl="~/Assets/img/delete.png" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                               
                                                                        </div>


                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">

                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label7">New Salary Grade </asp:Label>
                                                                        <span style="color: red">&nbsp;*</span>
                                                                        <asp:DropDownList ID="NewSalaryGradeDropDownList" OnTextChanged="NewSalaryGradeDropDownList_Changed" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group" runat="server">
                                                                        <asp:Label runat="server" ID="Label8">New Salary Step </asp:Label>
                                                                        <span style="color: red">&nbsp;*</span>
                                                                        <asp:DropDownList ID="NstepDropDownList" runat="server" AutoPostBack="False" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label6">New Designation </asp:Label>
                                                                        <span style="color: red">&nbsp;*</span>
                                                                        <asp:DropDownList ID="NewDesignationDropDownList" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label9">New Reporting Body </asp:Label>
                                                                        <span style="color: red">&nbsp;*</span>


                                                                        <asp:TextBox ID="NewReportingBodyTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="NewReportingBodyTextBox_OnTextChanged"></asp:TextBox>
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
                                                                                
                                                                                  <asp:Label runat="server" ID="Label5">Directly Supervisor </asp:Label>



                                                                        <asp:TextBox ID="directlySuperTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="directlySuperTextBox_OnTextChanged"></asp:TextBox>
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
                                                                 
                                                                    </div>



                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label90">Promotion Type </asp:Label>
                                                                         
                                                                        <asp:DropDownList ID="PromotionTypeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                    </div>
                                                                    
                                                                      <div class="form-group">
                                                                             
                                                                          <asp:CheckBox ID="Chkreappointment" CssClass="chkChoiceSigle" Text="Reappointment" runat="server" />
                                                                        
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

                                    <div class="form-row" runat="server" Visible="False">
                                        <div class="col-12 ">
                                            <asp:CheckBox ID="manualUpdateCheckBox" runat="server" />
                                            <span>&nbsp; Manually Update to Employee Information</span>
                                        </div>
                                    </div>

                              
                                      <div class="form-row"   runat="server" Visible="False">
                                <div class="col-12 ">
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                    <span>&nbsp; Manually Update to Employee Information</span>
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

