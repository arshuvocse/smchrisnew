<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="MPBudgetListReport.aspx.cs" Inherits="Report_Pages_MPBudgetListReport" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading" style="font-style: italic">
                    <div class="page-heading__container">
                        <div class="icon"></div>
                        <span></span>
                        <h1 class="title" style="font-size: 16px; padding-top: 1px;">Manpower Budget List Report</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                         <asp:Button ID="homeButton" Visible="False" Text="Home" CssClass="btn btn-sm btn-outline-secondary "  runat="server" OnClick="vcchomeButton_OnClick" />
                        <asp:Button ID="btn_New" Text="Create New" Visible="False" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btn_New_OnClick" />
                        <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>

                </div>


                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <div class="row" runat="server" visible="false">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList runat="server" ID="ddlDepartment" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList runat="server" ID="ddlFinYear" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-1 ">
                                    <div class="form-group" style="margin-top: 16px;">
                                       
                                        <asp:Button ID="btnFilterSearch" Text="Search" OnClick="btnFilterSearch_OnClick" CssClass="btn btn-outline-info btn-block disabled btn-sm" runat="server" />
                                        
                                    </div>
                                </div>
                            </div>

                            

                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered  text-center thead-light" DataKeyNames="MPBudgetMasterId"
                                    OnRowCommand="loadGridView_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="Report">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("MPBudgetMasterId") %>'
                                                        CommandName="ViewReport" ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       <%-- <asp:BoundField DataField="ShortName" HeaderText="Company" />--%>
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                        <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />
                                        <%--<asp:BoundField DataField="Designation" Visible="False" HeaderText="Budget Code" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />--%>

                                     <%--   <asp:TemplateField HeaderText="Edit">
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
                                            <HeaderStyle Width="115px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="viewImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                                    ImageUrl="~/Assets/img/list-view.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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

