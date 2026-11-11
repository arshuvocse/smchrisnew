<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingBudgetReport.aspx.cs" Inherits="Report_Pages_TrainingBudgetReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style>
        #cpFormBody_gv_trainingBgtList > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

        #cpFormBody_gv_trainingBgtList > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }
    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                    <div class="page-heading" style="background-color: #F0FFFF; font-style: italic">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Training Budget Report</h1>
                        </div>
                        <%--<div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                            <asp:Button ID="addNewButton" Text="Add New Budget" OnClick="btnAddNewTrainingBudget_OnClick" CssClass="btn btn-sm btn-outline-secondary" runat="server" />
                        </div>--%>
                    </div>
                    <div class="card">
                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-3" runat="server" visible="False">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>


                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-1">
                                    <div class="form-group" >
                                        <label>&nbsp;&nbsp;</label>
                                        <asp:Button ID="btnFilterSearch" Text="Search" OnClick="btn_Search_OnClick" CssClass="btn btn-outline-info btn-block disabled btn-sm" runat="server" />

                                    </div>

                                </div>
                            </div>
                            <asp:GridView ID="gv_trainingBgtList" runat="server" DataKeyNames="TrainingBudget2Id" AutoGenerateColumns="false"
                               OnRowCommand="loadGridView_RowCommand" CssClass="table table-bordered text-center thead-dark">

                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hdpk" runat="server" Value='<%#Eval("TrainingBudget2Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Company" runat="server" class="form-control form-control-sm" Text='<%#Eval("ShortName") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Financial Year">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_finYear" runat="server" Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Budget Head">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Number" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Total Budget ">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Torg" runat="server" Text='<%#Eval("TotalBudget") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Preview">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("TrainingBudget2Id") %>'
                                                CommandName="ViewReport" ImageUrl="~/Assets/report_magnify.png" />
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

