<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AdvanceBillView.aspx.cs" Inherits="HealthCare_UI_AdvanceBillView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    
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
                            filename: ' Advance Bill List',
                            title: 'SMC',
                            messageTop: ' Advance Bill List',
                            exportOptions: {
                                columns: [0,1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
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
                                   filename: ' Advance Bill List',
                                   title: 'SMC',
                                   messageTop: ' Advance Bill List',
                                   exportOptions: {
                                       columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]

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
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">  Advance Bill  List</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                     <%--    <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />--%>
                                      
                        <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="HomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="btn_New"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="addNewButton_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card" >
                        <div class="card-body" >
                            
                             <div class="row" runat="server" visible="false">
                                <div class="col-md-3">
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
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-2" runat="server" visible="False">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList runat="server" ID="ddlDepartment" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                <%--   <div class="col-2">
                                <div class="form-group">
                                    <label class="control-label"> From Date</label>
                                    <asp:TextBox runat="server" ID="startDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="startDate" />
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label class="control-label"> To Date</label>
                                    <asp:TextBox runat="server" ID="endDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="endDate" />
                                </div>
                            </div>--%>

                                <div class="col-1 ">
                                    <div class="form-group" style="margin-top: 18px;">
                                        <asp:LinkButton ID="btnFilterSearch" OnClick="btnFilterSearch_OnClick" class="btn btn-info btn-sm" runat="server"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp;Search</asp:LinkButton>
                                    </div>
                                </div>
                                </div>

                             
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                   OnPreRender="gv_DocumentUpload_PreRender" 
                                    CssClass="AddToListCssTable" DataKeyNames="AdvanceBillId"  
                                   >
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="ShortName" HeaderText="Company" />
                                        <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />
                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Id" />
                                         <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                         <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                         <asp:BoundField DataField="Designation" HeaderText="Designation " />
                                        <%--<asp:BoundField DataField="RequitisionNo" HeaderText="Requitision No" />--%>
                                        <asp:BoundField DataField="IsOPD" HeaderText="Type" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" />                                  
                                        <asp:BoundField DataField="CreateBy" HeaderText="Create By" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Create Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                        
                                                                        
                                        
                                 <%--         <asp:TemplateField HeaderText="Edit" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editHRDeclaration" runat="server" CssClass="btn btn-sm btnMyDesignEdit" 
                                                        CommandArgument='<%#Eval("HRDeclerationId") %>'   CommandName="HRDeclaration" ToolTip="HR Declaration"
                                                       ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                                      
                                   
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

