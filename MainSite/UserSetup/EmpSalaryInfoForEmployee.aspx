<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpSalaryInfoForEmployee.aspx.cs" Inherits="UserSetup_EmpSalaryInfoForEmployee" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
        <link href="ButtonGrups.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
     <div class="container-fluid">
        <div class="page-heading">
            <div class="page-heading__container">
                <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />Update Information</h1>
            </div>
            <div class="page-heading__container">
                <asp:Label runat="server" ID="lbl_Mode"></asp:Label>
                <span>
                    <label class="btn infoN" style="font-size: 13px;">Employee ID:  &nbsp;
                        <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 13px;" ID="empMasterCode"></asp:Label></label>
                   
                    
                  <label class="btn infoN" style="font-size: 13px;">Employee Name: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 13px;" ID="lblEmpName"></asp:Label></label>
                    
                    <label class="btn infoN" style="font-size: 11px;">Designation: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 9px;" ID="lblDesignation"></asp:Label></label>
                </span>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
                 <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                      
              <%--  <asp:Button ID="detailsViewButton" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                   <asp:Button ID="btnEditInfo" Text="Back to List" Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />--%>
            </div>
        </div>
        <div class="card">
            <div class="card-body">
                
                
                   <style>
                            .NavbarAcc {
                                color: white!important;
    background-color: #5078B3!important;
    font-family: Arial, Sans-Serif!important;
    font-size: 14px!important;
    font-weight: bold!important;
    padding: 10px!important;
    margin-top: 5px!important;
    cursor: pointer!important;
                            }
                        </style>
                        <nav class="navbar navbar-light bg-light NavbarAcc">
 <span>11. Salary</span>
