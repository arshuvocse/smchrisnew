<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="FinancialYear.aspx.cs" Inherits="CSTL_ACC_UI_FinancialYear" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
   


    
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
                     <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" 
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    
                      <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"> <img src="../Report_Pages/app.png"  width="20px" /> Financial Year </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="View" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                         
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                         
                        

                                 

                                                  <div class="col-md-12">
                                                        <div class="row">
                                                      <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Company</label>&nbsp;<label style="color: #a52a2a">*</label>
                                                                <asp:DropDownList ID="companyDropDownList" runat="server" TabIndex="1" class="form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                               
                              
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Year Start Date</label>&nbsp;<label style="color: #a52a2a">*</label>
                                                                
                                                                  <asp:TextBox ID="yearStartDateTextBox" AutoCompleteType="Disabled"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="yearStartDateTextBox" CssClass="MyCalendar"
                                            TargetControlID="yearStartDateTextBox" />
                                                              
                                                            </div>
                                                        </div>
                                                    </div>
                                             
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Year End Date</label>&nbsp;<label style="color: #a52a2a">*</label>
                                                                
                                                                   
                                                                  <asp:TextBox ID="yearEndTextBox" AutoCompleteType="Disabled"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="yearEndTextBox" CssClass="MyCalendar"
                                            TargetControlID="yearEndTextBox" />
                                                               
                                                            </div>
                                                        </div>
                                                    </div>
                                                  
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <div class="form-group">
                                                                <label>Is Active</label>&nbsp;<label style="color: #a52a2a">*</label>
                                                                <asp:CheckBox ID="isactiveCheckBox" runat="server" AutoPostBack="True" OnCheckedChanged="isactiveCheckBox_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                      
                                                      
                                                        <div class="row" runat="server"  visible="False" id="Divactive">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Active Date</label>
                                                                
                                                                
                                                                      
                                                                  <asp:TextBox ID="activeDateTextBox" AutoCompleteType="Disabled"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="activeDateTextBox" CssClass="MyCalendar"
                                            TargetControlID="activeDateTextBox" />
                                                               
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" runat="server" visible="False" id="DivInactive">
                                                        
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Inactive Date</label>
                                                                    
                                                                    
                                                                      <asp:TextBox ID="inactiveDateTextBox" AutoCompleteType="Disabled"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="inactiveDateTextBox" CssClass="MyCalendar"
                                            TargetControlID="inactiveDateTextBox" />
                                                                  
                                                                </div>
                                                            </div>
                                                        </div>
                                                  

                                                    <br>
                                                    <br>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                
                                                                  <asp:Button ID="submitButton" Text="Submit" OnClick="submitButton_Click"   CssClass="btn btn-sm btn-info" runat="server" />
                                                                   
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                  </div>
                                                </div>
                                               
                                            </div>
                                        </div>
                    
                    </div>
                <asp:HiddenField runat="server" ID="hiddenField"/>
                
                   
                                    </ContentTemplate>
                                </asp:UpdatePanel>

          </div>       
</asp:Content>

