<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeJobLeftEntryView.aspx.cs" Inherits="ExitManagement_UI_EmployeeJobLeftEntryView" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">


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
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Separation List </h1>
                    </div>

                    <div class="page-heading__container float-right d-none d-sm-block">
                        
   <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                       
                        
                         <asp:LinkButton ID="addNewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="addNewButton_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
  </div>



                </div>
                <!-- //END PAGE HEADING -->
                
                
                
                  <style>
        .pagination .page-item.active .page-link  {
            background-color: #007BFF !important;
            border-style: none !important;
            /* W3C */
        }

        
    </style>
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <%--<script src="https://code.jquery.com/jquery-3.3.1.js"></script>--%>
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
                           filename: 'Employee Separation List',
                           title: 'SMC',
                           messageTop: 'Employee  Separation List',
                           exportOptions: {
                               columns: [0, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
                           }
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
                                  filename: 'Employee Separation List',
                                  title: 'SMC',
                                  messageTop: 'Employee Separation List',
                                  exportOptions: {
                                      columns: [0, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
                                  }
                              }
                              ]


                          }
                          );
                      }
                  });
              };

              // Apply the search
              table.columns().every(function () {
                  var that = this;

                  $('input', this.footer()).on('keyup change', function () {
                      if (that.search() !== this.value) {
                          that
                              .search(this.value)
                              .draw();
                      }
                  });
              });
          });

      </script>
    
       <style>
           .table   thead th {
               background-color: #5B799E;
               color: white;
                font-size: 13px !important;
                                            font-family: "Times New Roman", Times, serif !important;
                                            font-style: italic !important;
                                            font-weight: bold!important;
           }


          
                                              
                                              .dt-button.buttons-print,
                                               .dt-button.buttons-excel.buttons-html5,
                                               .dt-button.buttons-pdf.buttons-html5 {
                                                   background-color: white!important;
                                                    color:#880e4f !important;
                                                   border: none!important;
                                                  
                                                   padding: 5px 18px!important;
                                                   text-align: center!important;
                                                   text-decoration: none!important;
                                                   display: inline-block!important;
                                                   font-size: 16px!important;
                                                   margin: 2px 1px!important;
                                                   cursor: pointer!important;
                                                   -webkit-transition-duration: 0.4s!important;
                                                   transition-duration: 0.4s!important;
                                                   box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 3px 10px 0 rgba(0,0,0,0.19)!important;
                                               }


                                              .dt-buttons {
                                                  align-content: center;
                                                  text-align: right;
                                                  margin-top: -50px;
                                              }
                                              .dt-button.buttons-excel.buttons-html5:hover,
                                              .dt-button.buttons-pdf.buttons-html5:hover {
                                                  box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24),0 17px 50px 0 rgba(0,0,0,0.19)!important;
                                                  color:white!important;
              background-color: #880e4f !important;
                                              }
                                         
       </style>


                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="row" runat="server" visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                          
                                <div class="form-row">
                                
                                  <div  class="col-md-3">
                                     </div>
                                  
                                 <div  class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label> 
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>
   
                                      <div  class="col-md-2">
                                                <div class="form-group">
                                                    <label>Division</label>
                                                    <asp:DropDownList runat="server" ID="ddlDivision" AutoPostBack="True"  class="form-control form-control-sm" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged"  />
                                                </div>
                                            </div>
                                 <div class="col-md-2" runat="server" id="wing">
                                                        <div class="form-group">
                                                            <label>Wing</label>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="form-control form-control-sm" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                
                                           <div class="col-md-2" runat="server" id="dept">
                                                        <div class="form-group">
                                                            <label>Department</label>
                                                            
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>

   

                                    </div>
                              <div class="form-row">
                                
                                  <div  class="col-md-3">
                                     </div>

                                 <div  class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                     
                                        <asp:DropDownList runat="server" ID="ddlFinYear" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>
                                
                                  <div  class="col-md-2">
                                            <div class="form-group">
                                            <label>Effective From Date </label> 
                                           
                                            <asp:TextBox ID="EffectiveDateTextBox" AutoCompleteType="Disabled"   AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EffectiveDateTextBox" CssClass="MyCalendar"
                                                TargetControlID="EffectiveDateTextBox" />
                                        </div>
                                        </div>
                                
                                    <div  class="col-md-2">
                                            <div class="form-group">
                                            <label>Effective To Date </label>  
                                           
                                            <asp:TextBox ID="EffectToDate" AutoCompleteType="Disabled"   AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EffectToDate" CssClass="MyCalendar"
                                                TargetControlID="EffectToDate" />
                                        </div>
                                        </div>
                                  </div>
                            
                            
                             <div class="form-row">

                                    <div class="col-4">
                                     </div>
                                  <div  class="col-md-4">

                                        <div class="form-group">
                                            <label>Single Employee</label>
                                            
                                                <asp:DropDownList   runat="server"   ID="ddlEmpInfo" class="form-control form-control-sm" />
                                                    <script type="text/javascript">
                                                        function pageLoad() {
                                                            $('#<%=ddlEmpInfo.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                            $('#<%=ddlDivision.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                            $('#<%=ddlWing.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                            $('#<%=ddlDepartment.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                                                        }
</script>
                                            <asp:TextBox ID="txtSearch" Visible="False" runat="server" AutoPostBack="True" placeholder=" Search Single Employee" CssClass="form-control form-control-sm"
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
                                 </div>
                                    <div class="form-row">

                                    <div class="col-5">
                                     </div>
                                <div class="col-md-2" style="margin-top:5px;">
                                    <div class="form-group" >

                                      
                                           <asp:LinkButton runat="server" ID="LinkButton1" OnClick="SearchButton_OnClick"  ToolTip="Click To Search"    class="btn btn-info btn-sm"    ><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp;Search</asp:LinkButton>
                                        
                                             <asp:LinkButton runat="server" ID="appraisalResetButton" OnClick="appraisalResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                               


                            </div>

                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                      CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="EmployeeJobLeftId,EmployeeId,IsJobLeft"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="EmployeeId" runat="server" Value='<%#Eval("EmployeeId") %>' />
                                                <asp:HiddenField ID="EmployeeJobLeftId" runat="server" Value='<%#Eval("EmployeeJobLeftId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Print Letter" runat="server" Visible="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_SendMail" OnClick="lb_SendMail_OnClick" runat="server"><img src="../Assets/file.png"/></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="ExitFormReport">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmployeeId") %>'
                                                    CommandName="ExitFormReport" ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Clearence">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewReportImageButton1" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmployeeId") %>'
                                                    CommandName="Clearence" ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--   <asp:TemplateField HeaderText="Print Letter">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="VisitingLetterImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmployeeJobLeftId") %>' 
                                                        CommandName="PrintLetter" ImageUrl="~/Assets/file.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>

                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Empployee ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Empployee Name" />


                                        <asp:BoundField DataField="JobLeftType" HeaderText="Job Left Type" />

                                        <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="JobLeftDate" HeaderText="Seperation Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                        <asp:BoundField DataField="ExitFormStatus" HeaderText="Exit Form Status" />
                                            <asp:BoundField DataField="ActionStatus2" HeaderText="Approval Status" />
                                        
                                        <asp:BoundField DataField="AwEmpName" HeaderText="Awaiting Employee" />

                                        <asp:BoundField DataField="EntryBy" HeaderText="Create By" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Create Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="UpdateBy" HeaderText="UpdateBy" />
                                        <asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="editImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                                    ImageUrl="~/Assets/img/rsz_edit.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="deleteImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="DeleteData"
                                                    ImageUrl="~/Assets/img/delete.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="viewImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                                    ImageUrl="~/Assets/img/list-view.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

