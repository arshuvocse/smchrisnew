<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MeetingApprovalView.aspx.cs" Inherits="MeetingMinors_MeetingApprovalView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
      <style>
        fieldset.for-panel {
          
            padding: 10px 8px;
            background-color: white;
            margin-bottom: 12px;
        }

            fieldset.for-panel legend {
               
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

  
  .chkChoice label {
            padding-left: 4px;
            padding-right: 4px;
        }

  
.title-widget {
	color: #898989;
	font-size: 20px;
	font-weight: 300;
	line-height: 1;
	position: relative;
	text-transform: uppercase;
	font-family: 'Fjalla One', sans-serif;
	margin-top: 0;
	margin-right: 0;
	margin-bottom: 25px;
	 
	padding-left: 12px;

}

.title-widget::before {
    background-color: #ea5644;
    content: "";
    height: 22px;
    left: 0px;
    position: absolute;
    top: -2px;
    width: 5px;
}
          </style>
    
     <div class="content" id="content">
                 <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Meeting Information Approval List </h1>
                        </div>
                         <div class="page-heading__container float-right d-none d-sm-block">
                         
                <asp:Button ID="AddNewButton" Visible="False" Text="Add New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="AddNewButton_OnClick"  />
                <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
            </div>
                    </div>
                     
<div class="card">
   <div class="card-body">
       <asp:UpdatePanel runat="server"> 
           <ContentTemplate>
                 <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
         <div class="row" style="display: none">
               
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:</label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlCompany"  class="form-control form-control-sm" /></div>
                          </div>
                            <div style="padding-top: 5px;"></div>
                                <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Title:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"    ID="DropDownList1"  class="form-control form-control-sm" /></div>
                          </div>
                             <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Propuse:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"      ID="DropDownList2"  class="form-control form-control-sm" /></div>
                          </div>
                            </div>
                        </div>
             
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          
                            
                                <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Created By:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"    ID="TextBox1"  class="form-control form-control-sm" /></div>
                          </div>
                            <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Created Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date"    ID="TextBox2"  class="form-control form-control-sm" /></div>
                          </div>
                            <div style="padding-top: 5px;"></div>
                            
                              <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Key Search:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"    ID="TextBox3"  class="form-control form-control-sm" /></div>
                          </div>
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
    <%--<script src="https://code.jquery.com/jquery-3.3.1.js"></script>--%>
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
              var table = $('#cpFormBody_gv_ViewList').DataTable(
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
                           filename: 'Miscellaneous Information List',
                           title: 'SMC',
                           messageTop: 'Miscellaneous Information List',
                           exportOptions: {
                               columns: [0, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                           }
                       }
                       ]
                   }
                  );

              var prm = Sys.WebForms.PageRequestManager.getInstance();
              if (prm != null) {
                  prm.add_endRequest(function (sender, e) {
                      if (sender._postBackSettings.panelsToUpdate != null) {
                          table = $('#cpFormBody_gv_ViewList').DataTable(
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
                                  filename: 'Miscellaneous Information List',
                                  title: 'SMC',
                                  messageTop: 'Miscellaneous Information List',
                                  exportOptions: {
                                      columns: [0, 2, 3, 4, 5, 6, 7, 8, 9, 10]
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

            <asp:GridView Width="100%" ShowHeader="True" ID="gv_ViewList" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Company">
                                                    <ItemTemplate>
                                                             <asp:HiddenField runat="server" ID="hfMasterId" Value='<%#Eval("MeetingInfoID")%>' />
                                                        <asp:Label ID="lbl_ShortName"   runat="server" Text='<%#Eval("ShortName")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfMeetingInfoID" Value='<%#Eval("MeetingInfoID")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_MeetingCategory" runat="server" Text='<%#Eval("MeetingCategory") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Purpose">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Purpose" runat="server" Text='<%#Eval("MeetingPurpose") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                   <asp:TemplateField HeaderText="Created By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CreateBy" runat="server" Text='<%#Eval("CreateBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                  <asp:BoundField DataField="CreateDate" HeaderText="Created Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                                
                                                
                                                
                                                  <asp:TemplateField HeaderText="Updated By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_UpdateBy" runat="server" Text='<%#Eval("UpdateBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                  <asp:BoundField DataField="UpdateDate" HeaderText="Updated Date" DataFormatString="{0:dd-MMM-yyyy}" /> 



                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>
                                                        
                                                        <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_OnClick"   CssClass="btn btn-sm btn-success">&nbsp; Go to Approve&nbsp;&#8921; </asp:LinkButton>
                                                    <%--  <asp:LinkButton runat="server" ID="btnRemove" OnClick="btnRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                        
                                                        <asp:LinkButton runat="server" ID="btnView" OnClick="btnView_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa   fa-eye"></i> </asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
        
      </ContentTemplate>
          </asp:UpdatePanel>
              </div>
        
        </div>
                               </div>
        
        </div>
</asp:Content>

