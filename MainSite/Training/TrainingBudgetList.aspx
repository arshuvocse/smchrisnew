<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingBudgetList.aspx.cs" Inherits="Training_TrainingBudgetList" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="trainingListpContent" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Budget List</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="btnAddNewTrainingBudget" Text="Add New Budget" CssClass="btn btn-sm btn-outline-secondary" OnClick="btnAddNewTrainingBudget_Click" runat="server" />
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <asp:GridView ID="gv_trainingBgtList" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">

                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hdpk" runat="server" Value='<%#Eval("TrainingBudgetMasterId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Training No">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Number" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingBudgetNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Training Title">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Title" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Company" runat="server" class="form-control form-control-sm" Text='<%#Eval("ShortName") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Financial Year">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_finYear" runat="server" class="form-control form-control-sm" Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specific For ">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Torg" runat="server" class="form-control form-control-sm" Text='<%#Eval("ForSector") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Internal/External">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Year" runat="server" class="form-control form-control-sm" Text='<%#Eval("ExOrIn") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Local/Foreign ">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_LocForeign" runat="server" class="form-control form-control-sm" Text='<%#Eval("LocalForeign") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approval Status ">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Status" runat="server" class="form-control form-control-sm" Text='<%#Eval("ApprovalStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Operation">
                                        <ItemTemplate>
                                           
                                            <asp:LinkButton ID="lb_Edit" OnClick="lb_Edit_Click" runat="server">Edit</asp:LinkButton>|
                                             <asp:LinkButton ID="lb_delete" OnClick="lb_delete_Click" runat="server">Remove</asp:LinkButton>
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
