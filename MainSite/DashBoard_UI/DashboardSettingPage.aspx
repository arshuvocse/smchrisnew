<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="DashboardSettingPage.aspx.cs" Inherits="DashBoard_UI_DashboardSettingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
     <div class="content" id="content">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                      <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
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
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 16px; padding-top: 1px;">Dashboard Setting </h1>
                    </div>
                 <%--   <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="AddNewButton" Text="View" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="AddNewButton_OnClick" />
                        <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div> 

                 


                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            
                            
                            <div class="row">
                                
      <div class="col-md-12">
           <asp:HiddenField ID="HiddenFieldUserID" runat="server" />
           <asp:HiddenField ID="IdHiddenField" runat="server" />
                                    <div class="form-group">
      <label   >Contractual Employee:</label>
                                    <asp:DropDownList ID="ContractualDropDownList" CssClass="form-control form-control-sm   col-md-2" runat="server">
                                        <asp:ListItem Value="0">Please Select One</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                         <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                         <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                      
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                        </asp:DropDownList>
    </div>
           </div>
      </div>
                                    

                             <div class="row">
                                  <div class="col-md-12">
    <div class="form-group">
      <label  >Probation Employee:</label>
      <asp:DropDownList ID="ProhibitionDropDownList" CssClass="form-control form-control-sm  col-md-2" runat="server">
            <asp:ListItem Value="0">Please Select One</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                         <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                         <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                      
                                        <asp:ListItem Value="12">12</asp:ListItem>
      </asp:DropDownList>
    </div>
                                      </div>
      </div>
                             <div class="row">                          
                                 <div class="col-md-12">
                                  <div class="form-group">
      <label  >Retirement:</label>
      <asp:DropDownList ID="RetirementDropDownList" CssClass="form-control form-control-sm  col-md-2" runat="server">
            <asp:ListItem Value="0">Please Select One</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                         <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                         <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                      
                                        <asp:ListItem Value="12">12</asp:ListItem>
      </asp:DropDownList>
    </div>
           </div>                      
</div>
                             <div class="row">
     <div class="col-md-2">
      <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info"   runat="server" OnClick="submitButton_Click" />
                                           <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:Button ID="cancelButton" Text="Back to List" CssClass="btn btn-sm warning" runat="server" Visible="False" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
                                 </div>
                                 </div>
                            </div>

                               </div>

                </div>

                   
                
                 
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
