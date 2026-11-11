<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="HealthcareReport.aspx.cs" Inherits="HealthCare_UI_HealthcareReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">


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

        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                
                
                     <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWaeeeewit" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>  

                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Health Care Report </h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block" runat="server" >
                            
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
            <label>Employee Name: </label>
            
              <asp:DropDownList runat="server" ID="ddlEmpInfo" class="form-control form-control-sm selectme" />
         
            
             

        </div>
    </div>

                                      <div class="col-md-2">
    <div class="form-group">
        <label>Employee Status</label>
        <asp:DropDownList runat="server" ID="ActiveStatusDropDownList" CssClass="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ActiveStatusDropDownList_OnSelectedIndexChanged">
            <asp:ListItem Text="All" Value="-1"></asp:ListItem>
            <asp:ListItem  Text="Active" Selected="True" Value="1"></asp:ListItem>
            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>

                                 <div class="col-md-2">
                                     <div class="form-group">
                                         <label>Financial Year</label>
                                         <asp:DropDownList ID="ddlFinancialYear"  runat="server" class="form-control form-control-sm selectme"></asp:DropDownList>
                                     </div>
                                 </div>
                                                              <div class="col-2">
<div class="form-group">
    <label>Division</label>
    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="form-control form-control-sm selectme" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
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
                                         <label>Bill Type</label>
                                         
                                         <asp:DropDownList id="ddlType" CssClass="form-control form-control-sm selectme" runat="server">  

                                          
                                             <asp:ListItem value="OPD">  
                                                 OPD 
                                             </asp:ListItem>
                                             <asp:ListItem Selected="True" value="IPD" >  
                                                 IPD   
                                             </asp:ListItem>  
                                             
                                             <asp:ListItem value="Special" >  
                                                 Special   
                                             </asp:ListItem>  

                                         </asp:DropDownList>

                                     </div>
                                 </div>
                                 
                              

                                 <div class="col-md-2">
                                     <div class="form-group">
                                         <label>Bill Status</label>
                                         
                                         <asp:DropDownList id="ddlActoinStatus" CssClass="form-control form-control-sm selectme" runat="server">  

