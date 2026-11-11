<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ClearenceFormEL.aspx.cs" Inherits="Survey_ClearenceFormEL" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        /*AutoComplete flyout */
       
    </style>
    
        <style>
          div#cpFormBody_ddlDepartment_chosen {
    width: 300px!important;
}
           .Label_Title {
               background-color: #C7C7C7;
               width: 100%;
               text-align: center;
               margin: 0px;
               padding: 5px;
               text-align: center;
               color: #000;
               margin-right: 5%;
               font-weight: bold;
               font-size: 13px;
           }
           .sadasd {
               font-size: 18px;
              color: 
           }
          fieldset.for-panel {
              background-color: #fcfcfc;
              border: 1px solid #999;
              padding: 15px 10px;
              background-color: white;
              margin-bottom: 12px;
          }

          .buttonFinish {
              display: none !important;
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

       
          .chkChoice label {
              padding-left: 4px;
              padding-right: 4px;
          }
          .modal-dialog2 {
              max-width: 90% !important;
              margin-left: 14%;
              margin-top: 1.75rem;
          }
          div#cpFormBody_ddlEmp_chosen {
              width: 100%!important
          }
          .pending-issues-details {
              display: none;
          }
          .office-item-status label {
              font-weight: 600;
              margin-left: 5px;
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
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:HiddenField runat="server" ID="hfExitMasterId" />
                <asp:HiddenField runat="server" ID="hfExitDetailId" />
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Clearance Form</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                       <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                               <asp:Button ID="detailsViewButton" Text="Back to List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            
                            
                            
                            
                                <div class="row">
                                <style>
                                    .tblTHColorChang{
                                        background-color: #EDF2F5!important;
                                        font-weight: bold;
                                        font-size: 13px;
                                    }
  .SelectchkChoice label {
            padding-left: 6px;
            font-weight: bold;
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
table.AddToListCssTable tbody td {
    font-size: 13px!important;
    padding: 10px!important;
}
  .chkChoiceStep label {
            padding-left: 5px;
            padding-right: 5px;
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
.table > tbody > tr > th {
    font-weight: bold !important;
    font-style: italic!important;
    text-align: left!important;
    align-content: left;
}


                                </style>
                                     <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                        <td> <asp:Label runat="server" ID="lblEmpId"></asp:Label></td>

                                                        
                                                         <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                        <td>  <asp:Label ID="ReportingLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                        <td> <asp:Label runat="server" ID="lblEmployeeName"></asp:Label></td>

                                                         
                                                              
                                                         <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Department</td>
                                                        <td>  <asp:Label ID="deptNameLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Designation</td>
                                                        <td>   <asp:Label ID="desigNameLabel" runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Office</td>
                                                        <td>  <asp:Label ID="LocationLabel"   runat="server"></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                    
                                                    
                                                     
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                        <td>     <asp:Label ID="joiningDateLabel"  runat="server"></asp:Label></td>
  <td style="width: 20%; padding: 10px;" class="tblTHColorChang" >Place</td>
                                                        <td> <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                 
                                                    
                                                    
                                                    </table>
                            </div>

                            <%--<legend>Search By</legend>--%>
                            <div class="form-row" runat="server" visible="False">
                                <div class="col-3">
                                    <div class="form-group ">
                                        <label class="control-label">Company</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                 
                                    </div>
                                </div>

                                <div class="col-4">
                                    <div class="form-group ">
                                        <label class="control-label">Employee Name</label>
                                        <br />
                                        <asp:TextBox ID="txt_EmpName" runat="server" AutoPostBack="True" Enabled="False" CssClass="form-control form-control-sm" OnTextChanged="txt_EmpName_OnTextChanged"></asp:TextBox>
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



                            <div class="form-row" runat="server" Visible="False">

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Code</label>
                                        <asp:Label runat="server" ID="empCode" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hdfjobLeft" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Name</label>
                                        <br />
                                        <asp:Label runat="server" ID="empName" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField" runat="server" />

                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Date of Joining</label>
                                        <asp:Label runat="server" ID="dtJoining" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Division</label>
                                        <asp:Label runat="server" ID="ddlDivision" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfDivision" runat="server" />

                                    </div>
                                </div>
                            </div>

                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Designation</label>
                                        <asp:Label runat="server" ID="ddlDesignation" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfDesignation" runat="server" />
                                         <asp:HiddenField ID="hfSalaryLoationId" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3" style="display: none">
                                    <div class="form-group">
                                        <label>Slary Grade</label>
                                        <asp:Label runat="server" ID="ddlSalaryGrade" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfSalaryGrade" runat="server" />
                                    </div>
                                </div>

                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Description </label>
                                        <asp:TextBox runat="server" ID="descriptionTextbox" TextMode="MultiLine" Rows="2" class="form-control" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">

                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Resource Information </legend>
                                        
                                        <asp:HiddenField runat="server" ID="hfSetInfo"/> 
                                        <asp:HiddenField runat="server" ID="hfIsAutoForwarded" Value="0" />
                                        <asp:HiddenField runat="server" ID="hfDoneAndForwardedEmpIds" Value="" />
                                        <asp:HiddenField runat="server" ID="hfConfirmedReforwardEmpId" Value="" />
                                        <div   runat="server" id="forward">
                                            <div class="row">
                                                <div class="col-md-6"></div>
                                              <div class="col-md-2" >
                                                  <asp:CheckBox runat="server" ID="chkIsForword" AutoPostBack="True" OnCheckedChanged="chkIsForword_OnCheckedChanged" CssClass="SelectchkChoice" Text="Assign to Other"/>
                                              </div>
                                            <div class="col-md-4">
                                                 <div class="form-group">
                                 
                                                 <asp:DropDownList   runat="server"   ID="ddlEmpInfoList" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpInfoList_SelectedIndexChanged" CssClass="form-control form-control-sm cls-SelectJq" />
                                                     </div>
                                            </div>
                                            </div>
                                            
                                                 <div class="row" runat="server" Visible="False" id="divRemarks">
                                                <div class="col-md-6"></div>
                                              <div class="col-md-2" >
                                                  </div> 
                                                     <div class="col-md-4">
                                                 <div class="form-group">
                                                     <label>Remarks for Forward</label>
                                         <asp:TextBox runat="server"  CssClass="form-control" ID="txtForwardRemarks" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                                     </div>
                                                         </div>
                                        </div>
                                              </div>
                                        
                                           <div class="row" runat="server" Visible="False" id="divShowRemarks">
                                                <div class="col-md-6"></div>
                                              <div class="col-md-2" >
                                                  </div> 
                                                     <div class="col-md-4">
                                                 <div class="form-group">
                                                     <label>Remarks for Forward</label>
                                         <asp:TextBox runat="server"  CssClass="form-control" ID="lblRemarks"   TextMode="MultiLine" Rows="2"></asp:TextBox>
                                                     </div>
                                                         </div>
                                        </div>

                                        <div class="form-group office-item-status">
                                            <div>
                                                <asp:RadioButton ID="chkNoPendingIssues" runat="server" ClientIDMode="Static"
                                                    GroupName="OfficeItemStatus" CssClass="office-item-checkbox" Text="No pending issues" />
                                            </div>
                                            <div>
                                                <asp:RadioButton ID="chkPendingIssues" runat="server" ClientIDMode="Static"
                                                    GroupName="OfficeItemStatus" CssClass="office-item-checkbox" Text="Pending issues" />
                                            </div>
                                        </div>

                                        <asp:GridView ID="itemsDetailGridView" runat="server" CssClass="table table-bordered table-striped datatable" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="itemsDetailGridView_RowDataBound">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                          <%--      <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <span class="pending-issues-details">Please state below, if any Office Item belongs to the employee</span>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="OtherConsumptionTextBox" runat="server" TextMode="MultiLine" Rows="3" Width="100%" Text='<%#Eval("Otherconsumption") %>'
                                                            CssClass="form-control pending-issues-details"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remarks" Visible="False" runat="server">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="remarksTextBox" runat="server" Width="100%"
                                                            Text='<%#Eval("Remarks") %>' CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Actions" Visible="False" runat="server">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="itemsDetailaddImageButton" runat="server" CommandName="AddData" ImageUrl="~/Assets/Actions-list-add-icon.png" OnClick="additemImageButton_Click" />
                                                        <asp:ImageButton ID="itemsDetaildeleteImageButton" runat="server" CommandName="DeleteData" ImageUrl="~/Assets/delete-icon.png" OnClick="deleteitemLButton_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#CCD7E3" Font-Bold="True" HorizontalAlign="Left" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </fieldset>
                                </div>

                            </div>
                            
                            
                            <div class="form-row" runat="server" >
                                <div class="col-md-3" runat="server" Visible="False" id="showReccomend">
                                    <div class="form-group">
                                        <label>Future Employeement Recomendation:</label>
                                        <asp:RadioButtonList ID="rbRecommend" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="False">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                
                                 <div class="col-6">
                                     
                                        <fieldset class="for-panel">
                                <legend>Document </legend>
                                          <asp:GridView Width="100%" ShowHeader="True" ID="gv_DocumentUpload" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Document">
                                                    <ItemTemplate>
                                                          <asp:HyperLink ID="HLDocumentLink"   Target="_blank"  runat="server" NavigateUrl='<%# Eval("DocumentLink") %>'  Text='Download' >
        </asp:HyperLink>
                                                        
                                                <%--        <a   Target="_blank"    href="<%# Eval("DocumentLink")%>">Download</a>--%>
                                                        <asp:Label ID="lbl_DocumentLink" Visible="False" runat="server" Text='<%#Eval("DocumentLink")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfFileName" Value='<%#Eval("FileName")%>' />
                                                        <asp:HiddenField runat="server" ID="hfDocumentLink" Value='<%#Eval("DocumentLink")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Summary Note	">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DocumentNote" runat="server" Text='<%#Eval("DocumentNote") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             

                                            <%--    <asp:TemplateField HeaderText="Remove">
                                                    <ItemTemplate>
                                                      <asp:LinkButton runat="server" ID="btnDocRemove" OnClick="btnDocRemove_OnClick"   CssClass="btn btn-sm btn-danger"><ass class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                            </fieldset>
                                     </div>
                                </div>
                            
                            
                            
                      <style>
                            table {
                                font-family: arial, sans-serif;
                                border-collapse: collapse;
                                width: 100%;
                            }

                            td, th {
                                border: 1px solid #ccc;
                                text-align: center;
                                padding: 8px;
                            }


                        </style>

                        
                        <div class="row" runat="server" Visible="False" ID="Emp_Clea">
                            
                            <table style="width: 100%" class="ssss">
  <thead>
  
  <tr>
      <th colspan='8'>
          
          <div class="row" style="padding-left: 6px">
              <h3 style="color: black">Please Tick Capable Box</h3>
          </div>

          <div class="row" style="padding-left: 7px">
                     
              <asp:CheckBoxList id="checkboxlist1" 
                                CellPadding="5"
                                CellSpacing="5"
                                RepeatColumns="1"
                                RepeatLayout="Flow"
                                TextAlign="Right"
                       
                                runat="server">
                  <asp:ListItem Value="Nodues">The Employee Does not have any dues, Retured all company assets provided to him/her.</asp:ListItem>
                  <asp:ListItem Value="dues">The Employee has dues, Did not return following company assets provided to him/her.</asp:ListItem>

              </asp:CheckBoxList>

          </div>
   

      </th>
  </tr>

    <tr>
      <th rowspan='3'>Name of Item</th>
      <th rowspan='3'>Issuing date</th>
      <th rowspan='3'>Actual Price</th>
      <th colspan='3'>Deduction amount </th>
      <th>Remark </th>
    </tr>
      <tr>
   
   
      <th >Before 1 year </th>
      <th> Before 1 year to 02 years</th>
       <th> Above 2 year</th>
      <th> </th>
    </tr>
      <tr>
    
      <th>100%  </th>
      <th> 50% </th>
       <th>25% </th>
      <th> </th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td class="text-left" >1) Mobile Device</td>
        <td> <asp:TextBox runat="server" ID="MoIssueDate" AutoCompleteType="Disabled"  class="form-control form-control-sm"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                  Format="dd-MMM-yyyy" PopupButtonID="MoIssueDate" CssClass="MyCalendar"
                                  TargetControlID="MoIssueDate" />
        </td>
        <td ><asp:TextBox runat="server" ID="MoActualPrice" class="form-control form-control-sm"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                 TargetControlID="MoActualPrice"
                                                 FilterType="Custom, Numbers"
                                                 ValidChars="." />
        </td>
        <td ><asp:TextBox runat="server" ID="MoDABeforOne" class="form-control form-control-sm"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="sssss" runat="server"
                                                 TargetControlID="MoDABeforOne"
                                                 FilterType="Custom, Numbers"
                                                 ValidChars="." />
        </td>
        <td ><asp:TextBox runat="server" ID="MoDABeforeOneTwo" class="form-control form-control-sm"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                 TargetControlID="MoDABeforeOneTwo"
                                                 FilterType="Custom, Numbers"
                                                 ValidChars="." />
        </td>
        <td ><asp:TextBox runat="server" ID="MoAboveTwo" class="form-control form-control-sm">
          
        </asp:TextBox>
          
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                 TargetControlID="MoAboveTwo"
                                                 FilterType="Custom, Numbers"
                                                 ValidChars="." />
        </td>
        <td ><asp:TextBox runat="server" ID="MoRemark" class="form-control form-control-sm"></asp:TextBox></td>
      
    </tr>
    
     <tr>
     
      <td class="text-left">2) Detailing Bag </td>
         <td> <asp:TextBox runat="server" ID="DBissueDate" AutoCompleteType="Disabled"  class="form-control form-control-sm"></asp:TextBox>
             <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                   Format="dd-MMM-yyyy" PopupButtonID="DBissueDate" CssClass="MyCalendar"
                                   TargetControlID="DBissueDate" />
         </td>
         <td ><asp:TextBox runat="server" ID="DBActualPrice" class="form-control form-control-sm"></asp:TextBox>
             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                  TargetControlID="DBActualPrice"
                                                  FilterType="Custom, Numbers"
                                                  ValidChars="." />
         </td>
         <td colspan='3'><asp:TextBox runat="server" ID="DBDeductionAmount" class="form-control form-control-sm"></asp:TextBox>
             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                  TargetControlID="DBDeductionAmount"
                                                  FilterType="Custom, Numbers"
                                                  ValidChars="." />
         </td>
         <td ><asp:TextBox runat="server" ID="DBRemark" class="form-control form-control-sm"></asp:TextBox></td>
     </tr>
    
     <tr>
         <td class="text-left">3) Promo item </td>
       
      <td colspan='2'>

          <div class="row">
             
                  <div class="col-md-4" style="padding-top: 9px">
                      <label>Actual Cost </label>
                  </div>
                  
                  <div class="col-md-8">
                      <asp:TextBox runat="server" ID="PIActualCost" class="form-control form-control-sm" />
                      <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                           TargetControlID="PIActualCost"
                                                           FilterType="Custom, Numbers"
                                                           ValidChars="." />
                  </div>

          </div>

      </td>
      <td colspan='3'> <asp:TextBox runat="server" ID="PIDeductionAmount" class="form-control form-control-sm" />
          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                               TargetControlID="PIDeductionAmount"
                                               FilterType="Custom, Numbers"
                                               ValidChars="." />
      </td>
         

       <td><asp:TextBox runat="server" ID="PIRemarks" class="form-control form-control-sm"></asp:TextBox> </td>
      
    </tr>
    
     <tr>

      <td class="text-left">4) ID card </td> 
        <td colspan='5'> <asp:TextBox runat="server" ID="IDCard" class="form-control form-control-sm"></asp:TextBox> </td>
     
      <td > <asp:TextBox runat="server" ID="IDCardRemark" class="form-control form-control-sm"></asp:TextBox> </td>
     
      
    </tr>
    
     <tr>
      
      <td class="text-left">5) Market dues </td>
         
         <td  colspan='5'> <asp:TextBox runat="server" ID="MarketDues" class="form-control form-control-sm"></asp:TextBox>
             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                  TargetControlID="MarketDues"
                                                  FilterType="Custom, Numbers"
                                                  ValidChars="." />
         </td>
     
      <td  > <asp:TextBox runat="server" ID="MarketDuesRemark" class="form-control form-control-sm"></asp:TextBox></td>
      
    </tr>
    
    <tr>
      
      <td colspan='2'> </td>
       
        <td  colspan='4'>
            
            <div class="row">
             
                <div class="col-md-4" style="padding-top: 9px">
                    <label> Total Deduction Amount </label>
                </div>
                  
                <div class="col-md-8">
                    <asp:TextBox runat="server"  ID="TotalDeductionAmount" class="form-control form-control-sm" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                         TargetControlID="TotalDeductionAmount"
                                                         FilterType="Custom, Numbers"
                                                         ValidChars="." />
                </div>

            </div>

        </td>
     
      <td  >  <asp:TextBox runat="server" ID="TotalDeductionAmountRemark"  class="form-control form-control-sm" /></td>
      
    </tr>
    
    
  </tbody>
