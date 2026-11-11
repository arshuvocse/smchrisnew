<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeJobLeftApprove.aspx.cs" Inherits="ExitManagement_UI_EmployeeJobLeftEntry" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style type="text/css">
        /*AutoComplete flyout */
          .chkChoiceHeader label {
            padding-left: 2px;
            padding-right: 10px;
            font-size: 13px;
        }


               #cpFormBody_AppLogCommentGridView td {
            border: 1px solid #ddd!important;
            padding: 8px;
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
                        <div class="icon"></div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"><img src="../Report_Pages/app.png" width="20px"  />Employee Separation Approval</h1>
                    </div>

                    <%-- <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>

                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="ListViewButton" Visible="True" Text="&#8920; Back to List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            
                            <div class="row">
                                
                                     <div class="col-md-4">
                               </div>
                                
                                  <div class="col-md-1.5">
                               <label style="font-weight: bold">Approval Status:&nbsp;<span style="color: #a52a2a">*</span></label>
                               </div>
                                 <div class="col-md-4">
                                    <div class="form-group">
                                        
                                        <asp:RadioButtonList ID="actionRadioButtonList" runat="server" CssClass="chkChoiceHeader"  RepeatDirection="Horizontal">
                                                    
                                            
                                                    
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                           
                            </div>
                            
                            <div class="row">
                                       <div class="col-md-4">
                               </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Comments</label>
                                        <asp:TextBox runat="server" ID="commentsTextBox" CssClass="form-control" ReadOnly="False"  TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                      
                            </div>
                            
                            
                            <div class="row">
                                  <div class="col-md-5">
                               </div>

                                <div class="col-md-4">
                                                                <div class="form-group">
                                  <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                      <div class="ui-group-buttons">
                                        
                                <asp:LinkButton ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="Button2_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"> <i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>  
                                    <asp:LinkButton ID="Button1" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="Button1_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"  ><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>  
                                            <div class="or or-sm" runat="server"   id="orBTN"></div>
                                    <asp:LinkButton ID="Button2a" Text="Cancel"  CssClass="btn btn-sm btn-danger"  runat="server" OnClick="Button2a_OnClick" OnClientClick="return confirm('Are you sure you want to Reject ?')"  ><i class="fa fa-times" aria-hidden="true"></i>
&nbsp; Reject </asp:LinkButton> 
 
                                <asp:HiddenField ID="entryempinfoIdHiddenField" runat="server" />
                                    <asp:HiddenField ID="actionstatusHiddenField" runat="server" />
                                <%--<asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />--%>
                                <%--<asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_Click" />--%>
                                <%--<asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" Visible="False" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />--%>
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
                                            <td style="width: 20%; padding: 10px;">Job Left Type</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblJobLeftType"></asp:Label></td>

                                        </tr>


                                        
                                        
                                        
                                        
                                        <tr>
                                            <td style="width: 20%; padding: 10px;">Employee Information</td>
                                            <td>

                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td style="width: 20%; padding: 10px;">Employee ID</td>
                                                        <td>        <asp:Label ID="lblEmployeeCode" runat="server" Text=""></asp:Label></td>
                                                         
                                                    </tr>


                                                             
                                                     <tr>
                                                        <td style="width: 20%; padding: 10px;">Employee Name</td>
                                                        <td>
                                                            
                                                             <asp:Label ID="lblEmp" runat="server" Text=""></asp:Label>
                                                              
                                                        </td>
                                                          
                                                    </tr>
                                                    
                                                       <tr>
                                                        <td style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                        <td>
                                                            <asp:Label ID="lblJdate" runat="server" Text=""></asp:Label>  
                                                        </td>
                                                           
                                                    </tr>

                                                    
                                                        <tr>
                                                        <td style="width: 20%; padding: 10px;">Designation</td>
                                                        <td>        <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label></td>
                                                              
                                                    </tr>
                                                    
                                                    <tr>
                                                           <td style="width: 20%; padding: 10px;">Department</td>
                                                        <td>   <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                    
                                                          <tr>
                                                        <td style="width: 20%; padding: 10px;">Employement Type</td>
                                                        <td>
                                                            
                                                            <asp:Label ID="lblSubSection" runat="server" Text=""></asp:Label>   
                                                        </td>
                                                            
                                                    </tr>
                                                    
                                                    </table>
                                                
                                                </tr>



                                        
                                          <tr>

                                              
                                              
                                              
                                   <tr>
                                            <td style="width: 20%; padding: 10px;">Benefit Information
