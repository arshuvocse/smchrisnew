<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingTypeEntry.aspx.cs" Inherits="Training_TrainingTypeEntry" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Training Type Information  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary "  runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">


                            <div class="row">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Training Type </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="trainingTypeTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Description</label>
                                        <%--  <label style="color: #a52a2a">*</label>--%>
                                        <asp:TextBox ID="descTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Training Effectiveness Evaluation</label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:RadioButtonList ID="effecevoRadioButtonList" AutoPostBack="True" OnSelectedIndexChanged="effecevoRadioButtonList_OnSelectedIndexChanged" runat="server">

                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>

                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group" runat="server" visible="False" id="month">
                                        <label>Month</label>
                                        <label style="color: #a52a2a">*</label>
                                       <asp:TextBox runat="server" ID="monthNameDropDownList" class="form-control form-control-sm"></asp:TextBox>
                                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                                            Enabled="True" TargetControlID="monthNameDropDownList" FilterType="Custom" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                    </div>

                                </div>

                            </div>


                            <div class="row">

                                <div class="col-md-3">
                                    <div class="form-group">

                                        <asp:Button ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_OnClick" />
                                        <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:Button ID="cancelButton" Visible="False" Text="Cancel" CssClass="btn btn-sm warning" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>

                                <div class="col-md-4">
                                </div>
                            </div>
                            <asp:HiddenField ID="trainingTypeHiddenField" runat="server" />

                        </div>


                    </div>
                </div>
                </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


</asp:Content>

