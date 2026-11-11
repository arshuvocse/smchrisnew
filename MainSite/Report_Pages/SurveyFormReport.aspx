<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SurveyFormReport.aspx.cs" Inherits="Report_Pages_SurveyFormReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    
    
     <div class="content" id="content">

        <!-- PAGE HEADING -->
        <div class="page-heading">
            <div class="page-heading__container">
             
                <h1 class="title" style="font-size: 18px; padding-top: 9px;"><img src="app.png" />Survey Report </h1>
            </div>

            <div class="page-heading__container float-right d-none d-sm-block">
                <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                <asp:Button ID="addNewButton" Text="Add New Information" Visible="False" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
            </div>



        </div>
        <!-- //END PAGE HEADING -->

        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaiting">
                                        <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <div class="row" runat="server" visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            
                            
                               <div class="row">
                                    <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server"  >
                                    <div class="form-group">
                                        <label>Financial Year </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="SeparationFinancialYearDropDownList" class="form-control form-control-sm" runat="server" OnSelectedIndexChanged="SeparationFinancialYearDropDownList_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                            
                            
                            
                                <div class="col-md-2" runat="server"  >
                                    <div class="form-group">
                                        <label>Survey Name  </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="SurveyNameDropDownList" class="form-control form-control-sm" runat="server"  ></asp:DropDownList>
                                    </div>
                                </div>
                               </div>
                                   <br />
                                    <br />

                                    <div class="row">
                                        <div class="col-md-4">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="lbReset" OnClick="lbReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                    </div>
                            <br/>
                            <br/>
                            <br/>
                            
                                 <div id="gridContaindder1" style="height: 300px; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;" >
                                
                                <asp:GridView ID="EmpSaveGridView1" runat="server" AutoGenerateColumns="False"
                                   CssClass="table table-bordered text-center thead-dark" DataKeyNames="EmpInfoId"
                                    OnRowCommand="loadGridView_RowCommand"  Font-Size="12px"  PageIndex="1"    >
                                    <Columns>
                                        
                                         
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Report">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmpInfoId") %>'
                                                        CommandName="ViewReport" ImageUrl="~/Assets/report_magnify.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    
                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="DepartmentName"  HeaderText="Department" />
                                       
                                        <asp:BoundField DataField="SalaryLocation"  HeaderText="Office" />
                                        <asp:BoundField DataField="EmpType"  HeaderText="Employee Type" />
                                      


                                       
                                    </Columns>
                                    
                                </asp:GridView>
                                 </div>
                            
                               </ContentTemplate>
                     <%--   <Triggers>
                           <asp:PostBackTrigger ControlID="btnExportToExcel"/>
                            </Triggers>--%>
       
                </asp:UpdatePanel>
                    </div>
            </div>
        </div>
         </div>
        <br />
        <br />
        <br />
        <br />
        <br />
</asp:Content>