</table>

                        </div>
                              <fieldset class="for-panel">
                                <legend>Any Other Attachment </legend>
                                        <div class="row">
                                           


    
                                            <asp:HiddenField runat="server" ID="hfDocFileName"/>
  
                                            <asp:HiddenField runat="server" ID="hfDocFile"/>
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <label>Document Upload<span   style="color:red; " title="please fill out this field"> * </span></label>
                                                    <div>
   <input type="file" name="postedFile" class="form-control form-control-sm" />
  <asp:FileUpload ID="FUDocument" Visible="False" CssClass="form-control form-control-sm" runat="server"   />
                                                         <br/>
                                                        <input type="button"  class="btn btn-sm  btn-info" id="btnUpload" value="Upload Document" />
                                                          <asp:LinkButton  runat="server" Visible="False" OnClick="btnDocUp_OnClick" ID="btnDocUp"  CssClass="btn btn-sm  btn-info">
                                                          
                                                      
              &nbsp;    <span class="btn-label"><i class="fa fa-upload"></i></span>  &nbsp;   &nbsp;Upload Document
                                                          </asp:LinkButton>
                                                        <br />
                                                          <progress id="fileProgress" style="display: none"></progress>
                                                        <br />

                                             <span id="lblMessage" style="color: Green"></span>
                                                        <asp:HyperLink ID="HyperLink2" Visible="False" runat="server"
    
     Target="_blank" ToolTip="Click to Show Document"></asp:HyperLink> 
                                                           
                                                        
                                                       
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            
                                            <div class="col-4">
                                             <div class="form-group">
                                                    <label>Summary Note<span   style="color:red; " title="please fill out this field"> * </span></label>
                                                    <div>

                                                     <asp:TextBox runat="server"   ID="txtSummaryNote"  TextMode="MultiLine" Rows="2"    class="form-control" />
                                                         
                                                    </div>
                                                </div>
                                                
                                                  <div class="form-group">
                                                       <asp:LinkButton runat="server" ID="brnAddDoc"   OnClick="brnAddDoc_OnClick"    CssClass="btn btn-success   btn-sm pull-right"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp; Add to List </asp:LinkButton> 
                                                      </div>
                                            </div>

                                        </div>
               <br/>
                                                <div class="row">
                                                    <div class="col-md-8">
                                                        
                                                        
                                                          <asp:GridView Width="100%" ShowHeader="True" ID="gv_Doc" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Document">
                                                    <ItemTemplate>
                                                          <asp:HyperLink ID="HLDocumentLink"   Target="_blank"  runat="server" NavigateUrl='<%# Eval("DocumentLink") %>'  Text='Download'>
        </asp:HyperLink>
                                                        <asp:Label ID="lbl_DocumentLink" Visible="False" runat="server" Text='<%#Eval("DocumentLink")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfFileName" Value='<%#Eval("FileName")%>' />

                                                        <asp:HiddenField runat="server" ID="hfDocumentLink" Value='<%#Eval("DocumentLink")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Summary Note	">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DocumentNote" runat="server" Text='<%#Eval("DocumentNote") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             

                                                <asp:TemplateField HeaderText="Remove">
                                                    <ItemTemplate>
                                                      <asp:LinkButton runat="server" ID="btnDocRemove" OnClick="btnDocRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                                        
                                                
                                                    </div>
                                                    </div>
                                               </fieldset>
                            <br />
                            
                               <div class="form-row" runat="server" >
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>State your comments (if any):</label>
                                         <asp:TextBox runat="server"  CssClass="form-control" ID="txtRemarrks" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                                </div>

                            <div>
                                <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                                <asp:Button runat="server" ID="Button1" Visible="False" OnClick="Button1_OnClick" Text="Save & Forward " CssClass="btn btn-sm btn-success" />
                                <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" Visible="False" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
                  <Triggers>  
  
         <asp:PostBackTrigger ControlID="btnDocUp" />  
  
