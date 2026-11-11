<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalOwnMarkApprove.aspx.cs" Inherits="Appraisal_AppraisalOwnMarkApprove" %>

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
                            
                            
                            
                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" id="AppraisalOwn" CssClass="table table-bordered text-center thead-dark gridDatatable">
                                
                                                <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:HiddenField ID="id_empId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                        <asp:HiddenField ID="selfMaster" runat="server" Value='<%#Eval("AppraisalSelfMasterId") %>' />
                                        <asp:HiddenField ID="id_appraisalMaster" runat="server" Value='<%#Eval("AppraisalMasterId") %>' />
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_EmpMasterCode" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_EmpName" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date of Joining">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_DateOfJoin" runat="server" class="form-control form-control-sm" Text='<%#Eval("DateOfJoin") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_DepartmentName" runat="server" class="form-control form-control-sm" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Designation" runat="server" class="form-control form-control-sm" Text='<%#Eval("Designation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                <asp:TemplateField HeaderText="Functional">
                                    <ItemTemplate>
                                        <asp:Label runat="server"   Id="PartAOwn" Text='<%#Eval("PartA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Behavioral">
                                    <ItemTemplate>
                                        <asp:Label runat="server"   Text='<%#Eval("PartB") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                     <ItemTemplate>
                                       <asp:LinkButton runat="server" OnClick="View_OnClickClick" Id="View" Text="View"></asp:LinkButton>
                                         <asp:LinkButton runat="server"  Id="Approve" Text="Approve"></asp:LinkButton>
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
</asp:Content>

