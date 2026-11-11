<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="BenefitInformationEntry.aspx.cs" Inherits="Benefit_UI_BenefitInformationEntry" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    <style>
        fieldset.for-panel {
            background-color: #fcfcfc;
            border: 1px solid #999;
            padding: 15px 10px;
            background-color: white;
            margin-bottom: 12px;
        }

            fieldset.for-panel legend {
                background-color: #fafafa;
                border: 1px solid #ddd;
                border-radius: 1px;
                font-size: 12px;
                font-weight: bold;
                line-height: 10px;
                margin: inherit;
                padding: 7px;
                width: auto;
                margin-bottom: 0;
                color: black;
            }
    </style>
    
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
                       
                        <span></span>
                        <h1 class="title" style="font-size: 16px; padding-top: 1px;">Benefit Entry </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                         <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                <asp:Button ID="AddNewButton" Text="View" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="AddNewButton_OnClick"  />
                <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
            </div>

                </div>
             

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    
                                    <div class="form-group">
                                        <label>Benefit Name: </label>
                                        
                                        <asp:TextBox ID="benefittextBox"  runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                     <br/>
                                        
                                          <label class="btn btn-default">
                                        <asp:CheckBox ID="isactiveCheckBox"  CssClass="checkbox margin-right" runat="server" />
                                                <label>Is Active </label>
                                            </label>
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
                           
                            <div class="row" runat="server" Visible="False">
                                  <div class="col-md-12">
                                 <fieldset class="for-panel">
                                                    <legend>Employee Category</legend>
                                      <div class="row">
                                <div class="col-md-6">
                                    
                                    <div class="form-group">
                                        <label><asp:CheckBox ID="manCheckBox" Text="Management" runat="server" AutoPostBack="True" OnCheckedChanged="manCheckBox_OnCheckedChanged" /> </label>
                                          <div style="max-height: 200px; overflow: scroll">
                                        <asp:CheckBoxList ID="managementCheckBoxList" runat="server"></asp:CheckBoxList>
                                              </div>
                                        
                                        
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label><asp:CheckBox ID="gradedCheckBox" Text="Graded" AutoPostBack="True" runat="server" OnCheckedChanged="gradedCheckBox_OnCheckedChanged" /></label>
                                          <div style="max-height: 200px; overflow: scroll">
                                         <asp:CheckBoxList ID="gradedCheckBoxList" runat="server"></asp:CheckBoxList>
                                               </div>
                                    </div>
                                </div>
                                          </div>
                                     </fieldset>
                         
                                </div>
                                </div>
                            <div class="row" runat="server" Visible="False">
                                  <div class="col-md-12">
                                 <fieldset class="for-panel">
                                                    <legend>Job Nature Type</legend>
                                      <div class="row">
                                <div class="col-md-3">
                                    
                                    <div class="form-group">
                                        
                                        <asp:CheckBoxList ID="natureCheckBoxList" AutoPostBack="True" runat="server" OnSelectedIndexChanged="natureCheckBoxList_SelectedIndexChanged"   >
                                            <asp:ListItem>Permanent</asp:ListItem>
                                            <asp:ListItem>Contractual</asp:ListItem>
                                            <asp:ListItem>Casual</asp:ListItem>
                                        </asp:CheckBoxList>
                                        
                                        
                                    </div>
                                </div>
                                          </div>
                                     
                                         <div class="row">
                                <div class="col-md-3" runat="server" Visible="False" id="perm">
                                    
                                    <div class="form-group">
                                        <label>Permanent </label>
                                        <asp:CheckBoxList ID="permCheckBoxList" Visible="False" runat="server">
                                            <asp:ListItem>Confirmed</asp:ListItem>
                                            <asp:ListItem>Probationary</asp:ListItem>
                                        </asp:CheckBoxList>
                                        
                                        
                                    </div>
                                </div>
                                <div class="col-md-3" runat="server" Visible="False" id="cont">
                                    
                                    <div class="form-group">
                                        <label>Contractual </label>
                                        <asp:CheckBoxList ID="contCheckBoxList" runat="server" Visible="False">
                                            <asp:ListItem>Confirmed</asp:ListItem>
                                            <asp:ListItem>Probationary</asp:ListItem>

                                        </asp:CheckBoxList>
                                        
                                        
                                    </div>
                                </div>
                                <div class="col-md-3" runat="server" Visible="False" id="cas">
                                    
                                    <div class="form-group">
                                        <label>Casual </label>
                                        <asp:CheckBoxList ID="casualCheckBoxList" runat="server" Visible="False" >
                                            <asp:ListItem>Confirmed</asp:ListItem>
                                            <asp:ListItem>Probationary</asp:ListItem>

                                        </asp:CheckBoxList>
                                        
                                        
                                    </div>
                                </div>
                                <div class="col-md-3" runat="server" Visible="False" id="contyear">
                                    <div class="form-group">
                                        <label>Condition Job Length in Year</label>
                                        <asp:TextBox ID="contractyearTextBox"  runat="server" class="form-control form-control-sm"></asp:TextBox>
                                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderqty" runat="server"
                                                                            Enabled="True" TargetControlID="contractyearTextBox" FilterType="Custom" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                                     </fieldset>
                                      </div>
                                 </div>
                                <div class="col-md-3" runat="server" Visible="False" id="type">
                                    <div class="form-group">
                                       
                                        
                                        
                                        

                                    </div>
                                </div>
                                  <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="saveButton" runat="server" CssClass="btn btn-sm btn-info" Visible="False" Text="Submit" OnClick="saveButton_OnClick" />
                                          <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:HiddenField ID="beneftIdHiddenField" runat="server" />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning"  OnClick="cancelButton_OnClick" runat="server" BackColor="#FFCC00" />
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

