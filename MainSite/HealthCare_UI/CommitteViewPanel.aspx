<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="CommitteViewPanel.aspx.cs" Inherits="HealthCare_UI_CommitteViewPanel" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
      
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
    

    <style>
        .star {
            color: #e24f3b;
        }
    </style>


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
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Committee View Panel  </h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block" runat="server" >
                            
                           <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                            
                         <%--   <asp:LinkButton ID="LinkButton3"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="addNewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>--%>
                           
                        </div>

                    </div>
                    
                    <div class="card">
                        <div class="card-body">
                                                                                                                                                             
                           <div class="form-row">
                      

                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm selectme" ></asp:DropDownList>
                                    </div>
                                  </div>

                                 <div class="col-md-2">
                                     <div class="form-group">
                                         <label>Type</label>

                                        <asp:DropDownList id="ddlType" CssClass="form-control form-control-sm selectme" runat="server">                                           
                                             <asp:ListItem value="">  
                                                 Select From List   
                                             </asp:ListItem>  
                                             <asp:ListItem value="IPD">  
                                                 IPD   
                                             </asp:ListItem>  
                                             <asp:ListItem value="OPD">  
                                                 OPD   
                                             </asp:ListItem> 
                                             <asp:ListItem value="Special">  
                                                 Special   
                                             </asp:ListItem> 
                                         </asp:DropDownList> 
                                                                                                                       
                                         <script type="text/javascript">
                                             function pageLoad() {
                                                 $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });
                                             }
                                         </script>
                                     </div>
                                 </div>
                               
                               
                               
                               
                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Finalcial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" runat="server"  class="form-control form-control-sm selectme" ></asp:DropDownList>
                                    </div>
                                  </div>

                                 <div class="col-md-2">
                                     <div class="form-group">
                                         <label>Status <span class="star">*</span></label>

                                        <asp:DropDownList id="ddlStatus" CssClass="form-control form-control-sm selectme" runat="server">                                           
                                             <asp:ListItem value="">  
                                                 Select From List   
                                             </asp:ListItem>  
                                             <asp:ListItem value="Pending">  
                                                 Pending   
                                             </asp:ListItem>  
                                             <asp:ListItem value="Complete">  
                                                 Complete 
                                             </asp:ListItem> 
                                            
                                         </asp:DropDownList> 
                                                                                                                       
                                         <script type="text/javascript">
                                             function pageLoad() {
                                                 $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });
                                             }
                                         </script>
                                     </div>
                                 </div>
                               
                               
                                      <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Meeting From Date</label>
                                     
                                        <asp:TextBox runat="server" ID="MeetFromDate" class="form-control form-control-sm" ></asp:TextBox>
                                                                                                                      
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="MeetFromDate" />

                                    </div>
                                  </div>

                                 <div class="col-md-2">
                                     <div class="form-group">
                                         <label>Meeting From Date</label>

                                                                                                                       
                                          <asp:TextBox runat="server" ID="MeetingTodate" class="form-control form-control-sm" ></asp:TextBox>
                                         
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="MeetingTodate" />
                                     </div>
                                 </div>

                             </div>
                                                                                             
                        
                        <div class="form-row" >
                            <div class="col-md-5"></div>
                            <div class="col-md-2">
                                <div class="form-group" style="margin-top: 13px;">
                                    <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>
                                      
                                </div>
                            </div>
                        </div>

                            
                            
           
                        <div class="form-row" >
                            <div class="col-md-3">
                                
                                <label class="title-widget"><h3> Application Information </h3> </label>
                            </div>
                        </div>  


                            <hr/>

                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_JdBoard" OnRowCommand="gv_JdBoard_OnRowCommand" CssClass="AddToListCssTable" EmptyDataText="There is no Data in this grid"  OnPreRender="gv_DocumentUpload_PreRender">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hfeimbursFromMasterId" runat="server" Value='<%#Eval("ReimbursFromMasterId") %>' />
                                            <asp:HiddenField ID="hfCompanyId" runat="server" Value='<%#Eval("CompanyId") %>' />
                                            <asp:HiddenField ID="EmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                            <asp:HiddenField ID="HFApplicationType" runat="server" Value='<%#Eval("Type") %>' />
                                            <asp:HiddenField ID="HFSalaryLocaion" runat="server" Value='<%#Eval("SalaryLoationId") %>' />
                   

                                        </ItemTemplate>
                                    </asp:TemplateField>

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
                                    
                                    <asp:TemplateField HeaderText="Division">
                                        <ItemTemplate>
                                            <asp:Label ID="DivisionName" runat="server"   Text='<%#Eval("DivisionName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="DepartmentName" runat="server"   Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                           <asp:BoundField DataField="Type" HeaderText="Application Type" />  
                                           <asp:BoundField DataField="PatientName" HeaderText="Patient Name" />  

                                                  <asp:BoundField DataField="Relationship" HeaderText="Relation" />  

                                                  <asp:BoundField DataField="Amount" HeaderText="Amount" />  
                                        <asp:BoundField DataField="SubmitDate" HeaderText="Aplication Date" />  
                                        <asp:BoundField DataField="ReturnFromCommiteMeeting" HeaderText="Return from committee Meeting" />  

                                   <%-- <asp:TemplateField HeaderText="Approval Status">
                                        <ItemTemplate>
                                            <asp:Label ID="ActionStatus" runat="server"   Text='<%#Eval("ActionStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                 <%--   <asp:TemplateField HeaderText="Payment Status">
                                        <ItemTemplate>
                                            <asp:Label ID="PaymentStatus" runat="server"   Text='<%#Eval("PaymentStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                             
                                    
                                 <%--   <asp:TemplateField>
                                        
                                         <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                            </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckApproved"   runat="server" AutoPostBack="True"  OnCheckedChanged="ckApproved_OnCheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    
                                 <%--   <asp:TemplateField HeaderText="Reset Approval Person">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_KPI"  OnClick="btn_KPI_OnClick" CssClass="btn btn-xs btn-KPI" >KPI
                                            </asp:LinkButton>
                                            <div style="padding-top: 3px!important"></div>
                                            &nbsp;
                                            &nbsp;
                                             <asp:LinkButton runat="server" ID="btnApprisal" OnClick="btnApprisal_OnClick" CssClass="btn btn-xs btn-Appraisal" >Appraisal
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                        <%--             <asp:TemplateField HeaderText="Cancel Appraisal Approval">
                                           <ItemTemplate>
                                          <asp:LinkButton runat="server" ID="lbApprisalCancel" OnClick="btnApprisalCancel_OnClick" CssClass="btn btn-xs btn-CancelApp" ToolTip="Click to Cancel Appraisal Approval" >Cancel
                                            </asp:LinkButton>
                                      </ItemTemplate>
                                    
                                    </asp:TemplateField>--%>
                                    


