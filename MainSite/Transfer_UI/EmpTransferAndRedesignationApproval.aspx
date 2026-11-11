<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpTransferAndRedesignationApproval.aspx.cs" Inherits="Transfer_UI_EmpTransferAndRedesignationApproval" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

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
             .chkChoiceHeader label {
            padding-left: 2px;
            padding-right: 10px;
            font-size: 13px;
        }


               #cpFormBody_AppLogCommentGridView td {
            border: 1px solid #ddd!important;
            padding: 8px;
        }


                #cpFormBody_presuperGridView td {
            border: 1px solid #ddd!important;
            padding: 8px;
        }


                 #cpFormBody_loadGridView td {
            border: 1px solid #ddd!important;
            padding: 8px;
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
                       
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Employee Transfer Approval Information </h1>
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
                            <div class="col-md-12">
                                
                                <div class="row">
                                      <div class="col-md-4">
                               </div>
                           <div class="col-md-1.5">
                               <label style="font-weight: bold">Approval Status:&nbsp;<span style="color: #a52a2a">*</span></label>
                               </div>
                                      <div class="col-md-4">
                                    <div class="form-group">
                                        
                                        <asp:RadioButtonList ID="actionRadioButtonList" CssClass="chkChoiceHeader"  runat="server" RepeatDirection="Horizontal">
                                                    
                                            
                                                    
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
                                                        <td>  <asp:Label ID="txtEmpId" runat="server"  ></asp:Label></td>

                                                    </tr>
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Employee Name</td>
                                                        <td>
                                                            
                                                             <asp:Label ID="lblEmp" runat="server" Text=""></asp:Label>
                                                              <asp:Label ID="EmployeeNameTextBox" Visible="False" runat="server" ></asp:Label>
                                                        </td>

                                                    </tr>
                                                    
                                                       <tr>
                                                        <td style="width: 30%; padding: 10px;">Date Of Joining</td>
                                                        <td> <asp:Label ID="JoiningDateTextBox" runat="server"  ></asp:Label></td>

                                                    </tr>

                                                    
                                                        <tr>
                                                        <td style="width: 30%; padding: 10px;">Designation</td>
                                                        <td>  <asp:Label ID="DesignationTextBox" runat="server"  ></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                    

                                                    
                                                   
                                                    </table>
                                                
                                                </tr>


                                          <tr>
                                            <td style="width: 20%; padding: 10px;font-size: 15px;font-weight: bold"><asp:Label ID="lblTransferRadioButtonList" runat="server" Text=""></asp:Label>
</td>
                                            <td>

                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td style="width:20%; padding: 10px;" colspan="2">
                                                            
                                                      <p style="font-size: 15px;font-weight: bold">Existing</p>      
                                                            
                                                            <table class="table table-bordered table-striped">
                                                                <tr>
<td style="width:20%; padding: 10px;" >Company Name</td>
                                                                    <td><asp:Label ID="lblEComName" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Office</td>
                                                                    <td><asp:Label ID="lblEOffice" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Place</td>
                                                                    <td><asp:Label ID="lblEPlace" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Division</td>
                                                                    <td><asp:Label ID="lblEDivision" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Wing</td>
                                                                    <td><asp:Label ID="lblEWing" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Department</td>
                                                                    <td><asp:Label ID="lblEDepartment" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Section</td>
                                                                    <td><asp:Label ID="lblESection" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Sub-Section</td>
                                                                    <td><asp:Label ID="lblESubSection" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                 <tr>
<td style="width:20%; padding: 10px;" >Old Reporting Body</td>
                                                                    <td><asp:Label ID="lblEOldReportingBody" runat="server" Text=""></asp:Label></td>
                                                                </tr>



                                                                 <tr>
<td  colspan="2" >   <asp:GridView ID="presuperGridView" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="table table-bordered  text-center thead-dark" DataKeyNames="EmpInfoId">
                                                                    <Columns>
                                                                        
  <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                               

                                                                        <asp:BoundField DataField="EmpName" HeaderText="Previous Directly Supervised Employee List	" />


                                                                  
                                                                    </Columns>
                                                                </asp:GridView></td>
                                                                   
                                                                </tr>
                                                            </table>
</td>
                                                       


 <td style="width:20%; padding: 10px;" colspan="2">
     <p style="font-size: 15px;font-weight: bold">New</p>
     
      <table class="table table-bordered table-striped">
                                                                <tr>
<td style="width:20%; padding: 10px;" >Company Name</td>
                                                                    <td><asp:Label ID="lblNComName" runat="server" Text=""></asp:Label></td>
                                                                </tr>
          
             <tr>
<td style="width:20%; padding: 10px;" >Office</td>
                                                                    <td><asp:Label ID="lblNOffice" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Place</td>
                                                                    <td><asp:Label ID="lblNPlace" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Division</td>
                                                                    <td><asp:Label ID="lblNDivision" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Wing</td>
                                                                    <td><asp:Label ID="lblNWing" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Department</td>
                                                                    <td><asp:Label ID="lblNDepartment" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Section</td>
                                                                    <td><asp:Label ID="lblNSection" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                
                                                                <tr>
