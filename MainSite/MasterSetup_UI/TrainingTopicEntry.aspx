<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="TrainingTopicEntry.aspx.cs" Inherits="MasterSetup_UI_TrainingTopicEntry" %>

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
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Training Topic Information </h1>
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
                                        <label>Training Heading: </label>
                                        <asp:DropDownList ID="TrainingHeadingDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"  ></asp:DropDownList>

                                    </div>
                                

                                    <div class="form-group">
                                        <label>Topic Name  </label>
                                        <label style="color: #a52a2a">*</label>
                                         
                                        <asp:HiddenField ID="TrainingTopicIdHiddenField" runat="server" />
                                        <asp:TextBox ID="TrainingTopicTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>

                                      <div class="form-group" runat="server" Visible="False">
                                          <asp:RadioButtonList ID="rbSuSOrRelease" runat="server" RepeatDirection="Horizontal">
                                              <asp:ListItem>For Suspend</asp:ListItem>
                                              <asp:ListItem>For Disciplinary </asp:ListItem>
                                          </asp:RadioButtonList>
                                          </div>
                                    
                                    
                                        
                                    
                                     <div class="form-group">
                                        <label>Sorting Order </label>
                                        <label style="color: #a52a2a">*</label>
                                         
                                       
                                       <asp:DropDownList runat="server" ID="ddlTrainingSerial" CssClass="form-control form-control-sm"/>
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
                                        <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
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

