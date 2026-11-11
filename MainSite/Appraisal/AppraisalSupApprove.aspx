<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalSupApprove.aspx.cs" Inherits="Appraisal_AppraisalSupApprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style>
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
    </style>
    <div class="content">
        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                <img src="../Report_Pages/app.png" width="20px" />
                                KPI Approval</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                               <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                            <%--<asp:Button ID="detailsViewButton" Text="Add New" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary "  runat="server"  />--%>
                        </div>

                    </div>

                    <div class="card">
                        <div class="card-body">

                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_JdBoard" CssClass="table table-bordered text-center thead-dark gridDatatable"
                                DataKeyNames="AppraisalSelfMasterId,AppraisalSelfAppLogId,ForEmpInfoId">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="AppraisalSelfMasterId" runat="server" Value='<%#Eval("AppraisalSelfMasterId") %>' />
                                            <asp:HiddenField ID="EmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                            <asp:HiddenField ID="FinancialYearId" runat="server" Value='<%#Eval("FinancialYearId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="employee" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="HFEmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="HFDesignation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="DepartmentName" runat="server" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Company">
                                    <ItemTemplate>
                                        <asp:Label ID="CompanyName" runat="server" class="form-control form-control-sm" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Financial Year">
                                        <ItemTemplate>
                                            <asp:Label ID="FinancialYearDesc" runat="server" Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Opetation">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_View" CssClass="MyBtnInfoCss" OnClick="btn_View_OnClick">Go to Approve</asp:LinkButton>
                                        <asp:Label ID="lblExpireStatus" runat="server" CssClass="alert-warning"  ToolTip="Please Inform HR to Extend Deadline"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>









                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
