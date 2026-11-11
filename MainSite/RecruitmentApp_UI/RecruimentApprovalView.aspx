<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="RecruimentApprovalView.aspx.cs" Inherits="RecruitmentManagement_UI_JobCreationView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    
    
    
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
    </style>
    <div class="content" id="content">
        

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                        <h1 class="title" style="font-size: 16px; padding-top: 1px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Recruitment Approval List </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                         <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>

                    </div>

                </div>


                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">


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
                                
                            <div class="row" runat="server" Visible="False" id="divinfo">
                             
                                <div class="col-md-2">

                                    <div class="form-group">
                                        <label>Company Name: </label>
                                        <asp:DropDownList ID="companyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged"></asp:DropDownList>

                                    </div>
                                </div>

                                

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="financialYearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True" ></asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Department </label>
                                       
                                        <asp:DropDownList ID="deptDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>

                                </div>
                               
                                 <div class="col-md-2">
                                    <div class="form-group" style="margin-top: 17px;">
                                        
                                         <asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search"  Text="Search"    class="btn btn-info btn-sm"  /> 
                                        </div>
                                     </div>
                                     
                                      
                            </div> 

                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                      CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="RecruitmentId,RecruitmentAppLogId,ForEmpInfoId,JobID,CompanyId"
                                    OnRowCommand="loadGridView_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                   
                                                  <asp:HiddenField ID="JobReqId" runat="server" Value='<%#Eval("JobID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          
                                        <asp:BoundField DataField="Position" HeaderText="Job Title" />
                                        
                                        <asp:BoundField DataField="CirculationStartDate" DataFormatString="{0:dd-MMM-yyyy}"
                                            HeaderText="Circulation Date" />

                                     <%--   <asp:BoundField DataField="EntryBy" HeaderText="Create By" />--%>
                                        <asp:BoundField DataField="EntryDate" DataFormatString="{0:dd-MMM-yyyy}"
                                            HeaderText="Create Date" />
                                        <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />
                                        <asp:BoundField DataField="JobTitle" HeaderText="Designation" />
                                        <asp:BoundField DataField="ReqDate" HeaderText="Requisition Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="Nos" HeaderText="Total Vacancy" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />

                                   <%--     <asp:BoundField DataField="Updateby" HeaderText="Update by" />
                                        <asp:BoundField DataField="UpdateDate" DataFormatString="{0:dd-MMM-yyyy}"--%>
                                        <%--    HeaderText="Update Date" />--%>
                                    <asp:TemplateField HeaderText="Submit">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" BackColor="#0069D9" ToolTip="Go To Approval" BorderStyle="None" OnClick="btnSubmit_OnClick" ID="btnSubmit" >Go To Approval</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       

                                        
                                    </Columns>
                                    
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

