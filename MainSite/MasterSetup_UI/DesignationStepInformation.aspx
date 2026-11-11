<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="DesignationStepInformation.aspx.cs" Inherits="MasterSetup_UI_DesignationStepInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Designation Step Information  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            
                              <div class="row">
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-3">
                                    
                                     <div class="form-group">
                                        <label> Designation Step Name </label><label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="designationStepNameTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        <asp:HiddenField ID="designatonStepHiddenField" runat="server" />
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
                                   <div class="col-md-3">
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                  </div>


                       
                               <div class="row">
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-3">
                                     <div class="form-group">
                                        <asp:Button ID="Button1" Text="Save" CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_Click" />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
                                    </div>
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

