<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MemberInformationView.aspx.cs" Inherits="MeetingMinors_MemberInformationView" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
        
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
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
                        <%--<div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>--%>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />   Member Information List</h1>
              
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />

                        <%--<asp:Button ID="reloadButton" Text="Refresh" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>

                </div>
                
                    </ContentTemplate>
        </asp:UpdatePanel>
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
              var table = $('#cpFormBody_gv_loadGridView').DataTable(
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
                           filename: 'Member Information List',
                           title: 'SMC',
                           messageTop: 'Member Information List',
                           exportOptions: {
                               columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]

                           }
                       }
                       ]
                   }
                  );

              var prm = Sys.WebForms.PageRequestManager.getInstance();
              if (prm != null) {
                  prm.add_endRequest(function (sender, e) {
                      if (sender._postBackSettings.panelsToUpdate != null) {
                          table = $('#cpFormBody_gv_loadGridView').DataTable(
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
                                  filename: 'Member Information List',
                                  title: 'SMC',
                                  messageTop: 'Member Information List',
                                  exportOptions: {
                                      columns: [0,1, 2, 3, 4, 5, 6,7,8,9,10,11]
                                  }
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
                            
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">
               <div class="col-md-3">
                   </div>
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:</label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlCompany" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" />
                                
                                    <script type="text/javascript">
                                        function pageLoad() {
                                            


                                              $('#<%=ddlCreateBy.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                                          }
               </script>

                            </div>
                          </div>
                             <div style="padding-top: 5px;"></div>
                             <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Member Type:</label></div>
                                <div class="col-md-6">
                                    <asp:DropDownList runat="server" ID="ddlMemberTypeSearch" class="form-control form-control-sm" />
                                </div>
                             </div>
                             <div style="padding-top: 5px;"></div>
                            
                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Meeting Category:</label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlCategory"    class="form-control form-control-sm" >
                               
                                </asp:DropDownList>
                                </div>
                          </div>
                            <div style="padding-top: 5px;"></div>
                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Create By:</label></div>
                                <div class="col-md-6">  <asp:DropDownList   runat="server"   ID="ddlCreateBy"  class="form-control form-control-sm" /></div>
                                
                              <%--  <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                          EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                          ServiceMethod="GetUserbyCompanyId" ServicePath="~/WebService.asmx" TargetControlID="txtCreateBy"
                                                          UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                          CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                          ShowOnlyCurrentWordInCompletionListItem="true">
                                </cc1:AutoCompleteExtender>--%>
                                <asp:HiddenField ID="hfEmpId" runat="server"/>
                              
                            </div>
                            <div style="padding-top: 5px;"></div>
                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Create From Date:</label></div>
                                <div class="col-md-6">  <asp:TextBox TextMode="Date" runat="server"   ID="TxtCreateDate"  class="form-control form-control-sm" /></div>
                            </div>
                            <div style="padding-top: 5px;"></div>
                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Create To Date:</label></div>
                                <div class="col-md-6">  <asp:TextBox TextMode="Date" runat="server"   ID="TxtToDate"  class="form-control form-control-sm" /></div>
                            </div>
                            </div>
                        </div>
             </div>
                            
                            
                            
                            <div class="row">
                                <div class="col-md-4"> </div>
                                <div class="col-md-1"> </div>
                                <div class="col-md-4">
                                    <asp:LinkButton runat="server" ID="LinkButton1" OnClick="ButtonView_OnClick" ToolTip="Click To Search"    class="btn btn-info btn-sm" ><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp;Search</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnReset" OnClick="btnReset_OnClick" ToolTip="Click To Reset"      CssClass="btn btn-warning   btn-sm"   ><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset</asp:LinkButton>
                                </div>
                                <div class="col-md-4"> </div>

                            </div>

                
                            
                               </ContentTemplate>
                    </asp:UpdatePanel>
                            
                               <div class="row">
                        <div class="col-md-12">
                            <label></label>
                        </div>


                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-1">
                        </div>
                                  <style>
                                    .btnexcel {
                                        background-color: #4CAF50;
                                        border: none;
                                        color: white;
                                        padding: 8px 12px;
                                        text-align: center;
                                        text-decoration: none;
                                        display: inline-block;
                                        font-size: 12px;
                                        margin: 4px 2px;
                                        cursor: pointer;
                                    }
                                </style>
                        <div class="col-md-2 ">
                            <asp:LinkButton ID="btnExportToExcel" Visible="False" runat="server" CssClass="btnexcel pull-right" OnClick="btnExportToExcel_Click"><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>




                        </div>
                    </div>
                            <br/>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="row" runat="server" visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>


                                    </div>
                                </div>
                            </div>

                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: hidden;">
                                <asp:GridView ID="gv_loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="AddToListCssTable"    OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="MemberSetupDetailsID" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                     <asp:HiddenField runat="server" ID="hfMasterId" Value='<%#Eval("MemberSetupDetailsID")%>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="OrderNo" HeaderText="Order No" />
                                        <asp:BoundField DataField="ShortName" HeaderText="Company" />
                                        <asp:BoundField DataField="MemberType" HeaderText="Member Type" Visible="False" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Address" HeaderText="Address" />
                                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile No." />
                                        <asp:BoundField DataField="Email" HeaderText="Email" />
                                     
                                        <asp:BoundField DataField="JoiningDate" HeaderText="Joining Date"  DataFormatString="{0:dd-MMM-yyyy}" /> 
                                           <asp:BoundField DataField="MembershipDate" HeaderText="Membership Date"  DataFormatString="{0:dd-MMM-yyyy}" /> 
                                        <asp:BoundField DataField="Note" HeaderText="Note" />

                                        <asp:TemplateField HeaderText="Created By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CreateBy" runat="server" Text='<%#Eval("CreateBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                  <asp:BoundField DataField="CreateDate" HeaderText="Created Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                                
                                                
                                                
                                              <%--    <asp:TemplateField HeaderText="Updated By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_UpdateBy" runat="server" Text='<%#Eval("UpdateBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                  <asp:BoundField DataField="UpdateDate" HeaderText="Updated Date" DataFormatString="{0:dd-MMM-yyyy}" /> 


                                     --%>
                                        
                                        
                                           <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>
                                                        
                                                        <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_OnClick"   CssClass="btn btn-sm btn-warning"><i class="fa fa-pencil-square-o"></i> </asp:LinkButton>
                                                      <asp:LinkButton runat="server" ID="btnRemove" OnClick="btnRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                        
                                                        <asp:LinkButton runat="server" ID="btnView" OnClick="btnView_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa   fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                         


                                    </Columns>
                                   
                                </asp:GridView>
                            </div>
                            
                                    </ContentTemplate>
        </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
         
    </div>
    <style>.GridPager a,
                                                                                                                                                                            .GridPager span {
                                                                                                                                                                                display: inline-block;
                                                                                                                                                                                padding: 3px 14px;
                                                                                                                                                                                margin-right: 8px;
                                                                                                                                                                                border-radius: 3px;
                                                                                                                                                                                height: 20px;
                                                                                                                                                                                border: solid 1px #c0c0c0;
                                                                                                                                                                                background: #e9e9e9;
                                                                                                                                                                                box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
                                                                                                                                                                                font-size: 14px;
                                                                                                                                                                                font-weight: bold;
                                                                                                                                                                                text-decoration: none;
                                                                                                                                                                                color: #717171;
                                                                                                                                                                                text-shadow: 0px 1px 0px rgba(255,255,255, 1);
                                                                                                                                                                            }

                                                                                                                                                                            .GridPager a {

                                                                                                                                                                                background-color: #f5f5f5;
                                                                                                                                                                                color: #969696;
                                                                                                                                                                                border: 1px solid #969696;
                                                                                                                                                                            }

                                                                                                                                                                            .GridPager span {

                                                                                                                                                                                background: #616161;
                                                                                                                                                                                box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
                                                                                                                                                                                color: #f0f0f0;
                                                                                                                                                                                text-shadow: 0px 0px 3px rgba(0,0,0, .5);
                                                                                                                                                                                border: 1px solid #3AC0F2;
                                                                                                                                                                            }

    </style>
</asp:Content>

