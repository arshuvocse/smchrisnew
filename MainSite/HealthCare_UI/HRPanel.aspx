<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="HRPanel.aspx.cs" Inherits="HealthCare_UI_HRPanel" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    < 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">

    
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
    
      
    

  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.css" integrity="sha512-nNlU0WK2QfKsuEmdcTwkeh+lhGs6uyOxuUs+n+0oXSYDok5qy0EI0lt01ZynHq6+p/tbgpZ7P+yUb+r71wqdXg==" crossorigin="anonymous" referrerpolicy="no-referrer" />

<script src="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.js" integrity="sha512-j7/1CJweOskkQiS5RD9W8zhEG9D9vpgByNGxPIqkO5KrXrwyDAroM9aQ9w8J7oRqwxGyz429hPVk/zR6IOMtSA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    
              <style>
        .pagination .page-item.active .page-link  {
            background-color: #007BFF !important;
            border-style: none !important;
            /* W3C */
        }

        
    </style>
<%--    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
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
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.print.min.js"></script>--%>

       <script>

           $(document).ready(function () {
               // Setup - add a text input to each footer cell


               // DataTable
               var table = $('#cpFormBody_gv_JdBoard').DataTable(
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
                               filename: ' HR Panel',
                               title: 'SMC',
                               messageTop: ' HR Panel',
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
                           table = $('#cpFormBody_gv_JdBoard').DataTable(
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
                                           filename: ' HR Panel',
                                           title: 'SMC',
                                           messageTop: ' HR Panel',
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

      <div class="content">

         <div class="modal fade" id="exampleModal23" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
   <div class="modal-dialog  modal-lg" style=" width: 90% !important; " role="document">
  <asp:UpdatePanel runat="server">
      <ContentTemplate>
              <div class="modal-content">
         <div class="modal-header">
            

             <h3 class="modal-title" id="exampleModalLabel2"  style="color:#2196F3; text-shadow:  0 0 1px black;">Feedback List </h3>

          
         </div>
         <div class="modal-body">
                 <div class="row">
                            <div class="col-md-12">

                              <div class="row">
                                  <div class="col-md-12">
                                        <asp:GridView Width="100%" ShowHeader="True" ID="gv_EmpListSearch" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Feedback">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Family" runat="server" Text='<%#Eval("Feedback") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                  </div>
                              </div>
                                                        
      
       
              <br/>
       <div class="row">
           <div class="col-4"></div>
           <div class="col-4"></div>
         
       </div>
                            </div>


                        </div>
             
              
         </div>
         <div class="modal-footer"> <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button> </div>
      </div>
      </ContentTemplate>
  </asp:UpdatePanel>
   </div>
</div>
          
      
         <div class="modal fade" id="MDModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
   <div class="modal-dialog  modal-lg" style=" width: 90% !important; " role="document">
  <asp:UpdatePanel runat="server">
      <ContentTemplate>
              <div class="modal-content">
         <div class="modal-header">
            

             <h3 class="modal-title" id="ddd"  style="color:#2196F3; text-shadow:  0 0 1px black;">Forward To MD </h3>

          
         </div>
         <div class="modal-body">
                 <div class="row">
                            <div class="col-md-12">

                              <div class="row">
                                  
                                         <div class="col-md-3">

                                    <asp:TextBox runat="server" ID="txtcomments" TextMode="MultiLine" Rows="2" Columns="3" placeholder="Comments" CssClass="form-control "></asp:TextBox>

                                </div>

                                <div class="col-md-3">

                                    <asp:LinkButton runat="server" ID="btnFtoMD" OnClick="btnFtoMD_OnClick" CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Submit Information </asp:LinkButton>

                                </div>
                                 
                              </div>
                                                        
      
       
              <br/>
       <div class="row">
           <div class="col-4"></div>
           <div class="col-4"></div>
         
       </div>
                            </div>


                        </div>
             
              
         </div>
         <div class="modal-footer"> <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button> </div>
      </div>
      </ContentTemplate>
   </asp:UpdatePanel>
   </div>
</div>
          
      
         <div class="modal fade" id="DoctorModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
   <div class="modal-dialog  modal-lg" style=" width: 90% !important; " role="document">
  <asp:UpdatePanel runat="server">
      <ContentTemplate>
              <div class="modal-content">
         <div class="modal-header">
            

             <h3 class="modal-title" id="DoctorModalLabel"  style="color:#2196F3; text-shadow:  0 0 1px black;">Forward To Doctor </h3>

          
         </div>
         <div class="modal-body">
                 <div class="row">
                            <div class="col-md-12">

                              <div class="row align-items-end">
                                  <div class="col-md-10 offset-md-1">
                                      <div class="row align-items-end">
                                          <div class="col-md-8 col-sm-12">
                                              <div class="form-group mb-2">
                                                  <label class="font-weight-bold">Comment</label>
                                                  <asp:TextBox runat="server" ID="txtDoctorComment" TextMode="MultiLine" Rows="3" Columns="3" placeholder="Add comment for doctor" CssClass="form-control form-control-sm"></asp:TextBox>
                                                  <asp:RequiredFieldValidator ID="rfvDoctorComment" runat="server" ControlToValidate="txtDoctorComment" ValidationGroup="DoctorForward" CssClass="text-danger small" ErrorMessage="Comment is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                              </div>
                                          </div>
                                          <div class="col-md-4 col-sm-12">
                                              <asp:LinkButton runat="server" ID="btnSubmitDoctor" OnClick="btnSubmitDoctor_OnClick" ValidationGroup="DoctorForward" CssClass="btn btnMyDesignSearch btn-block btn-sm mt-2"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Submit Information </asp:LinkButton>
                                          </div>
                                      </div>
                                  </div>
                              </div>
                                                        
      
       
              <br/>
       <div class="row">
           <div class="col-4"></div>
           <div class="col-4"></div>
         
       </div>
                            </div>


                        </div>
             
              
         </div>
         <div class="modal-footer"> <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button> </div>
      </div>
      </ContentTemplate>
  </asp:UpdatePanel>
   </div>
</div>

        <div class="modal fade" id="HRRejectModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
   <div class="modal-dialog  modal-lg" style=" width: 90% !important; " role="document">
  <asp:UpdatePanel runat="server">
      <ContentTemplate>
              <div class="modal-content">
         <div class="modal-header">
            

             <h3 class="modal-title" id="HRRejectModalLabel"  style="color:#dc3545; text-shadow:  0 0 1px black;">HR Rejection</h3>

          
         </div>
         <div class="modal-body">
                 <div class="row">
                            <div class="col-md-12">

                              <div class="row align-items-end">
                                  <div class="col-md-10 offset-md-1">
                                      <div class="form-group mb-2">
                                          <label class="font-weight-bold text-danger">Rejection Comment</label>
                                          <asp:TextBox runat="server" ID="txtHRRejectComment" TextMode="MultiLine" Rows="3" Columns="3" placeholder="Add rejection reason" CssClass="form-control form-control-sm"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="rfvHRRejectComment" runat="server" ControlToValidate="txtHRRejectComment" ValidationGroup="HRReject" CssClass="text-danger small" ErrorMessage="Comment is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                      </div>
                                  </div>
                              </div>
                                                        
      
       
              <br/>
       <div class="row">
           <div class="col-4"></div>
           <div class="col-4"></div>
         
       </div>
                            </div>


                        </div>
             
              
         </div>
         <div class="modal-footer">
             <asp:LinkButton runat="server" ID="btnSubmitHRReject" OnClick="btnSubmitHRReject_OnClick" OnClientClick="hideHRRejectModal();" ValidationGroup="HRReject" CssClass="btn btn-danger btn-sm"><span aria-hidden="true" class="fa fa-times"></span>  &nbsp; Reject Application </asp:LinkButton>
             <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
         </div>
      </div>
      </ContentTemplate>
  </asp:UpdatePanel>
   </div>
</div>

        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                
                
                     <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="upFormBody">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWaeeeewit" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>  

                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> HR Panel </h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block" runat="server" >
                            
                            <a href="HRPanelForAllBill.aspx" target="_blank"> all Bills Submit</a>

                           <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                           
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
                             <div class="form-row">

                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm selectme" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                    

                                </div>

                                 <div class="col-md-2">
                                     <div class="form-group">
                                         <label>Financial Year</label>
                                         <asp:DropDownList ID="ddlFinancialYear"  runat="server" class="form-control form-control-sm selectme"></asp:DropDownList>
                                     </div>
                                 </div>
                                 
                                 
                                 <div class="col-md-2">
                                    
                                     <div class="form-group">
                                         <label>Department</label>
                                         <asp:DropDownList runat="server" ID="ddlDept" CssClass="form-control form-control-sm selectme" />
                                        
                                         <script type="text/javascript">
                                             function pageLoad() {
                                                 $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                             }
                                         </script>

                                     </div>
                                 </div>
                                 
                                 <div class="col-md-2">
                                     <div class="form-group">
                                         <label>Type</label>
                                         
                                         <asp:DropDownList id="ddlType" CssClass="form-control form-control-sm selectme" runat="server">  

                                             <asp:ListItem value="">  
                                                 Select From List   
                                             </asp:ListItem>
                                             <asp:ListItem value="OPD">  
                                                 OPD 
                                             </asp:ListItem>
                                             <asp:ListItem value="IPD" >  
                                                 IPD   
                                             </asp:ListItem>  
                                             
                                             <asp:ListItem value="Special" >  
                                                 Special   
                                             </asp:ListItem>  

                                         </asp:DropDownList>

                                     </div>
                                 </div>
                                 
                             </div>
                        
                        <div class="form-row">

                                 <div class="col-md-2">
                                     <div class="form-group">
                                         <label>Approval Status</label>
                                         
                                         <asp:DropDownList id="ddlActoinStatus" CssClass="form-control form-control-sm selectme" runat="server">  

<%--                                             <asp:ListItem value="Draft">  
                                                 Draft   
                                             </asp:ListItem>--%>
                                             <asp:ListItem value="Rejected">  
                                                 Rejected 
                                             </asp:ListItem>

                                              <asp:ListItem value="Review">  
     Return 
 </asp:ListItem>
                                             <asp:ListItem value="Verified" Selected="True">  
                                                 Verified   
                                             </asp:ListItem>  
                                             <asp:ListItem value="Approved">  
                                                 Approved   
                                             </asp:ListItem> 
                                         </asp:DropDownList> 

                                     </div>

                                 </div>
                                 
                            
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application From Date </label>
                                    
                                    <asp:TextBox runat="server" class="form-control form-control-sm" ID="App_FromDate"></asp:TextBox>
                                    
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="App_FromDate" />
                                </div>
                            </div>
                            
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application To Date </label>
                                    <asp:TextBox runat="server" class="form-control form-control-sm" ID="App_ToDate"></asp:TextBox>
                                    
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="App_ToDate" />
                                </div>
                            </div>

                        </div>
                        
                        <div class="form-row">
                            <div class="col-md-5"></div>
                            <div class="col-md-2">
                                <div class="form-group" style="margin-top: 13px;">
                                    <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>
                                      
                                </div>
                            </div>
                        </div>
                            
                            


    

                                
  <script>


      function hideHRRejectModal() {
          setTimeout(function () {
              $('#HRRejectModal').modal('hide');
              $('body').removeClass('modal-open');
              $('.modal-backdrop').remove();
              $('#UpdateProgress2, .divWaiting').hide();
          }, 0);
      }



  </script>
    
    
                        

                             
                            <asp:CheckBox runat="server" ID="isKPI" Text="KPI" Visible="False" />
                                            <asp:HiddenField ID="hfMainAppraisalMasterId" runat="server"  />
                                            <asp:HiddenField ID="hfMasterId" runat="server"  />
                                            <asp:HiddenField ID="hfDoctorForwardId" runat="server" />
                                            <asp:HiddenField ID="hfRejectMasterId" runat="server" />
                                            <asp:HiddenField ID="hfRejectForEmpId" runat="server" />
                        
                            
                            
                            
                          
                            
                           
                                                                                                

                             <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;" runat="server" >
                                 
                                 
                                                       <div class="form-row">
                            
                            <div class="col-md-10"></div>

                            <div class="col-md-2">
                                <div class="form-group" style="margin-top: 18px;">
                                    <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
                                </div>
                            </div>
                        </div>

                             
                             <asp:GridView  ID="gv_JdBoard"  runat="server" AutoGenerateColumns="False" 
                                            CssClass="table table-bordered text-center thead-dark"  DataKeyNames="ForEmpInfoId,ReimbursFromMasterId,HR"  OnRowCommand="gv_JdBoard_OnRowCommand" >
                             <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>

                                            <asp:Label ID="Label1" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                   <%--         <asp:Label ID="LabelSL"  runat="server"></asp:Label>--%>
                                         <%--   <asp:HiddenField ID="hfAppraisalMasterId" runat="server" Value='<%#Eval("AppraisalMasterId") %>' />--%>
                                            <asp:HiddenField ID="hfAppraisalSelfMasterId" runat="server" Value='<%#Eval("ReimbursFromMasterId") %>' />
                                            <asp:HiddenField ID="EmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                            <asp:HiddenField ID="FinancialYearId" runat="server" Value='<%#Eval("FinancialYearId") %>' />
                                            <asp:HiddenField ID="hfActionStatus" runat="server" Value='<%#Eval("ActionStatus") %>' />
                                            <asp:HiddenField ID="HFIsMDApproval" runat="server" Value='<%#Eval("IsMDApproval") %>' />
                                            <asp:HiddenField ID="hfSalaryLoationId" runat="server" Value='<%#Eval("SalaryLoationId") %>' />
                                            <asp:HiddenField ID="hfApplicationType" runat="server" Value='<%#Eval("ApplicationType") %>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Financial Year">
                                        <ItemTemplate>
                                            <asp:Label ID="FinancialYearDesc" runat="server"   Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="employfee" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="emeepxzloyee" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="emploezxzeyee" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="DepartmentName" runat="server"   Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                              <asp:BoundField DataField="SectionName" HeaderText="Section" />  
                                       <asp:TemplateField HeaderText="Office">
                                        <ItemTemplate>
                                            <asp:Label ID="Office" runat="server"   Text='<%#Eval("SalaryLocation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                         <asp:BoundField DataField="DateOfConformation" HeaderText="Date Of Conformation" /> 
                                    <asp:TemplateField HeaderText="Application Type ">
                                        <ItemTemplate>
                                            <asp:Label ID="ApplicationType" runat="server"   Text='<%#Eval("ApplicationType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                        <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="Amount" runat="server"   Text='<%#Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                   

                                     <asp:TemplateField HeaderText="Doctor Feedback">
                                        <ItemTemplate>
                                            <asp:Label ID="ActizxxzonStatus" runat="server"  ToolTip='<%#Eval("Feedback") %>'  Text='<%#Eval("FeedbackStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pending to">
                                        <ItemTemplate>
                                            <asp:Label ID="Awaiting" runat="server"   Text='<%#Eval("PendingEmp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Approved From Meeting">
                                        <ItemTemplate>
                                            <asp:Label ID="FromMeeting" runat="server"   Text='<%#Eval("MeetingStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                  <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:d-MMM-yyyy}" HtmlEncode="false" />  
                                  <asp:BoundField DataField="SubmitDate" HeaderText="Submit Date"  DataFormatString="{0:d-MMM-yyyy}" HtmlEncode="false" />
                                    <asp:TemplateField HeaderText="Dpt. Approved Date">
     <ItemTemplate>
         <asp:Label ID="ApprovedDate" runat="server"   Text='<%# Eval("ApprovedDate", "{0:d-MMM-yyyy}") %>'>></asp:Label>
     </ItemTemplate>
 </asp:TemplateField>
                                     <asp:BoundField DataField="HeadofDptDate" HeaderText="HoD Approved Date" DataFormatString="{0:d-MMM-yyyy}" HtmlEncode="false" />  
                                 
                                                  <asp:BoundField DataField="ForwardtoDoctorDate" HeaderText="Doctor's Recommendation Date"  DataFormatString="{0:d-MMM-yyyy}" HtmlEncode="false" />  

                                  <asp:BoundField DataField="FinalApproveDate" HeaderText="Approve Date"   DataFormatString="{0:d-MMM-yyyy}" HtmlEncode="false" />  
                                 
                              <%--    <asp:BoundField DataField="SectionName" HeaderText="Supervisor's Recommendatio Date" />  --%>

                                              

                                                  <asp:BoundField DataField="CommeApprovedDate" DataFormatString="{0:d-MMM-yyyy}" HtmlEncode="false"  HeaderText="Committee Approve Date" />  

                             <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_print" OnClick="btn_print_OnClick" CssClass="btn btn-sm btn-info" ><i class="fa fa-eye" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 
                                 
                                 
                                                                                                                                      
                                    <asp:TemplateField HeaderText="Change Payment">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_view" OnClick="btn_eview_OnClick" CssClass="btn btn-sm btnMyDesignEdit" ><i class="fa fa-edit" aria-hidden="true"></i>
 </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                       
                                    <asp:TemplateField HeaderText="Forward to Doctor">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_ForwardDoctor" OnClick="btn_ForwardDoctor_OnClick" CssClass='<%#Eval("ForwardtoDoctorCSS") %>' Enabled='<%# Eval("ForwardtoDoctor").ToString().Equals("Yes".ToString()) ? Convert.ToBoolean(0) : Convert.ToBoolean(1) %>'
 ><%#Eval("ForwardtoDoctor") %>
 </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField HeaderText="View Committee Feedback">
                                        <ItemTemplate>
                                            <asp:LinkButton  runat="server" ID="btn_PopUp"    OnClick="btn_Fview_OnClick" CssClass="btn btn-sm btnMyDesignOne" ><i class="fa fa-square fa-inverse"   aria-hidden="true"></i>

                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    

                                    <asp:TemplateField HeaderText="Forward To MD">
                                        <ItemTemplate>
                                            <asp:LinkButton  runat="server" ID="btn_MD" OnClick="btn_MD_OnClick"  CssClass="btn btn-sm btnMyDesignOne"   > <%#Eval("MDApproval") %>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="MD Approval Status">
                                        <ItemTemplate>
                                            <asp:Label ID="MDApprovalStatus" runat="server"   Text='<%#Eval("MDAppStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Is MD Approval">
                                        <ItemTemplate>
                                            <asp:Label ID="IsMDApproval" runat="server"   Text='<%#Eval("MDApproval") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Application Return">
                                        <ItemTemplate>
                                            <asp:LinkButton  runat="server" ID="btn_Return" OnClick="btn_Return_OnClick"  CssClass="btn btn-sm btnMyDesignOne" ><i class="fa fa-undo"   aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HR Reject">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_Reject" OnClick="btn_Reject_OnClick" CssClass="btn btn-sm btn-danger"><i class="fa fa-times" aria-hidden="true"></i> Reject</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                 <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                            </asp:GridView>
                            </div>

                             <div style="display: none">  <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_Export"    CssClass="AddToListCssTable" DataKeyNames="ForEmpInfoId">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                         <%--   <asp:HiddenField ID="hfAppraisalMasterId" runat="server" Value='<%#Eval("AppraisalMasterId") %>' />--%>
                                            <asp:HiddenField ID="hfAppraisalSelfMasterId" runat="server" Value='<%#Eval("ReimbursFromMasterId") %>' />
                                            <asp:HiddenField ID="EmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                            <asp:HiddenField ID="FinancialYearId" runat="server" Value='<%#Eval("FinancialYearId") %>' />
                                            <asp:HiddenField ID="HFIsMDApproval" runat="server" Value='<%#Eval("IsMDApproval") %>' />
                                            
                                            <asp:HiddenField ID="hfSalaryLoationId" runat="server" Value='<%#Eval("SalaryLoationId") %>' />
                                            <asp:HiddenField ID="hfApplicationType" runat="server" Value='<%#Eval("ApplicationType") %>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Financial Year">
                                        <ItemTemplate>
                                            <asp:Label ID="FinancialYearDesc" runat="server"   Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="employfee" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="emeepxzloyee" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="emploezxzeyee" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="DepartmentName" runat="server"   Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                       <asp:TemplateField HeaderText="Office">
                                        <ItemTemplate>
                                            <asp:Label ID="Office" runat="server"   Text='<%#Eval("SalaryLocation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Application Type">
                                        <ItemTemplate>
                                            <asp:Label ID="ApplicationType" runat="server"   Text='<%#Eval("ApplicationType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                        <asp:TemplateField HeaderText="Application Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="Amount" runat="server"   Text='<%#Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                      <asp:TemplateField HeaderText="Dpt. Approved Date">
                                        <ItemTemplate>
                                            <asp:Label ID="ApprovedDate" runat="server"   Text='<%#Eval("ApprovedDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                             <%--       <asp:TemplateField HeaderText="Set KPI" Visible="False">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_edit" OnClick="btn_edit_OnClick" Text="Set KPI"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    
                                     <asp:TemplateField HeaderText="Doctor Feedback Status">
                                        <ItemTemplate>
                                            <asp:Label ID="ActizxxzonStatus" runat="server"    Text='<%#Eval("FeedbackStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                       <asp:TemplateField HeaderText="Doctor Feedback">
                                        <ItemTemplate>
                                            <asp:Label ID="ActizxxsazonStatus" runat="server"     Text='<%#Eval("Feedback") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                               <%--     <asp:TemplateField HeaderText="Approval Status">
                                        <ItemTemplate>
                                            <asp:Label ID="ActionStatus" runat="server"   Text='<%#Eval("ActionStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Waiting Employee">
                                        <ItemTemplate>
                                            <asp:Label ID="Awaiting" runat="server"   Text='<%#Eval("PendingEmp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Approved From Meeting">
                                        <ItemTemplate>
                                            <asp:Label ID="FromMeeting" runat="server"   Text='<%#Eval("MeetingStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                  

                                <%--    <asp:TemplateField HeaderText="Appraisal Approval Status">
                                        <ItemTemplate>
                                            <asp:Label ID="Awaitingf" runat="server" Text='<%#Eval("ActionStatusAppraisal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Appraisal Awaiting Employee">
                                        <ItemTemplate>
                                            <asp:Label ID="Awaitingh" runat="server"   Text='<%#Eval("PendingEmpApp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reset Approval Person">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_KPI"  OnClick="btn_KPI_OnClick" CssClass="btn btn-xs btn-KPI" >KPI
                                            </asp:LinkButton>
                                            <div style="padding-top: 3px!important"></div>
                                            &nbsp;
                                            &nbsp;
                                             <asp:LinkButton runat="server" ID="btnApprisal" OnClick="btnApprisal_OnClick" CssClass="btn btn-xs btn-Appraisal" >Appraisal
                                            </asp:LinkButton>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cancel Appraisal Approval">
                                           <ItemTemplate>
                                          <asp:LinkButton runat="server" ID="lbApprisalCancel" OnClick="btnApprisalCancel_OnClick" CssClass="btn btn-xs btn-CancelApp" ToolTip="Click to Cancel Appraisal Approval" >Cancel
                                            </asp:LinkButton>
                                      </ItemTemplate>
                                    
                                    </asp:TemplateField>--%>
                                                                                                     
                                    <asp:TemplateField HeaderText="Is MD Approval">
                                        <ItemTemplate>
                                            <asp:Label ID="IsMDApproval" runat="server"   Text='<%#Eval("MDApproval") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MD Approval Status">
                                        <ItemTemplate>
                                            <asp:Label ID="MDApprovalStatus" runat="server"   Text='<%#Eval("MDAppStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                  

                                </Columns>
                            </asp:GridView></div>   
                            
                            
                            
                            
                            
                            
                              <div id="gridContainer14" style="height: auto; overflow: auto; width: auto;" runat="server" Visible="False">


                             
                             <asp:GridView  ID="GridView1"  runat="server" AutoGenerateColumns="False" 
                                            CssClass="table table-bordered text-center thead-dark"  DataKeyNames="ForEmpInfoId,ReimbursFromMasterId"  OnRowCommand="gv_JdBoard_OnRowCommand" >
                             <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>

                                            <asp:Label ID="Label1" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
              

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Financial Year">
                                        <ItemTemplate>
                                            <asp:Label ID="FinancialYearDesc" runat="server"   Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="employfee" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="emeepxzloyee" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="emploezxzeyee" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="DepartmentName" runat="server"   Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                       <asp:TemplateField HeaderText="Office">
                                        <ItemTemplate>
                                            <asp:Label ID="Office" runat="server"   Text='<%#Eval("SalaryLocation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                         <asp:BoundField DataField="DateOfConformation" HeaderText="Date Of Conformation" /> 
                                    <asp:TemplateField HeaderText="Application Type ">
                                        <ItemTemplate>
                                            <asp:Label ID="ApplicationType" runat="server"   Text='<%#Eval("Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                        <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="Amount" runat="server"   Text='<%#Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Dpt. Approved Date">
                                        <ItemTemplate>
                                            <asp:Label ID="ApprovedDate" runat="server"   Text='<%#Eval("ApprovedDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                     
                                    
                                      <asp:TemplateField HeaderText="Return Date">
                                        <ItemTemplate>
                                            <asp:Label ID="ReturnApproveDate" runat="server"   Text='<%#Eval("ReturnApproveDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                     


                              <%--      <asp:TemplateField HeaderText="Approved From Meeting">
                                        <ItemTemplate>
                                            <asp:Label ID="FromMeeting" runat="server"   Text='<%#Eval("MeetingStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                                                                                                                                                                                                                                                          
                                </Columns>
                                 <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                            </asp:GridView>
                            </div>
                            

                          

                              <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>  <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>  <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                        </div>
                          <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>  <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>  <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    </div>
                </div>
            </ContentTemplate>
                 <Triggers>
                       <asp:PostBackTrigger ControlID="btnExportToExcel" />
                 </Triggers>
        </asp:UpdatePanel>

    </div>

<style>.GridPager a,
       .GridPager span {
           display: inline-block;
           padding: 3px 14px;
           margin-right: 8px;
           border-radius: 3px;
           height: 20px;
           border: solid 1px #c0c0c0;
           background: #e9e9e9;
           box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
           font-size: 14px;
           font-weight: bold;
           text-decoration: none;
           color: #717171;
           text-shadow: 0px 1px 0px rgba(255,255,255, 1);
       }

       .GridPager a {

           background-color: #f5f5f5;
           color: #969696;
           border: 1px solid #969696;
       }

       .GridPager span {

           background: #616161;
           box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
           color: #f0f0f0;
           text-shadow: 0px 0px 3px rgba(0,0,0, .5);
           border: 1px solid #3AC0F2;
       }

</style>
</asp:Content>

