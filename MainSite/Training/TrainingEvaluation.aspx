<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingEvaluation.aspx.cs" Inherits="Training_TrainingEvaluation" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="trainingSetupContent" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Evaluation Form Setup</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block" runat="server" Visible="False">
                            <asp:Button ID="detailsViewButton" Visible="True" Text="Form Information " OnClick="detailsViewButton_Click" CssClass="btn btn-sm btn-outline-secondary" runat="server" />
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                
                                <div class="col-md-3" >
                                    <div class="form-group">
                                        <label>Training Heading</label>
                                        <asp:DropDownList ID="ddlTrainingheading"  runat="server" class="form-control form-control-sm" AutoPostBack="True"
                                             OnSelectedIndexChanged="ddlTrainingheading_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div> 
                                <div class="col-md-3" >
                                    <div class="form-group">
                                        <label>Training Topic</label>
                                        <asp:DropDownList ID="ddlTrainingtopic"  runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                

                            </div>

                            <div class="form-row">

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Failed</label>
                                        <asp:HiddenField ID="detailIdHiddenField" runat="server" Value="0" />
                                        <asp:TextBox ID="txt_failed" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control "></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">

                                        <label>Average</label>
                                        <asp:TextBox ID="txt_average" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control "></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Above Average</label>
                                        <asp:TextBox ID="txt_abv_avarage" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control "></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Excellent </label>
                                        <asp:TextBox ID="txt_excellent" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control "></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                      
                                        <asp:CheckBox ID="isActiveCheckBox" Text="Is Active" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <br/>
                            
                            <div class="form-row">
                                 <div class="col-5">
                                     </div>
                                <div class="col-1" style="align-content: center">
                                    <asp:Button Text="Add to List" runat="server" ID="addTopic" CssClass="btn btn-sm btn-info" OnClick="addTopic_Click" />
                                </div>
                                
                                 <div class="col-4">
                                     </div>
                            </div>
                             <br/>
                            
                            <div>
                                <asp:GridView ShowFooter="False" ID="gv_Topic" Width="100%" sh CssClass="table table-bordered text-center thead-dark" DataKeyNames="TrainingTopicId,TraingingHeadingId,EvaluationFormDetailsId" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <%--<asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>'/>--%>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Heading">
                                            <ItemTemplate>
                                                <asp:Label ID="gv_heading" runat="server"   Text='<%#Eval("heading") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Topic">
                                            <ItemTemplate>
                                                <asp:Label ID="gv_topic" runat="server"   Text='<%#Eval("topic") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Failed">
                                            <ItemTemplate>

                                                <asp:Label ID="gv_failed" runat="server"   Text='<%#Eval("failed") %>'></asp:Label>


                                                <%--<asp:HiddenField runat="server" ID="trainnerId"   />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Avarage">
                                            <ItemTemplate>

                                                <asp:Label ID="gv_avarage" runat="server"   Text='<%#Eval("avarage") %>'></asp:Label>


                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Above Avarage">
                                            <ItemTemplate>

                                                <asp:Label ID="gv_above_avarage" runat="server"   Text='<%#Eval("abvavarage") %>'></asp:Label>


                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Excellent">
                                            <ItemTemplate>

                                                <asp:Label ID="gv_excellent" runat="server"   Text='<%#Eval("excellent") %>'></asp:Label>


                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active Status">
                                            <ItemTemplate>

                                                <asp:Label ID="gv_active" runat="server"   Text='<%#Eval("IsActive") %>'></asp:Label>


                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                
                                                 
                                                
                                                <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_OnClick" runat="server"><img src="../Assets/img/rsz_edit.png" /></asp:LinkButton>
                                            </ItemTemplate>


                                        </asp:TemplateField>
                                        
                                          
                                         <asp:TemplateField HeaderText="Remove">
                                            <ItemTemplate>
                                                
                                                
                                                <asp:LinkButton ID="lb_remave" OnClick="lb_remave_Click" runat="server"><img src="../Assets/img/delete.png" /></asp:LinkButton>
                                                 </ItemTemplate>


                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>



                            <br />



                            <asp:HiddenField runat="server" ID="hdpk" />
                            <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_Click" Text="Submit " CssClass="btn btn-sm btn-info" />
                            <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
