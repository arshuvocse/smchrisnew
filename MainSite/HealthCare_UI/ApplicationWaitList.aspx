<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ApplicationWaitList.aspx.cs" Inherits="HealthCare_UI_ApplicationWaitList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
       <div class="content">
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
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Application Waiting list </h1>
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
                                <div class="col-md-2" >
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
                                 
                                 
                                   <div class="col-md-2" style="display: none">
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
                                         <label>Applicant Name </label>
                                         
                                         <asp:DropDownList id="ddlEmployee" CssClass="form-control form-control-sm selectme" runat="server">  
                                        
                                         </asp:DropDownList> 
                                         
                                     </div>
                                 </div>
                                                                                                                                                                                       

                                 </div>
                        
                        <div class="form-row">
                            
                            <div class="col-md-5"></div>
                            <div class="col-md-2">
                                <div class="form-group" style="padding-left: 10%">
                                    <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>
                                      
                                </div>
                            </div>
                        </div>
                            
                            

                            <div class="form-row" style="display: none">
                                <div class="col-md-10"></div>
                                <div class="col-md-2">
                                    <div class="form-group" style="margin-top: 18px;">
                                        <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
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

                                              .btn-KPI {
                                                      color: #fff !important;
    background-color: #3f51b5 !important;font-weight: bold !important;
                                                font-size: 15px!important;
}
                                           
                                              
                                                  .btn-Appraisal {
                                                     color: #000 !important;
background-color: #00ffff !important;font-weight: bold !important;
                                                font-size: 15px!important;
}   
                                            .btn-CancelApp {
                                                     color: white !important;
background-color: #E24F3B !important;
                                                font-weight: bold !important;
                                                font-size: 15px!important;
}   
       </style>
                            
                            <asp:CheckBox runat="server" ID="isKPI" Text="KPI" Visible="False" />
                                            <asp:HiddenField ID="hfMainAppraisalMasterId" runat="server"  />
                                            <asp:HiddenField ID="hfMasterId" runat="server"  />

                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_JdBoard" DataKeyNames="ReimbursFromMasterId" OnRowCommand="gv_JdBoard_OnRowCommand" CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender" AllowPaging="True" PageIndex="0" OnPageIndexChanging="loadGridView_PageIndexChanging">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hfReimbursFromMasterId" runat="server" Value='<%#Eval("ReimbursFromMasterId") %>' />
                                            <asp:HiddenField ID="EmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                            <asp:HiddenField ID="FinancialYearId" runat="server" Value='<%#Eval("FinancialYearId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

