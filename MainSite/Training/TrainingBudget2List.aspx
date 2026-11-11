<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingBudget2List.aspx.cs" Inherits="Training_TrainingBudget2List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="trainingListpContent" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style>
        #cpFormBody_gv_trainingBgtList > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

        #cpFormBody_gv_trainingBgtList > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }
    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                    <div class="page-heading" style="font-style: italic">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Training Budget List</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
<%--                            <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                            <asp:Button ID="addNewButton" Text="Add New Budget" OnClick="btnAddNewTrainingBudget_OnClick" CssClass="btn btn-sm btn-outline-secondary" runat="server" />--%>
                            
                            
                                         <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="HomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                     
                             <asp:LinkButton ID="addNewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="btnAddNewTrainingBudget_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                             <style>
        .pagination .page-item.active .page-link  {
            background-color: #007BFF !important;
            border-style: none !important;
            /* W3C */
        }

        
    </style>
        
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
                         var table = $('#cpFormBody_gv_trainingBgtList').DataTable(
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
                                      filename: 'Training Budget List',
                                      title: 'SMC',
                                      messageTop: 'Training Budget List',
                                      exportOptions: {
                                          columns: [0, 1, 2]
                                      }
                                  }
                                  ]
                              }
                             );

                         var prm = Sys.WebForms.PageRequestManager.getInstance();
                         if (prm != null) {
                             prm.add_endRequest(function (sender, e) {
                                 if (sender._postBackSettings.panelsToUpdate != null) {
                                     table = $('#cpFormBody_gv_trainingBgtList').DataTable(
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
                                             filename: 'Training Budget List',
                                             title: 'SMC',
                                             messageTop: 'Training Budget List',
                                             exportOptions: {
                                                 columns: [0, 1,2]
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
      
                            


                            <div class="row">
                                <div class="col-md-3" runat="server" visible="False">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>


                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group" style="margin-top: 17px;">

                                      <%--  <asp:Button ID="btnFilterSearch" Text="Search" OnClick="btn_Search_OnClick" CssClass="btn btn-outline-info btn-block disabled btn-sm" runat="server" />--%>
                                        <asp:LinkButton runat="server" ID="btnFilterSearch" OnClick="btn_Search_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>Search Information</asp:LinkButton>

                                    </div>

                                </div>
                            </div>
                            <asp:GridView ID="gv_trainingBgtList" runat="server" DataKeyNames="TrainingBudget2Id" ShowFooter="False"  AutoGenerateColumns="false" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">

                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hdpk" runat="server" Value='<%#Eval("TrainingBudget2Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Company" runat="server" class="form-control form-control-sm" Text='<%#Eval("ShortName") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Financial Year">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_finYear" runat="server" Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Budget Head">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Number" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Total Budget ">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Torg" runat="server" Text='<%#Eval("TotalBudget") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lb_Edit" OnClick="lb_Edit_OnClick" runat="server"><img src="../Assets/img/rsz_edit.png" /> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lb_delete" OnClick="lb_delete_OnClick" runat="server"><img src="../Assets/img/delete.png" /></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lb_View" OnClick="lb_View_OnClick" runat="server"><img src="../Assets/img/list-view.png" /></asp:LinkButton>
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

