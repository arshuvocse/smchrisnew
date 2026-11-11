<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SupervisorApprovalEntry.aspx.cs" Inherits="MenuSetup_SupervisorApprovalEntry" %>

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
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> Supervisor Approval Setup </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <%--<asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />--%>
                    </div>
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                             <div class="row" >
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" CssClass="" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Menu Name</label>
                                        <asp:DropDownList ID="menuDropDownList" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="menuDropDownList_OnSelectedIndexChanged" CssClass="" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="SuperMenuAppId,MainMenuId,IsChecked,EmpInfoId,DepartmentId">
                                    <Columns>
                                       <asp:TemplateField HeaderText="Delete">
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkAll" Text="Select" OnCheckedChanged="chkAll_OnCheckedChanged"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSingle"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
                                        <asp:BoundField DataField="MenuName" HeaderText="Manu Name" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                        <asp:TemplateField HeaderText="Employee Information">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="employeeDropDownList" class="form-control form-control-sm" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="True" runat="server" OnClick="submitButton_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


</asp:Content>

