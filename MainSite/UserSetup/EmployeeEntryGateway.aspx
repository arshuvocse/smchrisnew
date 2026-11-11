<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="EmployeeEntryGateway.aspx.cs" Inherits="UserSetup_EmployeeEntryGateway" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Employee Information Entry Gateway</h1>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <asp:LinkButton runat="server" ID="lb_ExistingEmp" Text="Existing Employee" OnClick="lb_ExistingEmp_OnClick"></asp:LinkButton>
                                    </div>
                                </div>
                                
                                <div class="col-3">
                                    <div class="form-group">
                                        <asp:LinkButton runat="server" ID="lb_NewEmp" Text="New Employee From Interview Process" OnClick="lb_NewEmp_OnClick"></asp:LinkButton>
                                    </div>
                                </div>

                            </div>
                            
                            <br />
                            <div>
                                <asp:GridView Width="100%" ID="gv_InterviewBoardMember" runat="server" 
                                              AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark"
                                              >
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpID" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpDesignation" runat="server" class="form-control form-control-sm" Text='<%#Eval("Designation") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpDepartment" runat="server" class="form-control form-control-sm" Text='<%#Eval("DepartmentName") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpCompany" runat="server" class="form-control form-control-sm" Text='<%#Eval("CompanyName") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpEmail" runat="server" class="form-control form-control-sm" Text='<%#Eval("OfficialEmail") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Email Body">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmailBody" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmailBody") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Phone">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpPhone" runat="server" class="form-control form-control-sm" Text='<%#Eval("OfficialMobile") %>'></asp:TextBox>
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
