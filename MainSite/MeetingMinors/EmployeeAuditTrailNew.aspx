<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeAuditTrailNew.aspx.cs" Inherits="MeetingMinors_EmployeeAuditTrailNew" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
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
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />  Audit Trail  </h1>
                        </div>
                         <div class="page-heading__container float-right d-none d-sm-block">
                             <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="vcchomeButton_OnClick" />
              
            </div>
                    </div>
                     
<asp:UpdatePanel runat="server">
    <ContentTemplate>
         <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
        <div class="card">
   <div class="card-body">
       
          <div class="row">
               
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          <div class="row">
                               <div class="col-md-2"></div>
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:</label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"  ID="ddlCompany"  class="form-control form-control-sm" /></div>
                          </div>
                            
                            <div style="padding-top: 5px;"></div>
                             <div class="row" runat="server">
                                  <div class="col-md-2"></div>
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">From Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date"    ID="txtCreatedDate"  class="form-control form-control-sm" /></div>
                          </div>
                          <%--  <div style="padding-top: 5px;"></div>--%>
                            
                            
                             
                                <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Title:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"    ID="txtTitle"  class="form-control form-control-sm" /></div>
                          </div>
                         <%--   <div style="padding-top: 5px;"></div>--%>

                             <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Propuse:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"      ID="txtPropuse"  class="form-control form-control-sm" /></div>
                          </div>
                            
                           <%--  <div style="padding-top: 5px;"></div>--%>
                            
                             
                            </div>
                        </div>
             
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          
                                 <div class="row">
                                     
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Operation:</label></div>
                            <div class="col-md-6">
                                    <asp:DropDownList runat="server"    ID="ddlOperation" AutoPostBack="True" OnSelectedIndexChanged="ddlOperation_OnSelectedIndexChanged"    class="form-control form-control-sm" >
                                      <asp:ListItem Value="0">Please Select One...</asp:ListItem>
                                          <asp:ListItem Value="EmpInfomation">Employee Information</asp:ListItem>
                                           <asp:ListItem Value="EmpProbation">Employee Probation</asp:ListItem>
                             <asp:ListItem Value="JD">Employee Job Description</asp:ListItem>
                                        
                                        
                                             <asp:ListItem Value="MPBudget">MP Budget</asp:ListItem>
                                               <asp:ListItem Value="JobCirculation">Job Circulation</asp:ListItem>
                                        
                                          <asp:ListItem Value="KPI">KPI</asp:ListItem>
                                        
                                         <asp:ListItem Value="TrainningBudget">Trainning Budget</asp:ListItem>
  <asp:ListItem Value="Suspend">Suspend</asp:ListItem>
                                      <asp:ListItem Value="DiciplinaryAction">Diciplinary Action</asp:ListItem>
                                        
                                              <asp:ListItem Value="separation">Separation</asp:ListItem>
                                      <asp:ListItem Value="Promotion">Promotion</asp:ListItem>
                                        
                                           <asp:ListItem Value="Redesignation">Re-designation</asp:ListItem>
                                            <asp:ListItem Value="Transfer">Transfer</asp:ListItem>
                                    
                                         <asp:ListItem Value="ContractualEmployee">Contractual Employee</asp:ListItem>
                                       <asp:ListItem Value="Increment">Increment</asp:ListItem>
                                                <asp:ListItem Value="JobRequisition">Job Requisition </asp:ListItem>
                                   
                                   
                                
                                   
                                      
                                
                                    
                                 
                                    
                                </asp:DropDownList>
                                
                                  <asp:DropDownList runat="server" Visible="False"   ID="ddlCreatedBy"  class="form-control form-control-sm" />
                                
                                  <script type="text/javascript">
                                      function pageLoad() {
                                          $('#<%=ddlCreatedBy.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                          $('#<%=ddlKeySearch.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                                      }
               </script>
                            </div>
                          </div>
                          
                            
                            
                             <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">To Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date"    ID="txtToDate"  class="form-control form-control-sm" /></div>
                          </div>
                           
                            </div>
                        </div>
             
             
              <div class="col-md-12" runat="server" Visible="False">
                        <div class="form-group ">
                                          
                          
                                 <div class="row">
                                      
                               
                                    <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Key Search:</label></div>
                            <div class="col-md-6">
                                
                                  <asp:TextBox runat="server"    ID="txtKeySearch"  class="form-control form-control-sm" />
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="getMeetingKeySearch" ServicePath="~/WebService.asmx" TargetControlID="txtKeySearch"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>
                                  <asp:DropDownList runat="server"    ID="ddlKeySearch"  Visible="False" class="form-control form-control-sm" />
                            </div>
                               </div>
                            </div>
                                       </div>
              
              
                   <div class="col-md-12" runat="server" Visible="False">
                        <div class="form-group ">
                                          
                          
                                 <div class="row">
                                      
                               
                                    <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Operation:</label></div>
                            <div class="col-md-6">
                                
                                <%--  <asp:TextBox runat="server"    ID="TextBox1"  class="form-control form-control-sm" />
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="getMeetingKeySearch" ServicePath="~/WebService.asmx" TargetControlID="txtKeySearch"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>--%>
                              
                            </div>
                               </div>
                            </div>
                                       </div>
             </div>
       
       
            

                                    <div class="row">
                                        <div class="col-md-4">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton runat="server" ID="btn_Search" OnClick="btn_Search_OnClick"   CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="lbReset" OnClick="lbReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                    </div>
       
        <br />
       
        
    
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
       
       
         
                                <div class="row">
                                     <div class="col-md-8">
                                         
                                     </div>
                                    
                                      <style>.ssss {
                                                                                                                     font-size: 13px;
                                                                                                                     font-weight: bold;
                                                                          
                                               
                                                                                                                 }</style>
                              
                                     <div class="col-md-2"  style="margin-top: 22px; padding: 5px;">
                                       
                                            
                                         
                                             
                                                    
                                      
                                     </div>
                             
                               
       
                                       <div class="col-md-2" style="margin-top: 17px;">
                                         <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
                                         </div>
                                </div>
                                <%-- <div id="tbl_container" style="width:100%" class="">

                    </div>--%>
                                  <br/>
            <asp:GridView Width="100%" ShowHeader="True" ID="gv_ViewList" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfIncrementId" Value='<%#Eval("IncrementId")%>' />   
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                                                    
                                                   <asp:BoundField DataField="ShortName" HeaderText="Company" /> 
                                                   <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year"/> 
                                                   <asp:BoundField DataField="IncrementType" HeaderText="Increment Type" /> 
                                                   <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Id " />                            
                                                   <asp:BoundField DataField="EmpName" HeaderText="Employee Name"  /> 
                                                   <asp:BoundField DataField="IncremantalStep" HeaderText="Incremantal Step"  />                                                                                  
                                                   <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                                 <asp:BoundField DataField="UserName" HeaderText="Modification By" />                                                                                                                                                                                                         
                                                 <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Incement_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                                                                    
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnViewMeeting" OnClick="btnIncrementalview_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa   fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
       
                   <asp:GridView Width="100%" ShowHeader="True" ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfEmployeeReDesignationId" Value='<%#Eval("EmployeeReDesignationId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>                                                                                                                        
                                                   <asp:BoundField DataField="ShortName" HeaderText="Company" /> 
                                                   <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year"/> 
                                                   <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                                   <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Id " />                            
                                                   <asp:BoundField DataField="EmpName" HeaderText="Employee Name"  /> 
                                                   <asp:BoundField DataField="Designation" HeaderText="New Designation "  />                                                                                  
                                                   <asp:BoundField DataField="Remarks" HeaderText="Remarks"  />   
                                                   <asp:BoundField DataField="UserName" HeaderText="Modification By" />                                                                                                                                                                                                         
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Incement_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                  <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnRedesignation" OnClick="btnRedesignationview_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>    
       
       
         <asp:GridView Width="100%" ShowHeader="True" ID="GridViewPromotion" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfEmployeePromotionId" Value='<%#Eval("EmployeePromotionEntryId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>                                                                                                                        
                                                   <asp:BoundField DataField="ShortName" HeaderText="Company" /> 
                                                   <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year"/> 
                                                   <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                                   <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />                            
                                                   <asp:BoundField DataField="EmpName" HeaderText="Employee Name"  />  
                                                 <asp:BoundField DataField="GradeName" HeaderText="Salary Grade"  />  
                                                   <asp:BoundField DataField="SalaryStepName" HeaderText="Salary Step"  />     
                                                   <asp:BoundField DataField="Designation" HeaderText="Designation "  /> 
                                                    
                                                   <asp:BoundField DataField="newRepotingBody" HeaderText="Repoting Body"  />                                                                                 
                                                   <asp:BoundField DataField="Remarks" HeaderText="Remarks"  />   
                                                   <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                     <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date"   DataFormatString="{0:dd-MMM-yyyy}" />                                                                                                                                                                                                      
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Promotion_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                  <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnPromotiontion" OnClick="btnPromotionview_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  
       
       
                <asp:GridView Width="100%" ShowHeader="True" ID="GridViewSuspend" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfSupendId" Value='<%#Eval("SuspendId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>    
                                                                                                                                                                    
                                                   <asp:BoundField DataField="ShortName" HeaderText="Company" /> 
                                                   <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year"/>                                         
                                                   <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Id " />                            
                                                   <asp:BoundField DataField="EmpName" HeaderText="Employee Name"  />                                                                                                                                            
                                                   <asp:BoundField DataField="EffectiveDate" HeaderText="Effective from Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                                   <asp:BoundField DataField="EffectiveToDate" HeaderText="Effective To Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                                

                                                   <asp:BoundField DataField="Description" HeaderText="Description "  /> 
                                     
                                                                                                                
                                                   <asp:BoundField DataField="Remarks" HeaderText="Remarks"  />   
                                                   <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                   <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date"  DataFormatString="{0:dd-MMM-yyyy}" />    
                                                                                                                                                                                                                                                  
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Suspend_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                  <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnSuspend" OnClick="btnSuspendview_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  
       
       
          <asp:GridView Width="100%" ShowHeader="True" ID="GridViewDiciplinaryAction" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfDiciplinaryActionId" Value='<%#Eval("DiciplinaryId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>    
                                                                                                                                                                    
                                                   <asp:BoundField DataField="ShortName" HeaderText="Company" /> 
                                                   <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year"/>                                         
                                                   <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Id " />                            
                                                   <asp:BoundField DataField="EmpName" HeaderText="Employee Name"  />                                                                                                                                            
                                                   <asp:BoundField DataField="EffectiveDate" HeaderText="Effective  Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                    
                                                   <asp:BoundField DataField="Description" HeaderText="Description "  />                                                                                                                                               
                                                   <asp:BoundField DataField="Remarks" HeaderText="Remarks"  />   
                                                   <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                   <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date"  DataFormatString="{0:dd-MMM-yyyy}" />    
                                                                                                                                                                                                                                                  
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DiciplinaryAction_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                  <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnDiciplinaryAction" OnClick="DiciplinaryActionview_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  
       
       
       
       
          <asp:GridView Width="100%" ShowHeader="True" ID="GridViewTransfer" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hftransferId" Value='<%#Eval("EmpTransferAndRedesignationId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>    
                                                                                                                                                                    
                                                   <asp:BoundField DataField="ShortName" HeaderText="Company" /> 
                                                   <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year"/>                                         
                                                   <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Id " />                            
                                                   <asp:BoundField DataField="EmpName" HeaderText="Employee Name"  />                                                                                                                                            
                                                   <asp:BoundField DataField="EffectiveDate" HeaderText="Effective  Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                                                                                
                                                   <asp:BoundField DataField="NCompany" HeaderText="New Company "  />                                                    
                                                    <asp:BoundField DataField="SalaryLocation" HeaderText="Office"  />           
                                                    <asp:BoundField DataField="Location" HeaderText="Place"  />   
                                                    <asp:BoundField DataField="DivisionName" HeaderText=" Division"  />   
                                                    <asp:BoundField DataField="DivisionWingName" HeaderText=" Wing"  />                                      
                                                    <asp:BoundField DataField="DivisionName" HeaderText=" Division"  />   
                                                    <asp:BoundField DataField="DivisionWingName" HeaderText=" Wing"  />   
                                                    <asp:BoundField DataField="DepartmentName" HeaderText=" Department"  />                                                                                                                                                                                                                                                                                
                                                   <asp:BoundField DataField="SectionName" HeaderText=" Section"  />  
                                                   <asp:BoundField DataField="SubSectionName" HeaderText=" SubSection"  />  
                                                   <asp:BoundField DataField="NReportingBody" HeaderText=" Reporting Body"  />  
                                                                                                                                                          
                                                   <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                   <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" />    
                                                                                                                                                                                                                                                  
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Trasfer_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                  <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnTransfer" OnClick="Transferview_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  

       
       
            <asp:GridView Width="100%" ShowHeader="True" ID="GridViewSeperation" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfEmployeeJobLeftId" Value='<%#Eval("EmployeeJobLeftId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>                                                                                                                                                                      
                                                   <asp:BoundField DataField="ShortName" HeaderText="Company" />                                                                        
                                                   <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Id " />                            
                                                   <asp:BoundField DataField="EmpName" HeaderText="Employee Name"  />                                                                                                                                                                                                                                                                                                                                                    
                                                   <asp:BoundField DataField="JobLeftType" HeaderText="JobLeft Type"  />                                                                                             
                                                   <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:dd-MMM-yyyy}" />           
                                                   <asp:BoundField DataField="JobLeftDate" HeaderText="JobLeft Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                                           
                                                   <asp:BoundField DataField="Reason" HeaderText="Reason"  />                                                                                                                                      
                                                   <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                   <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                                                                                                                                                   
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Seperation_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                 <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnSeperation" OnClick="Seperationview_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  
       
       
       
          <asp:GridView Width="100%" ShowHeader="True" ID="GridViewJD" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfJDId" Value='<%#Eval("JdMasterId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>                                                                                                                                                                      
                                                                                                           
                                                   <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Id" />                            
                                                   <asp:BoundField DataField="EmpName" HeaderText="Employee Name"  />                                                                                                                                                                                                                                                                                                                                                    
                                                   <asp:BoundField DataField="JdSummary" HeaderText="Job Summary"  />                                                                                             
                                                                                                                                                                                                                                                                                                  
                                                   <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                   <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                                                                                                                                                   
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_JD_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                 <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnJD" OnClick="JDview_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  
       
       
          <asp:GridView Width="100%" ShowHeader="True" ID="GridViewMPBudget" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfMPBudgetId" Value='<%#Eval("MPBudgetMasterId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:BoundField DataField="ShortName" HeaderText="Company Id" />                            
                                                   <asp:BoundField DataField="DepartmentName" HeaderText="Department"  />                                                                                                                                                                                                                                                                                                                                                    
                                                   <asp:BoundField DataField="FinancialYearDesc" HeaderText="FinancialYear"  />
                                                <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                   <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                                                                                                                                                   
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_MPBudget_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                 <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnMpBudget" OnClick="MPBudgetView_OnClick"  CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  
       
       
              <asp:GridView Width="100%" ShowHeader="True" ID="GridViewContructualEmployee" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfContractualEmployeetId" Value='<%#Eval("ContractualEmpManageId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>                                                                                                                                                                      
                                                                                                                                                         
                                                   <asp:BoundField DataField="ShortName" HeaderText="Company"  />                                                
                                                   <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Id" />                            
                                                   <asp:BoundField DataField="EmpName" HeaderText="Employee Name"  />                                                                                                                                                                                                                                                                                                                                                                                                                            
                                                   <asp:BoundField DataField="IsExtension" HeaderText="IsExtension"  />                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                                                   <asp:BoundField DataField="IsRenew" HeaderText="IsRenew"  />                                                                                                                                                                                                                                                                                                                                                                                                           
                                                   <asp:BoundField DataField="IsContractualToPermanent" HeaderText="IsContractualToPermanent"  />  
                                                   <asp:BoundField DataField="Remarks" HeaderText="Remarks"  />                                                    
                                                   <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                                                                                                                                                                                                                                                                                  
                                                   <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                   <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                                                                                                                                                   
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ContactualEmployee_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                 <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnContructualEmployee" OnClick="ContructualView_OnClick"  CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  
       


   <asp:GridView Width="100%" ShowHeader="True" ID="GridViewKIPMaster" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfKPIMastertId" Value='<%#Eval("AppraisalSelfMasterId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Id" />                            
                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name"  />                                                                                                                                                                                                                                                                                                                                                                                                                             
                                                <asp:BoundField DataField="Designation" HeaderText="Designation"  /> 
                                                <asp:BoundField DataField="DepartmentName" HeaderText="Department Name"  /> 
                                                <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                                                                                                                                                   
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_KPI_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                 <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnKPIMaster" OnClick="KPIMasterView_OnClick"  CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  
   
   
   
   

   
   
   
   
      <asp:GridView Width="100%" ShowHeader="True" ID="GridViewJobCirculation" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>

                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfJobId" Value='<%#Eval("JobID")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>

                                                <asp:BoundField DataField="ShortName" HeaderText="ShortName" />                            
                                                <asp:BoundField DataField="ReqCode" HeaderText="Employee Name"  />                                                                                                                                                                                                                                                                                                                                                                                                                             
                                                <asp:BoundField DataField="JobContext" HeaderText="Job Context"  /> 
                                                <asp:BoundField DataField="CirculationStartDate" HeaderText="Circulation Start Date"  DataFormatString="{0:dd-MMM-yyyy}"  />
                                                <asp:BoundField DataField="CirculationsdeadlineDate" HeaderText="Circulation End Date" DataFormatString="{0:dd-MMM-yyyy}"  /> 
                                                <asp:BoundField DataField="ProbableInterviewDate" HeaderText="Probable Interview Date" DataFormatString="{0:dd-MMM-yyyy}"  />
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks"  />
                                                <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" />   

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_JobCirulation_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                 <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnJobCirculation" OnClick="JobCirculationView_OnClick"  CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  
   
        <asp:GridView Width="100%" ShowHeader="True" ID="GridViewTrainningBudget" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfTrainingBudget2Id" Value='<%#Eval("TrainingBudget2Id")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>                                                                                                                                                                      
                                                                                                                                                         
                                                   <asp:BoundField DataField="ShortName" HeaderText="ShortName"  />                                                
                                                   <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />                                                                                               
                                                   <asp:BoundField DataField="TotalYearlyBudgetCost" HeaderText="Total Yearly BudgetCost" />                                                                                                                                                                                                                                                                                                                                                                                                                       
                                                   <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                   <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                                                                                                                                                   
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TrainningBudget_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                 <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnTrainingBudgetId" OnClick="TrainingBudgetlView_OnClick"  CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  
       
       
         <asp:GridView Width="100%" ShowHeader="True" ID="GridViewJobRequisition" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfJobReqId" Value='<%#Eval("JobReqId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>                                                                                                                                                                      
                                                                                                                                                         
                                                   <asp:BoundField DataField="ShortName" HeaderText="ShortName"  />       
                                                     <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" />                                           
                                                   <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />                                                                                               
                                                   <asp:BoundField DataField="JobTitle" HeaderText="Designation" />     
                                                   <asp:BoundField DataField="Nos" HeaderText="Total Vacancy" />                                                                                                                                                                                                                                                                                                                                                                                                         
                                                   <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                   <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                                                                                                                                                   
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_JobRequisition_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                 <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnJobReqId" OnClick="JobRequisitionlView_OnClick"  CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>  

       
                 <asp:GridView Width="100%" ShowHeader="True" ID="GridViewEmployeeInformation" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfEMpId" Value='<%#Eval("EmpInfoId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>                                                                                                                                                                      
                                                                                                                                                         
                                                   <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Id"  />       
                                                     <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />                                           
                                                                                                                                
                                                   <asp:BoundField DataField="DivisionName" HeaderText="Division" />     
                                                   <asp:BoundField DataField="DepartmentName" HeaderText="Department" />  
                                                
                                                
                                                   <asp:BoundField DataField="Designation" HeaderText="Designation" />     
                                           
                                                                                                                                                                                                                                                                                                                                                                                                                                                       
                                                   <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                   <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                                                                                                                                                   
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EmployeeInformation_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                 <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnEmpId" OnClick="EmployeeInformationView_OnClick"  CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView> 

 
                 <asp:GridView Width="100%" ShowHeader="True" ID="GridViewEmployeeProbation" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         <asp:HiddenField runat="server" ID="hfEmpProbationId" Value='<%#Eval("ProbationEvaluationMasterId")%>' />   
                                                    </ItemTemplate>
                                                 </asp:TemplateField>                                                                                                                                                                      
                                                                                                                                                         
                                                   <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Id"  />       
                                                     <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />                                           
                                                                                                                             
                                                     <asp:BoundField DataField="DateOfJoin" HeaderText="Date Of Join" DataFormatString="{0:dd-MMM-yyyy}" />   
                                                
                                                  <asp:BoundField DataField="ProbationEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />   
                                                   
                                                   <asp:BoundField DataField="DivisionName" HeaderText="Division" />     
                                                   <asp:BoundField DataField="DepartmentName" HeaderText="Department" />  
                                                
                                                
                                                   <asp:BoundField DataField="Designation" HeaderText="Designation" />     
                                                                                                                                                                                                                                                                                                                                                                                                    
                                                   <asp:BoundField DataField="UserName" HeaderText="Modification By" />      
                                                   <asp:BoundField DataField="ModifyDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" />                                                                                                                                                                                                   
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EmployeeProbation_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  </asp:TemplateField>                                                                                                    
                                                 <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>                                                                               
                                                        <asp:LinkButton runat="server" ID="btnEmpProbationId" OnClick="EmployeeProbationView_OnClick"  CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
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
      
              </div>
           <br/> 
           <br/> 
           <br/> 
           <br/> 
           <br/> 
           <br/> 
           <br/> 
           <br/> 
        
        </div>
    </ContentTemplate>
    
             <Triggers>
                 
                 <asp:PostBackTrigger ControlID="btnExportToExcel"/>
             </Triggers>
</asp:UpdatePanel>
                               </div>
        
        </div>
</asp:Content>
