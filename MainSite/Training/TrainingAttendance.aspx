<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingAttendance.aspx.cs" Inherits="Training_TrainingAttendance" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="trainingListpContent" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                
                
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
                         var table = $('#cpFormBody_gv_trainingList').DataTable(
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
                                      filename: 'Training Attendance List',
                                      title: 'SMC',
                                      messageTop: 'Training Attendance List',
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
                                     table = $('#cpFormBody_gv_trainingList').DataTable(
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
                                             filename: 'Training Attendance List',
                                             title: 'SMC',
                                             messageTop: 'Training Attendance List',
                                             exportOptions: {
                                                 columns: [0, 1, 2]
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
                   <div class="page-heading">
                      <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  />Training Attendance</h1>
                    </div>
                         <div class="page-heading__container float-right d-none d-sm-block">
                       
                    </div>
                   </div>
                   <div class="card">
                    <div class="card-body">
                        <div class="row">
                                <div class="col-md-3" runat="server" Visible="False" >
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
                                <%--<div class="col-2">
                                    <div class="form-group">
                                        <label>Training Title</label>
                                        <asp:DropDownList ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>--%>
                              <%--  <div class="col-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:Button runat="server" ID="btn_Search" OnClick="btn_Search_OnClick" CssClass="btn btn-sm btn-info" Text="Search"></asp:Button>
                                    </div>
                                </div>--%>
                                   <div class="col-1 ">
                                    <div class="form-group" style="margin-top: 16px;">
                                       
                                        <asp:Button ID="btnFilterSearch" Text="Search" OnClick="btnFilterSearch_OnClick" CssClass="btn btn-outline-info btn-block disabled btn-sm" runat="server" />
                                        
                                    </div>
                                </div>
                            </div>
                        <asp:GridView  ID="gv_trainingList" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">

                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hdpk" runat="server" Value='<%#Eval("TrainingRecordMasterId") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Numer" runat="server"  Text='<%#Eval("CompanyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Training Title">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Title" runat="server"  Text='<%#Eval("TrainingTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="TrainingType">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Company" runat="server"  Text='<%#Eval("TrainingType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Financial Year ">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Year" runat="server"  Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                             
                               <%--  <asp:TemplateField HeaderText="Specific For ">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Specific" runat="server" class="form-control form-control-sm" Text='<%#Eval("SpecifcFor") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quater ">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("quater") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Training Orgaization ">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Torg" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingOrgName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                  
                              <asp:TemplateField HeaderText="Attendance">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lb_Attendance" backcolor="#0069D9" borderstyle="None" CssClass="btn btn-info btn-sm" OnClick="lb_Attendance_Click"  runat="server" >Attendance</asp:LinkButton>
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