<%--                                             <asp:ListItem value="Draft">  
                                                 Draft   
                                             </asp:ListItem>--%>
                                             <asp:ListItem value="Review">  
                                                 Return 
                                             </asp:ListItem>
                                             <asp:ListItem value="Verified" >  
                                                 Verified   
                                             </asp:ListItem>  
                                             <asp:ListItem  Selected="True" value="Approved">  
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
              var table = $('#cpFormBody_gv_JdBoard').DataTable(
                   {
                       "bInfo": true,
                       "bFilter": true,
                       lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                       pageLength: 10 
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
                              pageLength: 10 

                          }
                          );
                      }
                  });
              };


              table.columns().every(function () {
                 
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
                            
                        

                             
                            <asp:CheckBox runat="server" ID="isKPI" Text="KPI" Visible="False" />
                                            <asp:HiddenField ID="hfMainAppraisalMasterId" runat="server"  />
                                            <asp:HiddenField ID="hfMasterId" runat="server"  />
                        
                            
                            
                            
                          
                            
                           
                                                                                                

                             <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;" runat="server" >
                                 
                                 
                                                       <div class="form-row">
                            
                            <div class="col-md-10"></div>

                            <div class="col-md-2">
                                <div class="form-group" style="margin-top: 18px;">
                                    <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
                                </div>
                            </div>
                        </div>

                                 <br />
                             
                             <asp:GridView  ID="gv_OPD"    ShowFooter="True" runat="server" AutoGenerateColumns="False" 
                                            CssClass="table table-bordered text-center thead-dark"    OnRowCommand="gv_JdBoard_OnRowCommand" >
                             <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>

                                            <asp:Label ID="Label1" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
        <asp:BoundField DataField="EmpMasterCode" HeaderText="Emp ID." />
        <asp:BoundField DataField="EmpName" HeaderText="Name of Employee" />
        <asp:BoundField DataField="Designation" HeaderText="Designation" />
        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
       <asp:BoundField DataField="SectionName" HeaderText="Section" />
        <asp:BoundField DataField="SalaryLocation" HeaderText="Job Location" />
        <asp:BoundField DataField="Illness" HeaderText="Name of Patient" />
        <asp:BoundField DataField="HospitalName" HeaderText="Hospital Name" />
        <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
        <asp:BoundField DataField="DocVisitDate" HeaderText="Dr. visit date(s)" />
        <asp:BoundField DataField="SubmitDate" HeaderText="Bill received date by HRD" />
        <asp:BoundField DataField="PatientName" HeaderText="Name of illness" />
        <asp:BoundField DataField="DocumentStatus" HeaderText="Attached Documents" />
        <asp:BoundField DataField="ApplicableAmount" HeaderText="Claim Amount" />
        <asp:BoundField DataField="Ceilling" HeaderText="Actual amount to be disbursed (in BDT)" />
        <asp:BoundField DataField="RemainingBalance" HeaderText="Remaining balance" />


                                </Columns>
                                 
                            </asp:GridView>


                                 <asp:GridView ID="gv_IPD"    ShowFooter="True" runat="server" AutoGenerateColumns="false"   CssClass="table table-bordered text-center thead-dark" >
    <Columns>

                <asp:TemplateField HeaderText="SL#">
            <ItemTemplate>

                <asp:Label ID="Label1" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
    

            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="EmpMasterCode" HeaderText="Emp ID." />
        <asp:BoundField DataField="EmpName" HeaderText="Name of Employee" />
        <asp:BoundField DataField="Designation" HeaderText="Designation" />
        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
        <asp:BoundField DataField="SectionName" HeaderText="Section" />
        <asp:BoundField DataField="SalaryLocation" HeaderText="Job Location" />
        <asp:BoundField DataField="Illness" HeaderText="Name of Patient" />
        <asp:BoundField DataField="HospitalName" HeaderText="Hospital Name" />
        <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
        <asp:BoundField DataField="DateOfAdmission" HeaderText="Date of Admission" />
        <asp:BoundField DataField="DateOfDischarge" HeaderText="Date of Discharge" />
        <asp:BoundField DataField="SubmitDate" HeaderText="Received by HRD" />
        <asp:BoundField DataField="PatientName" HeaderText="Name of Illness" />
        <asp:BoundField DataField="ApplicableAmount" HeaderText="Claim Amount" />
        <asp:BoundField DataField="Ceilling" HeaderText="Actual Amount to be Disbursed" />
        <asp:BoundField DataField="RemainingBalance" HeaderText="Remaining Balance & Remarks" />
    </Columns>
</asp:GridView>

                            </div>

                        
                            
                            
                            
                            
                            
                            
                              <div id="gridContainer14" style="height: auto; overflow: auto; width: auto;" runat="server" Visible="False">

                                  
                                                       <div class="form-row">
                            
                            <div class="col-md-10"></div>

                            <div class="col-md-2">
                                <div class="form-group" style="margin-top: 18px;">
                                    <asp:LinkButton ID="btnReturnExport" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnReturnExport_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                             
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
                                     <asp:BoundField DataField="SectionName" HeaderText="Section" />
                                       <asp:TemplateField HeaderText="Office">
                                        <ItemTemplate>
                                            <asp:Label ID="Office" runat="server"   Text='<%#Eval("SalaryLocation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="DateOfConformation" HeaderText="Date Of Conformation" 
    DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="False" />

                                    <asp:TemplateField HeaderText="Application Type ">
                                        <ItemTemplate>
                                            <asp:Label ID="ApplicationType" runat="server"   Text='<%#Eval("Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:BoundField DataField="HospitalName" HeaderText="Hospital Name" />
                                    
                                        <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="Amount" runat="server"   Text='<%#Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Dpt. Approved Date">
                                        <ItemTemplate>
                                            <asp:Label ID="ApprovedDate" runat="server"   Text='<%# Eval("ApprovedDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
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
                       <asp:PostBackTrigger ControlID="btnReturnExport" />
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