</nav>
                 
                                <br />

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        
                                        
                                       <%--          <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
                                                 <asp:HiddenField runat="server" ID="hfMasterId" />
                                                 <asp:HiddenField runat="server" ID="hdpkEmpId" />
                                        
                                          <div class="row">
               <div class="col-md-1">
                   </div>
                    <div class="col-md-5">
                        
                          <div class="row">
                                <div class="col-md-5" style="padding-top: 8px">  <label class="control-label pull-right">Basic Pay:<span   style="color:red; " title="please fill out this field"> *  </span></label></div>
                                <div class="col-md-5">  <asp:TextBox runat="server"  ReadOnly="True" ID="txtBasic"    class="form-control form-control-sm" /></div>
                               
                          
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        Enabled="True" TargetControlID="txtBasic" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                          <div style="padding-top: 5px;"></div>
                        
                            <div class="row">
                                <div class="col-md-5" style="padding-top: 8px">  <label class="control-label pull-right">House Rent:<span   style="color:red; " title="please fill out this field"> *  </span></label></div>
                                <div class="col-md-5">  <asp:TextBox runat="server"  ReadOnly="True"   ID="txtHouseRent"    class="form-control form-control-sm" /></div>
                               
                              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        Enabled="True" TargetControlID="txtHouseRent" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>

                                </div>
                        
                        <div style="padding-top: 5px;"></div>
                        
                            <div class="row">
                                <div class="col-md-5" style="padding-top: 8px">  <label class="control-label pull-right">Medical: &nbsp; </label></div>
                                <div class="col-md-5">  <asp:TextBox runat="server"  ReadOnly="True"    ID="txtMedical"    class="form-control form-control-sm" /></div>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        Enabled="True" TargetControlID="txtMedical" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                          

                                </div>
                        
                        
                        <div style="padding-top: 5px;"></div>
                        
                            <div class="row">
                                <div class="col-md-5" style="padding-top: 8px">  <label class="control-label pull-right">Conveyance: &nbsp; </label></div>
                                <div class="col-md-5">  <asp:TextBox runat="server"  ReadOnly="True"   ID="txtConveyance"    class="form-control form-control-sm" /></div>
                               
                          
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                        Enabled="True" TargetControlID="txtConveyance" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                </div>
                        
                        
                        <div style="padding-top: 5px;"></div>
                        
                            <div class="row">
                                <div class="col-md-5" style="padding-top: 8px">  <label class="control-label pull-right">Washing: &nbsp; </label></div>
                                <div class="col-md-5">  <asp:TextBox runat="server"  ReadOnly="True"   ID="txtWashing"    class="form-control form-control-sm" /></div>
                               
                           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                        Enabled="True" TargetControlID="txtWashing" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>

                                </div>
                        </div>
                                              
                                                <div class="col-md-5">
                        
                          <div class="row">
                                <div class="col-md-5" style="padding-top: 8px">  <label class="control-label pull-right">Payment Type: </label></div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="ddlPaymentType"  Enabled="False"  class="form-control form-control-sm"> 
                                        <asp:ListItem Value="">Select One....</asp:ListItem>
                                        <asp:ListItem Value="Bank">Bank</asp:ListItem>
                                        <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                        <asp:ListItem Value="Both">Both</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                               
                          
                               
                                </div>
                          <div style="padding-top: 5px;"></div>
                        
                            <div class="row">
                                <div class="col-md-5" style="padding-top: 8px">  <label class="control-label pull-right">Bank Name: </label></div>
                                <div class="col-md-5">  
                                    
                                     <asp:DropDownList runat="server" ID="ddlBankName" class="form-control form-control-sm selectme"> 
                                       
                                    </asp:DropDownList>
                                  <script type="text/javascript">
                                      function pageLoad() {
                                          $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                      }
                                                        </script>  
                                     
                                                     
                                                   
                                </div>
                               
                             

                                </div>
                        
                        <div style="padding-top: 5px;"></div>
                        
                            <div class="row">
                                <div class="col-md-5" style="padding-top: 8px">  <label class="control-label pull-right">Bank Account No: &nbsp; </label></div>
                                <div class="col-md-5">  <asp:TextBox runat="server"   ID="txtBankAccountNo"    class="form-control form-control-sm" /></div>
                                    
                          

                                </div>
                        
                        
                        <div style="padding-top: 5px;"></div>
                        
                            <div class="row">
                                <div class="col-md-5" style="padding-top: 8px">  <label class="control-label pull-right">Provident Fund Eligible: &nbsp; </label></div>
                                <div class="col-md-5" style="padding-top: 5px;">  <asp:RadioButtonList Enabled="False" ID="rbProvidentFundEligible" runat="server" RepeatDirection="Horizontal"> 
                                    <asp:ListItem   Value="True">Yes</asp:ListItem>
                                    <asp:ListItem Value="False">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                               
                          
                                   
                                </div>
                        
                        
                        <div style="padding-top: 5px;"></div>
                        
                            <div class="row">
                                <div class="col-md-5" style="padding-top: 8px">  <label class="control-label pull-right">PF: &nbsp; </label></div>
                                <div class="col-md-5">  <asp:TextBox runat="server"  ReadOnly="True"   ID="txtPF"    class="form-control form-control-sm" /></div>
                               
                           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                        Enabled="True" TargetControlID="txtPF" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>

                                </div>
                                                    
                                                    
                                                      <div style="padding-top: 5px;"></div>
                        
                            <div class="row">
                                <div class="col-md-5" style="padding-top: 8px">  <label class="control-label pull-right">Monthly Tax: &nbsp; </label></div>
                                <div class="col-md-5">  <asp:TextBox runat="server"   ReadOnly="True"   ID="txtMonthlyTax"    class="form-control form-control-sm" /></div>
                               
                           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                        Enabled="True" TargetControlID="txtMonthlyTax" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>

                                </div>
                        </div>
                        </div>
                                        
                                           <br />
                                        <div class="form-row">
                                            <div class="col-md-10">

                                                <link href="ButtonGrups.css" rel="stylesheet" />
                                                <div class="ui-group-buttons">

                                                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="  Save  " OnClientClick="return confirm('Are you sure you want to Save ?')"
                                                        CssClass="btn btn-sm btn-info" />
                                              

                                                  
                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                              

                                            </div>
                                        </div>
                                        
                                           </ContentTemplate>
                                </asp:UpdatePanel>

                            


            </div>
        </div>
    </div>
</asp:Content>

