<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="AreaInformationView.aspx.cs" Inherits="MasterSetup_UI_AreaInformationView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Area Information List </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                             <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label> </label>
                                             <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True"  RepeatDirection="Horizontal" Enabled="False" >
                                        </asp:CheckBoxList>
                                            

                                        </div>
                                    </div>
                                </div>
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="AreaId"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="AreaName" HeaderText="Area Name" />
                                        <asp:BoundField DataField="AreaCode" HeaderText="Area Code" />
                                        <asp:BoundField DataField="RegionName" HeaderText="Region Name" />
                                        <asp:BoundField DataField="IsActive" HeaderText="Active State" />
                                        <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                        <asp:BoundField DataField="EntryBy" HeaderText="Entry By" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="UpdateBy" HeaderText="UpdateBy" />
                                        <asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="editImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                                    ImageUrl="~/Assets/img/rsz_edit.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="deleteImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="DeleteData"
                                                    ImageUrl="~/Assets/img/delete.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="View" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="viewImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                                    ImageUrl="~/Assets/img/list-view.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

