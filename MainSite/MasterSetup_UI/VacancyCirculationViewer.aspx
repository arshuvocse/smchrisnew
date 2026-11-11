<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="VacancyCirculationViewer.aspx.cs" Inherits="MasterSetup_UI_VacancyCirculationViewer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">


      <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Vacancy Circulation List</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
                    </div>
                    <nav aria-label="breadcrumb" role="navigation">
                        <ol class="breadcrumb">
                          <%--  <li class="breadcrumb-item"><a href="../DashBoard_UI/DashBoard.aspx">Master Setup </a></li>
                            <li class="breadcrumb-item"><a href="#">Vacancy Circulation View</a></li>--%>
                        </ol>
                    </nav>
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="VacancyCirculationId"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="CirculationWay" HeaderText="Circulation Way" />   
                                       
                                        <asp:BoundField DataField="IsActive" HeaderText="Active Status"/>
                                        <asp:BoundField DataField="EntryBy" HeaderText="Create By"/>
                                        <asp:BoundField DataField="EntryDate" HeaderText="Create Date" DataFormatString="{0:dd-MMM-yyyy}" />                                        
                                        <asp:BoundField DataField="UpdateBy" HeaderText="Update By"/>
                                        <asp:BoundField DataField="UpdateDate" HeaderText="Update Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="115px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="editImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                                    ImageUrl="~/Assets/img/rsz_edit.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="115px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="deleteImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="DeleteData"
                                                    ImageUrl="~/Assets/img/delete.png" />
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

