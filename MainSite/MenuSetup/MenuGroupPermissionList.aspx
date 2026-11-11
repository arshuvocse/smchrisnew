<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="MenuGroupPermissionList.aspx.cs" Inherits="MenuSetup_MenuGroupPermissionList" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Assets/MaterialDT/dataTables.material.min.css" rel="stylesheet" />
    <link href="../Assets/MaterialDT/material.min.css" rel="stylesheet" />
    <script src="../Assets/MaterialDT/jquery-3.3.1.js"></script>
    <script src="../Assets/MaterialDT/jquery.dataTables.min.js"></script>
    <script src="../Assets/MaterialDT/dataTables.material.min.js"></script>
    <style type="text/css">
        .dt-table {
            padding: 0px !important;
            width: 100%;
        }

           table#cpFormBody_gv_Menu > tbody > tr > th {
            padding: 9px 0!important;
            color: #fff!important;
            background-color: #5B799E!important;
            /*background-color: #98A9C0;*/
        }

        table#cpFormBody_gv_Menu > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
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
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Menu Group Permission List</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="btn_New" Text="Create New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btn_New_OnClick"/>
                    </div>
                </div>
                <div class="card">
                    <div class="card-body">
                        <div>
                            <asp:GridView Width="100%" ID="gv_Menu" runat="server" ShowFooter="False" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark" OnRowDataBound="gv_Menu_OnRowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hdpk" runat="server" Value='<%#Eval("MenuGroupPermissionId") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Company" runat="server"  Text='<%#Eval("ShortName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="User">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_User" runat="server"  Text='<%#Eval("UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Menu Group Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_MenuGroupName" runat="server"  Text='<%#Eval("MenuGroupName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lb_Edit" runat="server" OnClick="lb_Edit_Click"><img src="../Assets/img/rsz_edit.png"/></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lb_Remove" runat="server" OnClick="lb_Remove_Click"><img src="../Assets/img/delete.png"/></asp:LinkButton>
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
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            createDataTable();
        });
        createDataTable();
        function createDataTable() {
            $('#cpFormBody_gv_Menu').DataTable();
        }
    </script>
</asp:Content>