<%--                                    <asp:TemplateField HeaderText="Financial Year">
                                        <ItemTemplate>
                                            <asp:Label ID="FinancialYearDesc" runat="server"   Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="employfee" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="emeeployee" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="emploeeyee" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    

                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="DepartmentName" runat="server"   Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                
                                     <asp:TemplateField HeaderText="Exiting Supervisor">
                                        <ItemTemplate>
                                            <asp:Label ID="ExSupervisor" runat="server" Text='<%#Eval("ReportBoss") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                                                                                             
                                    <asp:TemplateField HeaderText="Exiting Office">
                                        <ItemTemplate>
                                            <asp:Label ID="ExOffice" runat="server" Text='<%#Eval("SalaryLocation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                      
                                         <asp:TemplateField HeaderText="Exiting FinalApproval">
                                        <ItemTemplate>
                                            <asp:Label ID="ExFinalApproval" runat="server" Text='<%#Eval("FinalApproval") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                                                   
                                      <asp:TemplateField HeaderText="Application Type">
                                        <ItemTemplate>
                                            <asp:Label ID="ApplicationType" runat="server" Text='<%#Eval("Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                         <asp:TemplateField HeaderText="Patient Name">
                                        <ItemTemplate>
                                            <asp:Label ID="PatientName" runat="server" Text='<%#Eval("PatientName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    
                                         <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="Amount" runat="server" Text='<%#Eval("ClaimTKByUser") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                                                                               
                                      <asp:TemplateField HeaderText="Pending Employee">
                                        <ItemTemplate>
                                            <asp:Label ID="waitId" runat="server"   Text='<%#Eval("HoldPerson") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

  <%--                        
                                   <asp:TemplateField HeaderText="Approval Status">
                                        <ItemTemplate>
                                            <asp:Label ID="ApprovalStatus" runat="server"   Text='<%#Eval("ActionStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    
                                    
                                    
                                   <asp:TemplateField HeaderText="Is Departmental">
                                                                <ItemTemplate>
                                                             
                                                                    
                                                                                                                                      
                                                                    <asp:CheckBox runat="server" ID="IsCheck"/>
                                                  

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                                                                      
                                   <asp:TemplateField HeaderText="Forward Any Person">
                                                                <ItemTemplate>                                                                                                                            
                                                                      <asp:DropDownList Width="300px"  runat="server"   ID="ddlForwordEmp" CssClass="form-control form-control-sm selectme"></asp:DropDownList>                                                  
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                    
                                                                                                    
                      
                                    
                                    
                                   <asp:TemplateField HeaderText="Application Forword">
                                        <ItemTemplate>
                                            <asp:LinkButton  runat="server" ID="btn_Return" OnClick="btn_Return_OnClick"  CssClass="btn btn-sm btnMyDesignOne" ><i class="fa fa-forward"   aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                            
                                    

                                </Columns>
                            </asp:GridView>
                            
                            
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
        
        
        <div>
        <ajaxToolkit:ModalPopupExtender ID="mpFunctionalSup" runat="server" TargetControlID="FunctionalSup_Test" PopupControlID="pnlFunctionalSup"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="FunctionalSup_Test" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnlFunctionalSup" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="400px" Width="60%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress5" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWawwwwwwwit" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                                                                                 <div class="row">
                                                    <div class="col-md-6" style="padding-left: 15px;padding-top: 12px;">
                                                          <div class="text-left">
                                                   <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> <span runat="server" id="lblHeader"></span></h1>
                                                </div>
                                                    </div>
                                                    
                                                    <div class="col-md-6" style="padding-top: 15px;padding-right: 15px;">
                                                          <asp:LinkButton ID="btnFunctionalSupClose"   OnClick="btnFunctionalSupClose_Click" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server"><i style="font-size: 16px" class="fa fa-times" aria-hidden="true"></i>
