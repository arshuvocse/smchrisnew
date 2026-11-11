<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="MenuGroupPermission.aspx.cs" Inherits="MenuSetup_MenuGroupPermission" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: #262626;
            filter: alpha(opacity=60);
            opacity: 0.5;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 300px;
            border-top: 3px solid #4D97C2;
            border-radius: 12px;
            -webkit-box-shadow: 1px 1px 4px 1px rgba(0,0,0,0.41);
            -moz-box-shadow: 1px 1px 4px 1px rgba(0,0,0,0.41);
            box-shadow: 1px 1px 4px 1px rgba(0,0,0,0.41);
        }
        .ListPadd {
            padding: 5px;
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
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Menu Group Permission</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                </div>
                <div class="card">
                    <div class="card-body">
                                
                        <div class="form-row">
                            <div class="col-3"  runat="server" Visible="False">
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
                                            <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("MenuGroupPermissionDtlId") %>'/>
                                            <asp:HiddenField runat="server" ID="hdMenuGroupSetupId" Value='<%#Eval("MenuGroupSetupId") %>'/>
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
                                    <asp:TemplateField HeaderText="Menu Group Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_MenuGroupName" runat="server" class="form-control form-control-sm" Text='<%#Eval("MenuGroupName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Menu Type">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_MenuType" runat="server" class="form-control form-control-sm" Text='<%#Eval("MenuTypeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Details">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lb_MenuGroupDetails" OnClick="lb_MenuGroupDetails_OnClick" Text="Group Details"></asp:LinkButton>
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
    

    <div>
        <ajaxToolkit:ModalPopupExtender ID="mpe_1" runat="server" TargetControlID="hnd_Test" PopupControlID="pnl_1"
                                        BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:HiddenField ID="hnd_Test" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnl_1" runat="server" Style="display: none; padding: 6px;padding-top: 10px;" Height="530px" Width="30%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <style>
                        .DemoTextttt {
                            text-shadow: 1px 2px 2px #000000;
                            font-size: 26px;
                            font-weight: bolder;
                       

                        }
                    </style>
                    <h2 style="text-align: center;">List of Menus for : <span><asp:Label  CssClass="DemoTextttt"   runat="server" ID="m_MemberName"></asp:Label></span></h2>
                     <hr/>
                    <div>
                        <asp:HiddenField runat="server" ID="m_hdpkd"></asp:HiddenField>
                         <div id="gridContainer1" style="height: 380px; overflow: auto; width: auto;">
                        <asp:ListView runat="server" ID="lvMenus" >
                                <ItemTemplate >
                                    
                                    <asp:Label ID="txt_MenuName" runat="server" CssClass="form-control ListPadd"   Text='<%#Eval("TextField") %>'></asp:Label>
                                </ItemTemplate>
                        </asp:ListView>
                             </div>
                       
                
                        
                        <hr/>
                         <br/>
                        <asp:Button ID="btnNo" Text=" Close " OnClick="btnNo_Click" CssClass="btn btn-sm btn-danger" runat="server"   />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>




