<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="VacancyCirculationSetup.aspx.cs" Inherits="MasterSetup_UI_VacancyCirculationSetup" %>

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
                        <h1 class="title" style="font-size: 16px; padding-top: 1px;">Vacancy Circulation Information  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                    </div>
                </div>
                <!-- //END PAGE HEADING -->


                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-6">
                                    <div class="view">
                                        <div class="main">
                                            <div class="tabs no_tab_selector">
                                                <article class="view_tab_details tab loaded">
                                                    <div class="content">
                                                        <div class="view-columns">
                                                            <table class="view-layout set_auto_focus">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tab_columns">
                                                                                <tbody>
                                                                                    <tr class="view_field_box" data-automation="printLabelsRow">
                                                                                        <td id="details_22" class="view_field_box " colspan="3"><%-- <h3>Company Information</h3>--%>
                                                                                            <table>
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table class="tab_columns">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td class="label">
                                                                                                                            <label for="">
                                                                                                                                Circulation Way :
                                                                                                                            </label>
                                                                                                                        </td>
                                                                                                                        <td class="">
                                                                                                                            <asp:TextBox ID="circulationWayTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                                                                                                            <asp:HiddenField ID="VacancyCirculationIdHiddenField" runat="server" />
                                                                                                                        </td>
                                                                                                                    </tr>

                                                                                                                    <tr>
                                                                                                                        <td class="label">
                                                                                                                            <label for="">
                                                                                                                                Is Active :
                                                                                                                            </label>
                                                                                                                        </td>
                                                                                                                        <td class="">
                                                                                                                            <asp:CheckBox ID="chkActive" runat="server" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>
                                                            <td>
                                                                <table class="view-column ">
                                                                </table>
                                                            </td>
                                                            <td>

                                                                <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-info btn-sm" runat="server" OnClick="submitButton_Click" />
                                                                <asp:Button ID="btnCancel" Text="Cancel" CssClass="btn btn-warning btn-sm" runat="server" OnClick="cancelButton_OnClick" />

                                                            </td>
                                                        </div>
                                                    </div>
                                                </article>
                                            </div>
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

