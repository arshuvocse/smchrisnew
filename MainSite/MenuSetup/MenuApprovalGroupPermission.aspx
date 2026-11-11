<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="MenuApprovalGroupPermission.aspx.cs" Inherits="MenuSetup_MenuApprovalGroupPermission" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                <div class="page-heading">
                    <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;">Menu Approval Group Permission</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                </div>
                <div class="card">
                    <div class="card-body">
                                
                        <div class="form-row">
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Company</label>
                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm"/>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>User</label>
                                    <asp:DropDownList runat="server" ID="ddlUser"  class="form-control form-control-sm"/>
                                </div>
                            </div>
                

                        </div>
                        <br />
                        <div>
                            <asp:GridView Width="100%" ID="gv_Menu" runat="server" AutoGenerateColumns="false" 
                                          CssClass="table table-bordered text-center thead-dark">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("MenuApprovalGroupPermissionDtlId") %>'/>
                                            <asp:HiddenField runat="server" ID="hdMenuApprovalGroupSetupId" Value='<%#Eval("MenuApprovalGroupSetupId") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField >
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" AutoPostBack="True" ID="chkAll" Text="Check All" OnCheckedChanged="chkAll_OnCheckedChanged"/>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkSingle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approval Group Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_MenuApprovalGroupName" runat="server" class="form-control form-control-sm" Text='<%#Eval("MenuApprovalGroupName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Details">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lb_MenuApprovalGroupDetails" OnClick="lb_MenuApprovalGroupDetails_OnClick" Text="Group Details"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                        
                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
                        <br />
                        <div>
                            <asp:HiddenField runat="server" ID="hdpk"/>
                            <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info"/>
                            <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />
                        </div>
                          
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

