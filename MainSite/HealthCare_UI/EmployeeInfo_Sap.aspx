<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeInfo_Sap.aspx.cs" Inherits="HealthCare_UI_EmployeeInfo_Sap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
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
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  />Employee Information SAP Integration</h1>
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

                             <div class="form-row" style="display: none">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                 
                                   <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm selectme"></asp:DropDownList>
                                        
                                        
                                    
                                    </div>
                                </div>
                                 
                                 
                                   <div class="col-2">
                                    <div class="form-group">
                                        <label>Division</label>
                                        <asp:DropDownList runat="server" ID="ddlDivision" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" CssClass="form-control form-control-sm selectme" />
                                        
                                           
                                    </div>
                                </div>
                                 
                                   <div class="col-2">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList runat="server" ID="ddlDept" CssClass="form-control form-control-sm selectme" />
                                        
                                           
                                    </div>
                                </div>


                                 
                                     <div class="col-2">
                                    <div class="form-group" style="margin-top: 13px;">
  <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>
                                      
                                    </div>
                                </div>
                                 
                                   <div class="col-2">
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

                            <div class="row">
                                                    <div class="col-md-4">

<div class="form-group">
    <label>Employee Name: </label>
    
      <asp:DropDownList runat="server" ID="ddlEmpInfoNew" class="form-control form-control-sm selectme" />
           <script type="text/javascript">
               function pageLoad() {
                   $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

               }
           </script>
    </div>
</div> 
                                 <div class="col-md-2">
     <div class="form-group">
         <label>Receive From Date </label>
         
         <asp:TextBox runat="server" class="form-control form-control-sm" ID="App_FromDate"></asp:TextBox>
         
         <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                       Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                       TargetControlID="App_FromDate" />
     </div>
 </div>
 
 <div class="col-md-2">
     <div class="form-group">
         <label>Receive To Date </label>
         <asp:TextBox runat="server" class="form-control form-control-sm" ID="App_ToDate"></asp:TextBox>
         
         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                       Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                       TargetControlID="App_ToDate" />
     </div>
 </div>

                                       
                     
                            
                            <div class="col-md-2">
                                <div class="form-group" style="margin-top: 13px;">
                                    <asp:LinkButton runat="server" ID="LinkButton1" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>
                                      
                                </div>
                            </div>
                       
                            
                            </div>
                            
                            <asp:CheckBox runat="server" ID="isKPI" Text="KPI" Visible="False" />
                                            <asp:HiddenField ID="hfMainAppraisalMasterId" runat="server"  />
                                            <asp:HiddenField ID="hfMainId" runat="server"  />
                                            <asp:HiddenField ID="hfMasterId" runat="server"  />
                            
                             <div id="gridContainer1" style=" overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll; height:600px">

                              <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_JdBoard" DataKeyNames="EmpId" OnRowCommand="gv_JdBoard_OnRowCommand" CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender">
                                <Columns>
                                    
                                          <asp:TemplateField HeaderText="SL#">
            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="50px" />
        </asp:TemplateField>

        <asp:BoundField DataField="Pernr" HeaderText="EMP ID." />
        <asp:BoundField DataField="Name" HeaderText="EMP Name" />

        <asp:BoundField DataField="ActionReason" HeaderText="Action Reason" />
        <asp:BoundField DataField="Action" HeaderText="Action" />
        <asp:BoundField DataField="ActDate" HeaderText="Action Date" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="False" NullDisplayText="" />

        <asp:BoundField DataField="CompCode" HeaderText="Company Code" />
        <asp:BoundField DataField="CompName" HeaderText="Company Name" />
        <asp:BoundField DataField="Doj" HeaderText="DOJ" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="False" NullDisplayText="" />

        <asp:BoundField DataField="DivId" HeaderText="Division Id" />
        <asp:BoundField DataField="DivName" HeaderText="Division" />

        <asp:BoundField DataField="DeptId" HeaderText="Dept Id" />
        <asp:BoundField DataField="DepartName" HeaderText="Department" />

        <asp:BoundField DataField="DesigId" HeaderText="Designation Id" />
        <asp:BoundField DataField="EmpTypeId" HeaderText="Emp Type Id" />
        <asp:BoundField DataField="EmpCat" HeaderText="Emp Category" />

        <asp:BoundField DataField="FName" HeaderText="Father Name" />
        <asp:BoundField DataField="FDob" HeaderText="Father DOB" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="False" NullDisplayText="" />
        <asp:BoundField DataField="FDod" HeaderText="Father DOD" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="False" NullDisplayText="" />
        <asp:BoundField DataField="FDType" HeaderText="Father Doc Type" />

        <asp:BoundField DataField="MName" HeaderText="Mother Name" />
        <asp:BoundField DataField="MDob" HeaderText="Mother DOB" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="False" NullDisplayText="" />
        <asp:BoundField DataField="MDod" HeaderText="Mother DOD" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="False" NullDisplayText="" />
        <asp:BoundField DataField="MDType" HeaderText="Mother Doc Type" />
                                     
        <asp:TemplateField HeaderText="Mobile">
            <ItemTemplate>
                <a href='<%# "tel:" + Eval("Mobile") %>'><%# Eval("Mobile") %></a>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Email">
            <ItemTemplate>
                <a href='<%# "mailto:" + Eval("Email") %>'><%# Eval("Email") %></a>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="Place" HeaderText="Place" />
        <asp:BoundField DataField="Office" HeaderText="Office" />
        <asp:BoundField DataField="OfficeName" HeaderText="Office Name" />

        <asp:BoundField DataField="SalGrade" HeaderText="Salary Grade" />
        <asp:BoundField DataField="SalStep" HeaderText="Salary Step" />

        <asp:BoundField DataField="ReportId" HeaderText="Report Id" />
        <asp:BoundField DataField="Status" HeaderText="Sync with HRIS" />
                                     

        <asp:BoundField DataField="IsSpTransfer" HeaderText="Is SP Transfer" />
        <asp:BoundField DataField="SectionId" HeaderText="Section Id" />
        <asp:BoundField DataField="SectionName" HeaderText="Section" />

        <asp:BoundField DataField="PreviousEmpCode" HeaderText="Previous Emp Code" />
        <asp:BoundField DataField="kpiApprId" HeaderText="KPI Appr Id" />
        <asp:BoundField DataField="Gender" HeaderText="Gender" />

                                              <asp:BoundField DataField="ServerDateTime" HeaderText="Receive Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" HtmlEncode="False" NullDisplayText="" />                                                                                                                        

                                </Columns>
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

