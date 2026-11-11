<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master"  EnableViewState="true" MaintainScrollPositionOnPostback="true"  AutoEventWireup="true" CodeFile="JobRequisitionFormView.aspx.cs" Inherits="MasterSetup_UI_JobRequisitionFormView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
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

            #cpFormBody_loadGridView > tbody > tr > th {
                padding: 9px 0;
                color: #fff;
                background-color: #5B799E;
                /*background-color: #98A9C0;*/
            }
        </style>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
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
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Job Requisition List </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        
                        
                          <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                       
                        <asp:LinkButton ID="addNewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="addNewButton_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
                        <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->



                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

             
    <!-- Modal Popup -->
    <script type="text/javascript">
        function ShowPopup() {
           
            $("#exampleModal").modal("show");
        }
    </script>
                            <style>
                                .w3-tag{background-color:#FF9800;color:#fff;padding: 4px;border-radius:10%}
                                .w3-green,.w3-hover-green:hover{color:#fff!important;background-color:#4CAF50}
                              
                            </style>
                    

                             <div class="modal fade" ID="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" >
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h3 class="modal-title" id="exampleModalLabel" style="color:#FF9800;">All Recruitment Process</h3>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                         
                                            
                                             <div class="col-md-12">
                                              <h5>Job Circulation: <asp:Label runat="server" ID="PJobCirculation"  class="label w3-tag w3-green" style="font-size: 12px;"  ><label   runat="server" id="lblJobCirculation"></label></asp:Label></h5>
                                            </div>
                                            
                                             <div class="col-md-12">
                                              <h5>Interview Candidate Information: <asp:Label runat="server" ID="PewCandidateInformation"  class="label w3-tag w3-green" style="font-size: 12px;" ><label runat="server" id="lblInterviewCandidateInformation"></label></asp:Label></h5>
                                            </div>
                                            
                                             <div class="col-md-12">
                                              <h5>Interview Board Information: <asp:Label runat="server" ID="PInterviewBoardInformation" class="label  w3-tag w3-green" style="font-size: 12px;"><label runat="server" id="lblInterviewBoardInformation"></label></asp:Label></h5>
                                            </div>
                                            
                                              <div class="col-md-12">
                                              <h5>Interview Candidate Invitation: <asp:Label runat="server" ID="PInterviewCandidateInvitation" class="label  w3-tag w3-green" style="font-size: 12px;" ><label runat="server" id="lblInterviewCandidateInvitation"></label></asp:Label></h5>
                                            </div>
                                          <div class="col-md-12">
                                              <h5> Candidate Attandance: <asp:Label runat="server" ID="PCandidateAttandance" class="label  w3-tag w3-green" style="font-size: 12px;" ><label runat="server" id="lblCandidateAttandance"></label></asp:Label></h5>
                                            </div>
                                            
                                            
                                               <div class="col-md-12">
                                              <h5> Interview Board Member Marks Entry: <asp:Label runat="server" ID="PMarksEntry"  class="label  w3-tag w3-green" style="font-size: 12px;" ><label runat="server" id="lblMarksEntry"></label></asp:Label></h5>
                                            </div>
                                            
                                            
                                              <div class="col-md-12">
                                              <h5> Employee Information: <asp:Label runat="server" ID="PEmployeeInformation" CssClass="label  w3-tag w3-green" style="font-size: 12px;" ><asp:Label runat="server" id="lblEmployeeInformation"></asp:Label></asp:Label></h5>
                                            </div>
                                            <div class="col-md-12">
                                              <h5> Recruitment Process Current Status: <asp:Label runat="server" ID="Label1" CssClass="label  w3-tag w3-green" style="font-size: 12px;" ><asp:Label runat="server" id="lblLast"></asp:Label></asp:Label></h5>
                                            </div>
                                             
                                        </div>
                                        <div class="modal-footer">
                                        </div>
                                    </div>
                                </div>
                            </div>

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
                                        <asp:DropDownList ID="financialYearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Department </label>

                                        <asp:DropDownList ID="deptDropDownList" CssClass="form-control form-control-sm  clsSelect" runat="server"></asp:DropDownList>
                                        
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



                           <div class="row">
                               <div class="col-md-12">
                                   
                                     
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
                           filename: 'Job Requisition List',
                           title: 'SMC',
                           messageTop: 'Job Requisition List',
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
                                  filename: 'Job Requisition List',
                                  title: 'SMC',
                                  messageTop: 'Job Requisition List',
                                  exportOptions: {
                                      columns: [0,  2, 3, 4, 5]
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
                                   <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                     CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   DataKeyNames="JobReqId, ActionStatus"
                                    OnRowCommand="loadGridView_RowCommand" ShowFooter="False">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="ActionStatus" runat="server" Value='<%#Eval("ActionStatus") %>' />
                                                <asp:HiddenField ID="JobReqId" runat="server" Value='<%#Eval("JobReqId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Print">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("JobReqId") %>'
                                                    CommandName="Preview" ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="ReqCode" HeaderText="Requisition Code" />--%>
                                        <%--    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
                                        <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />
                                        <asp:BoundField DataField="JobTitle" HeaderText="Designation" />
                                        <asp:BoundField DataField="ReqDate" HeaderText="Requisition Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="Nos" HeaderText="Total Vacancy" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                      <%--  <asp:BoundField DataField="ActionStatus" HeaderText=" " />--%>
                                        <asp:BoundField DataField="ActionStatus2" HeaderText="Approval Status" />
                                        
                                        <asp:BoundField DataField="EmpName" HeaderText="Awaiting Employee" />
                                        <%--RecruitmentStatus--%>
                                        <asp:BoundField DataField="jobCirculation" Visible="False" HeaderText="Job Circulation" />

                                        <%--              <asp:BoundField DataField="GradeName" HeaderText="Grade Name" />--%>
                                        <asp:TemplateField HeaderText="Submit" Visible="False">
                                            <ItemTemplate>
                                                
                                                <asp:linkbutton runat="server" cssclass="btn btn-primary btn-xs" backcolor="#0069D9" tooltip="Click To Change Action Status" borderstyle="None" onclick="btnSubmit_OnClick" id="btnSubmit"  >Submit</asp:linkbutton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="View Status">
                                            <ItemTemplate>
                                                   <asp:LinkButton runat="server" CausesValidation="false" CssClass="btn btn-info btn-sm"  ToolTip="Click To View Status" backcolor="#0069D9" borderstyle="None" OnClick="ShowPopup" ID="btnStatus">Status</asp:LinkButton>
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
                               </div>
                           </div>
                            <br/>
                            <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>
                        </div>
                          <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>  <br/>
                    </div>
                </div>
            </ContentTemplate>
           
          <Triggers>
      <asp:PostBackTrigger ControlID="loadGridView"  />
</Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