</Triggers> 
        </asp:UpdatePanel>
    </div>
          <script type="text/javascript">
              function setOfficeItemStatus() {
                  var pendingCheckbox = $("#chkPendingIssues");
                  var noPendingCheckbox = $("#chkNoPendingIssues");
                  var detailsTextBox = $(".pending-issues-details.form-control").first();
                  var detailsText = $.trim(detailsTextBox.val() || "");

                  if (!pendingCheckbox.is(":checked") && !noPendingCheckbox.is(":checked") && detailsText !== "") {
                      if (detailsText.toLowerCase() === "no pending issues") {
                          noPendingCheckbox.prop("checked", true);
                      } else {
                          pendingCheckbox.prop("checked", true);
                      }
                  }

                  $(".pending-issues-details").toggle(pendingCheckbox.is(":checked"));
              }

              function pageLoad() {
                  $('.cls-SelectJq').chosen({ disable_search_threshold: 5, search_contains: true });
                  $('.cls-SelectJq').trigger('chosen:updated');
                  setOfficeItemStatus();
              }

              var pendingIssuesPrefix = "Pending issues: ";

              function protectPendingIssuesPrefix(textBox) {
                  if (!$("#chkPendingIssues").is(":checked")) {
                      return;
                  }

                  if (textBox.value.indexOf(pendingIssuesPrefix) !== 0) {
                      textBox.value = pendingIssuesPrefix + textBox.value.replace(/^Pending issues:\s*/i, "");
                  }
              }

              $("body").on("change", "#chkPendingIssues", function () {
                  if (this.checked) {
                      $("#chkNoPendingIssues").prop("checked", false);
                      var detailsTextBox = $(".pending-issues-details.form-control").first();
                      var detailsText = $.trim(detailsTextBox.val() || "");

                      if (detailsText === "" || detailsText.toLowerCase() === "no pending issues") {
                          detailsTextBox.val(pendingIssuesPrefix);
                      }
                  }

                  setOfficeItemStatus();
              });

              $("body").on("change", "#chkNoPendingIssues", function () {
                  if (this.checked) {
                      $("#chkPendingIssues").prop("checked", false);
                      $(".pending-issues-details.form-control").val("No pending issues");
                  }

                  setOfficeItemStatus();
              });

              $("body").on("keydown", ".pending-issues-details.form-control", function (event) {
                  if (!$("#chkPendingIssues").is(":checked")) {
                      return;
                  }

                  var prefixLength = pendingIssuesPrefix.length;
                  var selectionStart = this.selectionStart;
                  var selectionEnd = this.selectionEnd;

                  if ((event.key === "Backspace" && selectionStart <= prefixLength) ||
                      (event.key === "Delete" && selectionStart < prefixLength) ||
                      (selectionStart < prefixLength && selectionEnd > selectionStart)) {
                      event.preventDefault();
                      this.setSelectionRange(prefixLength, prefixLength);
                  }
              });

              $("body").on("input", ".pending-issues-details.form-control", function () {
                  protectPendingIssuesPrefix(this);
              });

              $("body").on("focus click", ".pending-issues-details.form-control", function () {
                  if ($("#chkPendingIssues").is(":checked") && this.selectionStart < pendingIssuesPrefix.length) {
                      this.setSelectionRange(pendingIssuesPrefix.length, pendingIssuesPrefix.length);
                  }
              });

              $("body").on("click", "#btnUpload", function () {
                  debugger;
                  $.ajax({
                      url: '/HandlerDoc.ashx',
                      type: 'POST',
                      data: new FormData($('form')[0]),
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (file) {
                          $("#cpFormBody_hfDocFile").val('');
                          $("#cpFormBody_hfDocFileName").val('');
                          $("#fileProgress").hide();
                          $("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                          $("#cpFormBody_hfDocFile").val(file.dbfilename);
                          $("#cpFormBody_hfDocFileName").val(file.name);

                      },
                      xhr: function () {
                          var fileXhr = $.ajaxSettings.xhr();
                          if (fileXhr.upload) {
                              $("progress").show();
                              fileXhr.upload.addEventListener("progress", function (e) {
                                  if (e.lengthComputable) {
                                      $("#fileProgress").attr({
                                          value: e.loaded,
                                          max: e.total
                                      });
                                  }
                              }, false);
                          }
                          return fileXhr;
                      }
                  });
              });




    </script>
</asp:Content>

