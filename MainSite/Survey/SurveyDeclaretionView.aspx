<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="SurveyDeclaretionView.aspx.cs" Inherits="Survey_SurveyDeclaretionView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server" >
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                       
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"><img src="../Report_Pages/app.png"  width="20px" />  Survey Declaration List</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                         <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid" style="background-color: #DFF9FB">
                    <div class="card" >
                        <div class="card-body" >
                             <div class="row">
                                    <div class="col-md-3" runat="server" Visible="False">
                                        <div class="form-group">
                                            <label> </label>
                                             <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True"  RepeatDirection="Horizontal" Enabled="False" >
                                        </asp:CheckBoxList>
                                            

                                        </div>
                                    </div>
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
                           filename: 'Survey_Question_Group_List',
                           title: 'SMC',
                           messageTop: 'Survey Question Group List',
                           exportOptions: {
                               columns: [0, 1, 2, 3, 4]
                           }
                       },
       {
           extend: 'pdf',
           footer: true,
           text: '<i class="fa fa-file-pdf-o"> Pdf </i>',
           titleAttr: 'Export to PDF',
           messageTop: 'Survey Question Group List',

           filename: 'Survey_Question_Group_List',
           title: 'SMC',
           exportOptions: {
               columns: [0, 1, 2, 3, 4]

           }

       },
       {
           extend: 'print',
           footer: true,
           text: '<i class="fa fa-print" > Print </i>',
           titleAttr: 'Export to Print',
           messageTop: 'Survey Question Group List',
           filename: 'Survey_Question_Group_List',
           title: 'SMC',
           exportOptions: {
               columns: [0, 1, 2, 3, 4]

           }

       }
                       ]
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

                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="SurveyMasterId" ShowFooter="True"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                  <asp:HiddenField ID="hdpk" runat="server" Value='<%#Eval("SurveyMasterId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                     <asp:TemplateField HeaderText="Send Mail">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lb_SendMail" OnClick="lb_SendMail_OnClick" runat="server"><img src="../Assets/img/email-1.png" width="20px"  /></asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="ShortName" HeaderText="Company" />
                                        <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />

                                        <asp:BoundField DataField="SurveyName" HeaderText="Survey Name" />
                                        <asp:BoundField DataField="SurveyFrom" HeaderText="Survey From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="SurveyTo" HeaderText="Survey To Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:BoundField DataField="IsActive" HeaderText="Active Status" />
                                       
                                     
                                       
                                        <asp:BoundField DataField="EntryBy" HeaderText="Create By" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Create Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="UpdateBy" HeaderText="Update By" />
                                        <asp:BoundField DataField="UpdateDate" HeaderText="Update Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        
                                     

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
                                        
                                          <asp:TemplateField HeaderText="View" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="viewImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                                    ImageUrl="~/Assets/img/list-view.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

