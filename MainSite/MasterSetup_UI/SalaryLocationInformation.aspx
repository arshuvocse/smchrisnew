<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="SalaryLocationInformation.aspx.cs" Inherits="MasterSetup_UI_SalaryLocationInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
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
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Office Location Information </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                           <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
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
                                            <label>Office Location </label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:HiddenField ID="salaryLocationIdField" runat="server" />
                                            <asp:HiddenField ID="salaryLocationHiddenField" runat="server" />
                                            <asp:TextBox ID="salaryLocationTextBox" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="salaryLocationTextBox_OnTextChanged"></asp:TextBox>
                                        </div>
                                    
                                       <div class="form-group">
                                            <label>Office Name </label>
                                            <asp:DropDownList ID="OfficeNameDownList" class="form-control form-control-sm"  runat="server" ></asp:DropDownList>
                                        </div>
                                    
                                     <div class="form-group">
                                            <label>Remarks </label>
                                            <asp:TextBox ID="remarksTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                </div>
                                <div class="col-md-3">
                                    
                                   
                                      <div class="form-group">
                                            <label>Description </label>
                                            <asp:TextBox ID="descriptionTexBox" runat="server"  CssClass="form-control resize" Rows="1" Columns="20"  ></asp:TextBox>
                                        </div>
                                    
                                       <div class="form-group">
                                             <br />


                                        <div class="checkbox">

                                            <label class="btn btn-default">
                                                <asp:CheckBox ID="isActiveCheckBox" CssClass="checkbox margin-right" runat="server" />
                                                <label>Is Active </label>
                                            </label>

                                        </div>
                                        </div>
                                </div>
                                   <div class="col-md-4">
                                    </div>
                                </div>
                                     <div class="row">
                                  
                                  <div class="col-md-3">
                                      
                                       <div class="form-group">
                                                        <asp:Button ID="submitButton" Text="Save" Visible="False" CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_Click" />
                                                 <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                            <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" Visible="False" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
                                           
                                           </div>
                                         </div>
                                  <div class="col-md-3">
                          
                                    </div>
                                  <div class="col-md-4">
                                    </div>
                            </div>
                                <div class="form-row">
                                    <div class="col-3">
                                     
                                    </div>
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

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

