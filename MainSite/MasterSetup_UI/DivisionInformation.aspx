<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="DivisionInformation.aspx.cs" Inherits="MasterSetup_UI_DivisionInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .checkbox label {
            display: inline;
        }

        .margin-right {
            margin-right: 5px;
        }
    </style>

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
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Division Information  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        
                        
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="ListViewButton" Visible="True" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="ListViewButton_Click" />
                    </div>
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div runat="server" visible="True">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Company Name </label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" runat="server"></asp:DropDownList>

                                        </div>
                                        <div class="form-group">
                                            <label>Division Name </label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:HiddenField ID="divisionHiddenField" runat="server" />
                                            <asp:TextBox ID="DivisionNameTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>


                                        <div class="form-group">
                                            <label>Division Short Name </label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:TextBox ID="shortNameTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>



                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Description </label>
                                            <asp:TextBox ID="descriptionTexBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Remarks </label>
                                            <asp:TextBox ID="remarksTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <br>
                                            </br>
                                      

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

                                            <%--       <div class="checkbox">
                        <input id="checkbox1" type="checkbox">
                        <label for="checkbox1">
                            Default
                        </label>
                    </div>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>

                            </div>









                            <br />
                            <br />
                            <div class="row">
                               

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                        <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:Button ID="cancelButton" Text="Cancel" Visible="False" CssClass="btn btn-sm warning"  runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
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

