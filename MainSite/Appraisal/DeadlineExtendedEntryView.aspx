<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="DeadlineExtendedEntryView.aspx.cs" Inherits="Appraisal_DeadlineExtendedEntryView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content">
        
        
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"> Deadline Extension  Request View </h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" OnClick="detailsViewButton_OnClick" Visible="False" Text="Add New" CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                        </div>

                    </div>

                    <div class="card">
                        <div class="card-body">


                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>


                                    </div>
                                </div>
                            </div>

                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_kpiSetup" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="DeadlineExtensionRequestId" runat="server" Value='<%#Eval("DeadlineExtensionRequestId") %>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="CompanyName" runat="server" class="form-control form-control-sm" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Financial Year">
                                        <ItemTemplate>
                                            <asp:Label ID="FinancialYearDesc" runat="server" class="form-control form-control-sm" Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="DepartmentName" runat="server" class="form-control form-control-sm" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Extension Date" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="ExtensionDate" runat="server" class="form-control form-control-sm" DataFormatString="{0:dd-MMM-yyyy}" Text='<%#Eval("ExtensionDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Entry By">
                                        <ItemTemplate>
                                            <asp:Label ID="EntryBy" runat="server" class="form-control form-control-sm" Text='<%#Eval("EntryBy") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Entry Date">
                                        <ItemTemplate>
                                            <asp:Label ID="EntryDate" runat="server" class="form-control form-control-sm" DataFormatString="{0:dd-MMM-yyyy}" Text='<%#Eval("EntryDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    

                                    <asp:TemplateField HeaderText="Edit" Visible="False">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" OnClick="btn_edit_OnClick" ID="btn_edit" Text="Edit"></asp:LinkButton>                                    
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Delete" Visible="False">
                                        <ItemTemplate>
                                      <asp:LinkButton runat="server" ID="btn_Remove" OnClick="btn_Remove_OnClick" Text="Remove"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" OnClick="btn_View_OnClick" ID="btn_View" Text="View"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
           
      
    </div>
</asp:Content>
