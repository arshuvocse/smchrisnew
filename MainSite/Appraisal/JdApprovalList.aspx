<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="JdApprovalList.aspx.cs" Inherits="Appraisal_JdList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content">
        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Job Description Approval </h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <%--<asp:Button ID="detailsViewButton" OnClick="detailsViewButton_OnClick" Text="Add New" CssClass="btn btn-sm btn-outline-secondary " runat="server" />--%>
                        </div>

                    </div>
                    <style>.MyBtnInfoCss {
    
                                                   background-color:#880e4f !important;
                                                    color:white !important;
                                                   border: none!important;
                                                  
                                                   padding: 6px 18px!important;
                                                   text-align: center!important;
                                                   text-decoration: none!important;
                                                   display: inline-block!important;
                                                   font-size: 16px!important;
                                                
                                                   cursor: pointer!important;
                                                   -webkit-transition-duration: 0.4s!important;
                                                   transition-duration: 0.4s!important;
                                                   box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 3px 10px 0 rgba(0,0,0,0.19)!important;
                                               }</style>
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

                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_JdBoard" CssClass="table table-bordered text-center thead-dark gridDatatable" DataKeyNames="JDAppLogId">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="JdMasterId" runat="server" Value='<%#Eval("JdMasterId") %>' />
     <asp:HiddenField ID="empIdHF" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpMasterCode" runat="server"  Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                  <%--  <asp:TemplateField HeaderText="Employee">
                                        <ItemTemplate>
                                            <asp:Label ID="employee" runat="server"  Text='<%#Eval("employee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    
                                    
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="EmpName" runat="server"  Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    


                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="Designation" runat="server"  Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="DepartmentName" runat="server"  Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Summary">
                                        <ItemTemplate>
                                            <asp:Label ID="JdSummary" runat="server"  Text='<%#Eval("JdSummary") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Approval">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server"  CssClass="MyBtnInfoCss" OnClick="btn_edit_OnClick" ID="btn_edit" > Go to Approval </asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   <%-- <asp:TemplateField HeaderText="Remove">
                                        <ItemTemplate>
                                      <asp:LinkButton runat="server" OnClick="btn_Remove_OnClick" ID="btn_Remove" Text="Remove"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" OnClick="btn_view_OnClick" ID="btn_view" Text="View"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>









                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