<%--                                 <asp:TemplateField HeaderText="Print">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_print" OnClick="btn_print_OnClick" CssClass="btn btn-sm btn-info" ><i class="fa fa-print" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    
                                    
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_view" OnClick="btn_view_OnClick" CssClass="btn btn-sm btn-success" ><i class="fa fa-eye" aria-hidden="true"></i>
                                          </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    

                                </Columns>
                            </asp:GridView>
                            
                            
                              <br/>
                        
                        
                        

                        
                        
                        <div class="form-row" style="display: none">
                            <div class="col-md-5">
                            </div>
                            <div class="col-md-6">
                                <div class="form-group" style="margin-top: 17px;">
                                            
                                    <asp:LinkButton runat="server" ID="btn_save" Visible="True"  OnClick="btn_save_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Submit Information </asp:LinkButton>
                   
                                    <asp:LinkButton runat="server" ID="UpdateBtn" Visible="False"    CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Update Information </asp:LinkButton>

                                    <asp:LinkButton runat="server" ID="btnReset"  CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset Information</asp:LinkButton>
                                          
                                </div> 
                            </div>

   
        
                        </div>

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
              <%--   <Triggers>
                       <asp:PostBackTrigger ControlID="btnExportToExcel" />  
        
    </Triggers>--%>
        </asp:UpdatePanel>

        
    </div>
    

</asp:Content>

