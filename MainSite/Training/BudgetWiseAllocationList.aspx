<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="BudgetWiseAllocationList.aspx.cs" Inherits="Training_BudgetWiseAllocationList" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Assets/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Budget Wise Allocation</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="btnbgtWiseAllocation" Text="Budget Wise Allocation" OnClick="btnbgtWiseAllocation_OnClick" CssClass="btn btn-sm btn-outline-secondary"  runat="server"  />
                    </div>
                       
                    </div>
                </div>
                <div class="card">
                    <div class="card-body">
                        <asp:GridView ID="gv_trainingBgtList" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">

                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hdpk" runat="server" Value='<%#Eval("TrainingBudgetAllocationId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Training">
                                        <ItemTemplate>
                                            <asp:Label ID="Training" runat="server" class="form-control form-control-sm" Text='<%#Eval("Training") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Alloation No">
                                        <ItemTemplate>
                                            <asp:Label ID="BudgetAllocationNumber" runat="server" class="form-control form-control-sm" Text='<%#Eval("BudgetAllocationNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Quater">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("Quater") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specific For">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_SpecificFor" runat="server" class="form-control form-control-sm" Text='<%#Eval("SpecificFor") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                          

                                   <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            
                                            <asp:LinkButton ID="lb_Edit"  OnClick="lb_Edit_Click" runat="server">Edit</asp:LinkButton>|
                                             <asp:LinkButton ID="lb_remove" OnClick="lb_remove_Click"   runat="server">Remove</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>
                    </div>
                    
                   

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


</asp:Content>
