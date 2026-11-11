<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MeetingRoomEntry.aspx.cs" Inherits="MeetingMinors_MeetingRoomEntry" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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


<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
      <div class="content" id="content" >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                  <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Meeting Room Information  </h1>
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
                            
                            
                            
                            
                            
                               <div class="row">
               <div class="col-md-3">
                   </div>
                    <div class="col-md-6">
                        <div class="form-group ">
                          
                          <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:</label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server" Enabled="True"  ID="ddlCompany" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" /></div>
                          </div>
                           <div style="padding-top: 5px;"></div>
                                <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right"> Office:</label></div>
                            <div class="col-md-6">   <asp:DropDownList runat="server" Enabled="True"  ID="ddlOffice" AutoPostBack="True" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged" class="form-control form-control-sm" /></div>
                                   
                               
                                 
                          </div>
                             <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Location:</label></div>
                                <div class="col-md-6">  <asp:DropDownList runat="server"  Enabled="True"  ID="ddlLocation"  AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_OnSelectedIndexChanged"  class="form-control form-control-sm" /></div>
                        
                          

                                </div>
                            
                            <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Floor:</label></div>
                                <div class="col-md-6">  <asp:DropDownList runat="server"  ID="ddlFloor"  class="form-control form-control-sm" Enabled="True" /></div>
                
                   
                            </div>
                            
                            
                            <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Meeting Room Name:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"    ID="txtMeetingRoom"  class="form-control form-control-sm" /></div>

                            </div>
                            
                            <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Capacity:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"    ID="TxtsCapacity"  class="form-control form-control-sm" /></div>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="TxtsCapacity" FilterType="Numbers" FilterMode="ValidChars">
                                </cc1:FilteredTextBoxExtender>
                               <asp:HiddenField runat="server" ID="MeetingRoomIdHiddenField"/>
                                 
                            </div>
                            

                            </div>
                        
                     
                        </div>
             </div>
                            

                            <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="submit_Button" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                         <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                         <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" runat="server" OnClick="cancelButton_OnClick" Visible="False" BackColor="#FFCC00" />
                                        
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
                </div>
                </div>

            </ContentTemplate>
  
        </asp:UpdatePanel>
    </div>
</asp:Content>

