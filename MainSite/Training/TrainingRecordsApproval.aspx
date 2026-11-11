<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingRecordsApproval.aspx.cs" Inherits="Training_TrainingRecords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="trainingListpContent" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

               <div class="container-fluid">
                   <div class="page-heading">
                      <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;">Training Records Approval</h1>
                    </div>
                         <div class="page-heading__container float-right d-none d-sm-block">
                             <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <%--<asp:Button ID="btnAddNewTraining" Text="Add New Training" CssClass="btn btn-sm btn-outline-secondary" OnClick="btnAddNewTraining_OnClick" runat="server"  />--%>
                    </div>
                   </div>
                   <div class="card">
                    <div class="card-body">
                        <div class="row">
                              
                                 </div>
                        <div class="row">
                                
                                 
                                 </div>
                        <asp:GridView ID="gv_trainingList" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark" DataKeyNames="EmpInfoId,TrainingRecordMasterId,TrainingRecordMasterAppLogId">

                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hdpk" runat="server" Value='<%#Eval("TrainingRecordMasterId") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Training Number">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Numer" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingRecordNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Training Title">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Title" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Company" runat="server" class="form-control form-control-sm" Text='<%#Eval("ShortName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Fianacial Year ">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_finYear" runat="server" class="form-control form-control-sm" Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type ">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_type" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Training Orgaization ">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Torg" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingOrgName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                              <asp:TemplateField HeaderText="Operations">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lb_Edit" OnClick="lb_Edit_OnClick"  runat="server" >Approve</asp:LinkButton>|
                                             

                                        </ItemTemplate>
                                    </asp:TemplateField>
                              <%--  <asp:TemplateField >
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkAll" OnCheckedChanged="chkAll_OnCheckedChanged"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSingle" />
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

