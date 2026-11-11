<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="VivaSetupInfoEntry.aspx.cs" Inherits="MasterSetup_UI_VivaSetupInfoEntry" %>

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
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Viva Setup Information  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                           <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
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
                                            <label>Company </label>
                                            <label style="color: #a52a2a">*</label>


                                            <asp:DropDownList ID="CompanyDropDown" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    
                                       <div class="form-group">
                                            <label>Category </label>
                                            <label style="color: #a52a2a">*</label>


                                            <asp:DropDownList ID="CategoryDropDownList" runat="server" class="form-control form-control-sm">
                                                <asp:ListItem Selected="True" Value="0">Select One</asp:ListItem>
                                                <asp:ListItem Value="Viva">Viva</asp:ListItem>
                                                <asp:ListItem Value="Others">Others</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    <div class="form-group">
                                        <label>Viva </label>
                                        <label style="color: #a52a2a">*</label>
                                         
                                        <asp:HiddenField ID="hdpk" runat="server" />
                                        <asp:TextBox ID="VIvaTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    
                                    
                                        <div class="form-group">
                                        <label>Remarks </label>
                                       
                                         
                                       
                                        <asp:TextBox ID="RemarksTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
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
                              
                            </div>


                            <div class="row">
                                
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="submitButton" Text=" Save " CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                         <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                    <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                       
                                        
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

