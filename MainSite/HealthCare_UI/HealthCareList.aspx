<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="HealthCareList.aspx.cs" Inherits="HealthCare_UI_HealthCareList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> HR Panel </h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block" runat="server" >
                            
                           <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                            
                            <asp:LinkButton ID="btn_New"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="addNewButton_OnClick" > <i class="fa fa-plus"></i>&nbsp; Top Sheet Generate</asp:LinkButton>
                           
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
                                        <asp:DropDownList ID="ddlCompany" runat="server"  class="form-control form-control-sm" ></asp:DropDownList>
                                    </div>
                                </div>
                                 

                                     <div class="col-4">
                                    <div class="form-group" style="margin-top: 13px;">
  <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>
                                      
                                    </div>
                                </div>
                                 
                               <%--    <div class="col-2">
                                    <div class="form-group" style="margin-top: 18px;">
                                     <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
                                     </div>
                                </div>--%>

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
                            
  
                            <hr/>

                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_JdBoard" OnRowCommand="gv_JdBoard_OnRowCommand" CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hfeimbursFromMasterId" runat="server" Value='<%#Eval("ReimbursFromMasterId") %>' />
                                            <asp:HiddenField ID="hfCompanyId" runat="server" Value='<%#Eval("CompanyId") %>' />
                                            <asp:HiddenField ID="EmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                          
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

                                    <asp:TemplateField HeaderText="Approval Status">
                                        <ItemTemplate>
                                            <asp:Label ID="ActionStatus" runat="server"   Text='<%#Eval("ActionStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Payment Status">
                                        <ItemTemplate>
                                            <asp:Label ID="PaymentStatus" runat="server"   Text='<%#Eval("PaymentStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                             
                                    
                                  <%--  <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckApproved"   runat="server"  />
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
                                    </asp:TemplateField>
                                    

                                    
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_view" OnClick="btn_eview_OnClick" CssClass="btn btn-sm btn-    success" ><i class="fa fa-eye" aria-hidden="true"></i>
                                          </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    

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
                   
                 
                   
                </div>
            </ContentTemplate>
              <%--   <Triggers>
                       <asp:PostBackTrigger ControlID="btnExportToExcel" />  
        
    </Triggers>--%>
        </asp:UpdatePanel>
        
        
        
        
        
        
        
        
        
    </div>
</asp:Content>

