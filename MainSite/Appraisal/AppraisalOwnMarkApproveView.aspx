<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalOwnMarkApproveView.aspx.cs" Inherits="Appraisal_AppraisalOwnMarkApproveView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    <div class="content">
        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                <div class="container-fluid" >
                    <div class="page-heading"  >
                       <div class="page-heading__container" >
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"> Appraisal Score</h1>
                        </div>
                       <%-- <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="" CssClass="btn btn-sm btn-outline-secondary " runat="server"  />
                        </div>--%>

                    </div>
                    
                    <div class="card">
                        <div class="card-body">
                            
                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" id="AppraisalOwnA" CssClass="table table-bordered text-center thead-dark gridDatatable">
                                
                                                <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="KPI Info">
                                    <ItemTemplate>
                                        <asp:Label ID="KpiInfo" runat="server" class="form-control form-control-sm" Text='<%#Eval("KpiInfo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="KpiWeight" runat="server" class="form-control form-control-sm" Text='<%#Eval("KpiWeight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mid Year Status">
                                    <ItemTemplate>
                                        <asp:Label ID="MidYearStatus" runat="server" class="form-control form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Target">
                                    <ItemTemplate>
                                        <asp:Label ID="Target" runat="server" class="form-control form-control-sm" Text='<%#Eval("Target") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Score">
                                    <ItemTemplate>
                                        <asp:Label ID="SelfMark" runat="server" class="form-control form-control-sm" Text='<%#Eval("SelfMark") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               
                                                    
                                                    

                                                    



                            </Columns>
                            </asp:GridView>
                            
                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" id="AppraisalOwnB" CssClass="table table-bordered text-center thead-dark gridDatatable">
                                
                                                <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Skill Info">
                                    <ItemTemplate>
                                        <asp:Label ID="SkillInfo" runat="server" class="form-control form-control-sm" Text='<%#Eval("SkillInfo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supporting Info">
                                    <ItemTemplate>
                                        <asp:Label ID="SupportingEmp" runat="server" class="form-control form-control-sm" Text='<%#Eval("SupportingEmp") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="Score" runat="server" class="form-control form-control-sm" Text='<%#Eval("Score") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Score">
                                    <ItemTemplate>
                                        <asp:Label ID="SelfScore" runat="server" class="form-control form-control-sm" Text='<%#Eval("SelfScore") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               
                                                    
                                                    

                                                    



                            </Columns>
                            </asp:GridView>
                            <asp:Button runat="server" OnClick="Approve_OnClick" ID="Approve" CssClass="btn btn-success" Text="Approve"/>
                            <asp:HiddenField runat="server" ID="appMaster"/>
                            
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

