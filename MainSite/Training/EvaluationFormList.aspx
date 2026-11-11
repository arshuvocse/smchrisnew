<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EvaluationFormList.aspx.cs" Inherits="Training_EvaluationFormList" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="trainingSetupContent" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Evaluation Form List</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="New Form" CssClass="btn btn-sm btn-outline-secondary " OnClick="detailsViewButton_Click" runat="server"  />
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
              $('#cpFormBody_gv_formList tfoot td').each(function () {
                  var title = $(this).text();
                  $(this).html('<input class="form-control form-control-sm" type="text" placeholder="Search ' + title + '" />');
              });

              // DataTable
              var table = $('#cpFormBody_gv_formList').DataTable(
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
                           filename: 'Evaluation Form List',
                           title: 'SMC',
                           messageTop: 'Evaluation Form List',
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
                          table = $('#cpFormBody_gv_formList').DataTable(
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
                                  filename: 'Evaluation Form List',
                                  title: 'SMC',
                                  messageTop: 'Evaluation Form List',
                                  exportOptions: {
                                      columns: [0, 1, 2, 3, 4, 5]
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
                           
                           
                             <asp:GridView  ShowFooter="true" ID="gv_formList" Width="100%"  CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                                    <%--<Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("EvaluationFormMasterId") %>'/>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From No">
                                                <ItemTemplate>
                                                    <asp:Label ID="EvaluationFormNo" runat="server" class="form-control form-control-sm" Text='<%#Eval("EvaluationFormNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Training No">
                                                <ItemTemplate>
                                                     
                                                    <asp:Label  ID="TrainingSetupNumber" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingSetupNumber") %>'></asp:Label>
                                                   
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Training Title">
                                                <ItemTemplate>
                                                     
                                                    <asp:Label  ID="gv_avarage" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingTitle") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company">
                                                <ItemTemplate>
                                                     
                                                    <asp:Label  ID="ShortName" runat="server" class="form-control form-control-sm" Text='<%#Eval("ShortName") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Financial Year">
                                                <ItemTemplate>
                                                     
                                                    <asp:Label  ID="FinancialYearDesc" runat="server" class="form-control form-control-sm" Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_edit" OnClick="btn_edit_Click"  runat="server"
                                                        >Edit</asp:LinkButton>|

                                                     <asp:LinkButton ID="btn_remove" OnClick="btn_remove_Click"  runat="server"
                                                        >Remove</asp:LinkButton>
                                                </ItemTemplate>
                                             
                                                
                                            </asp:TemplateField>
                                    
                                        </Columns>
                                        --%>
                                 
                                 
                                 
                                 
                                 
                                 <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("TrainingRecordMasterId") %>'/>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                     
                                        <asp:BoundField DataField="EmpName" HeaderText="Participant Name"/>
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name"/>
                                        <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />
                                        <asp:BoundField DataField="TrainingTitle" HeaderText="Training Title"/>
                                        <asp:BoundField DataField="TrainingType" HeaderText="Training Type" />                                                                            
                                        
                                        <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_evaluation" OnClick="btn_evaluation_Click"  runat="server"
                                                        >Evaluation</asp:LinkButton>

                                                   
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
