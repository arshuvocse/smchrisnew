<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="MenuGroupSetup.aspx.cs" Inherits="MenuSetup_MenuGroupSetup" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-group.required .control-label:after { 
            color: #d00;
            content: "*";
            position: absolute;
            margin-left: 4px;
            top:4px;
            font-size: large;
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
            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Menu Group Setup</h1>
        </div>
        <div class="page-heading__container float-right d-none d-sm-block">
            
              <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton> 
        </div>
    </div>
    <div class="card">
        <div class="card-body">
                                
            <div class="form-row">
                <div class="col-3" runat="server" Visible="False">
                    <div class="form-group"  >
                        <label class="control-label">Company</label>
                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm"/>
                    </div>
                </div>
                <div class="col-3" runat="server" Visible="False">
                    <div class="form-group">
                        <label>Menu Type</label>
                        <asp:DropDownList runat="server" ID="ddlMenuType" AutoPostBack="True" class="form-control form-control-sm" OnSelectedIndexChanged="ddlMenuType_OnSelectedIndexChanged"/>
                    </div>
                </div>
                <div class="col-3">
                    <div class="form-group required">
                        <label class="control-label">Group Name</label>
                        <asp:TextBox runat="server"  ID="txt_GroupName" class="form-control form-control-sm" ></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hdMenuGroupSetupId"/>
                        <ajaxToolkit:AutoCompleteExtender
                            ID="at_txt_GroupName"
                            TargetControlID="txt_GroupName"
                            runat="server"
                            ServiceMethod="GetMenuGroupNameAuto"
                            ServicePath="~/WebService.asmx"
                            MinimumPrefixLength="2"
                            CompletionInterval="10"
                            EnableCaching="false"
                            CompletionSetCount="1"
                            FirstRowSelected="false">
                        </ajaxToolkit:AutoCompleteExtender>
                    </div>
                </div>
                <div class="col-3">
                    <div class="form-group">
                        <label class="control-label">Group Description</label>
                        <asp:TextBox runat="server"  ID="txt_GroupDesc" class="form-control form-control-sm" ></asp:TextBox>
                    </div>
                </div>

            </div>
            <br />
            <div>
                <asp:GridView Width="100%" ID="gv_Menu" runat="server" AutoGenerateColumns="false" 
                     CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"  >
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                                <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("MenuGroupSetupDetailId") %>'/>
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
                        
                        <asp:TemplateField HeaderText="Add">
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkAddAll" Text="Add" OnCheckedChanged="chkAddAll_OnCheckedChanged"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkSingleAdd" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkEditAll" Text="Edit" OnCheckedChanged="chkEditAll_OnCheckedChanged"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkSingleEdit"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View">
                             <HeaderTemplate>
                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkViewAll" Text="View" OnCheckedChanged="chkViewAll_OnCheckedChanged"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkSingleView" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkDelAll" Text="Delete" OnCheckedChanged="chkDelAll_OnCheckedChanged"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkSingleDelete"/>
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
