<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TopSheetGenerateView.aspx.cs" Inherits="HealthCare_UI_TopSheetGenerateView" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

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
                            filename: 'HR Decleration List',
                            title: 'SMC',
                            messageTop: 'HR Decleration List',
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
                                   filename: 'HR Decleration List',
                                   title: 'SMC',
                                   messageTop: 'HR Decleration List',
                                   exportOptions: {
                                       columns: [0,1, 2, 3, 4, 5, 6]
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
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> Meeting Prepared List</h1>
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
                              <div class="form-row">
                                
                                  <div  class="col-md-1">
                                     </div>
                                  
                                  
                                  
                                   
                                     <div  class="col-md-2">
                                            <div class="form-group">
                                            <label>Company </label> 
                                       <asp:DropDownList ID="ddlCompany" runat="server"  class="form-control form-control-sm" ></asp:DropDownList>
                                        </div>
                                        </div>
                                  
                                  
                                  
                         
                                  
                                 <div  class="col-md-2">
                                    <div class="form-group">
                                        <label>Meeting Status</label> 
                                        <asp:DropDownList runat="server"   ID="ddlMetStatus"  class="form-control form-control-sm" >
                                            <asp:listitem  value="0">All</asp:listitem>
                                            <asp:listitem  Selected="True" value="Pending">Pending</asp:listitem>
     <asp:listitem   value="Complete"> Complete</asp:listitem>
                                        </asp:DropDownList>
                                        

                                    </div>
                                </div>
                                  
                                     <div  class="col-md-2">
                                            <div class="form-group">
                                            <label>From Date </label> 
                                           
                                            <asp:TextBox ID="EffectiveDateTextBox" AutoCompleteType="Disabled"     runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EffectiveDateTextBox" CssClass="MyCalendar"
                                                TargetControlID="EffectiveDateTextBox" />
                                        </div>
                                        </div>
                                
                                    <div  class="col-md-2">
                                            <div class="form-group">
                                            <label>To Date </label>  
                                           
                                            <asp:TextBox ID="EffectToDate" AutoCompleteType="Disabled"     runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EffectToDate" CssClass="MyCalendar"
                                                TargetControlID="EffectToDate" />
                                        </div>
                                        </div>
                                  
                                  
                                     <div class="col-md-3" style="margin-top:15px;">
                                    <div class="form-group" >

                                      
                                           <asp:LinkButton runat="server" ID="LinkButton1" OnClick="SearchButton_OnClick"  ToolTip="Click To Search"    class="btn btn-info btn-sm"    ><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp;Search</asp:LinkButton>
                                        
                                             <asp:LinkButton runat="server" ID="appraisalResetButton" OnClick="appraisalResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                                  </div>
                            
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                   OnPreRender="gv_DocumentUpload_PreRender"  OnRowCommand="loadGridView_RowCommand"
                                    CssClass="AddToListCssTable" DataKeyNames="TopsheetGeneMasId"  
  
                                   >
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Excel">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewExcelImageButton" runat="server" class="btn btn-white btn-sm " CommandArgument="<%# Container.DataItemIndex %>"
                                                                 CommandName="ExcelReport" ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CSV">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewExcelImageButtonCSV" runat="server" class="btn btn-white btn-sm " CommandArgument="<%# Container.DataItemIndex %>"
                                                                 CommandName="CSVReport" ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        

                                           <asp:TemplateField HeaderText="Print">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm " CommandArgument="<%# Container.DataItemIndex %>"
                                                                 CommandName="ViewReport" ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MeetingNo" HeaderText="Refference No" />                
                                       
                                        <asp:BoundField DataField="MeetingDate" HeaderText="Date"  DataFormatString="{0:dd-MMM-yyyy}" />
                                        
                                        <asp:BoundField DataField="Venue" HeaderText="Venue" />
                                        
                                        <asp:BoundField DataField="MeetingTime" HeaderText="Time" />

                                        <asp:BoundField DataField="AppCount" HeaderText="Total NO. of Appication" />
                                        <asp:BoundField DataField="IPDAmt" HeaderText="IPD Amount" />
                                        <asp:BoundField DataField="OPDAmt" HeaderText="OPD Amount" />
                                               
                                        <%--<asp:BoundField DataField="EntryBy" HeaderText="Entry By" />--%>

                                        <asp:BoundField DataField="EntryDate" HeaderText="Create Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                        
                                        <asp:BoundField DataField="MeetingStatus" HeaderText="Meeting Status" />

                                        <%--<asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" DataFormatString="{0:dd-MMM-yyyy}" />--%> 
                                        

                                          <asp:TemplateField HeaderText="Action" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editCommitteeSetup" runat="server" CssClass="btn btn-sm btnMyDesignEdit" 
                                                        CommandArgument='<%#Eval("TopsheetGeneMasId") %>' Visible='<%# Eval("MeetingStatus").ToString().Equals("Complete".ToString()) ? Convert.ToBoolean(0) : Convert.ToBoolean(1) %>'  CommandName="CommitteeSetupEdit" ToolTip="Go to Approve >>"
                                                       >Go to Approve >> </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                
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
                                <br/>
                                <br/>
                                <br/>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

