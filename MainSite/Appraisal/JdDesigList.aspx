<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="JdDesigList.aspx.cs" Inherits="Appraisal_JdList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    <div class="content">
        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                <div class="container-fluid" >
                    <div class="page-heading">
                       <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"> Job Description List</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" OnClick="detailsViewButton_OnClick" Text="Add New" CssClass="btn btn-sm btn-outline-secondary "  runat="server"  />
                        </div>

                    </div>
                    
                    <div class="card">
                        <div class="card-body">
                            
                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" id="gv_JdBoard" CssClass="table table-bordered text-center thead-dark gridDatatable">
                                
                                                <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:HiddenField ID="JdMasterId" runat="server" Value='<%#Eval("JdDesigMasterId") %>' />
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>

                                                    <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                                    <asp:BoundField DataField="Designation" HeaderText="Job Title" />
                                                    <asp:BoundField DataField="FinancialYearDesc" HeaderText="Fin Year" />
                                                    
                                   <asp:TemplateField HeaderText="Opetation">
                                    <ItemTemplate>
                                      <asp:LinkButton runat="server" OnClick="btn_edit_OnClick" Id="btn_edit" Text="Edit"></asp:LinkButton>|
                                      <asp:LinkButton runat="server" OnClick="btn_Remove_OnClick" Id="btn_Remove" Text="Remove"></asp:LinkButton>
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


