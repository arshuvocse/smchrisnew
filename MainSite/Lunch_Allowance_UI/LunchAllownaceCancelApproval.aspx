<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="LunchAllownaceCancelApproval.aspx.cs" Inherits="Lunch_Allowance_UI_LunchAllownaceCancelApproval" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    <div class="content" id="content" >
        
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
                       
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">  <img src="../Report_Pages/app.png"  width="20px" />  Lunch  Cancel List  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                         <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                           
                        <asp:Button ID="detailsViewButton" Text="New Entry" CssClass="btn btn-sm btn-outline-secondary "   runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>   <asp:HiddenField ID="hdpk" runat="server" />
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <%--<form>--%>
                            <style>
                                
        .chkChoiceHeader label {
            padding-left: 2px;
            padding-right: 6px;
            font-size: 13px;
        }

                            </style>
                            
                            
                             <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" /> 
       <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" />
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
 <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.flash.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.html5.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.print.min.js"></script>

       <script>

           $(document).ready(function () {
               // Setup - add a text input to each footer cell


               // DataTable
               var table = $('#cpFormBody_loadGridView').DataTable(
                    {
                        "bInfo": true,
                        "bFilter": true,
                        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                        pageLength: 10,
                        dom: 'lBfrtip',

                        buttons: [


                        {
                            extend: 'excel',
                            footer: true,
                            text: '<i class="fa fa-file-excel-o" > Excel </i>',
                            titleAttr: 'Export to Excel'
                     ,
                            filename: ' Lunch Cancel List',
                            title: 'SMC',
                            messageTop: ' Lunch Cancel List'
                        }
                        ]
                    }
                   );

               var prm = Sys.WebForms.PageRequestManager.getInstance();
               if (prm != null) {
                   prm.add_endRequest(function (sender, e) {
                       if (sender._postBackSettings.panelsToUpdate != null) {
                           table = $('#cpFormBody_loadGridView').DataTable(
                           {
                               "bInfo": true,
                               "bFilter": true,
                               lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                               pageLength: 10,
                               dom: 'lBfrtip',

                               buttons: [


                               {
                                   extend: 'excel',
                                   footer: true,
                                   text: '<i class="fa fa-file-excel-o" > Excel </i>',
                                   titleAttr: 'Export to Excel'
                            ,
                                   filename: ' Lunch Cancel List',
                                   title: 'SMC',
                                   messageTop: ' Lunch Cancel List'
                               }
                               ]


                           }
                           );
                       }
                   });
               };

               // Apply the search

           });

      </script>
    
       <style>
           .table   thead th {
               background-color: #5B799E;
               color: white;
           }
       </style>
    
     <style>
        .dt-button.buttons-print,
        .dt-button.buttons-excel.buttons-html5,
        .dt-button.buttons-pdf.buttons-html5 {
            
            background-color: #4CAF50;
  border: none;
  color: white;
  padding: 5px 18px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 2px 1px;
  cursor: pointer;
  -webkit-transition-duration: 0.4s; 
  transition-duration: 0.4s;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 3px 10px 0 rgba(0,0,0,0.19);
        }

 
