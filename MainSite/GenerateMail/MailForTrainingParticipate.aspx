<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MailForTrainingParticipate.aspx.cs" Inherits="GenerateMail_MailForTrainingParticipate" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">




            <ContentTemplate>



                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>




                <div class="page-heading">
                    <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;">Increment Entry </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="View" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="col-md-5">

                                <div class="form-group">
                                    <label>Email: </label>
                                    &nbsp;<label style="color: #a52a2a">*</label>
                                    <asp:TextBox TextMode="Email" ID="txtEmailAddress" class="form-control" runat="server"></asp:TextBox>


                                </div>

                                <div class="form-group">
                                    <label>Subject: </label>
                                    &nbsp;<label style="color: #a52a2a">*</label>
                                    <asp:TextBox ID="txtSubject" class="form-control" runat="server"></asp:TextBox>
                                </div>




                                <div class="form-group">
                                    <label>Message Body: </label>
                                    &nbsp;<label style="color: #a52a2a">*</label><FTB:FreeTextBox ID="txtMessageBody" runat="server"></FTB:FreeTextBox>
                                   
                                    <%-- <textarea  id="txtMessageBody3"  Visible="False"  cols="35" rows="10" class="form-control resize" runat="server"></textarea>
                                         <FTB:FreeTextBox runat="server"></FTB:FreeTextBox>                          <textarea  id="txtMessageBody2"   Visible="False" cols="35" rows="10" class="form-control resize" runat="server"></textarea>--%>
                                </div>

                                   <div class="form-row">
                                    <div class="col-3 ">
                                        <div class="form-group">
                                            <asp:Button ID="submitButton" Text="Submit" OnClick="submitButton_OnClick" CssClass="btn btn-sm btn-info" runat="server" />
                                            <asp:Button ID="clearButton" Text="Cancel" Visible="False" OnClick="clearButton_OnClick" CssClass="btn btn-sm btn-warning" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>



</asp:Content>

