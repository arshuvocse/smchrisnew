<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="OrganizationList.aspx.cs" Inherits="Training_OrganizationSetup" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


</asp:Content>
<asp:Content ID="trainingListpContent" ContentPlaceHolderID="cpFormBody" runat="Server">


    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                  <div class="page-heading"  >
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  />Organization List</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                             <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                            <asp:Button ID="detailsViewButton" Text="Add New Organization" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_Click1" />
                        </div>
                      
                      
                      
                       
                    <style>
        .pagination .page-item.active .page-link  {
            background-color: #007BFF !important;
            border-style: none !important;
            /* W3C */
        }

        
    </style>
        
    
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
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
                         var table = $('#cpFormBody_gv_OrgList').DataTable(
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
                                      filename: 'Organization List',
                                      title: 'SMC',
                                      messageTop: 'Organization List',
                                      exportOptions: {
                                          columns: [0, 1, 2, 3, 4, 5]
                                      }
                                  }
                                  ]
                              }
                             );

                         var prm = Sys.WebForms.PageRequestManager.getInstance();
                         if (prm != null) {
                             prm.add_endRequest(function (sender, e) {
                                 if (sender._postBackSettings.panelsToUpdate != null) {
                                     table = $('#cpFormBody_gv_OrgList').DataTable(
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
                                             filename: 'Organization List',
                                             title: 'SMC',
                                             messageTop: 'Organization List',
                                             exportOptions: {
                                                 columns: [0, 1, 2,3,4,5]
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
                        <%-- <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                            ServiceMethod="Getitem" ServicePath="WebService.asmx" TargetControlID="itemTextBox"
                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            ShowOnlyCurrentWordInCompletionListItem="true">
                        </cc1:AutoCompleteExtender>--%>
                    </div>
               
                <div class="card">
                    <div class="card-body">
                        
                        
                         <div class="row" runat="server" Visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>


                                    </div>
                                </div>
                            </div>

                        <asp:GridView Width="100%" ID="gv_OrgList" runat="server" OnPreRender="gv_OrgList_PreRender" AutoGenerateColumns="false"   CssClass="table table-bordered text-center thead-dark gridDatatable">
                            <%--<div>

                           </div>--%>
                            <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:HiddenField ID="hdpk" runat="server" Value='<%#Eval("TrainingOrgId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Company">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_ShortName" runat="server"  Text='<%#Eval("ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Organization">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_OrgName" runat="server"  Text='<%#Eval("TrainingOrgName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                               
                                
                              
                                <asp:TemplateField HeaderText="Organization Type">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_OrgType" runat="server"  Text='<%#Eval("OrgTypeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Origin">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_Origin" runat="server"  Text='<%#Eval("Origin") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_Address" runat="server"  Text='<%#Eval("OrgAddress") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                             <%--   <asp:TemplateField HeaderText="Registration Year">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_RegYear" runat="server" class="form-control form-control-sm" Text='<%#Eval("RegistrationYear") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lb_Edit" OnClick="lb_Edit_Click" runat="server"><img src="../Assets/img/rsz_edit.png"/></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton ID="lb_delete" OnClick="lb_delete_Click" runat="server"><img src="../Assets/img/delete.png"/></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                 <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton ID="lb_View" OnClick="lb_View_Click" runat="server"><img src="../Assets/img/list-view.png"/></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>



