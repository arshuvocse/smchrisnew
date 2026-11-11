<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="PlaceInformation.aspx.cs" Inherits="MasterSetup_UI_PlaceInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .checkbox label {
            display: inline;
        }

        .margin-right {
            margin-right: 5px;
        }
    </style>

    <style>
        .resize {
            resize: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content" id="content" >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Place Information   </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <form>
                            <div class="row">
                                
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="regionDropDownList" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label>Place Name </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:HiddenField ID="areaHiddenField" runat="server" />
                                        <asp:HiddenField ID="areaCodeHiddenField" runat="server" />
                                        <asp:TextBox ID="areaNameTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>

                                    <div class="form-group" runat="server" Visible="False">
                                        <label>Area Code </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="areaCodeTextBox" runat="server" AutoPostBack="True" OnTextChanged="areaCodeTextBox_OnTextChanged" class="form-control form-control-sm"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-md-3">

                                    <div class="form-group">
                                        <label>Description </label>
                                        <asp:TextBox ID="descriptionTexBox" runat="server" CssClass="form-control resize" Rows="1" Columns="20" TextMode="MultiLine" ></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Remarks </label>
                                        <asp:TextBox ID="remarksTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <br />


                                        <div class="checkbox">

                                            <label class="btn btn-default">
                                                <asp:CheckBox ID="isActiveCheckBox" CssClass="checkbox margin-right" runat="server" />
                                                <label>Is Active </label>
                                            </label>

                                        </div>
                                        <style>
                                            .checkbox .btn,
                                            .checkbox-inline .btn {
                                                padding-left: 0em;
                                                min-width: 14em;
                                            }



                                            .checkbox label,
                                            .checkbox-inline label {
                                                text-align: left;
                                                padding-left: 0.7em;
                                            }
                                        </style>
                                    </div>


                                </div>
                                <div class="col-md-4">
                                </div>
                            </div>


                            <div class="row">
                                
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_Click" />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
                                        
                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>

                                <div class="col-md-4">
                                </div>
                            </div>

                                </form>
                        </div>


                    </div>
                    </div>
                </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
            

</asp:Content>

