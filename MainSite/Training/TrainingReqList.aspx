<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingReqList.aspx.cs" Inherits="Training_TrainingReqList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">

    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Training Requisition List</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="New Requisition " CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                            <asp:GridView ID="gv_EmpDetails" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requisition No">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="reqId" runat="server" Value='<%#Eval("TrainingRequisitionMasterId") %>'></asp:HiddenField>
                                                    <asp:Label ID="TrainingReqNumber" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingReqNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Training Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="TrainingTitle" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingTitle") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="Company">
                                                <ItemTemplate>
                                                    <asp:Label ID="ShortName" runat="server" class="form-control form-control-sm" Text='<%#Eval("ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Financial Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="FinancialYearDesc" runat="server" class="form-control form-control-sm" Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>

                                                   
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          


                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_edit" OnClick="lb_edit_Click"  runat="server">Edit</asp:LinkButton>|
                                                      <asp:LinkButton ID="lb_remove"  runat="server">Remove</asp:LinkButton>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                            <div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