<td style="width:20%; padding: 10px;" >Sub-Section</td>
                                                                    <td><asp:Label ID="lblNSubSection" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                
                                                                 <tr>
<td style="width:20%; padding: 10px;" >Old Reporting Body</td>
                                                                    <td><asp:Label ID="lblNReportingBody" runat="server" Text=""></asp:Label></td>
                                                                </tr>



                                                                 <tr>
<td  colspan="2" >
    <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="table table-bordered  text-center thead-dark" DataKeyNames="EmpInfoId,PrevEmpReportingBodyId">
                                                                    <Columns>
 <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                                        <asp:BoundField DataField="EmpName" HeaderText="New Directly Supervised Employee List" />


                                                                    

                                                                    </Columns>
                                                                </asp:GridView>
    
    
       
</td>
                                                            </table>
</td>
                                                                                                               </tr>

</table>

                                       
                                            </td>
                                                          </tr>

                                        
                                        
                                             <tr>
<td style="width:20%; padding: 10px;" >Remarks</td>
                                                                    <td><asp:Label ID="lblRemarks" runat="server" Text=""></asp:Label></td>
                                                                </tr>

                                        </table>
                                                </div>


</div>
                                <div runat="server" Visible="False">
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
                                            <label>Effective Date </label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox runat="server" OnTextChanged="EfectiveDate_TextChanged" AutoPostBack="True" CausesValidation="true" AutoCompleteType="Disabled" class="form-control form-control-sm" ID="EfectiveDate"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EfectiveDate" CssClass="MyCalendar"
                                                TargetControlID="EfectiveDate" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">



                                   
                                    <div class="col-md-3">

                                        <asp:HiddenField ID="EmpTransferAndRedesignationIdHiddenField" runat="server" />

                                        <div class="form-group">
                                            <label>Search Employee </label>
                                            <span style="color: red">&nbsp;*</span>
                                            <%--<asp:DropDownList ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" ></asp:DropDownList>--%>
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
                                        <%-- <div class="form-group">
                                        <label>Search Employee: </label>
                                        <asp:DropDownList ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="EmployeeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                    </div>--%>
                                    </div>

                                    <div class="col-md-2">

                                        <div class="form-group">
                                            <label>Employee ID: </label>
                                           
                                            <asp:HiddenField runat="server" ID="HiddenField1" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">

                                        <div class="form-group">
                                            <label>Employee Name: </label>
                                           
                                            <asp:HiddenField runat="server" ID="EmpTypeId" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">

                                        <div class="form-group">
                                            <label>Designation: </label>
                                           
                                        </div>
                                    </div>
                                    <div class="col-md-2">

                                        <div class="form-group">
                                            <label>Joining Date: </label>
                                            
                                        </div>
                                    </div>
                                    <div class="col-md-2" runat="server" visible="False">

                                        <div class="form-group" runat="server" visible="False">
                                            <label>Salary Grade: </label>
                                            <asp:TextBox ID="SalaryGradeTextBox" runat="server" ReadOnly="True" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>




                                </div>





                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <asp:RadioButtonList ID="TransferRadioButtonList" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="TransferRadioButtonList_SelectedIndexChanged">
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

                                                                <asp:DropDownList ID="NewSalaryLocationDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="NewSalaryLocationDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Place: </label>

                                                                <asp:DropDownList ID="NewJobLocationDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Division: </label>

                                                                <asp:DropDownList ID="NewDivisionDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="NewDivisionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>



                                                            <div class="form-group" runat="server" id="wing">
                                                                <label>Wing: </label>
                                                                <asp:DropDownList ID="NewWingDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="NewWingDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>

                                                            <div class="form-group" runat="server" id="dept">
                                                                <label>Department: </label>

                                                                <asp:DropDownList ID="NewDepartmentDropDownList1" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="NewDepartmentDropDownList1_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group" runat="server" id="sec">
                                                                <label>Section: </label>
                                                                <asp:DropDownList ID="NewSectionDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="NewSectionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group" runat="server" id="subsec">
                                                                <label>Sub-Section: </label>
                                                                <asp:DropDownList ID="NewSubSectionDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="NewSubSectionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </div>

                                                            <div class="form-group">
                                                                <label>New Reporting Body </label>
                                                                <span style="color: red">&nbsp;*</span>


                                                                <asp:TextBox ID="NewEmpBodyTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="NewEmpBodyTextBox_OnTextChanged"></asp:TextBox>
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



                                                                <asp:TextBox ID="directlySuperTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="directlySuperTextBox_OnTextChanged"></asp:TextBox>
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
                                                                
                                                            </div>



                                                        </div>

                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>

                                </asp:Panel>

                                <div class="form-row" runat="server" Visible="False">
                                    <div class="col-12 ">
                                        <asp:CheckBox ID="manualUpdateCheckBox" runat="server" />
                                        <span>&nbsp; Manually Update to Employee Information</span>
                                    </div>
                                </div>
                                
                                </div>
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
</asp:TemplateField>          <asp:BoundField DataField="PreEmp" HeaderText="Approval Person" HtmlEncode="False" />
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
                                    
                            <br />

                                <br />

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

