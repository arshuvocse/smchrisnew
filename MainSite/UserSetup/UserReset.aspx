<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="UserReset.aspx.cs" Inherits="UserSetup_UserReset" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
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
              $('#cpFormBody_loadGridView tfoot td').each(function () {
                  var title = $(this).text();
                  $(this).html('<input class="form-control form-control-sm" type="text" placeholder="Search ' + title + '" />');
              });

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
                           filename: 'User List',
                           title: 'SMC',
                           messageTop: 'User List',
                           exportOptions: {
                               columns: [0, 1, 2, 3, 4, 5, 6]
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
                                  filename: 'User List',
                                  title: 'SMC',
                                  messageTop: 'User List',
                                  exportOptions: {
                                      columns: [0, 1, 2, 3, 4, 5, 6]
                                  }
                              }
                              ]


                          }
                          );
                      }
                  });
              };


          }
                  );


         

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
 
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        

        <div class="container-fluid">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;User Reset Password</h1>
                </div>
               <%-- <div class="page-heading__container float-right d-none d-sm-block">
                    <asp:Button ID="btn_New" Text="Create New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btn_New_OnClick" />
                        
                </div>--%>
            </div>
            <div class="card">
                <div class="card-body">
                    <asp:UpdatePanel runat="server">
    <ContentTemplate>
         <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                  <%--      
                    <div id="tbl_container" style="width:100%" class="">

                    </div>--%>
                    <div class="row">
                           <div class="col-2" >
                                    <div class="form-group">
                                        <label>Company</label>
                                         <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true"  OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                    </div>
                    
                    
                        <div class="col-md-12">
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                    <ContentTemplate>
                                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                               CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   DataKeyNames="UserId,UserName" ShowFooter="False"
                                            OnRowCommand="loadGridView_RowCommand"   >
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                                <%--<asp:BoundField DataField="Password" HeaderText="Password" />--%>

                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />

                                                <asp:BoundField DataField="UserType" HeaderText="User Type" />
                                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                                <asp:BoundField DataField="Email" HeaderText="Email ID" />
                                                <%--<asp:BoundField DataField="Remarks" HeaderText="Remarks" />--%>



                                               
                                                <asp:TemplateField HeaderText="Reset Password">
                                                    <ItemTemplate>
                                                        
                                                        <asp:LinkButton CssClass="btn btn-sm btnMyDesignSearch" runat="server" ID="btn_edit"   CommandArgument="<%# Container.DataItemIndex %>" CommandName="ResetData" >
               
  	 <i class="fa fa-undo"></i>&nbsp; Reset Password
                                            </asp:LinkButton>
                                                        
                                                      
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <%--     <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="viewImageButton" runat="server"
                                                             CommandArgument='<%#Eval("UserId") %>' CommandName="ViewData"
                                                            ImageUrl="~/Assets/img/list-view.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            <%--    <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="deleteImageButton" runat="server"
                                                            CommandArgument='<%#Eval("UserId") %>' CommandName="DeleteData"
                                                            ImageUrl="~/Assets/img/delete.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

<%--                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="viewImageButton" runat="server"
                                                             CommandArgument='<%#Eval("UserId") %>' CommandName="ViewData"
                                                            ImageUrl="~/Assets/img/list-view.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                               
                                            </Columns>
                                          
                                        </asp:GridView>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                        </div>
                        
                            </ContentTemplate>
</asp:UpdatePanel>
                </div>
            </div>
            
        </div>
    </div>
    <%--<script src="../Assets/MaterialDT/jquery.dataTables.min.js"></script>
    <script src="../Assets/MaterialDT/dataTables.material.min.js"></script>
    <script type="text/javascript">
        $.getScript('../AppJs/UserList.js', function (data, textStatus, jqxhr) {
            console.log('Script File Load Status=' + jqxhr.status);
        });
    </script>--%>
</asp:Content>

