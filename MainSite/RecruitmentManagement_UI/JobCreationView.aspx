<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="JobCreationView.aspx.cs" Inherits="RecruitmentManagement_UI_JobCreationView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    
    
    
    <style>
        fieldset.for-panel {
            background-color: #fcfcfc;
            border: 1px solid #999;
            padding: 15px 10px;
            background-color: white;
            margin-bottom: 12px;
        }

            fieldset.for-panel legend {
                background-color: #fafafa;
                border: 1px solid #ddd;
                border-radius: 1px;
                font-size: 12px;
                font-weight: bold;
                line-height: 10px;
                margin: inherit;
                padding: 7px;
                width: auto;
                margin-bottom: 0;
                color: black;
            }
    </style>
    <div class="content" id="content">
        

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                           <style>
        .pagination .page-item.active .page-link  {
            background-color: #007BFF !important;
            border-style: none !important;
            /* W3C */
        }

        
    </style>
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    
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
                                      filename: 'Job Circulation List ',
                                      title: 'SMC',
                                      messageTop: 'Job Circulation List ',
                                      exportOptions: {
                                          columns: [0, 2, 3, 4, 5]
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
                                             filename: 'Job Circulation List ',
                                             title: 'SMC',
                                             messageTop: 'Job Circulation List',
                                             exportOptions: {
                                                 columns: [0, 2, 3, 4, 5]
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
                        <span></span>
                        <h1 class="title" style="font-size: 16px; padding-top: 1px;"><img src="../Report_Pages/app.png" width="20px"  /> Job Circulation List </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                         <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                       
                        <asp:LinkButton ID="AddNewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="addNewButton_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>

                    </div>

                </div>


                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">


                            <div class="row" runat="server" visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                           <asp:CheckBoxList runat="server" ID="lchk_Dpt" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                                
                            <div class="row">
                             
                                <div class="col-md-2">

                                    <div class="form-group">
                                        <label>Company Name: </label>
                                        <asp:DropDownList ID="companyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged"></asp:DropDownList>

                                    </div>
                                </div>

                                

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="financialYearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True" ></asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Department </label>
                                       
                                        <asp:DropDownList ID="deptDropDownList" CssClass="form-control form-control-sm clsSelect" runat="server"></asp:DropDownList>
                                                <script type="text/javascript">
                                                    function pageLoad() {
                                                        $('.clsSelect').chosen({ disable_search_threshold: 5, search_contains: true });







                                                    }
</script>
                                    </div>

                                </div>
                               
                                 <div class="col-md-2">
                                    <div class="form-group" style="margin-top: 17px;">
                                          <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search"  class="btn btn-sm btnMyDesignSearch" ><i class="fa fa-search-plus"></i>&nbsp; Search</asp:LinkButton>
                                        </div>
                                     </div>
                                     
                                      
                            </div> 

                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                       CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="JobID,ActionStatus" ShowFooter="False"
                                    OnRowCommand="loadGridView_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                   <asp:HiddenField ID="ActionStatus" runat="server" Value='<%#Eval("ActionStatus") %>' />
                                                  <asp:HiddenField ID="JobReqId" runat="server" Value='<%#Eval("JobID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Print">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("JobID") %>'
                                                        CommandName="Preview" ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                   <%--     <asp:BoundField DataField="JobCode" HeaderText="Job No" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                        <asp:BoundField DataField="ReqCode" Visible="False" HeaderText="Requisition Code" />--%>
                                        <asp:BoundField DataField="Position" HeaderText="Job Title" />
                                        <asp:BoundField DataField="CompensationandOtherBenefits" Visible="False" HeaderText="Other Benifits" />
                                        <asp:BoundField DataField="CirculationStartDate" DataFormatString="{0:dd-MMM-yyyy}"
                                            HeaderText="Circulation Date" />

                                     <%--   <asp:BoundField DataField="EntryBy" HeaderText="Create By" />--%>
                                        <asp:BoundField DataField="EntryDate" DataFormatString="{0:dd-MMM-yyyy}"
                                            HeaderText="Create Date" />
                                        <asp:BoundField DataField="ActionStatus" HeaderText="Action Status" />

                                   <%--     <asp:BoundField DataField="Updateby" HeaderText="Update by" />
                                        <asp:BoundField DataField="UpdateDate" DataFormatString="{0:dd-MMM-yyyy}"--%>
                                        <%--    HeaderText="Update Date" />--%>
                                             <asp:TemplateField HeaderText="Submit">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" BackColor="#0069D9" ToolTip="Click To Change Action Status" BorderStyle="None" OnClick="btnSubmit_OnClick" ID="btnSubmit" >Submit</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="viewImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                                    ImageUrl="~/Assets/img/list-view.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                   
                                </asp:GridView>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

