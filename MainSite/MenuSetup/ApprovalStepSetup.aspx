<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="ApprovalStepSetup.aspx.cs" Inherits="MenuSetup_ApprovalStepSetup" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function switchViews(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../Assets/img/add.png";
            } else {
                div.style.display = "none";
                img.src = "../Assets/img/minus.png";
            }
        }
    </script>
    <style>
        .about-team-right
        {
            float: right;
            width: 80%;
            z-index: -99;
        }
    </style>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                <div class="page-heading">
                    <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;">Apporval Step Setup</h1>
                    </div>
<%--                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
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
                        </div>
                        <br />
                         <div class="form-row">
                            <div class="col-6">
                            <asp:GridView Width="100%" ID="gv_Menu" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark" 
                                OnRowDataBound="gv_Menu_OnRowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                           
                                            <asp:HiddenField runat="server" ID="hdMainMenuId" Value='<%#Eval("MainMenuId") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Parent Menu Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_MenuName" runat="server"  Text='<%#Eval("MenuName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField ItemStyle-Width="100px" HeaderText="Child Menu">
                                        <ItemTemplate>
                                            <a href="javascript:switchViews('div<%# Eval("SerialNo") %>', 'one');">
                                                <img id="imgdiv<%# Eval("SerialNo") %>" alt="Click to show/hide orders" border="0"
                                                     src="../Assets/img/add.png" />
                                            </a>
                                            <asp:HiddenField ID="hfMainMenuId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "MainMenuId") %>'>
                                            </asp:HiddenField>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="80%">
                                                    <div id="div<%# Eval("SerialNo") %>" style="overflow: auto; right: 10px; display: initial;
                                                                                                                          position: relative;">
                                                    <asp:GridView ID="gv_Child" runat="server" Width="80%" CssClass="about-team-right"
                                                                  AutoGenerateColumns="false" DataKeyNames="MainMenuId" >
                                                        <RowStyle CssClass="rowStyle" />
                                                        <AlternatingRowStyle CssClass="alternetRowStyle" />
                                                        <Columns>
                                                            <asp:TemplateField >
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox runat="server" AutoPostBack="True" ID="chkAll" Text="Check All" OnCheckedChanged="chkAll_OnCheckedChanged"/>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" ID="chkSingle" AutoPostBack="True"  OnCheckedChanged="chkSingle_OnCheckedChanged"/>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Menu Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txt_MenuName" runat="server"  Text='<%#Eval("MenuName") %>'></asp:Label>
                                                                    <asp:HiddenField runat="server" ID="hdMainMenuId" Value='<%#Eval("MainMenuId") %>'/>
                                                                    <asp:HiddenField runat="server" ID="hdApprovalStepSetupId" Value='<%#Eval("ApprovalStepSetupId") %>'/>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Approval Step">
                                                                <ItemTemplate>
                                                                    <asp:CheckBoxList runat="server" ID="lchk_ApprovalStep" RepeatDirection="Horizontal">
                                                                    </asp:CheckBoxList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#4D92C1" ForeColor="White" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>   </div>
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
