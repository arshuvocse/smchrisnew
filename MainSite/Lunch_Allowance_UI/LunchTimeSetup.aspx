<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="LunchTimeSetup.aspx.cs" Inherits="Lunch_Allowance_UI_LunchTimeSetup" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

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
                       
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">  <img src="../Report_Pages/app.png"  width="20px" />  Lunch Cancel Last Time Setup  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                      <%--  <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>--%>
                      </div>

                </div>   <asp:HiddenField ID="hdpk" runat="server" />
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <form>
                            <div class="row">
                                
                                <div class="col-md-3">
                                     

                                     <div class="form-group" runat="server" Visible="False">
                                        <label>Company Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm"  runat="server" AutoPostBack="False" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged"  ></asp:DropDownList>
                                    </div>

                                    
                                    <div class="form-group" runat="server" Visible="False">
                                                                <label>Employee Type</label>
                                                                <label style="color: #a52a2a">*</label>
                                                                <asp:DropDownList runat="server" ID="ddlEmpCategory" class="form-control form-control-sm"  />

                                                            </div>
                               
                                      <div class="form-group">
                                        <label>Time </label> <span style="color: red">&nbsp;*</span>
                                        

                                        <asp:TextBox ID="txtHolidayFromDate" AutoCompleteType="Disabled" runat="server"   CausesValidation="true"
                                            class="form-control form-control-sm"></asp:TextBox>
                                       
                                    </div>
                                    
                                    
                                      <div class="form-group"  runat="server" Visible="False">
                                        <label>Rate </label> <span style="color: red">&nbsp;*</span>
                                        

                                        <asp:TextBox ID="txtFoodrate"  runat="server"    
                                            class="form-control form-control-sm"></asp:TextBox>
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                                    TargetControlID="txtFoodrate" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        
                                    </div>
                                     <div class="form-group" runat="server" Visible="False">
                                        <label>Details </label>
                                        <asp:TextBox ID="descriptionTexBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="form-group" runat="server" Visible="False">
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
                                        <asp:Button ID="submitButton" Text=" Submit " CssClass="btn btn-sm btn-info"  Visible="False"    runat="server" OnClick="submitButton_Click" />
                                         <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success"   runat="server" OnClick="editButton_OnClick" />
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

