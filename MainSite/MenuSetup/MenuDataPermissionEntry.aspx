<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MenuDataPermissionEntry.aspx.cs" Inherits="MenuSetup_MenuDataPermissionEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    <div class="content" id="content">
<asp:UpdatePanel ID="upFormBody" runat="server">
<ContentTemplate>
    <div class="container-fluid">
    <div class="page-heading">
        <div class="page-heading__container">
            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Menu Group Setup</h1>
        </div>
        <div class="page-heading__container float-right d-none d-sm-block">
            <%--<asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />--%>
        </div>
    </div>
    <div class="card">
        <div class="card-body">
                                
            <div class="form-row">
                
                <div class="col-3">
                    <div class="form-group">
                        <label>User</label>
                        <asp:DropDownList runat="server" ID="ddlUser" AutoPostBack="True" OnSelectedIndexChanged="ddlUser_OnSelectedIndexChanged"  class="form-control form-control-sm"/>
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
                                <%--<asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("MenuGroupSetupDetailId") %>'/>--%>
                                <asp:HiddenField runat="server" ID="hdMainMenuId" Value='<%#Eval("MainMenuId") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkAll" OnCheckedChanged="chkAll_OnCheckedChanged"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkSingle" AutoPostBack="True" OnCheckedChanged="chkSingle_OnCheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section">
                            <ItemTemplate>
                                <asp:Label ID="txt_Parent" runat="server" class="form-control form-control-sm" Text='<%#Eval("Parent") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Menu Name">
                            <ItemTemplate>
                                <asp:Label ID="txt_MenuName" runat="server" class="form-control form-control-sm" Text='<%#Eval("MenuName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Own">
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkOwnAll" Text="OWN" OnCheckedChanged="chkAddAll_OnCheckedChanged"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkSingleOwn" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SMC">
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkSMCAll" Text="SMC" OnCheckedChanged="chkEditAll_OnCheckedChanged"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkSingleSMC"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SMCEL">
                             <HeaderTemplate>
                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkSMCELAll" Text="SMCEL" OnCheckedChanged="chkViewAll_OnCheckedChanged"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkSMcELView" />
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
                <%--<asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />--%>
            </div>
                          
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</div>

</asp:Content>

