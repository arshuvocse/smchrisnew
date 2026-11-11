<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="CompanyInformation.aspx.cs" Inherits="MasterSetup_UI_CompanyInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
        
    <style>
         .resize
    {
        resize:none;
    }
    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Company Information Entry </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="CompanyListImageButton_Click" />
                        <%--<asp:Button ID="reportViewButton" Text="Report" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="rptImageButton_Click" />--%>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="MessageLabel" runat="server" Text=""></asp:Label>

                            <div class="row">
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-3">
                                    
                                    <div class="form-group">
                                            <label>Company Name</label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:TextBox ID="companynameTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                            <asp:HiddenField ID="companyIdHiddenField" runat="server" />
                                        </div>
                                    
                                      <div class="form-group">
                                            <label>Short Name</label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:TextBox ID="shortNameTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    
                                      <div class="form-group">
                                            <label>Address </label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:TextBox ID="companyAddressTextBox" CssClass="form-control resize" Rows="1" Columns="20" TextMode="MultiLine" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    
                                     <div class="form-group">
                                            <label>Contact Number </label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:TextBox ID="contactTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    
                                     <div class="form-group">
                                            <label>Fax Number </label>
                                            <asp:TextBox ID="faxNoTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    
                                </div>
                                <div class="col-md-3">
                                    
                                      <div class="form-group">
                                            <label>PABX </label>
                                            <asp:TextBox ID="pabxTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    
                                     <div class="form-group">
                                            <label>Email </label>
                                            <asp:TextBox ID="emailTextBox" runat="server" class="form-control form-control-sm" AutoPostBack="true" OnTextChanged="emailTextBox_OnTextChanged"></asp:TextBox>
                                        </div>
                                    
                                        <div class="form-group">
                                            <label>Hotline </label>
                                            <asp:TextBox ID="hotlineTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    
                                        <div class="form-group">
                                            <label>Description </label>
                                            <asp:TextBox ID="descriptionTextBox" CssClass="form-control resize" Rows="1" Columns="20" TextMode="MultiLine" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    
                                      <div class="form-group">
                                            <label>Remarks </label>
                                            <asp:TextBox ID="remarksTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                </div>
                                 <div class="col-md-4">
                                </div>
                                </div>


                              <div class="row">
                                <div class="col-md-1">
                              </div>
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
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

