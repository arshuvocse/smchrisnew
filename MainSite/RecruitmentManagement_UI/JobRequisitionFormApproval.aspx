<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="JobRequisitionFormApproval.aspx.cs" Inherits="RecruitmentManagement_UI_JobRequisitionFormApproval" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        /*AutoComplete flyout */
      
 
                  

                                
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




    <style>
           .radiostyle {
                                     height: auto;
                                 }

                                .radiostyle label {
                                    margin-left: 3px !important;
                                    margin-right: 10px !important;
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
                        
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Employee Requisition Form Approval</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <%--<asp:Button ID="ViewListButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="ViewListButton_OnClick" />--%>
                        <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>

                    <div class="page-heading__container float-right d-none d-sm-block">
                     
                        
                            <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="ViewListButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->
                <asp:HiddenField ID="empIdHiddenField" runat="server" />

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            
                            
                             <div class="form-row">
                                    <div class="col-md-4">
                                                       </div>
                                          
                                <div class="col-md-4">
                                    <div class="form-group">
                                    <span style="font-weight: bold"> Approval Status:</span> &nbsp; <span style="color: #a52a2a">*</span> &nbsp;
                                        <asp:RadioButtonList ID="actionRadioButtonList" runat="server"  CssClass="radiostyle" RepeatLayout="Flow"   RepeatDirection="Horizontal">
                                                    
                                            
                                                    
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                               
                                 
                                 </div>
                            
                            <div class="form-row">
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
                                   <div class="col-md-4">
                                        
                                                       </div>
                                
                                
                                
                                 <div class="col-md-4">
                                    <div class="form-group">
                                  <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                      <div class="ui-group-buttons">
 <asp:LinkButton ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="Button2_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" ><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>  
                                    <asp:LinkButton ID="Button1" Text="Submit" CssClass="btn btn-sm btn-success" Visible="False" OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"  runat="server" OnClick="Button1_OnClick" ><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>  
                                            <div class="or or-sm" runat="server"   id="orBTN"></div>
                                    <asp:LinkButton ID="Button2a"   runat="server" OnClick="Button2a_OnClick"  OnClientClick="return confirm('Are you sure you want to Reject ?')"  CssClass="btn btn-sm btn-danger"   ><i class="fa fa-times" aria-hidden="true"></i>
&nbsp; Reject </asp:LinkButton> 
                            </div>
</div>
                                     </div>
                                </div>
                             <div class="row">
                        <div class="col-md-12">
                                  <table class="table table-bordered table-striped">
                                      <tr>
        <td style="width: 30%;padding: 10px;">Company Name</td>
        <td><asp:Label runat="server" ID="lblcompanyDropDownList"></asp:Label></td>
        
      </tr>
                                      
                                      
                                      <tr>
        <td style="width: 30%;padding: 10px;">Financial Year</td>
        <td><asp:Label runat="server" ID="lblfinancialYearDropDownList"></asp:Label></td>
        
      </tr>
                                      
                                      
                                      
                                      <tr>
        <td style="width: 30%;padding: 10px;">Department</td>
     <td><asp:Label runat="server" ID="lbldeptDropDownList"></asp:Label></td>
        
      </tr>
                                      
                                      <tr>
        <td style="width: 30%;padding: 10px;">Is Management Approved</td>
     <td><asp:Label runat="server" ID="lblIsManagaa"></asp:Label></td>
        
      </tr>
                                      
                                      
                                      
                                      <tr>
        <td style="width: 30%;padding: 10px;">Is Budgeted</td>
        <td><asp:Label runat="server" ID="lblIsBudgeted"></asp:Label></td>
        
      </tr>
                                      
                                      
                                      
                                      <tr>
        <td style="width: 30%;padding: 10px;">Budgeted Position </td>
     <td><asp:Label runat="server" ID="lblBudgetPosition"></asp:Label></td>
        
      </tr>
                                      
                                      
                              
                                             
                                      
                                      <tr>
        <td style="width: 30%;padding: 10px;">Requisition Date</td>
        <td>  <asp:Label ID="reqDateTextBox"   runat="server"></asp:Label></td>
        
      </tr>
                                      
                                      <tr>
        <td style="width: 30%;padding: 10px;">Note</td>
        <td><asp:Label ID="NoteTextBox" runat="server"  ></asp:Label>  </td>
        
      </tr>
                                      </table>
                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company Name </label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>&nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="financialYearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True" OnTextChanged="financialYearDropDownList_OnTextChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Department </label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="deptDropDownList" AutoPostBack="True" CssClass="form-control form-control-sm" runat="server" OnSelectedIndexChanged="deptDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">

                                        <br />
                                        <asp:CheckBox ID="IsMangAppCheckBox" Text="Is Management Approved" AutoPostBack="True" runat="server" OnCheckedChanged="IsMangAppCheckBox_CheckedChanged" />

                                    </div>
                                </div>

                                <div class="col-md-1.2" runat="server" id="IsBudgetedDiv" visible="True">
                                    <div class="form-group">
                                        <label>Is Budgeted</label>
                                        <asp:RadioButtonList ID="isBudgetedCheckBox" RepeatDirection="Horizontal" runat="server" AutoPostBack="True" OnTextChanged="isBudgetedCheckBox_CheckedChanged">
                                            <asp:ListItem> &nbsp; Yes &nbsp; </asp:ListItem>
                                            <asp:ListItem> &nbsp; No &nbsp; </asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>
                                </div>




                                <div class="col-md-2" runat="server" visible="False">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="mainFinyearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" id="ShowBudgetInfo" visible="True" runat="server">
                                    <div class="form-group">
                                        <label>Budgeted Position </label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="BudgetCodeDropDownList" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="BudgetCodeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Requisition Date</label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                      
                                         
                                    </div>
                                </div>



                                <div class="col-md-2">
                                    <div class="form-group">
                                      
                                    </div>

                                    <div class="form-group">
                                        <asp:TextBox ID="ReqCodetextBox" runat="server" Visible="False" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <fieldset class="for-panel">
                                <legend>Position Description</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class="table table-bordered table-striped">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 30%; padding: 10px;">Job Title</td>
                                                    <td>
                                                        <asp:Label ID="jobTitleTextBox" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; padding: 10px;">Employee Type</td>
                                                    <td>
                                                        <asp:Label ID="lblEmpCategoryEx" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; padding: 10px;">Grade</td>
                                                    <td>
                                                        <asp:Label ID="lblgradeDropDownList" runat="server"></asp:Label></td>

                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; padding: 10px;">Total Vacancy</td>
                                                    <td>
                                                        <asp:Label ID="nosTextBox" runat="server"></asp:Label></td>

                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; padding: 10px;">Expected Date of joining</td>
                                                    <td>
                                                        <asp:Label ID="expDtJoinTextBox" runat="server"></asp:Label></td>

                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="col-2" runat="server" visible="False">
                                        <div class="form-group">
                                            <label></label>
                                            &nbsp;<label style="color: #a52a2a">*</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpCategoryEx" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpCategoryEx_OnSelectedIndexChanged" />
                                        </div>
                                    </div>
                                    <div class="col-md-2" runat="server" visible="False">
                                        <div class="form-group">
                                            <label>Grade </label>
                                            &nbsp;<label style="color: #a52a2a">*</label>
                                            <asp:DropDownList ID="gradeDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2" runat="server" visible="False">
                                        <div class="form-group">
                                            <label></label>
                                            &nbsp;<label style="color: #a52a2a">*</label>



                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label></label>
                                            <%--    &nbsp;<label style="color: #a52a2a">*</label>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-12">
                                        <label style="font-size: 14px">Employment Type</label>
                                        <hr />
                                        <div class="row">


                                            <div class="col-md-12">
                                                <table class="table table-bordered table-striped">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 30%; padding: 10px;">Employment Type</td>
                                                            <td>
                                                                <asp:Label ID="lbltypeOfPosRadioButtonList" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%; padding: 10px;">Project</td>
                                                            <td>
                                                                <asp:Label ID="lblprojectDropDownList" runat="server"></asp:Label></td>

                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%; padding: 10px;">Month</td>
                                                            <td>
                                                                <asp:Label ID="MonthTextBox" Enabled="False" runat="server"></asp:Label></td>

                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%; padding: 10px;">Fund Information</td>
                                                            <td>
                                                                <asp:Label ID="FundInfoTextBox" Enabled="False" runat="server"></asp:Label></td>

                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>

                                            <div class="col-md-2" runat="server" visible="False">
                                                <div class="form-group">
                                                    <label></label>
                                                    &nbsp;<label style="color: #a52a2a">*</label>
                                                    <asp:RadioButtonList ID="typeOfPosRadioButtonList" OnSelectedIndexChanged="typeOfPosRadioButtonList_OnSelectedIndexChanged" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <%--   <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                            <asp:ListItem><span style="font-size:12px">Permanent</span>&nbsp; </asp:ListItem>
                                                            <asp:ListItem> <span style="font-size:12px">Contractual</span> &nbsp;</asp:ListItem>
                                                            <asp:ListItem><span style="font-size:12px">Casual</span> &nbsp;</asp:ListItem>
                                                            <asp:ListItem><span style="font-size:12px">Other</span>&nbsp;</asp:ListItem>
                                                        </asp:RadioButtonList>--%>


                                            <div class="col-md-2" id="project" runat="server" visible="True" style="display: none">
                                                <div class="form-group">
                                                    <label>Project </label>

                                                    <asp:DropDownList ID="projectDropDownList" Enabled="False" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-2" id="MonthDiv" runat="server" visible="True" style="display: none">
                                                <div class="form-group">
                                                    <label>Month </label>



                                                </div>
                                            </div>
                                            <div class="col-md-2" id="FunDDiv" runat="server" visible="True" style="display: none">
                                                <div class="form-group">
                                                    <label></label>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-12">
                                        Supervisor
                                             <hr />

                                        <table class="table table-bordered table-striped">
                                            <tr>
                                                <td style="width: 30%; padding: 10px;">Reporting to</td>
                                                <td>
                                                    <asp:Label ID="reportToTextBox" runat="server"></asp:Label></td>

                                            </tr>


                                            <tr>
                                                <td style="width: 30%; padding: 10px;">Internal Contacts</td>
                                                <td>   <asp:Label ID="internalConTextBox" CssClass="form-control form-control-sm" runat="server"></asp:Label></td>

                                            </tr>



                                            <tr runat="server" Visible="False">
                                                <td style="width: 30%; padding: 10px;">Designation</td>
                                                <td>
                                                    <asp:Label ID="lblReportDesig" runat="server"></asp:Label></td>

                                            </tr>



                                            


                                            <tr>
                                                <td style="width: 30%; padding: 10px;">External Contacts</td>
                                                <td>
                                                    <asp:Label ID="externalConTextBox" runat="server"></asp:Label></td>

                                            </tr>


                                            <tr>
                                                <td style="width: 30%; padding: 10px;">Directly Supervice Info</td>
                                                <td>
                                                    <div style="max-height: 150px; overflow: scroll">
                                                     <asp:Label ID="lblDirectlySupervicesGridView" runat="server"></asp:Label>  

                                                        <asp:GridView ID="DirectlySupervicesGridView"      CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="DesignationId">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                                <asp:BoundField DataField="Designation" HeaderText="Directly Supervice" HtmlEncode="False" />
                                                                <asp:TemplateField HeaderText="No's">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="DSNoTextBox" ReadOnly="True" CssClass="form-control form-control-sm" Value='<%# Eval("Nos")%>' runat="server"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                            Enabled="True" TargetControlID="DSNoTextBox" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" runat="server" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="deleteImageButtonDirectlySupervices" runat="server" OnClick="deleteImageButtonDirectlySupervices_OnClick"
                                                                            ImageUrl="~/Assets/img/delete.png" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>

                                            </tr>
                                        </table>

                                    </div>
                                </div>
                                <div class="row" runat="server" visible="False">



                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Reporting to  </label>
                                            &nbsp; 
                                                    <asp:HiddenField runat="server" ID="HFreportTo" />

                                            <%--  <cc1:AutoCompleteExtender ID="AutoCompleteExtender21" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetCompanyWiseEmployeeInfo" ServicePath="~/WebService.asmx" TargetControlID="reportToTextBox"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>--%>
                                        </div>
                                    </div>

                                    <div class="col-md-3" runat="server" id="DesigRepDiv" visible="False">
                                        <div class="form-group">
                                            <label>Designation  </label>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label></label>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label></label>

                                        </div>
                                    </div>





                                </div>

                                <div class="row" runat="server" visible="False">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Directly Supervised </label>
                                            <asp:DropDownList ID="jobtitleDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1.5" style="margin-top: 19px;">
                                        <asp:Button ID="DirectlySupervicesButton" Text="Add To List" OnClick="DirectlySupervicesButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />
                                    </div>
                                </div>
                                <%--     <div style="max-height: 150px; overflow: scroll">--%>
                                <div class="row">
                                    <div class="col-5">
                                    </div>
                                </div>

                                <br />
                                <div class="row">

                                    <div class="col-md-12">
                                        Location
                                                <hr />

                                        <table class="table table-bordered table-striped">
                                            <tr>
                                                <td style="width: 30%; padding: 10px;">Office & Place</td>
                                                <td>
                                                    <asp:Label ID="txtOffice" runat="server"></asp:Label></td>

                                            </tr>
                                        </table>

                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label></label>


                                                    <asp:DropDownList ID="officeDropDownList" CssClass="form-control form-control-sm" runat="server" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="officeDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                    <asp:HiddenField runat="server" ID="officemainIdHiddendField" />
                                                </div>
                                            </div>
                                            <div class="col-md-1.5" style="margin-top: 19px;">
                                                <asp:Button ID="OfficeButton" Text="Add To List" Visible="False" OnClick="OfficeButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />
                                            </div>
                                            <div class="col-md-1.5" style="margin-top: 19px;">
                                                &nbsp;&nbsp;
                                                        <asp:Button ID="btnResetForOffice" Visible="False" OnClick="btnResetForOffice_Onclick" Text="Reset" CssClass="btn btn-warning btn-sm" runat="server" />
                                            </div>

                                            <div class="col-md-2" runat="server" visible="False">
                                                <div class="form-group">
                                                    <label>Place  </label>
                                                    <asp:TextBox ID="txtPlace" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    <asp:DropDownList ID="placeDropDownList" Visible="False" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row" runat="server" visible="False">
                                            <div class="col-5">
                                                <div style="max-height: 150px; overflow: scroll">

                                                     <asp:Label ID="lblOfficeGridView" runat="server"></asp:Label>
                                                    <asp:GridView ID="OfficeGridView" CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="SalaryLoationId,SalaryLocationMainId">
                                                        <Columns>
                                                            <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                            <asp:BoundField DataField="SalaryLocation" HeaderText="Office" HtmlEncode="False" />

                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="deleteImageButtonDirectlySupervices" runat="server" OnClick="deleteImageOfficeGridView_OnClick"
                                                                        ImageUrl="~/Assets/img/delete.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                </div>

                            </fieldset>

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
                                                    <asp:TextBox ID="placeOfPostingTextBox" Enabled="False" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="False" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetJobLocation" ServicePath="~/WebService.asmx" TargetControlID="placeOfPostingTextBox"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>

                                                </div>
                                            </div>


                                         

                                           
                                         
                                        </div>
                                         
                                         
                                      
                                    
                                <fieldset class="for-panel">
                        <legend>Justification & Job Summary </legend>

                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-bordered table-striped">
                                    <tr>
                                        <td style="width: 30%; padding: 10px;">Justification</td>
                                        <td>
                                            <asp:Label ID="JustificationTextBox" runat="server"></asp:Label></td>

                                    </tr>


                                    <tr>
                                        <td style="width: 30%; padding: 10px;">Job Summary</td>
                                        <td>
                                            <asp:Label ID="descriptionTextBox" runat="server"></asp:Label></td>

                                    </tr>
                                </table>

                                <table class="table table-bordered table-striped">

                                    <tr>
                                        <td style="width: 30%; padding: 10px;">Employee Information</td>
                                        <td>
                                            <asp:Label ID="lbljstRadioButtonList" runat="server"></asp:Label></td>



                                        <td>
                                          <div visible="False" ID="RepplaceView" runat="server" >
                                                <table class="table table-bordered table-striped" >
                                                
                                                
                                                  <tr>
                                                    <td style="width: 30%; padding: 10px;">Date Of Seperation</td>
                                                    <td>
                                                       <asp:Label ID="DateOfSeperationTextBox"   runat="server"></asp:Label>

                                                </tr>

                                                <tr>
                                                    <td style="width: 30%; padding: 10px;">Employee Code</td>
                                                    <td>
                                                        <asp:Label ID="codeLabel" runat="server"></asp:Label></td>

                                                </tr>

                                                <tr>
                                                    <td style="width: 30%; padding: 10px;">Employee Name</td>
                                                    <td>
                                                        <asp:Label ID="nameLabel" runat="server"></asp:Label></td>

                                                </tr>



                                                <tr>
                                                    <td style="width: 30%; padding: 10px;">Department</td>
                                                    <td>
                                                        <asp:Label ID="deptLabel" runat="server"></asp:Label></td>

                                                </tr>



                                                
                                                        <asp:Label ID="desigLabel" Visible="False" runat="server"></asp:Label> 

                                                
                                                    
                                                       <tr>
                                                    <td style="width: 30%; padding: 10px;">Salary Grade</td>
                                                    <td>
                                                       <asp:Label ID="salgradLabel" runat="server"></asp:Label>

                                                </tr>
                                                    
                                                    
                                                    
                                                    
                                                         <tr>
                                                    <td style="width: 30%; padding: 10px;">Gross Salary</td>
                                                    <td>
                                                       <asp:Label ID="lblGrossSalary" runat="server"></asp:Label>

                                                </tr>
                                                
                                                   <tr>
                                        <td style="width: 30%; padding: 10px;">Employeement Type</td>
                                        <td>
                                            <asp:Label ID="wingLabel" runat="server"></asp:Label>
                                        </td>

                                    </tr>
                                            </table>
                                          </div>
                                            
                                              <div visible="False" ID="NewView" runat="server" >
                                                <table class="table table-bordered table-striped" >
                                                
                                                    <tr>
                                                        <td style="width: 30%; padding: 10px;">Salary Range From</td>
                                                        <td>
                                                            <asp:Label ID="lblSFrom" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 30%; padding: 10px;">Salary Range To</td>
                                                        <td>
                                                            <asp:Label ID="lblSTo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    </table>
                                                  </div>
                                        </td>


                                    </tr>

                                     
                                     
                                



                                 


                                 
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4" runat="server" visible="False">

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Justification </label>
                                            &nbsp;<label style="color: #a52a2a">*</label>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label></label>
                                            &nbsp;<label style="color: #a52a2a">*</label>

                                        </div>
                                    </div>


                                </div>

                            </div>


                            <div class="col-md-4">

                                <div class="row" runat="server" visible="False">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Employee Information</label>
                                            <hr />
                                            <asp:RadioButtonList ID="jstRadioButtonList" RepeatDirection="Horizontal" runat="server" AutoPostBack="True" OnTextChanged="jstRadioButtonList_OnTextChanged">
                                                <asp:ListItem> &nbsp; New &nbsp; </asp:ListItem>
                                                <asp:ListItem> &nbsp; Replacement &nbsp; </asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>
                                </div>

                                <div runat="server" visible="False" id="DivProjecView">

                                   
                                </div>

                                <div runat="server" visible="False" id="detail">

                                    <div class="row" runat="server" Visible="False">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblEmpName" runat="server" Visible="False">
                                                            <Label> Search Employee Name </Label>
                                                </asp:Label>
                                                <div class="form-group">
                                                    <%--<asp:DropDownList ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" ></asp:DropDownList>--%>
                                                    <asp:TextBox ID="EmployeeNameTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="EmployeeNameTextBox_OnTextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetCompanyWiseEmployeeInfoActiveInactiveAll" ServicePath="~/WebService.asmx" TargetControlID="EmployeeNameTextBox"
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
                                                    <asp:Label ID="lblDatSep" runat="server" Visible="False">  </asp:Label>
                                                </label>
                                                
                                                
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>

                            <div class="col-md-4" style="margin-top: 50px; font-style: italic;" id="Showme" runat="server" visible="False">
                                <div class="row">

                                    <div class="col-md-12">
                                        <div class="form-group">

                                           



                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                 

                                    <div class="col-md-12" runat="server" visible="False">
                                        <div class="form-group">

                                            <%--<label style="color: grey">Division </label>--%>

                                            <asp:Label ID="divLabel" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                                <div class="row" runat="server">

                                    <div class="col-md-12">
                                        <div class="form-group">
                                        </div>
                                    </div>

                                    <div class="col-md-2" runat="server" visible="False">
                                        <div class="form-group">

                                            <%-- <label style="color: grey">Section </label>--%>

                                            <asp:Label ID="secLabel" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-2" runat="server" visible="False">
                                        <div class="form-group">

                                            <%--  <label style="color: grey">Sub Section </label>--%>

                                            <asp:Label ID="subsecLabel" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>

                    </fieldset>


                                   
                            


                              <div class="row">
                        <div class="col-md-12">
                            <fieldset class="for-panel">
                                <legend>Key Responsibilities</legend>

                                <div class="row">


                                    <div class="col-md-12">
                                        <div class="row" runat="server" visible="False">

                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label>JD Information</label>
                                                    <asp:TextBox ID="jdTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="textButton" Text="Add Free Text" OnClick="textButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <div class="vl"></div>
                                                <style>
                                                    .vl {
                                                        border-left: 3px solid green;
                                                        height: 200px;
                                                        margin-left: 30px;
                                                        vertical-align: middle;
                                                    }
                                                </style>
                                            </div>
                                            <div class="col-md-5">
                                                JD Search From Other Employee
                                            <hr />
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Division </label>
                                                                    <asp:DropDownList ID="dsnDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="dsnDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-8">
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

                                                                    <div style="max-height: 150px; overflow: scroll">
                                                                        <asp:CheckBox runat="server" Text="Select/Unselect All" AutoPostBack="True" ID="SelectAll" OnCheckedChanged="SelectAll_Checked" />
                                                                        <asp:CheckBoxList ID="jdCheckBoxList" runat="server"></asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Button ID="addImageButton" Text="Add Emp. JD" OnClick="addImageButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />
                                                                </div>
                                                            </div>



                                                        </div>

                                                        <div class="row">
                                                            <div class="form-group">
                                                                <div class="col-md-12">
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
                                                </div>
                                            </div>





                                        </div>





                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="form-group">

                                                     <asp:Label ID="lblKeyResponGridView" runat="server"></asp:Label> 

                                                <asp:GridView ID="KeyResponGridView" runat="server" Width="100%" CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   AutoGenerateColumns="False">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                        <asp:BoundField DataField="JobReqKeyResName" HeaderText="Key Responsibilite" HtmlEncode="False" />

                                                        <asp:TemplateField HeaderText="Edit" runat="server" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="addeduImageButton" runat="server" OnClick="editImageButton_OnClick"
                                                                    ImageUrl="~/Assets/img/rsz_edit.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delete" runat="server" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteImageButton_OnClick"
                                                                    ImageUrl="~/Assets/img/delete.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                    </div>
                                    <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                       CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   Font-Size="11px" Visible="False">
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
                                                    <div class="col-md-3" runat="server" visible="False">
                                                        <label>Education  </label>
                                                        &nbsp;<label style="color: #a52a2a">*</label>
                                                        <asp:DropDownList ID="EducationRequirementDropDownList" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-md-3" runat="server" visible="False">

                                                        <label>Major</label>
                                                        <asp:TextBox ID="txtMajor" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>

                                                    </div>

                                                    <div class="col-md-1.5" style="margin-top: 18px" runat="server" visible="False">
                                                        <div class="form-group">
                                                            <asp:Button ID="EducationRequirementImageButton" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" Text="Add To List" OnClick="EducationRequirementImageButton_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <table class="table table-bordered table-striped">

                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 30%; padding: 10px;">Professional Certification</td>
                                                                    <td>
                                                                        <asp:Label ID="profCertificationTextBox" runat="server"></asp:Label></td>

                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 30%; padding: 10px;">Relevant Experience</td>
                                                                    <td>
                                                                                                                                          <asp:Label ID="experienceTextBox" runat="server"></asp:Label></td>

                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 30%; padding: 10px;">Skill / Specialization</td>
                                                                    <td>
                                                                                                                                             <asp:Label ID="skillTextBox" runat="server"></asp:Label></td>

                                                                </tr>

                                                                <tr>
                                                                    <td style="width: 30%; padding: 10px;">Age</td>
                                                                    <td>
                                                                                                                                               <asp:Label ID="ageTextBox" runat="server"></asp:Label></td>

                                                                </tr>

                                                                <tr>
                                                                    <td style="width: 30%; padding: 10px;">Computer Skill</td>
                                                                    <td>
                                                                                                                                               <asp:Label ID="cmpSkillsTextBox" runat="server"></asp:Label></td>

                                                                </tr>


                                                                <tr runat="server" visible="False">
                                                                    <td style="width: 30%; padding: 10px;">Other Requirements</td>
                                                                    <td>
                                                                      <asp:Label ID="othersTextBox" runat="server"></asp:Label></td>

                                                                </tr>

                                                                <tr>
                                                                    <td style="width: 30%; padding: 10px;">Education Info
                                                                    </td>
                                                                    <td>

                                                                        <div style="max-height: 150px; overflow: scroll">
                                                     <asp:Label ID="lblEducationRequirementGridView" runat="server"></asp:Label>  

                                                                            <asp:GridView ID="EducationRequirementGridView" CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ERID">
                                                                                <Columns>
                                                                                    <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                                                    <asp:BoundField DataField="EducationRequirements" HeaderText="Education" HtmlEncode="False" />
                                                                                    <asp:BoundField DataField="Major" HeaderText="Major" HtmlEncode="False" />
                                                                                    <asp:TemplateField HeaderText="Delete" runat="server" Visible="False">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="deleteImageButtonEducationRequirement" runat="server" OnClick="deleteImageButtonEducationRequirement_OnClick"
                                                                                                ImageUrl="~/Assets/img/delete.png" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>

                                                                    </td>

                                                                </tr>

                                                                <tr>
                                                                    <td style="width: 30%; padding: 10px;">Other Requirements</td>
                                                                    <td>
                                                                        <div style="max-height: 150px; overflow: scroll">
                                                                            <asp:GridView ID="educationGridView" runat="server" AutoGenerateColumns="False"
                                                                               CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   Font-Size="11px" Visible="False">
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

                                                                                    <asp:TemplateField HeaderText="Add" runat="server" Visible="False">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="addeduImageButton" runat="server"
                                                                                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData" OnClick="addeduImageButton_OnClick"
                                                                                                ImageUrl="~/Assets/img/add.png" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Delete" runat="server" Visible="False">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="deleduImageButton" runat="server" OnClick="deleduImageButton_OnClick"
                                                                                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                                                                                ImageUrl="~/Assets/img/delete.png" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                            </asp:GridView>

                                                     <asp:Label ID="lblOtherRequirementsGridView" runat="server"></asp:Label>  

  <asp:GridView ID="OtherRequirementsGridView" runat="server" Width="100%" CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   AutoGenerateColumns="False">
                                                                                <Columns>
                                                                                    <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                                                    <asp:BoundField DataField="OtherRequirementsName" HeaderText="Other Requirements" HtmlEncode="False" />

                                                                                    <asp:TemplateField HeaderText="Edit" runat="server" Visible="False">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="addeduImageButton" runat="server" OnClick="editOtherRequirementsGridViewButton_OnClick"
                                                                                                ImageUrl="~/Assets/img/rsz_edit.png" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Delete" runat="server" Visible="False">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteOtherRequirementsGridViewButton_OnClick"
                                                                                                ImageUrl="~/Assets/img/delete.png" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </td>

                                                                </tr>
                                                            </tbody>
                                                        </table>


                                                    </div>


                                                </div>

                                            </div>


                                        </div>
                                        <div class="form-group">
                                        </div>

                                    </div>
                                </div>


                                <div class="row" style="padding: 10px;">
                                </div>

                                <div class="row">
                                </div>

                                <div class="row" runat="server" visible="False">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 18px">
                                            <div class="form-group">
                                                <asp:Button ID="Button2" Text="Add To List" OnClick="OtherRequirementsAddButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />

                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                    </div>

                            <div class="row">


                        <div class="col-md-12">


                            <%-- <fieldset class="for-panel">
                                        <legend>Experiences</legend>
                                      

                                    </fieldset>--%>




                            <fieldset class="for-panel col-md-5">
                                <legend>Prefered Way To Circulate The Vacancy</legend>
                                <div class="form-group">

                                    <asp:CheckBoxList ID="CheckBoxList1"  runat="server">
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
                                      <table class="table table-bordered table-striped">
                                           <tr>
        <td style="width: 30%;padding: 10px;">Remarks</td>
        <td> <asp:Label ID="RemarksTextBox" runat="server"></asp:Label></td>
        
      </tr>
                                          </table>
                                
                               
                            </div>


                               <br/>
                                       <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;"> Approval Status List</h2>
                            <hr/>

                             <div class="form-row">
                                
                                              <div class="col-md-12">
                                                <div style="max-height:200px; overflow: scroll">
                                                <asp:GridView ID="AppLogCommentGridView" CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   runat="server" Width="100%" AutoGenerateColumns="False">
                                                    <Columns>
 <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PreEmp" HeaderText="Initiator" HtmlEncode="False" />
                                                        <asp:BoundField DataField="Comments" HeaderText="Comments" HtmlEncode="False" />

                                                        <asp:BoundField DataField="ForEmp" HeaderText="Approval Person" HtmlEncode="False" />
                                                        <%--<asp:BoundField DataField="Version" HeaderText="Version" HtmlEncode="False" />--%>
                                                        <asp:BoundField DataField="ActionStatus" HeaderText="Action Status" HtmlEncode="False" />
                                                        

                                                        <asp:BoundField DataField="ApproveBy" HeaderText="Approved By" HtmlEncode="False" />
                                                        <asp:BoundField DataField="ApproveDate" HeaderText="Approved Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                     
                                                       
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                            </div>



                            <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                            <div class="ui-group-buttons">
                                <asp:Button ID="Button3" Text=" Save " CssClass="btn btn-sm btn-info" OnClientClick="return confirm('Are you sure you want to Save ?')" Visible="False" runat="server" OnClick="Button2_OnClick" />
                                <div class="or or-sm" runat="server" visible="False" id="Div1"></div>
                                <asp:Button ID="btnSubmit" Text=" Submit " CssClass="btn btn-sm btn-success" OnClientClick="return confirm('Are you sure you want to Submit ?')" Visible="False" runat="server" OnClick="btnSubmit_OnClick" />

                            </div>
                            <div class="ui-group-buttons">
                                <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" OnClientClick="return confirm('Are you sure you want to Update ?')" runat="server" OnClick="editButton_OnClick" />
                                <div class="or or-sm" runat="server" visible="False" id="orUp"></div>

                                <asp:Button ID="btnUpdateforSubmit" Text="Submit" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="btnUpdateforSubmit_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" />
                            </div>

                            <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />

                            <%--<asp:Button ID="Button2" Text="Save" CssClass="btn btn-info btn-sm" runat="server" OnClick="Button2_OnClick" />--%>
                            <asp:Button ID="Button4" Text="Cancel" CssClass="btn btn-warning btn-sm" runat="server" Visible="False" />


                        </div>
                    </div>
                            
                             

                                   
                                      </div>
                                    <asp:HiddenField ID="entryempinfoIdHiddenField" runat="server" />
                                    <asp:HiddenField ID="actionstatusHiddenField" runat="server" />
                            
                                   
                                </div>

                            </div>
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

