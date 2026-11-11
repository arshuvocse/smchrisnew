<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="IncrementEntryApproval.aspx.cs" Inherits="Increment_UI_IncrementEntryApproval" %>

<%--<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>--%>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">

        

        <style>
              .chkChoiceHeader label {
            padding-left: 2px;
            padding-right: 10px;
            font-size: 13px;
        }
            
                #cpFormBody_AppLogCommentGridView td {
            border: 1px solid #ddd!important;
            padding: 8px;
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
        </style>
        <asp:UpdatePanel ID="upFormBody" runat="server">




            <ContentTemplate>



                <%--                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>




                <div class="page-heading">
                    <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"> <img src="../Report_Pages/app.png" width="20px"  />  Increment Approval </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="&#8920; Back To List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                </div>
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
                                        
                                     
                                        <asp:RadioButtonList CssClass="chkChoiceHeader" ID="actionRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                    
                                            
                                                    
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
                                    <asp:LinkButton ID="Button1" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="Button1_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"  ><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

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
                            


                            <div class="form-row">
                                
                                

                                

                                <div class="col-md-12">
                                    <table class="table table-bordered table-striped">
                                        <tr>
                                            <td style="width: 30%; padding: 10px;">Company Name</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblCompany"></asp:Label></td>

                                        </tr>



                                        <tr>
                                            <td style="width: 30%; padding: 10px;">Financial Year</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblFinancialYearDesc"></asp:Label></td>

                                        </tr>



                                        <tr>
                                            <td style="width: 30%; padding: 10px;">Increment Type </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblIncreType"></asp:Label></td>

                                        </tr>



                                        <tr>
                                            <td style="width: 30%; padding: 10px;">Effective Date</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblEffDate"></asp:Label></td>

                                        </tr>



                                        <tr>
                                            <td style="width: 30%; padding: 10px;">Employee Information</td>
                                            <td>

                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td style="width: 30%; padding: 10px;">Employee ID</td>
                                                        <td> <asp:Label runat="server" ID="lblEmpId"></asp:Label></td>

                                                    </tr>
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Employee Name</td>
                                                        <td> <asp:Label runat="server" ID="lblEmployeeName"></asp:Label></td>

                                                    </tr>
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Designation</td>
                                                        <td> <asp:Label runat="server" ID="lblDesignation"></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Department</td>
                                                        <td> <asp:Label runat="server" ID="lblDepartment"></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Date Of Joining</td>
                                                        <td> <asp:Label runat="server" ID="lblDOJ"></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Salary Grade</td>
                                                        <td> <asp:Label runat="server" ID="lblSalaryGrade"></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                    
                                                     <tr>
                                                        <td style="width: 30%; padding: 10px;">Current Salary Step</td>
                                                        <td> <asp:Label runat="server" ID="lblCurrentSalaryStep"></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                    
                                                      <tr>
                                                        <td style="width: 30%; padding: 10px;">Incremental Step</td>
                                                        <td> <asp:Label runat="server" ID="lblIncrementalStep"></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                    
                                                      <tr>
                                                        <td style="width: 30%; padding: 10px;">Feed Salary (%)</td>
                                                        <td> <asp:Label runat="server" ID="lblFeedSalary"></asp:Label></td>

                                                    </tr>
                                                </table>


                                            </td>

                                        </tr>



                                    
                                    </table>
                                </div>

                                <asp:HiddenField runat="server" ID="hdpk" />
                               

                            </div>
                            
                            
                            <div class="row" runat="server" Visible="False">
                                 <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label><span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True" ID="ddlFinYear" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Increment Type</label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlIncrementType" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label runat="server" id="Label4">Effective Date </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:HiddenField ID="areaHiddenField" runat="server" />
                                        <asp:HiddenField ID="areaCodeHiddenField" runat="server" />
                                        <asp:TextBox ID="EffectiveDateTextBox" AutoCompleteType="Disabled" OnTextChanged="EffectiveDateTextBox_Changed" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="EffectiveDateTextBox" CssClass="MyCalendar"
                                            TargetControlID="EffectiveDateTextBox" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-row"  runat="server" Visible="False">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Searching Criteria</legend>
                                        <div class="form-row">
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Employee Category</label>
                                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Division</label>
                                                    <asp:DropDownList runat="server" ID="ddlDivision" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>


                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Department</label>
                                                    <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>


                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Employee Type</label>
                                                    <label style="color: #a52a2a">*</label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpType" class="form-control form-control-sm" />

                                                </div>
                                            </div>


                                            <div class="col-md-3">

                                                <div class="form-group">
                                                    <label>Search Employee: </label>
                                                    <asp:TextBox ID="SearchEmployeeNameTextBoxTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxTextBox"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </asp:AutoCompleteExtender>


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
                                                    <asp:HiddenField ID="EmployeeIdHiddenField" runat="server" />
                                                </div>

                                            </div>

                                            <div class="col-1 " style="margin-top: 18px;">
                                                <div class="form-group">
                                                    <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_OnClick" CssClass="btn btn-sm btn-success" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>





                            <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-sm table-bordered  text-center thead-dark" DataKeyNames="EmpInfoId,  DivisionId, DivisionWId, DepartmentId,  SectionId, SubSectionId, DesignationId,SalaryLoationId, JobLocationId , EmpTypeId
                                   ">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" CssClass="form-control-sm" AutoPostBack="True" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" />
                                    <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />

                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="designationDropDownList" Enabled="False" runat="server" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="departmentDropDownList" Enabled="False" runat="server" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="DateOfJoin" HeaderText="Date Of Joining" DataFormatString="{0:dd-MMM-yyyy}" />
                                    <asp:BoundField DataField="ServiceLength" HeaderText="Service Length(Dayes)" runat="server" Visible="False" />

                                    <asp:TemplateField HeaderText="Salary Grade">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="salaryGradeDropDownList" Enabled="False" runat="server" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Current Salary Step">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="salaryStepDropDownList" Enabled="False" runat="server" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Incremental Step">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="incrementalStepDropDownList" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True"
                                                OnTextChanged="incrementalStepDropDownList_OnTextChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Feed Salary (%)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="feedSalaryTextBox" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderconvRate" runat="server"
                                                Enabled="True" TargetControlID="feedSalaryTextBox" FilterType="Custom" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>


                            <div class="form-row"   runat="server" Visible="False">
                                <div class="col-12 ">
                                    <asp:CheckBox ID="manualUpdateCheckBox" runat="server" />
                                    <span>&nbsp; Manually Update to Employee Information</span>
                                </div>
                            </div>
                            
                            <div class="form-row">
                                  
                                              <div class="col-md-12">
                                                   <fieldset class="for-panel">
                                                            <legend>Approval Status List</legend>
                                                 <div style=" overflow: scroll">
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
                </div>




            </ContentTemplate>
        </asp:UpdatePanel>
    </div>



</asp:Content>

