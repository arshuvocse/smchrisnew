<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SalaryStepInformation.aspx.cs" Inherits="MasterSetup_UI_SalaryStepInformation" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .checkbox label
        {
            display:inline;
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
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Salary Step Information </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                                          <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                  
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            
                            <div class="row">
                                
                                <div class="col-md-3">
                                      
                                        <asp:HiddenField ID="salaryStepIdHiddenField" runat="server" />
                                        <asp:HiddenField ID="salaryStepHiddenField" runat="server" />
                                      
                                    
                                    
                                     <div class="form-group">
                                        <label> Salary Grade </label> <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="salaryGradeDropDownList" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                    
                                      <div class="form-group">
                                        <label> Salary Step Name </label> <label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="salaryStepTextBox" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="salaryStepTextBox_OnTextChanged"></asp:TextBox>
                                    </div>
                                     <div class="form-group">
                                        <label> Gross Amount </label> <label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="GrossAmountTextBox" runat="server" class="form-control form-control-sm" ></asp:TextBox>
                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                            TargetControlID="GrossAmountTextBox" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                           </div>
                                     <div class="form-group">
                                        <label> Basic Amount </label> <label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="BasicAmountTextBox" runat="server" class="form-control form-control-sm" ></asp:TextBox>
                                  <cc1:FilteredTextBoxExtender ID="FnumberTextBox" runat="server" Enabled="True"
                                                                            TargetControlID="BasicAmountTextBox" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                            </div>
                                    
                                          
                                    
                                  
                                    </div>
                                   <div class="col-md-3">
                                           <div class="form-group">
                                        <label>Remarks </label>
                                        <asp:TextBox ID="remarksTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                       
                                       <div class="form-group">
                                        <label>Description </label>
                                        <asp:TextBox ID="descriptionTexBox" runat="server" class="form-control resize" Rows="1" Columns="20"></asp:TextBox>
                                    </div>
                                       
                                       <div class="form-group">
                                     


                                        <div class="checkbox">

                                            <label class="btn btn-default">
                                                <asp:CheckBox ID="isActiveCheckBox" CssClass="checkbox margin-right" runat="server" />
                                                <label>Is Active </label>
                                            </label>

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
                                  <div class="col-md-4">
                                    </div>
                                </div>
                            
                                  <div class="row">
                                 
                                  <div class="col-md-3">
                                      
                                       <div class="form-group">
                                        <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                             <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:Button ID="cancelButton" Text="Cancel" Visible="False" CssClass="btn btn-sm warning" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
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