.dt-buttons {
    align-content: center;
    text-align: center;
}
.dt-button.buttons-pdf.buttons-html5:hover {
  box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24),0 17px 50px 0 rgba(0,0,0,0.19);
}
</style>
                            
                               <div class="row">
                                
                                
                                 <div class="col-md-2">                        

                                     <div class="form-group">
                                        <label>Cancel From Date </label> <span style="color: red">&nbsp;*</span>
                                        

                                        <asp:TextBox ID="effectiveDateTextBox" AutoCompleteType="Disabled" runat="server"   CausesValidation="true" 
                                            class="form-control form-control-sm"></asp:TextBox>
                                         
                                          
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupPosition="TopRight"  
                                            TargetControlID="effectiveDateTextBox" />
                                     
                                    </div>
                                </div>

                               
                                  <div class="col-md-2">                        

                                     <div class="form-group">
                                        <label>Cancel To Date </label> <span style="color: red">&nbsp;*</span>
                                        

                                        <asp:TextBox ID="txtToDate" AutoCompleteType="Disabled" runat="server" AutoPostBack="True" OnTextChanged="effectiveDateTextBox_OnTextChanged"  CausesValidation="true" 
                                            class="form-control form-control-sm"></asp:TextBox>
                                         
                                          
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupPosition="TopRight"  
                                            TargetControlID="txtToDate" />
                                     
                                    </div>
                                </div>

                                <div class="col-md-4">
                                </div>
                            </div>
                            <div class="row" runat="server" >
                                
                                <div class="col-md-2">                        

                                     <div class="form-group">
                                        <label>Company Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged"  ></asp:DropDownList>
                                    </div>
                                </div>
                                
                                
                                 <div class="col-2">
                                        <div class="form-group">
                                            <label>Division</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="form-control form-control-sm SelectMe" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                                            
                                                 <script type="text/javascript">
                                                function pageLoad() {
                                                    $('.SelectMe').chosen({ disable_search_threshold: 5, search_contains: true });
 


                                            

                                          }
               </script>

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="wing" visible="False">
                                        <div class="form-group">
                                            <label>Wing</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="form-control form-control-sm" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="dept">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm SelectMe" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="sec" visible="False">
                                        <div class="form-group">
                                            <label>Section</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="subsec" visible="False">
                                        <div class="form-group">
                                            <label>Sub Section</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSubSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSubSection_OnSelectedIndexChanged" />

                                        </div>
                                    </div>

                                    <div class="col-md-2"  runat="server" visible="False">

                                        <div class="form-group">
                                            <label>Employee ID </label>
                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" placeholder=" Employee ID" CssClass="form-control form-control-sm"
                                                OnTextChanged="EmployeeDropDownList2_SelectedIndexChanged"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetCompanyWiseEmployeeInfoForIDANdNae" ServicePath="~/WebService.asmx" TargetControlID="txtSearch"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>

                                        </div>
                                    </div>


                                    <div class="col-md-2"  runat="server" visible="False">

                                        <div class="form-group">
                                            <label>Employee Name </label>
                                            <asp:TextBox ID="NameTextBox" runat="server" AutoPostBack="True" placeholder=" Employee Name" CssClass="form-control form-control-sm" OnTextChanged="EmployeeDropDownList_SelectedIndexChanged"></asp:TextBox>
                                            
                                               <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetCompanyWiseEmployeeInfoForIDANdNae" ServicePath="~/WebService.asmx" TargetControlID="NameTextBox"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>

                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Designation</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignation" class="form-control form-control-sm SelectMe" />

                                        </div>
                                    </div>
                                
                                   <div class="col-md-2"  runat="server" visible="False">
                                        <div class="form-group">
                                            <label>Office</label>
                                            <asp:DropDownList runat="server" ID="ddlSalaryLocation" class="form-control form-control-sm SelectMe" />

                                        </div>
                                    </div>

                                    <div class="col-md-2"  runat="server" visible="False">
                                        <div class="form-group">
                                            <label>Confirmation Status</label>
                                            <asp:DropDownList runat="server" ID="ddlConformationStatus" CssClass="form-control form-control-sm">
                                                <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                         <div class="col-md-2"  runat="server" visible="False">
                                        <div class="form-group">
                                            <label>Active Status</label>
                                            <asp:DropDownList runat="server" ID="ActiveStatusDropDownList" CssClass="form-control form-control-sm">
                                                <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                
                                 <div class="col-md-4">

                                        <div class="form-group">
                                            <label>Employee Name </label>
                                               <asp:DropDownList   runat="server"   ID="ddlEmpInfo"   class="form-control form-control-sm SelectMe" />

                                        </div>
                                    </div>
                                
                                     <div class="col-2">
                                                        <div class="form-group">
                                                            <label>Employee Category</label>
                                                          
                                                            <asp:DropDownList runat="server"   ID="ddlEmpCategory" class="form-control form-control-sm"   />

                                                        </div>
                                                    </div>

                                 <div class="col-md-3">
                                    <div class="form-group" style="margin-top: 18px;">
                                        
                                         <asp:LinkButton runat="server" ID="Button1" OnClick="Button1_OnClick" ToolTip="Click To Search" Width="80" Text="Search" class="btn btn-info btn-sm" ><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                   &nbsp;&nbsp;

                                            <asp:LinkButton runat="server" ID="btnReset" OnClick="btnReset_OnClick" ToolTip="Click To Reset" Text="Reset"   Width="80" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>

                                         <%--<asp:Button runat="server" ID="Button1" Text=" Search  " OnClick="Button11_OnClick" />--%>
                                        
                                    </div>
                                </div>
                               
                              
                            </div>
                         
                             <div class="form-row" runat="server" Visible="False">
                                   
                                <div class="col-md-4">
                                </div>
                                
                                <div class="col-md-4">
                                    <div class="form-group">
 <fieldset class="for-panel">
                                        <legend>Approval Status:&nbsp;<span style="color: #a52a2a">*</span></legend>
                                        <asp:RadioButtonList ID="actionRadioButtonList" runat="server" CssClass="chkChoiceHeader" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Approved">Approve</asp:ListItem>
                                            <asp:ListItem Value="Returned">Return</asp:ListItem>
                                            <asp:ListItem Value="Canceled">Cancel</asp:ListItem>
                                        </asp:RadioButtonList>
     <br/>
      <div class="form-group" style="display: none">
                                        <label style="font-weight: bold">Comments</label>
                                        <asp:TextBox runat="server" ID="commentsTextBox" CssClass="form-control" ReadOnly="False" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>
     
     <div class="form-group">
                                        <asp:Button ID="submitButton" Text=" Submit " CssClass="btn btn-sm btn-info"     runat="server" OnClick="submitButton_OnClick" />
         </div>
     </fieldset>
                                    </div>
                                    
                                </div>

                            </div>
                           <div class="row">
                                
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="loadGridView" runat="server"    AutoGenerateColumns="False" 
                                                CssClass="table table-bordered text-center thead-dark table-hover table-striped"  OnPreRender="gv_DocumentUpload_PreRender"  >
                                                <Columns>
                                                    
                                                        <asp:TemplateField Visible="False">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" CssClass="form-control-sm"   runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                            
                                                              <asp:HiddenField runat="server" ID="hfLunchAlllowCancelId" Value='<%#Eval("LunchAlllowCancelId")%>' />
                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ShortName" HeaderText="Company" />
                                                    <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                                    <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                      <asp:BoundField DataField="DivisionName" HeaderText="Division" />
                                                    
                                                      <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" />

                                                      <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                         
                                                    
                                                      <asp:TemplateField HeaderText="Cancel Date">
                                                        <ItemTemplate>
                                                           
                                                            
                                        <asp:TextBox ID="lblDateData" AutoCompleteType="Disabled" runat="server"   CausesValidation="true" 
                                            class="form-control form-control-sm"  ReadOnly="True" Text='<%#Eval("EffectiveDate", "{0:dd-MMM-yyyy}") %>'></asp:TextBox>
                                         
                                          
                                         
                                                                
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    
                                                    
                                                    <%--<asp:BoundField DataField="Remarks" HeaderText="Comments" />--%>
                                                    
                                                     
                                                    
                                                        <asp:TemplateField HeaderText="Approval Status">
                                                        <ItemTemplate>
                                                           
                                                            
                                                               <asp:Label ID="lblActionStatus" runat="server"      Text='<%#Eval("ActionStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    
                                                    <asp:TemplateField HeaderText="Approval Remarks" Visible="False">
                                                        <ItemTemplate>
                                                            
                                                            
                                                            <asp:TextBox ID="txtApprovalRemarks" with="20%" runat="server" TextMode="MultiLine" Rows="2" Columns="2"
                                                                         class="form-control"  ></asp:TextBox>


                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                            </asp:GridView>
                                         
                                        
                                    </div>
                                </div>
                                
                            </div>

                            <div class="row">
                                
                                <div class="col-md-3">
                                    
                                         
                                        
                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>

                                <div class="col-md-4">
                                </div>
                            </div>
                            
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />

                                <%--</form>--%>
                        </div>

                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
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
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
            

</asp:Content>