</td>
                                            <td>
                                                
                                                  <div class="form-group" style="height: 200px; overflow: scroll">
                                      
                                        <asp:GridView ID="itemGridView" runat="server" AutoGenerateColumns="False" Height="60px"
                                            DataKeyNames="BenefitMasterId" CssClass="table table-bordered table-striped datatable" CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="isValueCheckBox" runat="server" AutoPostBack="True" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Benefit Name">
                                                    <ItemTemplate>


                                                        <asp:TextBox ID="itemTextBox" runat="server" CssClass="form-control form-control-sm" ReadOnly="True" Text='<%# Eval("Benefit")%>'></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="rcvQtyTextBox" runat="server" class="form-control form-control-sm" Text='<%# Eval("Amount")%>' Width="100%"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderqty" runat="server"
                                                            Enabled="True" TargetControlID="rcvQtyTextBox" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>








                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>

                                                        <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server">
                                                            <asp:ListItem>Addition</asp:ListItem>
                                                            <asp:ListItem>Deduction</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                                        </asp:GridView>
                                    </div>
                                            
                                            </td>

                                        </tr>
                                        
                                        
                                          <tr>
                                            <td style="width: 20%; padding: 10px;">Submission Date</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblSubmissionDate"></asp:Label></td>

                                        </tr>
                                        
                                          <tr>
                                            <td style="width: 20%; padding: 10px;">Separation Date</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblSeparationDate"></asp:Label></td>

                                        </tr>
                                        
                                        
                                          <tr>
                                            <td style="width: 20%; padding: 10px;">Duration Days</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblDurationDays"></asp:Label></td>

                                        </tr>
                                        
                                        
                                          <tr>
                                            <td style="width: 20%; padding: 10px;">Reason</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblReason"></asp:Label></td>

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
                                        <asp:DropDownList ID="companyDropDownList" Enabled="False" CssClass="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Job Left Type: </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="JobLeftTypeDropDownList"  Enabled="False"  OnSelectedIndexChanged="JobLeftTypeDropDownList_OnSelectedIndexChanged" AutoPostBack="True"  CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                             <asp:CheckBox ID="chkIsSubmissionDate" Visible="False" Text="Is Submission Date" CssClass="checkbox margin-right" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3" runat="server" Visible="False">

                                    <asp:HiddenField ID="EmployeeJobLeftIdHiddenField" runat="server" />
                                    <asp:HiddenField ID="typeHiddenField" runat="server" />
                                    <asp:HiddenField ID="isprobHiddenField" runat="server" />
                                    <asp:HiddenField ID="cateHiddenField" runat="server" />
                                    <asp:HiddenField ID="gradeHiddenField" runat="server" />
                                    <div class="form-group">
                                        <label>Search Employee </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <%--<asp:DropDownList ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" ></asp:DropDownList>--%>
                                        <asp:TextBox ID="SearchEmployeeNameTextBoxTextBox" AutoPostBack="True" runat="server" Enabled="False" CssClass="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
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



                            </div>



                            <fieldset class="for-panel">




                                <div class="row">

                                    <div class="col-md-3">

                                        <div class="form-group">
                                            <label>Employee Code: </label>
                                           
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Designation: </label>
                                         
                                        </div>
                                    </div>
                                    <div class="col-md-3" runat="server" visible="False">

                                        <div class="form-group">

                                            <label>Company Name: </label>
                                            <asp:Label ID="lblComName" runat="server" Text=""></asp:Label>

                                        </div>
                                    </div>



                                    <div class="col-md-3" runat="server">

                                        <div class="form-group" runat="server">
                                            <label>Employement Type: </label>
                                            
                                        </div>
                                    </div>
                                    <div class="col-md-2" runat="server" visible="False">


                                        <div class="form-group" runat="server" visible="False">
                                            <label>Salary Grade: </label>
                                            <asp:Label ID="lblSalaryGrade" runat="server" Text=""></asp:Label>
                                        </div>

                                    </div>

                                    <div class="col-md-2" runat="server" visible="False">
                                        <div class="form-group">

                                            <label>Division: </label>
                                            <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label>

                                        </div>
                                    </div>

                                    <div class="col-md-2" runat="server" visible="False">
                                        <div class="form-group">
                                            <label>Wing: </label>
                                            <asp:Label ID="lblWing" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>




                                    <div class="form-group" runat="server" visible="False">
                                        <label>Section: </label>
                                        <asp:Label ID="lblSection" runat="server" Text=""></asp:Label>
                                    </div>



                                </div>
                                <div class="row">
                                    <div class="col-md-3" runat="server">
                                        <div class="form-group">

                                            <label>Employee Name: </label>
                                        

                                        </div>
                                    </div>
                                    <div class="col-md-3" runat="server">
                                        <div class="form-group">
                                            <label>Department: </label>
                                          
                                        </div>
                                    </div>
                                    <div class="col-md-2" runat="server">

                                        <div class="form-group">
                                            <label>Joining Date: </label>
                                            
                                        </div>

                                    </div>

                                </div>



                            </fieldset>

                            <div class="row">


                                <div class="col-md-6">


                                    <label>Benefit Information</label>
                                  
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Submission Date: </label>
                                        

                                        <asp:TextBox ID="SubmissionDateTextBox" AutoCompleteType="Disabled" ReadOnly="True" AutoPostBack="True" runat="server" OnTextChanged="SubmissionDateTextBox_TextChanged" CausesValidation="true"
                                            class="form-control form-control-sm"></asp:TextBox>
                                        
                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Separation Date: </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:TextBox ID="JobLeftDateTextBox" AutoCompleteType="Disabled"  ReadOnly="True"  runat="server" AutoPostBack="True" OnTextChanged="JobLeftDateTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                     
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Duration Days: </label>

                                        <asp:TextBox ID="DurationDateTextBox" AutoCompleteType="Disabled" Enabled="False" runat="server" CausesValidation="true" CssClass="form-control form-control-sm"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Reason: </label>
                                        <asp:TextBox ID="ReasonTextBox" Rows="2" TextMode="MultiLine" runat="server" ReadOnly="True" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">


                                        <asp:CheckBoxList ID="ClearanceFormCheckBoxList" runat="server" RepeatDirection="Horizontal">

                                            <asp:ListItem> Exit Interview </asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-12 ">
                                    <asp:CheckBox ID="manualUpdateCheckBox" runat="server" /> <span>&nbsp; Manually Update to Employee Information</span>
                                </div>
                            </div>
                            
                            </div>

                            <br />
                            
                            <div class="form-row">
                                        <div class="col-md-12">
                                            
                                                    <fieldset class="for-panel">
                                                            <legend>Approval Status List</legend>   
                                               <div style="height: 150px; overflow: scroll">
                                                <asp:GridView ID="AppLogCommentGridView" CssClass="table table-bordered text-center thead-dark" runat="server" Width="100%" AutoGenerateColumns="False"  >
                                                    <Columns>
                                                      
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


</fieldset>                                        </div>
                            </div>



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


