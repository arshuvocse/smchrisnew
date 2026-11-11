<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="DiciplinaryActionReport.aspx.cs" Inherits="Report_Pages_DiciplinaryActionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">

                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Disciplinary Action Report </h1>
                    </div>
                    <%--<div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company Name </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Disciplinary Action </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="discActionDropDownList" class="form-control form-control-sm" runat="server">
                                            <asp:ListItem> In </asp:ListItem>
                                            <asp:ListItem> Not In </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Action Date </label>
                                        <label style="color: #a52a2a">* </label>
                                        <asp:DropDownList ID="actionDate" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="actionDate_SelectedIndexChanged">
                                            <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                            <asp:ListItem Value="2"> = </asp:ListItem>
                                            <asp:ListItem Value="3"> < </asp:ListItem>
                                            <asp:ListItem Value="4"> > </asp:ListItem>
                                            <asp:ListItem Value="5"> Between </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" id="singleDate" visible="False">
                                    <div class="form-group">
                                        <label>Date: </label>
                                        <asp:TextBox ID="dateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="dateTextBox" />
                                    </div>
                                </div>


                                <div runat="server" id="dateRange" visible="False" class="col-md-4">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>From Date: </label>
                                                <asp:TextBox ID="fromDateTextbox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="fromDateTextbox" />
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>To Date: </label>
                                                <asp:TextBox ID="toDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="toDateTextBox" />
                                            </div>
                                        </div>
                                    </div>





                                </div>
                            </div>

                            <br />
                            <br />


                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_Click" />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
                                    </div>
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

