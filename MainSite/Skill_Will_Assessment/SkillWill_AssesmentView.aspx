<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SkillWill_AssesmentView.aspx.cs" Inherits="Skill_Will_Assessment_SkillWill_AssesmentView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
           <style>
        #cpFormBody_gv_kpiSetup > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

        #cpFormBody_gv_kpiSetup > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }
    </style>
    <div class="content">
        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>

                <style>
                    .pagination .page-item.active .page-link {
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
                                                                 var table = $('#cpFormBody_gv_kpiSetup').DataTable(
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
                                                                              filename: 'KPI Declaration and Deadline List',
                                                                              title: 'SMC',
                                                                              messageTop: 'KPI Declaration and Deadline List',
                                                                              exportOptions: {
                                                                                  columns: [0, 2, 3, 4, 5, 6]
                                                                              }
                                                                          }
                                                                          ]
                                                                      }
                                                                     );

                                                                 var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                                 if (prm != null) {
                                                                     prm.add_endRequest(function (sender, e) {
                                                                         if (sender._postBackSettings.panelsToUpdate != null) {
                                                                             table = $('#cpFormBody_gv_kpiSetup').DataTable(
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
                                                                                     filename: 'KPI Declaration and Deadline List',
                                                                                     title: 'SMC',
                                                                                     messageTop: 'KPI Declaration and Deadline List',
                                                                                     exportOptions: {
                                                                                         columns: [0, 2, 3, 4, 5,6]
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
                                                                     .table thead th {
                                                                         background-color: #5B799E;
                        color: white;
                        font-size: 13px !important;
                        font-family: "Times New Roman", Times, serif !important;
                        font-style: italic !important;
                        font-weight: bold !important;
                    }




                    .dt-button.buttons-print,
                    .dt-button.buttons-excel.buttons-html5,
                    .dt-button.buttons-pdf.buttons-html5 {
                        background-color: white !important;
                        color: #880e4f !important;
                        border: none !important;
                        padding: 5px 18px !important;
                        text-align: center !important;
                        text-decoration: none !important;
                        display: inline-block !important;
                        font-size: 16px !important;
                        margin: 2px 1px !important;
                        cursor: pointer !important;
                        -webkit-transition-duration: 0.4s !important;
                        transition-duration: 0.4s !important;
                        box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 3px 10px 0 rgba(0,0,0,0.19) !important;
                    }


                    .dt-buttons {
                        align-content: center;
                        text-align: right;
                        margin-top: -50px;
                    }

                    .dt-button.buttons-excel.buttons-html5:hover,
                    .dt-button.buttons-pdf.buttons-html5:hover {
                        box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24),0 17px 50px 0 rgba(0,0,0,0.19) !important;
                        color: white !important;
                        background-color: #880e4f !important;
                    }
                </style>

                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                <img src="../Report_Pages/app.png" width="20px" /> Skill Will Assessment List</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                            <asp:Button ID="detailsViewButton" OnClick="detailsViewButton_OnClick" Text="Add New" CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                        </div>

                    </div>

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

                            <div class="row" runat="server" Visible="False">

                                <div class="col-md-2">

                                    <div class="form-group">
                                        <label>Company Name: </label>
                                        <asp:DropDownList ID="companyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged"></asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year:</label>
                                        <asp:DropDownList ID="financialYearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group" style="margin-top: 17px;">

                                        <asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search" Text="Search" CssClass="btn btn-outline-success disabled btn-sm" />
                                    </div>
                                </div>
                            </div>
                            

                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_kpiSetup" CssClass="table table-bordered text-center thead-dark table-hover table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="KPIDeadLineMasterId" runat="server" Value='<%#Eval("EmpSkillWillMasterId") %>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Subject" runat="server" Text='<%#Eval("employee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                              <%--      <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" OnClick="btn_edit_OnClick" ID="btn_edit"><img src="../Assets/img/rsz_edit.png" /> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                          

                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_View" OnClick="btn_View_OnClick"><img src="../Assets/img/list-view.png" /></asp:LinkButton>
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

