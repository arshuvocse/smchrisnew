<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ProbationInfoSetup.aspx.cs" Inherits="Training_TrainingEvaluation" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="trainingSetupContent" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Probation Information Setup</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Form Information " OnClick="detailsViewButton_Click" CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                
                                <div class="col-md-3" >
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="ddlCompany"  runat="server" class="form-control form-control-sm" AutoPostBack="True"
                                          OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged"   ></asp:DropDownList>
                                    </div>
                                </div> 
                               <div class="col-3">
                                    <div class="form-group">

                                        <label>Purpose</label>
                                        <asp:TextBox ID="purposeTextBox" runat="server"  CssClass="form-control "></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="evuId" Value="0"/>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Is Active </label>
                                        <asp:CheckBox ID="isActiveCheckBox" runat="server" />
                                    </div>
                                </div>
                            </div>


                            <div class="form-row">
                                <div class="col-3">
                                    <asp:Button Text="Add to grid" runat="server" ID="addTopic" CssClass="btn btn-sm btn-info" OnClick="addTopic_Click" />
                                </div>
                            </div>

                            <div>
                                <asp:GridView ShowFooter="true" ID="gv_Topic" Width="100%" CssClass="table table-bordered text-center thead-dark" DataKeyNames="tblProbationEvaluationRatingId,ValueField" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <%--<asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>'/>--%>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purpose">
                                            <ItemTemplate>
                                                <asp:Label ID="gv_purpose" runat="server" class="form-control form-control-sm" Text='<%#Eval("TextField") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>

                                                <asp:CheckBox ID="gv_active" runat="server" class="form-control form-control-sm" Checked='<%#Eval("IsActive") %>'></asp:CheckBox>


                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_remave" OnClick="lb_remave_Click" runat="server">Remove</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_OnClick" runat="server">Edit</asp:LinkButton>
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
