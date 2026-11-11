<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MeetingCategoryEntry.aspx.cs" Inherits="MeetingMinors_MeetingCategoryEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
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
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> Category Information  </h1>
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
                                     

                                    <div class="form-group">
                                        <label>Meeting Category </label>
                                        <label style="color: #a52a2a">*</label>
                                         
                                        <asp:HiddenField ID="MeetingCategoryIdHiddenField" runat="server" />
                                        <asp:TextBox ID="TxtCategory" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>

                                </div>
                              
                            </div>

              
                            <div class="row">
                                
                                <div class="col-md-3">
                                    <div class="form-group">

                                        
                                        <asp:Button ID="btn_Save" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
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
          <%--  <Triggers>
                <asp:AsyncPostBackTrigger ControlID="submit_Button" runat="server"/>
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
</asp:Content>

