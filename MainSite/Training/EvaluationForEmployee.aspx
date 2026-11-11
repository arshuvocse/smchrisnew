<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EvaluationForEmployee.aspx.cs" Inherits="Training_EvaluationForEmployee" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="trainingSetupContent" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        
        
         <style>
            #cpFormBody_gv_formList > tbody > tr > th {
                padding: 9px 0;
                color: #fff;
                background-color: #5B799E;
                /*background-color: #98A9C0;*/
            }

            #cpFormBody_gv_formList > tbody > tr:not(th):nth-child(odd) {
                background-color: #DFDFDF;
            }
</style>

        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />  Evaluation Form List</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                       
                        
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                             <asp:GridView   ID="gv_formList" Width="100%"   CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("TrainingRecordMasterId") %>'/>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderText="Company Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="CompanyName" runat="server" class="form-control form-control-sm" Text='<%#Eval("EvaluationFormNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                         <asp:TemplateField HeaderText="Financial Year">
                                                <ItemTemplate>
                                                     
                                                    <asp:Label  ID="FinancialYearDesc" runat="server"   Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Training Title">
                                                <ItemTemplate>
                                                    <asp:Label  ID="TrainingSetupNumber" runat="server"   Text='<%#Eval("TrainingTitle") %>'></asp:Label>
                                                       <%--<asp:HiddenField runat="server" ID="trainnerId"   />--%>
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Participant">
                                                <ItemTemplate>
                                                     
                                                    <asp:Label  ID="gv_avarage" runat="server"   Text='<%#Eval("EmpName") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                     <%--   <asp:TemplateField HeaderText="Company">
                                                <ItemTemplate>
                                                     
                                                    <asp:Label  ID="ShortName" runat="server" class="form-control form-control-sm" Text='<%#Eval("ShortName") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        

                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_edit" OnClick="btn_edit_Click" CssClass="btn btnMyDesignOne btn-sm"  runat="server"  >Evaluate  &nbsp;<i class="fa fa-forward"></i></asp:LinkButton>
                                                    
                                                      <asp:LinkButton ID="lbView" OnClick="lbView_Click"  CssClass="btn btnMyDesignOne btn-sm"   Visible="False" runat="server"
                                                        ><i class="fa fa-eye"></i></asp:LinkButton>
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