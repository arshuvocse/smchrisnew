<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MBSCOKRAppraisalSelfList.aspx.cs" Inherits="Appraisal_MBSCOKRAppraisalSelfList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style>
        #cpFormBody_gv_JdBoard td {
            padding: 8px;
        }


        .MyBtnInfoCss {
            background-color: #880e4f !important;
            color: white !important;
            border: none !important;
            padding: 6px 18px !important;
            text-align: center !important;
            text-decoration: none !important;
            display: inline-block !important;
            font-size: 16px !important;
            cursor: pointer !important;
            -webkit-transition-duration: 0.4s !important;
            transition-duration: 0.4s !important;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 3px 10px 0 rgba(0,0,0,0.19) !important;
        }

        .MyBtnInfoCss2 {
            background-color: #17a2b8 !important;
            color: white !important;
            border: none !important;
            padding: 6px 18px !important;
            text-align: center !important;
            text-decoration: none !important;
            display: inline-block !important;
            font-size: 16px !important;
            cursor: pointer !important;
            -webkit-transition-duration: 0.4s !important;
            transition-duration: 0.4s !important;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 3px 10px 0 rgba(0,0,0,0.19) !important;
        }
    </style>
    <div class="content">
        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                <img src="../Report_Pages/app.png" width="20px" />
                                BSC/OKR Mid-Year Setup</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block" runat="server">
                            <asp:LinkButton ID="homeButton" Style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="vcchomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>

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
                                <div class="col-md-2" runat="server" visible="False">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server">
                                    <div class="form-group">
                                        <label>Financial Year &nbsp;<span style="color: #a52a2a">*</span></label>

                                        <asp:DropDownList ID="FinancialYearDropDownList" class="form-control form-control-sm" runat="server" OnSelectedIndexChanged="FinancialYearDropDownList_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-9">
                                </div>

                                <div class="col-md-1" style="padding-top: 2px">
                                    <label style="font-weight: bold">Select Option:&nbsp;<span style="color: #a52a2a">*</span></label>
                                </div>
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">

                                        <asp:RadioButtonList CssClass="chkChoiceHeader" runat="server" ID="radOp" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="radOp_OnSelectedIndexChanged">
                                            <asp:ListItem Text="Own" Selected="True" Value="Own" />
                                            <asp:ListItem Text="Supervisor" Value="Supervisor" />
                                        </asp:RadioButtonList>
                                    </div>

                                </div>
                            </div>
                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gv_JdBoard_OnRowDataBound" ID="gv_JdBoard" CssClass="table table-bordered text-center thead-dark gridDatatable" DataKeyNames="ActionStatus">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="EmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                            <asp:HiddenField ID="FinancialYearId" runat="server" Value='<%#Eval("FinancialYearId") %>' />
                                            <asp:HiddenField ID="CurrentStatus" runat="server" Value='<%#Eval("CurrentStatus") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Financial Year">
                                        <ItemTemplate>
                                            <asp:Label ID="FinancialYearDesc" runat="server" Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Deadline Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExtensionDate" runat="server" Text='<%#Eval("ExtensionDate", "{0:dd- MMM -yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Employee Info">
                                        <ItemTemplate>
                                            <asp:Label ID="employee" runat="server" Text='<%#Eval("employee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="DepartmentName" runat="server" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Approval Status">
                                        <ItemTemplate>
                                            <asp:Label ID="Approval" runat="server" Text='<%#Eval("KPIActionStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Awaiting Employee">
                                        <ItemTemplate>
                                            <asp:Label ID="Awaiting" runat="server" Text='<%#Eval("PendingEmp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--  <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="CompanyName" runat="server" class="form-control form-control-sm" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton CssClass="btn btn-sm btnMyDesignSearch" runat="server" ID="btn_edit" OnClick="btn_edit_OnClick">
               
  	 <i class="fa fa-check-circle"></i>&nbsp; Update Mid-Year Status
                                            </asp:LinkButton>
                                            &nbsp;	
                                     &nbsp;	
                                            <asp:LinkButton runat="server" CssClass="btn btn-sm btnMyDesignAddtoList" ID="btn_view" OnClick="btn_eview_OnClick">	<i class="fa fa-eye"></i> &nbsp;View</asp:LinkButton>
                                            &nbsp;	
                                     &nbsp;	
                                              <asp:LinkButton   runat="server" ID="btn_print" OnClick="btn_print_OnClick" CssClass="btn btn-sm btn-info"><i class="fa fa-print" aria-hidden="true"></i> &nbsp; Print
                                              </asp:LinkButton>
                                            &nbsp;	
                                     &nbsp;	
                                              <asp:Label ID="lblExpiere" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--   <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                      <asp:LinkButton runat="server" ID="btn_Remove" Text="Remove"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