</asp:LinkButton>
                                                    </div>
                                                </div>
                                             
                                             <hr/>
                                 <div class="row">
                                <style>
                                    .tblTHColorChang{
                                        background-color: #EDF2F5!important;
                                        font-weight: bold;
                                        font-size: 13px;
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
 div#cpFormBody_ddlEmpInfo_chosen {
              width: 100%!important
          }

  div#cpFormBody_ddlForwordEmp_chosen {
              width: 100%!important
          }
                                </style>
                                     

                                    <div class="col-md-12">
                                          
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group ">
                                                    <label>Present Approval Person </label>
                                                    
                                                                <asp:DropDownList   runat="server" style="width: 320px !important" Enabled="False"   ID="ddlEmpInfo" CssClass="form-control form-control-sm selectme" />
                                                   <%-- <script type="text/javascript">
                                                        function pageLoad() {
                                                            $('#<%=ddlEmpInfo.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                            $('#<%=ddlForwordEmp.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                        }
                                                        </script>--%>
                                            </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group ">
                                                    <label>Forward Approval Person </label>
                                                    
                                                                <asp:DropDownList   runat="server"   ID="ddlForwordEmp" class="form-control form-control-sm selectme" />
                                                   
                                                    
                                                    
                                                      <br/>
                                                      <br/>
                                                      <br/>
                                                    
                                                     <asp:Button runat="server" ID="btnAppraisalFuncSUPSave" OnClick="btnAppraisalFuncSUPSave_OnClick" Text="Submit "  style="font-size: 20px!important;" CssClass="btn btn-info" />  
                                            </div>
                                                
                                                
                                                <br/>
                                            </div>
                                        </div>

                                        </div>
                                        </div>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
                 </div>
        
        
        
        
        
          <div>
        <ajaxToolkit:ModalPopupExtender ID="MPAppraisalApproval" runat="server" TargetControlID="hfAppraisalApproval" PopupControlID="pnAppraisalApproval"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="hfAppraisalApproval" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnAppraisalApproval" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="400px" Width="60%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWawwwwdfgwwwit" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                                                                                 <div class="row">
                                                    <div class="col-md-6" style="padding-left: 15px;padding-top: 12px;">
                                                          <div class="text-left">
                                                   <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Cancel Appraisal Approval</h1> <asp:CheckBox runat="server" Checked="True" Enabled="False" style="color: black;font-size: 15px" ID="chkCancelApprove"  />
                                                </div>
                                                    </div>
                                                    
                                                    <div class="col-md-6" style="padding-top: 15px;padding-right: 15px;">
                                                          <asp:LinkButton ID="btnApprasalResetClose"   OnClick="btnApprasalResetClose_Click" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server"><i style="font-size: 16px" class="fa fa-times" aria-hidden="true"></i>
</asp:LinkButton>
                                                    </div>
                                                </div>
                                             
                                             <hr/>
                                 <div class="row">
                                <style>
                                    .tblTHColorChang{
                                        background-color: #EDF2F5!important;
                                        font-weight: bold;
                                        font-size: 13px;
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
 div#cpFormBody_ddlEmpInfo_chosen {
              width: 100%!important
          }

  div#cpFormBody_ddlForwordEmp_chosen {
              width: 100%!important
          }


  
  div#cpFormBody_ddlEmpforCancelApproveAppraisal_chosen {
              width: 100%!important
          }
                                </style>
                                     

                                    <div class="col-md-12">
                                          
                                        <div style="overflow: scroll">
                                                  <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_Versions"         CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" >

                                                      <Columns>

                                                          <asp:TemplateField HeaderText="SL#">
                                                              <ItemTemplate>
                                                                  <%#Container.DataItemIndex+1 %>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Immediate Approved Person ">
                                                              <ItemTemplate>
                                                                  <asp:Label runat="server" ID="SkillsdfsdafaInfo" Text='<%#Eval("empImmediateName") %>'></asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>

                                                           <asp:TemplateField HeaderText="Last Approved Person ">
                                                              <ItemTemplate>
                                                                  <asp:Label runat="server" ID="lSsfkillInfo" Text='<%#Eval("empLastApprovName") %>'></asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>


                                                          



                                                      </Columns>
                                                  </asp:GridView>
                                                  </div>

                                        </div>
                                     
                                     <br/>
                                     <br/>
                                      <div class="col-md-12">
                                           <br/>
                                     <br/>
                                           <div class="row">
                                                <div class="col-md-2">
                                                    </div>
                                            <div class="col-md-6">
                                               
                                                  <br/>

                                                <div class="form-group ">
                                                    <label>Forward to Employee </label>
                                                    
                                                                <asp:DropDownList   runat="server" style="width: 320px !important"     ID="ddlEmpforCancelApproveAppraisal" CssClass="form-control form-control-sm selectme" />
                                                 
                                            </div>
                                                
                                                 <br/>
                                                      <br/>
                                                    
                                                     <asp:Button runat="server" ID="lbCancelAppraisalSubmit" OnClick="lbCancelAppraisalSubmit_OnClick" Text="Submit "  style="font-size: 20px!important;" CssClass="btn btn-info" />  
                                                
                                                </div>
                                                </div>
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
                    <br/>
                    
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
                 </div>
    </div>
    
    

</asp:Content>

