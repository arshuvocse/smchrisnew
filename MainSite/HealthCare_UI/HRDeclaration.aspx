<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="HRDeclaration.aspx.cs" Inherits="HealthCare_UI_HRDeclaration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style>

        .star{
            color:red;
        }

    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
        <div class="content" id="content">
            
            
                 <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                
                
                
                                       
                
                     <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" 
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
                
                
                

                 <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> HR Declaration  </h1>
                        </div>
                       <div class="page-heading__container float-right d-none d-sm-block">
                           
                       <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="HomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>

                        <asp:LinkButton ID="LinkButton3"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                    </div>
                    </div>
                     
<div class="card">
   <div class="card-body">
       
         <div class="row">
               <div class="col-md-3">
                   </div>
                    <div class="col-md-6">
                        <div class="form-group ">
                             <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:<span class="star">*</span></label></div>
                                <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlCompany"  AutoPostBack="True"  OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" /></div>
                         
                            </div>  
                            <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Financial Year:<span class="star">*</span></label></div>
                                <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlFinancialYear"  class="form-control form-control-sm" /></div>
                                <asp:HiddenField runat="server" ID="hfHRDeclerationId"/>
                            </div>
                            <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">OPD Amount:<span class="star">*</span></label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"    ID="OPDAmount"  class="form-control form-control-sm" />
                                    
                                    
                                               <ajaxToolkit:FilteredTextBoxExtender ID="freqQtyTextBox" runat="server"
                                                    TargetControlID="OPDAmount"
                                                    FilterType="Custom, Numbers"
                                                    ValidChars="." />
                                  <%--  <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator" ControlToValidate="OPDAmount" ValidationExpression="^\d+$" ErrorMessage="Please Enter Numbers Only" Display="Dynamic" SetFocusOnError="True" />--%>
                                </div>
                            </div>
                            <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">IPD Amount:<span class="star">*</span></label></div>
                                <div class="col-md-6">
                                      <asp:TextBox runat="server"       ID="IPDAmount"  class="form-control form-control-sm" />
                                      <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    TargetControlID="IPDAmount"
                                                    FilterType="Custom, Numbers"
                                                    ValidChars="." />
                                </div>
                            </div>
                         
                            </div>

                            </div>
                        </div>
             </div>
       
   
     <div class="form-row">
               <div class="col-md-5">
       </div>
                <div class="col-md-3">
                                        <div class="form-group" style="margin-top: 17px;">
                                            
                                             <asp:LinkButton runat="server" ID="btnsave"  OnClick="submitButton_Click"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Save Information </asp:LinkButton>
                    <%--                        <asp:LinkButton runat="server" ID="LinkButton1"  OnClick="submitButton_Click"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Save Information </asp:LinkButton>--%>

                                            <asp:LinkButton runat="server" ID="btnReset"  CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset Information</asp:LinkButton>
                                          
                                        </div> 
       </div>

   
        
        </div>

      
              </div>
        
        </div>
                
                
                
                    </ContentTemplate>
                     
                       

        </asp:UpdatePanel>
                               </div>
        
        
    

</asp:Content>

