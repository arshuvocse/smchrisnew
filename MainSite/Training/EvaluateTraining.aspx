<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EvaluateTraining.aspx.cs" Inherits="Training_EvaluateTraining" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="EvaluateTraining" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        
                 <style>
            #cpFormBody_gv_Topic > tbody > tr > th {
                padding: 9px 0;
                color: #fff;
                background-color: #5B799E;
                /*background-color: #98A9C0;*/
            }

            #cpFormBody_gv_Topic > tbody > tr:not(th):nth-child(odd) {
                background-color: #DFDFDF;
            }

            .chekDesign label {
                padding-left: 5px;
                padding-right: 5px;
            }
</style>
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                          
                             <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />  TRAINING EVALUATION FORM </h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="cancelButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <h5></h5>

                                
                                
                            </div>

                            
                            <div >


                      
             <style>
                 .elegantshd {
                     color: #131313;
  
                     letter-spacing: .15em;
                     text-shadow: 2px 2px 4px #000000;
                     text-decoration: underline;
                     font-family: 'Kreon', serif;
                     vertical-align:middle;  text-decoration-style: wavy;
                 }
             </style> 
                             <asp:GridView  ShowFooter="False" ID="gv_Topic" Width="100%"    CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender"  AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField runat="server" ID="detailsId" Value='<%#Eval("EvaluationFormDetailsId") %>'/>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Trainging Heading">
                                                <ItemTemplate>
                                                    <asp:Label ID="gv_heading" runat="server"   Text='<%#Eval("TraingingHeading") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trainging Topic ">
                                                <ItemTemplate>
                                                    <asp:Label ID="gv_topic" runat="server"   Text='<%#Eval("topic") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Failed">
                                                <ItemTemplate>
                                                     
                                                     <asp:CheckBox CssClass="chekDesign" ID="chk_failed" Text='<%#Eval("failed") %>' runat="server" AutoPostBack="True" OnCheckedChanged="chk_failed_OnCheckedChanged" ></asp:CheckBox>
                                                   <%-- <asp:Label  ID="gv_failed" runat="server" class="form-control form-control-sm" Text='<%#Eval("failed") %>'></asp:Label>--%>
                                                   
                                               
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Avarage">
                                                <ItemTemplate>
                                                     <asp:CheckBox  ID="chk_avarage" runat="server" AutoPostBack="True" OnCheckedChanged="chk_avarage_OnCheckedChanged"></asp:CheckBox>
                                                    <asp:Label  ID="gv_avarage" runat="server"   Text='<%#Eval("avarage") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Above Avarage">
                                                <ItemTemplate>
                                                      <asp:CheckBox  ID="chk_abvavarage" runat="server" AutoPostBack="True" OnCheckedChanged="chk_abvavarage_OnCheckedChanged" ></asp:CheckBox>
                                                    <asp:Label  ID="gv_above_avarage" runat="server"   Text='<%#Eval("abvavarage") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Excellent">
                                                <ItemTemplate>
                                                      <asp:CheckBox  ID="chk_excellent" runat="server" AutoPostBack="True" OnCheckedChanged="chk_excellent_OnCheckedChanged" ></asp:CheckBox>
                                                    <asp:Label  ID="gv_excellent" runat="server"   Text='<%#Eval("excellent") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                    </Columns>
                                </asp:GridView>
                                    <asp:GridView  ShowFooter="true" ID="GridView1" Width="100%"  CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField runat="server" ID="detailsId" Value='<%#Eval("EvaluationFormDetailsId") %>'/>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Training Course">
                                                <ItemTemplate>
                                                    <asp:Label ID="gv_topic" runat="server"   Text='<%#Eval("topic") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Failed">
                                                <ItemTemplate>
                                                     
                                                     <asp:CheckBox  ID="chk_failed" Text='<%#Eval("failed") %>' runat="server" ></asp:CheckBox>
                                                   <%-- <asp:Label  ID="gv_failed" runat="server" class="form-control form-control-sm" Text='<%#Eval("failed") %>'></asp:Label>--%>
                                                   
                                               
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Avarage">
                                                <ItemTemplate>
                                                     <asp:CheckBox  ID="chk_avarage" runat="server" ></asp:CheckBox>
                                                    <asp:Label  ID="gv_avarage" runat="server"   Text='<%#Eval("avarage") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Above Avarage">
                                                <ItemTemplate>
                                                      <asp:CheckBox  ID="chk_abvavarage" runat="server" ></asp:CheckBox>
                                                    <asp:Label  ID="gv_above_avarage" runat="server"   Text='<%#Eval("abvavarage") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Excellent">
                                                <ItemTemplate>
                                                      <asp:CheckBox  ID="chk_excellent" runat="server" ></asp:CheckBox>
                                                    <asp:Label  ID="gv_excellent" runat="server"   Text='<%#Eval("excellent") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                    </Columns>
                                </asp:GridView>
                                    <asp:GridView  ShowFooter="true" ID="GridView2" Width="100%"  CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField runat="server" ID="detailsId" Value='<%#Eval("EvaluationFormDetailsId") %>'/>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Training Course">
                                                <ItemTemplate>
                                                    <asp:Label ID="gv_topic" runat="server"   Text='<%#Eval("topic") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Failed">
                                                <ItemTemplate>
                                                     
                                                     <asp:CheckBox CssClass="chekDesign"   ID="chk_failed" Text='<%#Eval("failed") %>' runat="server" ></asp:CheckBox>
                                                   <%-- <asp:Label  ID="gv_failed" runat="server" class="form-control form-control-sm" Text='<%#Eval("failed") %>'></asp:Label>--%>
                                                   
                                               
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Avarage">
                                                <ItemTemplate>
                                                     <asp:CheckBox  ID="chk_avarage" CssClass="chekDesign" runat="server" ></asp:CheckBox>
                                                    <asp:Label CssClass="chekDesign"  ID="gv_avarage" runat="server"   Text='<%#Eval("avarage") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Above Avarage">
                                                <ItemTemplate>
                                                      <asp:CheckBox  ID="chk_abvavarage" CssClass="chekDesign" runat="server" ></asp:CheckBox>
                                                    <asp:Label  CssClass="chekDesign"  ID="gv_above_avarage" runat="server"   Text='<%#Eval("abvavarage") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Excellent">
                                                <ItemTemplate>
                                                      <asp:CheckBox  CssClass="chekDesign" ID="chk_excellent" runat="server" ></asp:CheckBox>
                                                    <asp:Label   ID="gv_excellent" runat="server" CssClass="chekDesign"   Text='<%#Eval("excellent") %>'></asp:Label>
                                                   
                                                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br/>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label> Comments</label>
                                        <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" class="form-control" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            

                            

                            
                                    <asp:HiddenField runat="server" ID="hdpk"/>
                                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_Click" Text="Submit " CssClass="btn btn-sm btn-info"/>
                                     <asp:Button ID="cancelButton" Text="&#8678; Back To List" Visible="False" OnClick="cancelButton_OnClick" CssClass="btn btn-sm btn-default" runat="server"  ForeColor="Blue" />
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

